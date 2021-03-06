using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Ganss.XSS;

namespace WAPP_Assignment.Admin
{
    public partial class AddChapter : UtilClass.BaseAdminPage
    {
        private int course_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            course_id = GetQueryString("course_id");
            DataTable courseTable = WAPP_Assignment.CourseC.GetCourseData(course_id);
            if (courseTable.Rows.Count == 0)
            {
                return;
            }
            ViewCourseLink.Text = courseTable.Rows[0]["title"].ToString();
            ViewCourseLink.NavigateUrl = $"/ViewCourse.aspx?course_id={course_id}";
            EditLink.NavigateUrl = $"/Admin/Course/Chapter/EditChapMenu.aspx?course_id={course_id}";

            if (!IsPostBack)
            {
                int maxSeq = ChapterC.GetChapterMaxSeq(course_id) + 1;
                ChapNoTxtBox.Attributes.Add("Max", maxSeq.ToString());
                ChapNoRangeValidator.MaximumValue = maxSeq.ToString();
                ChapNoRangeValidator.MinimumValue = "1";
                ChapNoTxtBox.Text = maxSeq.ToString();
            }
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            int maxSeq = Convert.ToInt32(ChapNoTxtBox.Attributes["Max"]);
            int seq = Convert.ToInt32(ChapNoTxtBox.Text);
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
                    ChapterC.UpdateChapterSequence(course_id, seq, maxSeq);
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO chapter (course_id, title, content, sequence) VALUES (@course_id, @title, @content, @sequence);";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@content", content);
                    cmd.Parameters.AddWithValue("@sequence", seq);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            Response.Redirect($"/Admin/Course/Chapter/EditChapMenu.aspx?course_id={course_id}");
        }

    }
}