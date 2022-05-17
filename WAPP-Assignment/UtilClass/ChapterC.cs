using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public static class ChapterC
    {
        public static void DeleteChapter(int chapter_id)
        {
            DataTable dt = ChapterC.GetChapterData(chapter_id);
            DataRow dr = dt.Rows[0];
            int course_id = (int)dr["course_id"];
            int seq = (int)dr["sequence"];
            int maxSeq = ChapterC.GetChapterMaxSeq(course_id);
            ChapterC.UpdateChapterSequence(course_id, maxSeq, seq);
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE chapter WHERE chapter_id=@chapter_id";
                    cmd.Parameters.AddWithValue("@chapter_id", chapter_id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
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

        public static int GetFirstChapterID(int course_id)
        {
            int chapter_id;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT chapter_id FROM chapter WHERE course_id=@course_id AND sequence=1;";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    var result = cmd.ExecuteScalar();
                    if (result == DBNull.Value)
                        chapter_id = 0;
                    else
                        chapter_id = Convert.ToInt32(result);
                }
                conn.Close();
            }
            return chapter_id;
        }

        public static bool IsValidChapterID(int chapter_id)
        {
            List<int> availableChapterID = new List<int>();
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT chapter_id FROM chapter;";
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            availableChapterID.Add(Convert.ToInt32(sdr["chapter_id"]));
                        }
                    }
                }
                conn.Close();
            }
            if (availableChapterID.Contains(chapter_id))
            {
                return true;
            }
            return false;
        }

        public static int GetNextOrPrevChapterID(int course_id, int seq, string direction)
        {
            int chapter_id;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT chapter_id FROM chapter WHERE course_id=@course_id AND sequence=@sequence;";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    if (direction == "next")
                        cmd.Parameters.AddWithValue("@sequence", seq+1);
                    else
                        cmd.Parameters.AddWithValue("@sequence", seq-1);
                    var result = cmd.ExecuteScalar();
                    if (result == DBNull.Value)
                    {
                        chapter_id = 0;
                    }
                    else
                    {
                        chapter_id = Convert.ToInt32(result);
                    }
                }
                conn.Close();
            }
            return chapter_id;
        }
    }
}