using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public static class StudentC
    {
        public static DataRow GetStudentData(int student_id)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = WAPP_Assignment.DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM student WHERE student_id=@student_id;";
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dataTable);
                    }
                }
                conn.Close();
            }
            if (dataTable.Rows.Count > 0)
            {
                return dataTable.Rows[0];
            }
            return null;
        }

        public static List<int> GetEnrolledCourseID(int student_id)
        {
            List<int> course_id_list = new List<int>();
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT course_id FROM enroll WHERE student_id=@student_id;";
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            course_id_list.Add(Convert.ToInt32(reader["course_id"]));
                        }
                    }
                }
                conn.Close();
            }
            return course_id_list;
        }

        public static bool IsEnrolled(int student_id, int course_id)
        {
            List<int> course_id_list = GetEnrolledCourseID(student_id);
            if (course_id_list.Contains(course_id))
            {
                return true;
            }
            return false;
        }

        public static DataTable GetExamResult(int student_id, int exam_id)
        {
            DataTable result = new DataTable();
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM grade WHERE student_id=@student_id AND exam_id=@exam_id;";
                    cmd.Parameters.AddWithValue("@exam_id", exam_id);
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(result);
                    }
                }
                conn.Close();
            }
            return result;
        }

    }
}