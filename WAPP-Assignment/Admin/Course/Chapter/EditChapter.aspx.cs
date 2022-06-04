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
        private int course_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            chapter_id = GetQueryString("chapter_id");
            DataTable dt = ChapterC.GetChapterData(chapter_id);
            if (dt.Rows.Count == 0)
            {
                return;
            }
            DataRow dr = dt.Rows[0];
            course_id = Convert.ToInt32(dr["course_id"]);
            DataTable courseTable = CourseC.GetCourseData(course_id);
            ViewCourseLink.Text = $"{courseTable.Rows[0]["title"]}";
            ViewCourseLink.NavigateUrl = $"/ViewCourse.aspx?course_id={course_id}";
            EditLink.NavigateUrl = $"/Admin/Course/Chapter/EditChapMenu.aspx?course_id={course_id}";
            ChapLbl.Text = $"{dr["sequence"]}. {dr["title"]}";
            if (!IsPostBack)
            {
                TitleTxtBox.Text = dr["title"].ToString();
                EditorTxtBox.Text = dr["content"].ToString();
                int maxSeq = ChapterC.GetChapterMaxSeq(course_id);
                ChapNoTxtBox.Attributes.Add("Max", maxSeq.ToString());
                ChapNoRangeValidator.MaximumValue = maxSeq.ToString();
                ChapNoRangeValidator.MinimumValue = "1";
                ChapNoTxtBox.Text = dr["sequence"].ToString();
                CourseIDField.Value = course_id.ToString();
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            int seq = Convert.ToInt32(ChapNoTxtBox.Text);
            DataTable dt = ChapterC.GetChapterData(chapter_id);
            int oldSeq = Convert.ToInt32(dt.Rows[0]["sequence"]);
            string title = MyUtil.SanitizeInput(TitleTxtBox);
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
            ChapLbl.Text = $"{seq}. {title}";
        }
    }
}
