using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public static class Course
    {
        public static DataTable GetAllCourseData()
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM course";
                    cmd.Connection = conn;
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
        public static DataTable GetCourseData(int course_id)
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM course WHERE course_id=@course_id";
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

        public static DataTable GetCourseChapterData(int course_id)
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM chapter WHERE course_id=@course_id ORDER BY sequence";
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
    }
}