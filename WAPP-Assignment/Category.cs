using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public class Category
    {
        public static DataTable GetCourseCategoryData(int course_id)
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM category WHERE category_id IN (SELECT category_id FROM course_category WHERE course_id=@course_id);";
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        DataTable dataTable = new DataTable();
                        sda.Fill(dataTable);
                        conn.Close();
                        return dataTable;
                    }
                }
            }
        }
        public static void UpdateCategory()
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT DISTINCT category_id FROM category;";
                    List<string> availCategories = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            availCategories.Add(sdr["category_id"].ToString());
                        }
                    }
                    cmd.CommandText = "SELECT DISTINCT category_id FROM course_category;";
                    List<string> inUsedCategories = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            inUsedCategories.Add(sdr["category_id"].ToString());
                        }
                    }
                    List<string> unusedCategories = availCategories.Except(inUsedCategories).ToList();
                    cmd.CommandText = "DELETE category WHERE category_id=@category_id";
                    foreach (string category in unusedCategories)
                    {
                        cmd.Parameters.AddWithValue("@category_id", category);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    conn.Close();
                }
            }
        }

        public static void AddNewCategory(List<string> inputCategories)
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                string query = "SELECT name FROM category;";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    List<string> currCategories = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            currCategories.Add(sdr["name"].ToString());
                        }
                    }
                    List<string> newCategories = inputCategories.Except(currCategories).ToList();
                    if (newCategories.Count == 0)
                    {
                        conn.Close();
                        return;
                    }
                    cmd.CommandText = "INSERT INTO category (name) VALUES (@name);";
                    foreach (string category in newCategories)
                    {
                        if (string.IsNullOrEmpty(category)) continue;
                        cmd.Parameters.AddWithValue("@name", category);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    conn.Close();
                }
            }
        }
    }
}