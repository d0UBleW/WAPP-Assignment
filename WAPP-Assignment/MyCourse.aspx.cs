using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace WAPP_Assignment
{
    public partial class MyCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["user_id"].ToString()) || (bool)Session["isAdmin"])
            {
                return;
            }
            int student_id = int.Parse(Session["user_id"].ToString());
            DataTable courseTable = Course.GetEnrolledCourseData(student_id);
            StringBuilder courseContainer = new StringBuilder();
            foreach (DataRow dr in courseTable.Rows)
            {
                courseContainer.AppendLine("<div class=\"container\">");
                courseContainer.AppendLine("<div class=\"image-container\">");
                courseContainer.AppendLine($"<img src=\"/upload/loading.gif\" onload=\"this.onload=null;this.src='/upload/thumbnail/{dr["thumbnail"]}'\" width=200px, height=200px />");
                courseContainer.AppendLine("</div>");
                courseContainer.AppendLine($"<h3><a href=\"/ViewCourse.aspx?course_id={dr["course_id"]}\">{dr["title"]}</a></h3>");
                courseContainer.AppendLine($"<span>{dr["description"]}</span>");
                courseContainer.AppendLine("</div>");
                courseContainer.AppendLine("<br />");
                // courseContainer.AppendLine($"<input type=\"button\" value=\"Edit Course\" onclick='javascript:__doPostBack(\"EditCourseBtn\", \"{dr["course_id"]}\")'/>");
                CoursePlaceholder.Controls.Add(new Literal { Text = courseContainer.ToString() });
                Button viewBtn = new Button
                {
                    Text = "View Course",
                    ID = $"viewBtn_{dr["course_id"]}"
                };
                viewBtn.Attributes.Add("data-course-id", dr["course_id"].ToString());
                //viewBtn.Click += new EventHandler()
                courseContainer.Clear();
            }
        }
    }
}