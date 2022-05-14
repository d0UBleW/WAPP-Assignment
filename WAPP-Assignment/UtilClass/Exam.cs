using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public static class Exam
    {
        public static DataTable GetCourseExamData(int course_id)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = WAPP_Assignment.DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM exam WHERE course_id=@course_id;";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        adapter.Fill(dataTable);
                    }
                }
                conn.Close();
            }
            return dataTable;
        }
    }
}