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

        public static void DeleteQuestion(int question_id)
        {
            DataTable questTable = Question.GetQuestionData(question_id);
            if (questTable.Rows.Count == 0) return;
            DataRow questRow = questTable.Rows[0];
            int exam_id = Convert.ToInt32(questRow["exam_id"]);
            int oldSeq = Convert.ToInt32(questRow["sequence"]);
            int maxSeq = Question.GetQueMaxSeq(exam_id);
            Question.UpdateQueSequence(exam_id, maxSeq, oldSeq);
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE [option] WHERE question_id=@question_id;";
                    cmd.Parameters.AddWithValue("@question_id", question_id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "DELETE question WHERE question_id=@question_id;";
                    cmd.Parameters.AddWithValue("@question_id", question_id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public static DataTable GetQuestionData(int question_id)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM question WHERE question_id=@question_id;";
                    cmd.Parameters.AddWithValue("@question_id", question_id);
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

        public static List<int> GetAnswerID(int question_id)
        {
            List<int> answerID = new List<int>();
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT option_id FROM [option] WHERE question_id=@question_id AND isAnswer='True';";
                    cmd.Parameters.AddWithValue("@question_id", question_id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            answerID.Add(int.Parse(reader["option_id"].ToString()));
                        }
                    }
                }
                conn.Close();
            }
            return answerID;
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