using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public static class Question
    {
        public static int GetQueMaxSeq(int exam_id)
        {
            int seq;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT MAX(sequence) AS seq FROM question WHERE exam_id=@exam_id;";
                    cmd.Parameters.AddWithValue("@exam_id", exam_id);
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

        public static void UpdateQueSequence(int exam_id, int seq, int oldSeq)
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
                        cmd.CommandText = $"UPDATE question SET sequence=sequence-1 WHERE exam_id=@exam_id AND sequence > {oldSeq} AND sequence <= {seq};";
                    }
                    else if (seq < oldSeq)
                    {
                        cmd.CommandText = $"UPDATE question SET sequence=sequence+1 WHERE exam_id=@exam_id AND sequence >= {seq} AND sequence < {oldSeq};";
                    }
                    cmd.Parameters.AddWithValue("@exam_id", exam_id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                conn.Close();
            }
        }
    }
}