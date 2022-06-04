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
            course_id = GetQueryString("course_id");
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
            OverallRatingLbl.Text = $"Rating: {overallRating:0.00}/5.00 ({ratingCount})";

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
                    CssClass = "list-group-item list-group-item-action",
                };
                title.NavigateUrl = "~/Login.aspx";
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
                    else
                    {
                        title.NavigateUrl = "#";
                        title.Attributes.Add("data-bs-toggle", "modal");
                        title.Attributes.Add("data-bs-target", "#enrollModal");
                    }
                }
                ChapListPanel.Controls.Add(title);
            }

            DataTable examTable = ExamC.GetCourseExamData(course_id);
            foreach (DataRow examRow in examTable.Rows)
            {
                HyperLink title = new HyperLink
                {
                    Text = $"{examRow["title"]}",
                    CssClass = "list-group-item list-group-item-action",
                };
                title.NavigateUrl = "~/Login.aspx";
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
                    else
                    {
                        title.NavigateUrl = "#";
                        title.Attributes.Add("data-bs-toggle", "modal");
                        title.Attributes.Add("data-bs-target", "#enrollModal");
                    }
                }
                ExamListPanel.Controls.Add(title);
            }

            RtgLbl.Text = $"Rating ({ratingCount})";
            DataTable ratingTable = CourseC.GetCourseRating(course_id);
            int i = 0;
            foreach (DataRow ratingData in ratingTable.Rows)
            {
                Panel p = new Panel
                {
                    CssClass = "list-group-item",
                };
                RatingListPanel.Controls.Add(p);
                Panel profileRow = new Panel
                {
                    CssClass = "row row-cols-3 d-flex flex-row align-items-center",
                };
                int student_id = Convert.ToInt32(ratingData["student_id"]);
                DataRow studentData = StudentC.GetStudentData(student_id);
                Image image = new Image
                {
                    ImageUrl = $"/upload/profile/{studentData["profile"]}",
                    CssClass = "img-fluid",
                    AlternateText = "Profile Image"
                };
                Panel imgPanel = new Panel
                {
                    CssClass = "col-sm-2",
                };
                imgPanel.Controls.Add(image);
                profileRow.Controls.Add(imgPanel);
                Panel namePanel = new Panel
                {
                    CssClass = "col-sm-8",
                };
                namePanel.Controls.Add(new Label { Text = studentData["full_name"].ToString() });
                profileRow.Controls.Add(namePanel);

                if (userType == "admin")
                {
                    Panel deleteRatingPanel = new Panel
                    {
                        CssClass = "col-sm-2",
                    };
                    HyperLink delLink = new HyperLink
                    {
                        NavigateUrl = $"~/Admin/Course/DeleteRating.aspx?course_id={course_id}&student_id={student_id}",
                        CssClass = "btn btn-outline-danger btn-sm rounded-circle",
                    };
                    delLink.Attributes.Add("data-action", "warn");
                    Literal delIcon = new Literal
                    {
                        Text = "<i class=\"bi bi-x-lg\"></i>",
                    };
                    delLink.Controls.Add(delIcon);
                    deleteRatingPanel.Controls.Add(delLink);
                    profileRow.Controls.Add(deleteRatingPanel);
                }

                Panel ratingRow = new Panel
                {
                    CssClass = "row",
                };
                Panel starPanel = new Panel();
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
                starPanel.Controls.Add(rating);
                ratingRow.Controls.Add(starPanel);
                Panel rateText = new Panel
                {
                    CssClass = "text-wrap text-break",
                };
                rateText.Controls.Add(new Literal { Text = ratingData["content"].ToString() });
                ratingRow.Controls.Add(rateText);
                p.Controls.Add(profileRow);
                p.Controls.Add(ratingRow);
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