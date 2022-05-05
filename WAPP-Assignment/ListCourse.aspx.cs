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
    public partial class ListCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Course.GetAllCourseData();
            foreach (DataRow dr in dt.Rows)
            {
                Panel cPanel = new Panel();
                Panel imgPanel = new Panel();
                cPanel.CssClass = "container bg-blue";
                Image thumbnail = new Image
                {
                    ImageUrl = "/upload/loading.gif",
                    Width = 200,
                    Height = 200,
                };
                cPanel.Controls.Add(imgPanel);
                thumbnail.Attributes.Add("onload", $"javascript:this.onload=null;this.src='/upload/thumbnail/{dr["thumbnail"]}'");
                imgPanel.Controls.Add(thumbnail);
                HyperLink title = new HyperLink
                {
                    NavigateUrl = $"/ViewCourse.aspx?course_id={dr["course_id"]}",
                    Text = dr["title"].ToString(),
                };
                title.Style.Add("font-size", "36px");
                cPanel.Controls.Add(title);
                Label description = new Label
                {
                    Text = dr["description"].ToString(),
                };
                cPanel.Controls.Add(new Literal { Text = "<br />" } );
                cPanel.Controls.Add(description);
                CoursePlaceholder.Controls.Add(cPanel);
                if (Session["isAdmin"] == null)
                {
                }
                else if ((bool)Session["isAdmin"])
                {
                    Button editBtn = new Button
                    {
                        Text = "Edit Course",
                        ID = $"editBtn_{dr["course_id"]}"
                    };
                    editBtn.Attributes.Add("data-course-id", dr["course_id"].ToString());
                    editBtn.Click += new EventHandler(EditCourseBtn_Click);
                    //editBtn.Click += (s, evt) =>
                    //{
                    //    Regex rg = new Regex(@"editBtn(\d+)");
                    //    Match match = rg.Match(editBtn.ID);
                    //    string course_id = match.Groups[1].Value;
                    //    Response.Redirect($"/Admin/EditCourse.aspx?course_id={course_id}");
                    //};
                    CoursePlaceholder.Controls.Add(editBtn);
                }
                else if (!(bool)Session["isAdmin"])
                {
                    Button enrollBtn = new Button
                    {
                        Text = "Enroll",
                        ID = $"enrollBtn_{dr["course_id"]}"
                    };
                    enrollBtn.Attributes.Add("data-course-id", dr["course_id"].ToString());
                    enrollBtn.Click += new EventHandler(EnrollBtn_Click);
                    CoursePlaceholder.Controls.Add(enrollBtn);
                }
                CoursePlaceholder.Controls.Add(new Literal { Text = "<br />" });
            }
        }

        protected void EnrollBtn_Click(object sender ,EventArgs e)
        {
            Button btn = sender as Button;
            string course_id = btn.Attributes["data-course-id"];
            Response.Redirect($"/EnrollCourse.aspx?course_id={course_id}");
        }

        protected void EditCourseBtn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Regex rg = new Regex(@"editBtn_(\d+)");
            Match match = rg.Match(btn.ID);
            string course_id = match.Groups[1].Value;
            Response.Redirect($"/Admin/Course/EditCourse.aspx?course_id={course_id}");
        }

        protected void AddCourseBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/Course/AddCourse.aspx");
        }
    }
}