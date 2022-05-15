using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAPP_Assignment
{
    public partial class ViewCourse : UtilClass.BasePage
    {
        private int course_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            string course_id_temp = Request.QueryString["course_id"];
            if (string.IsNullOrEmpty(course_id_temp))
            {
                return;
            }
            try
            {
                course_id = int.Parse(course_id_temp);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
            DataTable courseTable = Course.GetCourseData(course_id);
            if (courseTable.Rows.Count == 0)
            {
                CourseDetailPanel.Visible = false;
                return;
            }
            DataRow courseRow = courseTable.Rows[0];
            ThumbnailImage.ImageUrl = $"/upload/thumbnail/{courseRow["thumbnail"]}";
            TitleLbl.Text = courseRow["title"].ToString();
            Page.Title = courseRow["title"].ToString();
            DescriptionLbl.Text = courseRow["description"].ToString();

            if (userType == "student")
            {
                int student_id = Convert.ToInt32(Session["user_id"]);
                if (Student.IsEnrolled(student_id, course_id))
                {
                    // LearnBtn.Visible = true;
                    UnenrollBtn.Visible = true;
                    EnrollBtn.Visible = false;
                }
                else
                {
                    EnrollBtn.Visible = true;
                    UnenrollBtn.Visible = false;
                    // LearnBtn.Visible = false;
                }
            }

            DataTable chapterTable = Chapter.GetCourseChapterData(course_id);
            foreach (DataRow chapterRow in chapterTable.Rows)
            {
                HyperLink title = new HyperLink
                {
                    Text = $"{chapterRow["sequence"]}. {chapterRow["title"]}",
                };
                if (userType == "student" || userType == "admin")
                {
                    title.NavigateUrl = $"/Student/Learn/ViewChapter.aspx?chapter_id={chapterRow["chapter_id"]}";
                }
                else
                {
                    title.NavigateUrl = "#";
                }
                ChapterTOCPanel.Controls.Add(title);
                ChapterTOCPanel.Controls.Add(new Literal { Text = "<br />" });
            }
            DataTable examTable = Exam.GetCourseExamData(course_id);
            foreach (DataRow examRow in examTable.Rows)
            {
                HyperLink title = new HyperLink
                {
                    Text = $"{examRow["title"]}",
                };
                if (userType == "student" || userType == "admin")
                {
                    title.NavigateUrl = $"/Student/Learn/ViewExam.aspx?exam_id={examRow["exam_id"]}";
                }
                else
                {
                    title.NavigateUrl = "#";
                }
                ExamPanel.Controls.Add(title);
                ExamPanel.Controls.Add(new Literal { Text = "<br />" });
            }
        }

        protected void EnrollBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/Student/Course/EnrollCourse.aspx?course_id={course_id}");
        }

        protected void LearnBtn_Click(object sender, EventArgs e)
        {
            int chapter_id = Chapter.GetFirstChapterID(course_id);
            Response.Redirect($"/Student/Learn/ViewChapter.aspx?chapter_id={chapter_id}");
        }
    }
}