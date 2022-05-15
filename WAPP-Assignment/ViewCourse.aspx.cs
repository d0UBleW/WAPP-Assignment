using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

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
            double overallRating = Course.GetCourseOverallRating(course_id);
            OverallRatingLbl.Text = $"Rating: {overallRating.ToString("0.00")}/5";

            RatingSubPanel.Visible = false;
            if (userType == "student")
            {
                int student_id = Convert.ToInt32(Session["user_id"]);
                if (StudentC.IsEnrolled(student_id, course_id))
                {
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
            EnrollLink.NavigateUrl = $"/Student/Course/EnrollCourse.aspx?course_id={course_id}";
            UnenrollLink.NavigateUrl = "#";

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

            DataTable ratingTable = Course.GetCourseRating(course_id);
            int i = 0;
            foreach (DataRow ratingData in ratingTable.Rows)
            {
                Panel p = new Panel();
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
                p.Controls.Add(new Label { Text = ratingData["content"].ToString() });
            }
        }

        protected void RatingBtn_Click(object sender, EventArgs e)
        {
            int student_id = Convert.ToInt32(Session["user_id"]);
            int rating = Rating1.CurrentRating;
            string content = RatingContentTxtBox.Text;
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