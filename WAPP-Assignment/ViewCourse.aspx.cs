using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAPP_Assignment
{
    public partial class ViewCourse : System.Web.UI.Page
    {
        private int course_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            string course_id_temp = Request.QueryString["course_id"];
            if (string.IsNullOrEmpty(course_id_temp))
            {
                return;
            }
            string userType;
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
            }
            course_id = int.Parse(course_id_temp);
            DataTable courseTable = Course.GetCourseData(course_id);
            DataRow courseRow = courseTable.Rows[0];
            ThumbnailImage.ImageUrl = $"/upload/thumbnail/{courseRow["thumbnail"]}";
            TitleLbl.Text = courseRow["title"].ToString();
            DescriptionLbl.Text = courseRow["description"].ToString();

            if (userType == "student")
            {
                int student_id = Convert.ToInt32(Session["user_id"]);
                if (Student.IsEnrolled(student_id, course_id))
                {
                    LearnBtn.Visible = true;
                    EnrollBtn.Visible = false;
                }
                else
                {
                    EnrollBtn.Visible = true;
                    LearnBtn.Visible = false;
                }
            }

            DataTable chapterTable = Chapter.GetCourseChapterData(course_id);
            foreach (DataRow chapterRow in chapterTable.Rows)
            {
                Label title = new Label
                {
                    Text = $"{chapterRow["sequence"]}. {chapterRow["title"]}",
                };
                ChapterTOCPanel.Controls.Add(title);
                ChapterTOCPanel.Controls.Add(new Literal { Text = "<br />" });
            }
        }

        protected void EnrollBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/EnrollCourse.aspx?course_id={course_id}");
        }

        protected void LearnBtn_Click(object sender, EventArgs e)
        {
            int chapter_id = Chapter.GetFirstChapterID(course_id);
            Response.Redirect($"/Learn/ViewChapter.aspx?chapter_id={chapter_id}");
        }
    }
}