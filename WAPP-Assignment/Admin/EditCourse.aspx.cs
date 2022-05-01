using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WAPP_Assignment.Admin
{
    public partial class EditCourse : System.Web.UI.Page
    {
        private string course_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            course_id = Request.QueryString["course_id"];
            if (String.IsNullOrEmpty(course_id))
            {
                return;
            }
            if (!IsPostBack)
            {
                DataTable courseDataTable = GetCourseData(course_id);
                if (courseDataTable.Rows.Count == 0)
                {
                    return;
                }
                DataRow courseData = courseDataTable.Rows[0];
                TitleTxtBox.Text = courseData["title"].ToString();
                DescTxtBox.Text = courseData["description"].ToString();
                DataTable chapterDataTable = GetChapterData(course_id);
                StringBuilder sb = new StringBuilder();
                foreach (DataRow chapterData in chapterDataTable.Rows)
                {
                    sb.AppendLine("<div class=\"container\">");
                    sb.AppendLine($"<h3>{chapterData["title"]}</h3>");
                    sb.AppendLine($"<input type=\"button\" value=\"Edit Chapter\" onclick='javascript:__doPostBack(\"AspBtn\", \"{chapterData["chapter_id"]}\")'/>");
                    sb.AppendLine("</div>");
                }
                ChapterPlaceholder.Controls.Add(new Literal { Text = sb.ToString() });
            }
        }
        protected DataTable GetChapterData(string course_id)
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM chapter WHERE course_id=@course_id";
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@course_id", Convert.ToInt32(course_id));
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

        protected DataTable GetCourseData(string course_id)
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM course WHERE course_id=@course_id";
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@course_id", Convert.ToInt32(course_id));
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

        protected void AddChapBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/Admin/AddChapter.aspx?course_id={course_id}");
        }
    }
}