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
    public partial class EditChapter : UtilClass.BaseAdminPage
    {
        private int chapter_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            chapter_id = GetQueryString("chapter_id");
            DataTable dt = ChapterC.GetChapterData(chapter_id);
            if (dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            DataTable courseTable = CourseC.GetCourseData(Convert.ToInt32(dr["course_id"]));
            ViewCourseLink.Text = $"{courseTable.Rows[0]["title"]}";
            ViewCourseLink.NavigateUrl = $"/ViewCourse.aspx?course_id={dr["course_id"]}";
            EditLink.NavigateUrl = $"/Admin/Course/Chapter/EditChapMenu.aspx?course_id={dr["course_id"]}";
            ChapLbl.Text = $"{dr["sequence"]}. {dr["title"]}";
            if (!IsPostBack)
            {
                TitleTxtBox.Text = dr["title"].ToString();
                EditorTxtBox.Text = dr["content"].ToString();
                int maxSeq = ChapterC.GetChapterMaxSeq(Convert.ToInt32(dr["course_id"]));
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
            DataTable dt = ChapterC.GetChapterData(chapter_id);
            int course_id = Convert.ToInt32(dt.Rows[0]["course_id"]);
            int oldSeq = Convert.ToInt32(dt.Rows[0]["sequence"]);
            string title = MyUtil.SanitizeInput(TitleTxtBox);
            //string title = TitleTxtBox.Text;
            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedTags.Add("iframe");
            sanitizer.AllowedTags.Add("oembed");
            string rawHtml = EditorTxtBox.Text;
            string content = sanitizer.Sanitize(rawHtml);
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    ChapterC.UpdateChapterSequence(course_id, seq, oldSeq);
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
            DataTable dataTable = ChapterC.GetChapterData(chapter_id);
            DataRow dataRow = dataTable.Rows[0];
            Response.Redirect($"/Admin/Course/EditCourse.aspx?course_id={dataRow["course_id"]}");
        }
    }
}