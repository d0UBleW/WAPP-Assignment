using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Ganss.XSS;

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
            DataTable courseTable = CourseC.GetCourseData(course_id);
            if (courseTable.Rows.Count == 0)
            {
                CourseDetailPanel.Visible = false;
                Response.Redirect("~/ListCourse.aspx");
                return;
            }
            DataRow courseRow = courseTable.Rows[0];
            ThumbnailImage.ImageUrl = $"/upload/thumbnail/{courseRow["thumbnail"]}";
            TitleLbl.Text = courseRow["title"].ToString();

            BreadLiteral.Text = TitleLbl.Text;

            Page.Title = courseRow["title"].ToString();

            DataTable courseCatTable = Category.GetCourseCategoryData(course_id);
            foreach (DataRow categoryRow in courseCatTable.Rows)
            {
                Label cat = new Label
                {
                    Text = categoryRow["name"].ToString(),
                    CssClass = "course-category-item badge rounded-pill bg-secondary",
                };
                CategoryPanel.Controls.Add(cat);
            }

            DescriptionLbl.Text = courseRow["description"].ToString();
            double overallRating = CourseC.GetCourseOverallRating(course_id);
            int ratingCount = CourseC.GetCourseRatingCount(course_id);
            OverallRatingLbl.Text = $"Rating: {overallRating:0.00}/5 ({ratingCount})";

            RatingSubPanel.Visible = false;

            EnrollLink.NavigateUrl = $"/Student/Course/EnrollCourse.aspx?course_id={course_id}";
            UnenrollLink.NavigateUrl = $"/Student/Course/UnenrollCourse.aspx?course_id={course_id}";
            EditLink.NavigateUrl = $"/Admin/Course/EditCourse.aspx?course_id={course_id}";
            DelLink.NavigateUrl = $"/Admin/Course/DeleteCourse.aspx?course_id={course_id}";

            EnrollLink.Visible = true;
            UnenrollLink.Visible = false;

            AdminActionPanel.Visible = false;
            if (userType == "student")
            {
                int student_id = Convert.ToInt32(Session["user_id"]);
                if (StudentC.IsEnrolled(student_id, course_id))
                {
                    CourseLink.Text = "My Courses";
                    CourseLink.NavigateUrl = "~/Student/Course/MyCourse.aspx";
                    UnenrollLink.Visible = true;
                    EnrollLink.Visible = false;
                    RatingSubPanel.Visible = true;
                }
                else
                {
                    EnrollLink.Visible = true;
                    UnenrollLink.Visible = false;
                }
            }
            else if (userType == "admin")
            {
                NonAdminLinkPanel.Visible = false;
                AdminActionPanel.Visible = true;
                StudentDataLink.NavigateUrl = $"/Admin/Course/EnrolledStudent.aspx?course_id={course_id}";
                GradeLink.NavigateUrl = $"/Admin/Course/Grades.aspx?course_id={course_id}";
                EditChapMenuLink.NavigateUrl = $"/Admin/Course/Chapter/EditChapMenu.aspx?course_id={course_id}";
                EditExamMenuLink.NavigateUrl = $"/Admin/Course/Exam/EditExamMenu.aspx?course_id={course_id}";
            }

            DataTable chapterTable = ChapterC.GetCourseChapterData(course_id);
            foreach (DataRow chapterRow in chapterTable.Rows)
            {
                HyperLink title = new HyperLink
                {
                    Text = $"{chapterRow["sequence"]}. {chapterRow["title"]}",
                };
                title.NavigateUrl = "#";
                if (userType == "admin")
                {
                    title.NavigateUrl = $"/Student/Learn/ViewChapter.aspx?chapter_id={chapterRow["chapter_id"]}";
                }
                if (userType == "student")
                {
                    int student_id = Convert.ToInt32(Session["user_id"]);
                    List<int> enrolled = StudentC.GetEnrolledCourseID(student_id);
                    if (enrolled.Contains(course_id))
                    {
                        title.NavigateUrl = $"/Student/Learn/ViewChapter.aspx?chapter_id={chapterRow["chapter_id"]}";
                    }
                }
                ChapterTOCPanel.Controls.Add(title);
                ChapterTOCPanel.Controls.Add(new Literal { Text = "<br />" });
            }

            DataTable examTable = ExamC.GetCourseExamData(course_id);
            foreach (DataRow examRow in examTable.Rows)
            {
                HyperLink title = new HyperLink
                {
                    Text = $"{examRow["title"]}",
                };
                title.NavigateUrl = "#";
                if (userType == "admin")
                {
                    title.NavigateUrl = $"/Student/Learn/ReviewExam.aspx?exam_id={examRow["exam_id"]}";
                }
                if (userType == "student")
                {
                    int student_id = Convert.ToInt32(Session["user_id"]);
                    List<int> enrolled = StudentC.GetEnrolledCourseID(student_id);
                    if (enrolled.Contains(course_id))
                    {
                        title.NavigateUrl = $"/Student/Learn/ViewExam.aspx?exam_id={examRow["exam_id"]}";
                    }
                }
                ExamPanel.Controls.Add(title);
                ExamPanel.Controls.Add(new Literal { Text = "<br />" });
            }

            RtgLbl.Text = $"Rating ({ratingCount})";
            DataTable ratingTable = CourseC.GetCourseRating(course_id);
            int i = 0;
            foreach (DataRow ratingData in ratingTable.Rows)
            {
                Panel p = new Panel();
                int student_id = Convert.ToInt32(ratingData["student_id"]);
                DataRow studentData = StudentC.GetStudentData(student_id);
                Image image = new Image
                {
                    ImageUrl = $"/upload/profile/{studentData["profile"]}",
                    Width = 40,
                    Height = 40,
                };
                Panel imgPanel = new Panel
                {
                    CssClass = "user-rating-img",
                };
                p.Controls.Add(imgPanel);
                imgPanel.Controls.Add(image);
                Panel namePanel = new Panel
                {
                    CssClass = "user-rating-name",
                };
                p.Controls.Add(namePanel);
                namePanel.Controls.Add(new Literal { Text = studentData["full_name"].ToString() });

                RatingPanel.Controls.Add(p);
                AjaxControlToolkit.Rating rating = new AjaxControlToolkit.Rating
                {
                    ID = $"userRating_{i++}",
                    MaxRating = 5,
                    CurrentRating = Convert.ToInt32(ratingData["rating"]),
                    StarCssClass = "Star",
                    WaitingStarCssClass = "WaitingStar",
                    FilledStarCssClass = "FilledStar",
                    EmptyStarCssClass = "EmptyStar",
                    ReadOnly = true,
                };
                p.Controls.Add(rating);
                p.Controls.Add(new Literal { Text = "<br/>" });
                p.Controls.Add(new Literal { Text = ratingData["content"].ToString() });
                RatingPanel.Controls.Add(new Literal { Text = "<br/>" });
            }
        }

        protected void RatingBtn_Click(object sender, EventArgs e)
        {
            int student_id = Convert.ToInt32(Session["user_id"]);
            int rating = Rating1.CurrentRating;
            var sanitizer = new HtmlSanitizer();
            string rawContent = RatingContentTxtBox.Text;
            string content = sanitizer.Sanitize(rawContent);
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE rating WHERE student_id=@student_id AND course_id=@course_id;";
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "INSERT INTO rating (student_id, course_id, rating, content) VALUES (@student_id, @course_id, @rating, @content);";
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.Parameters.AddWithValue("@rating", rating);
                    cmd.Parameters.AddWithValue("@content", content);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            Response.Redirect(Request.UrlReferrer.ToString());
        }
    }
}