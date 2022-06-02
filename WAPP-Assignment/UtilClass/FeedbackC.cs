using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment.UtilClass
{
    public class FeedbackC
    {
        public static DataTable GetFeedbackData(int feed_id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM feed WHERE feed_id=@feed_id;";
                    cmd.Parameters.AddWithValue("@feed_id", feed_id);
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        adapter.Fill(dt);
                    }
                }
                conn.Close();
            }
            return dt;
        }
    }
}