using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public partial class ViewCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"] == "EditCourseBtn")
                {
                    EditCourseBtn_Click(null, null);
                }
            }
            DataTable dt = GetCourseData();
            StringBuilder courseContainer = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                courseContainer.AppendLine("<div class=\"container\">");
                courseContainer.AppendLine("<div class=\"image-container\">");
                courseContainer.AppendLine($"<img src=\"/upload/{dr["thumbnail"]}\" width=200px, height=200px />");
                courseContainer.AppendLine("</div>");
                courseContainer.AppendLine($"<h3>{dr["title"]}</h3>");
                courseContainer.AppendLine($"<span>{dr["description"]}</span><br/><br/>");
                courseContainer.AppendLine($"<input type=\"button\" value=\"Edit Course\" onclick='javascript:__doPostBack(\"EditCourseBtn\", \"{dr["course_id"]}\")'/>");
                courseContainer.AppendLine("</div>");
            }
            CoursePlaceholder.Controls.Add(new Literal { Text = courseContainer.ToString() });
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
            string course_id = Request.Form["__EVENTARGUMENT"];
            Response.Redirect($"/Admin/EditCourse.aspx?course_id={course_id}");
        }
    }
}