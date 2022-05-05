using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public static class Student
    {
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
    }
}