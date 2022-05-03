using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public static class Chapter
    {
        public static DataTable GetChapterData(int chapter_id)
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM chapter WHERE chapter_id=@chapter_id";
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@chapter_id", chapter_id);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        DataTable dataTable = new DataTable();
                        sda.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public static void UpdateChapterSequence(int course_id, int seq, int oldSeq)
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    if (seq == oldSeq)
                    {
                        conn.Close();
                        return;
                    }

                    if (seq > oldSeq)
                    {
                        cmd.CommandText = $"UPDATE chapter SET sequence=sequence-1 WHERE course_id=@course_id AND sequence > {oldSeq} AND sequence <= {seq};";
                    }
                    else if (seq < oldSeq)
                    {
                        cmd.CommandText = $"UPDATE chapter SET sequence=sequence+1 WHERE course_id=@course_id AND sequence >= {seq} AND sequence < {oldSeq};";
                    }
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                conn.Close();
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
        public static int GetChapterMaxSeq(int course_id)
        {
            int seq;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT MAX(sequence) AS seq FROM chapter WHERE course_id=@course_id;";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    var result = cmd.ExecuteScalar();
                    if (result == DBNull.Value)
                        seq = 0;
                    else
                        seq = Convert.ToInt32(result);
                }
                conn.Close();
            }
            return seq;
        }
    }
}