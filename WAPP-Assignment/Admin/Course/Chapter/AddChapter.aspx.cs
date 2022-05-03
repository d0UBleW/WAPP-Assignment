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
    public partial class AddChapter : System.Web.UI.Page
    {
        private int course_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            string course_id_temp = Request.QueryString["course_id"];
            if (String.IsNullOrEmpty(course_id_temp))
            {
                // throw error
                return;
            }
            course_id = Convert.ToInt32(course_id_temp);
            if (!IsPostBack)
            {
                int maxSeq = Chapter.GetChapterMaxSeq(course_id) + 1;
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
                    Chapter.UpdateChapterSequence(course_id, seq, maxSeq);
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
            Response.Redirect($"/Admin/Course/EditCourse.aspx?course_id={course_id}");
        }

    }
}