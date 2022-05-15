using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public static class Question
    {
        public static DataTable GetExamQuestion(int exam_id)
        {
            DataTable questTable = new DataTable();
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM question WHERE exam_id=@exam_id ORDER BY sequence;";
                    cmd.Parameters.AddWithValue("@exam_id", exam_id);
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        adapter.Fill(questTable);
                    }
                }
                conn.Close();
            }
            return questTable;
        }

        public static DataTable GetQuestionOption(int question_id)
        {
            DataTable optionTable = new DataTable();
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM [option] WHERE question_id=@question_id;";
                    cmd.Parameters.AddWithValue("@question_id", question_id);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(optionTable);
                    }
                }
                conn.Close();
            }
            return optionTable;
        }

        public static int GetNumOfAnswer(int question_id)
        {
            int numOfAnswer;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT COUNT(*) FROM [option] WHERE question_id=@question_id AND isAnswer='True';";
                    cmd.Parameters.AddWithValue("@question_id", question_id);
                    numOfAnswer = (int) cmd.ExecuteScalar();
                }
                conn.Close();
            }
            return numOfAnswer;
        }

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