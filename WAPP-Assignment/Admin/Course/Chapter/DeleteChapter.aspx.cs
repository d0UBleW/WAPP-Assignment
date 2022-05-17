using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment.Admin
{
    public partial class DeleteChapter : UtilClass.BaseAdminPage
    {
        private int chapter_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            chapter_id = GetQueryString("chapter_id");
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
            RedirectBack();
        }
    }
}