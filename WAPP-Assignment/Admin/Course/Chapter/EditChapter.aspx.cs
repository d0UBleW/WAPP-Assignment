using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Ganss.XSS;

namespace WAPP_Assignment.Admin
{
    public partial class EditChapter : System.Web.UI.Page
    {
        private int chapter_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            string chapter_id_temp = Request.QueryString["chapter_id"];
            if (string.IsNullOrEmpty(chapter_id_temp))
            {
                return;
            }
            chapter_id = int.Parse(chapter_id_temp);
            if (!IsPostBack)
            {
                DataTable dt = Chapter.GetChapterData(chapter_id);
                if (dt.Rows.Count == 0)
                {
                    return;
                }
                DataRow dr = dt.Rows[0];
                TitleTxtBox.Text = dr["title"].ToString();
                EditorTxtBox.Text = dr["content"].ToString();
                int maxSeq = Chapter.GetChapterMaxSeq(Convert.ToInt32(dr["course_id"]));
                ChapNoTxtBox.Attributes.Add("Max", maxSeq.ToString());
                ChapNoRangeValidator.MaximumValue = maxSeq.ToString();
                ChapNoRangeValidator.MinimumValue = "1";
                ChapNoTxtBox.Text = dr["sequence"].ToString();
                CourseIDField.Value = dr["course_id"].ToString();
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            int seq = Convert.ToInt32(ChapNoTxtBox.Text);
            DataTable dt = Chapter.GetChapterData(chapter_id);
            int course_id = Convert.ToInt32(dt.Rows[0]["course_id"]);
            int oldSeq = Convert.ToInt32(dt.Rows[0]["sequence"]);
            string title = TitleTxtBox.Text;
            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedTags.Add("iframe");
            sanitizer.AllowedTags.Add("oembed");
            var rawHtml = EditorTxtBox.Text;
            string content = sanitizer.Sanitize(rawHtml);
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    Chapter.UpdateChapterSequence(course_id, seq, oldSeq);
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE chapter SET title=@title, content=@content, sequence=@sequence WHERE chapter_id=@chapter_id";
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@content", content);
                    cmd.Parameters.AddWithValue("@sequence", seq);
                    cmd.Parameters.AddWithValue("@chapter_id", chapter_id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }


        protected void BackLinkButton_Click(object sender, EventArgs e)
        {
            DataTable dataTable = Chapter.GetChapterData(chapter_id);
            DataRow dataRow = dataTable.Rows[0];
            Response.Redirect($"/Admin/Course/EditCourse.aspx?course_id={dataRow["course_id"]}");
        }
    }
}