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
    public partial class DeleteChapter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
            {
                Response.Redirect("~/Home.aspx");
                return;
            }
            if (!(bool)Session["isAdmin"])
            {
                if (!string.IsNullOrEmpty(Request.UrlReferrer.ToString()))
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    Response.Redirect("~/Home.aspx");
                }
                return;
            }
            string chapter_id_temp = Request.QueryString["chapter_id"];
            if (string.IsNullOrEmpty(chapter_id_temp))
            {
                return;
            }

            int chapter_id = int.Parse(chapter_id_temp);
            DataTable dt = Chapter.GetChapterData(chapter_id);
            DataRow dr = dt.Rows[0];
            int course_id = (int)dr["course_id"];
            int seq = (int)dr["sequence"];
            int maxSeq = Chapter.GetChapterMaxSeq(course_id);
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
            Chapter.UpdateChapterSequence(course_id, maxSeq, seq);
            if (Request.UrlReferrer != null)
                Response.Redirect(Request.UrlReferrer.ToString());
        }
    }
}