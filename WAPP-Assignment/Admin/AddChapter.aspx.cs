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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void AddBtnASP_Click(object sender, EventArgs e)
        {
            string course_id = Request.QueryString["course_id"];
            if (String.IsNullOrEmpty(course_id))
            {
                // throw error
                return;
            }
            string title = TitleTxtBox.Text;
            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedTags.Add("iframe");
            var rawHtml = EditorTxtBox.Text;
            string content = sanitizer.Sanitize(rawHtml);
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "INSERT INTO chapter (course_id, title, content) VALUES (@course_id, @title, @content);";
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@content", content);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            Response.Redirect($"/Admin/EditCourse.aspx?course_id={course_id}");
        }
    }
}