using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public partial class ViewCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
            //    if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"] == "EditCourseBtn")
            //    {
            //        EditCourseBtn_Click(null, null);
            //    }
            //}
            DataTable dt = GetCourseData();
            StringBuilder courseContainer = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                courseContainer.AppendLine("<div class=\"container\">");
                courseContainer.AppendLine("<div class=\"image-container\">");
                courseContainer.AppendLine($"<img src=\"/upload/loading.gif\" onload=\"this.onload=null;this.src='/upload/thumbnail/{dr["thumbnail"]}'\" width=200px, height=200px />");
                courseContainer.AppendLine("</div>");
                courseContainer.AppendLine($"<h3>{dr["title"]}</h3>");
                courseContainer.AppendLine($"<span>{dr["description"]}</span>");
                courseContainer.AppendLine("</div>");
                courseContainer.AppendLine("<br />");
                // courseContainer.AppendLine($"<input type=\"button\" value=\"Edit Course\" onclick='javascript:__doPostBack(\"EditCourseBtn\", \"{dr["course_id"]}\")'/>");
                CoursePlaceholder.Controls.Add(new Literal { Text = courseContainer.ToString() });
                Button editBtn = new Button
                {
                    Text = "Edit Course",
                    ID = $"editBtn{dr["course_id"]}"
                };
                editBtn.Click += new EventHandler(EditCourseBtn_Click);
                //editBtn.Click += (s, evt) =>
                //{
                //    Regex rg = new Regex(@"editBtn(\d+)");
                //    Match match = rg.Match(editBtn.ID);
                //    string course_id = match.Groups[1].Value;
                //    Response.Redirect($"/Admin/EditCourse.aspx?course_id={course_id}");
                //};
                CoursePlaceholder.Controls.Add(editBtn);
                courseContainer.Clear();
            }
        }

        protected DataTable GetCourseData()
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM course";
                    cmd.Connection = conn;
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

        protected void EditCourseBtn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Regex rg = new Regex(@"editBtn(\d+)");
            Match match = rg.Match(btn.ID);
            string course_id = match.Groups[1].Value;
            Response.Redirect($"/Admin/EditCourse.aspx?course_id={course_id}");
        }

        protected void AddCourseBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/AddCourse.aspx");
        }
    }
}