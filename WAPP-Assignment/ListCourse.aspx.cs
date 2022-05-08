using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
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
            string userType;
            List<int> enroll_course_id = new List<int>();
            if (Session["user_id"] == null)
            {
                userType = "nobody";
            }
            else if ((bool)Session["isAdmin"])
            {
                userType = "admin";
            }
            else
            {
                userType = "student";
                enroll_course_id = Student.GetEnrolledCourseID(Convert.ToInt32(Session["user_id"]));
            }
            DataTable dt = Course.GetAllCourseData();
            foreach (DataRow dr in dt.Rows)
            {
                DataTable courseCatTable = Category.GetCourseCategoryData(Convert.ToInt32(dr["course_id"]));
                Panel cPanel = new Panel();
                Panel imgPanel = new Panel();
                cPanel.CssClass = "container bg-blue course-container";
                //cPanel.ID = $"CourseContainer_{dr["course_id"]}";
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
                    CssClass = "course-title",
                    //ID = $"CourseTitle_{dr["course_id"]}",
                };
                title.Style.Add("font-size", "24px");
                cPanel.Controls.Add(title);
                Panel categoryPanel = new Panel
                {
                    CssClass = "course-category"
                };
                cPanel.Controls.Add(new Literal { Text = "<br />" } );
                cPanel.Controls.Add(categoryPanel);

                foreach (DataRow categoryRow in courseCatTable.Rows)
                {
                    Label cat = new Label
                    {
                        Text = categoryRow["name"].ToString(),
                    };
                    categoryPanel.Controls.Add(cat);
                    if (courseCatTable.Rows.IndexOf(categoryRow) != courseCatTable.Rows.Count - 1)
                        categoryPanel.Controls.Add(new Literal { Text = ", "});
                }

                Label description = new Label
                {
                    Text = dr["description"].ToString(),
                };
                cPanel.Controls.Add(new Literal { Text = "<br />" } );
                cPanel.Controls.Add(description);
                cPanel.Controls.Add(new Literal { Text = "<br />" } );
                CoursePlaceholder.Controls.Add(cPanel);
                if (userType == "admin")
                {
                    HyperLink editLink = new HyperLink
                    {
                        Text = "Edit Course",
                        NavigateUrl = $"/Admin/Course/EditCourse.aspx?course_id={dr["course_id"]}",
                        CssClass = "btn btn-secondary btn-sm",
                    };
                    cPanel.Controls.Add(editLink);
                }
                else if (userType == "student")
                {
                    if (enroll_course_id.Contains(Convert.ToInt32(dr["course_id"]))) {
                        int chapter_id = Chapter.GetFirstChapterID(Convert.ToInt32(dr["course_id"]));
                        HyperLink learnLink = new HyperLink
                        {
                            NavigateUrl = $"/Learn/ViewChapter.aspx?chapter_id={chapter_id}",
                            Text = "Learn",
                            CssClass = "btn btn-secondary btn-sm",
                        };
                        HyperLink viewLink = new HyperLink
                        {
                            NavigateUrl = $"/ViewCourse.aspx?course_id={dr["course_id"]}",
                            Text = "View Course",
                            CssClass = "btn btn-secondary btn-sm",
                        };
                        cPanel.Controls.Add(learnLink);
                        cPanel.Controls.Add(new Literal { Text = "<br />"} );
                        cPanel.Controls.Add(viewLink);
                    }
                    else
                    {
                        HyperLink enrollLink = new HyperLink
                        {
                            Text = "Enroll",
                            NavigateUrl = $"/EnrollCourse.aspx?course_id={dr["course_id"]}",
                            CssClass = "btn btn-secondary btn-sm",
                        };
                        cPanel.Controls.Add(enrollLink);
                        HyperLink viewLink = new HyperLink
                        {
                            NavigateUrl = $"/ViewCourse.aspx?course_id={dr["course_id"]}",
                            Text = "View Course",
                            CssClass = "btn btn-secondary btn-sm",
                        };
                        cPanel.Controls.Add(new Literal { Text = "<br />"} );
                        cPanel.Controls.Add(viewLink);
                    }
                }
                else
                {
                    HyperLink viewLink = new HyperLink
                    {
                        NavigateUrl = $"/ViewCourse.aspx?course_id={dr["course_id"]}",
                        Text = "View Course",
                    };
                    cPanel.Controls.Add(viewLink);
                }
                CoursePlaceholder.Controls.Add(new Literal { Text = "<br />" });
            }
        }

        protected void AddCourseBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Admin/Course/AddCourse.aspx");
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            string keyword;
            if (true)
            {
                System.Diagnostics.Debug.WriteLine("title");
                keyword = SearchTitleTxtBox.Text;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("cat");
                keyword = SearchCatTxtBox.Text;
            }
            //Response.Redirect("/ListCourse.aspx?keyword=");
        }

        protected void FilterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(FilterList.SelectedValue);
        }

        [WebMethod]
        public static List<string> SearchCategory(string prefixText, int count)
        {
            return MyAutoComplete.ListCategory(prefixText, count);
        }

        [WebMethod]
        public static List<string> SearchTitle(string prefixText, int count)
        {
            return MyAutoComplete.ListCourseTitle(prefixText, count);
        }
    }
}