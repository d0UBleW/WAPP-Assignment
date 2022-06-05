using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace WAPP_Assignment.Learn
{
    public partial class ViewExam : UtilClass.BaseStudentPage
    {
        private int exam_id;
        private int student_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            exam_id = GetQueryString("exam_id");
            student_id = Convert.ToInt32(Session["user_id"]);
            if (userType == "admin")
            {
                SubmitBtn.Visible = false;
                student_id = 0;
            }
            DataTable examTable = ExamC.GetExamData(exam_id);
            if (examTable.Rows.Count == 0)
            {
                SubmitBtn.Visible = false;
                return;
            }
            DataRow examData = examTable.Rows[0];
            int course_id = Convert.ToInt32(examData["course_id"]);
            DataTable courseTable = CourseC.GetCourseData(course_id);
            CourseLink.NavigateUrl = $"/ViewCourse.aspx?course_id={course_id}";
            CourseLink.Text = $"{courseTable.Rows[0]["title"]}";
            ExamBreadLit.Text = $"{examData["title"]}";
            List<int> enrolled = StudentC.GetEnrolledCourseID(student_id);
            if (!enrolled.Contains(course_id) && userType != "admin")
            {
                if (Request.UrlReferrer != null)
                {
                    Response.Write($"<script>alert('Please enroll prior to viewing the exam'); window.location.href = '{Request.UrlReferrer}'</script>");
                    return;
                }
                Response.Write($"<script>alert('Please enroll prior to viewing the exam'); window.location.href = '/Home.aspx'</script>");
                return;
            }
            TitleLbl.Text = examData["title"].ToString();
            bool retake = Convert.ToBoolean(examData["retake"]);
            if (retake)
            {
                RetakeLbl.Text = "Yes";
            }
            else
            {
                RetakeLbl.Text = "No";
            }

            DataTable studentExamAttempt = StudentC.GetExamResult(student_id, exam_id);
            if (studentExamAttempt.Rows.Count > 0)
            {
                Panel p = new Panel
                {
                    CssClass = "alert alert-danger mb-3 fs-5",
                };
                Label info = new Label();
                if (!retake)
                {
                    info.Text = "You've attempted this exam before, no retake is allowed";
                    p.Controls.Add(info);
                    ContentPanel.Controls.Add(p);
                    SubmitBtn.Visible = false;
                    return;
                }
                else
                {
                    info.Text = "You've attempted this exam before, retaking will overwrite previous attempt!";
                    p.Controls.Add(info);
                    ContentPanel.Controls.Add(p);
                }
            }

            DataTable questionTable = Question.GetExamQuestion(exam_id);
            foreach (DataRow questData in questionTable.Rows)
            {
                Panel qPanel = ExamC.DisplayQue(exam_id, questData);
                ContentPanel.Controls.Add(qPanel);
                ContentPanel.Controls.Add(new Literal { Text = "<br/><br/>" });
            }
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            int score = 0;
            DataTable questionTable = Question.GetExamQuestion(exam_id);
            List<string> worksheet = new List<string>();
            foreach (DataRow questData in questionTable.Rows)
            {
                StringBuilder sb = new StringBuilder();
                int question_id = int.Parse(questData["question_id"].ToString());
                List<int> answer_id = Question.GetAnswerID(question_id);
                int numOfAnswer = answer_id.Count;
                if (numOfAnswer > 1)
                {
                    CheckBoxList optList = ContentPanel.FindControl($"optList_{question_id}") as CheckBoxList;
                    sb.Append($"{ question_id}.");
                    List<string> studentAns = new List<string>();
                    foreach (ListItem optListItem in optList.Items)
                    {
                        if (optListItem.Selected)
                        {
                            studentAns.Add(optListItem.Value);
                            if (answer_id.Contains(int.Parse(optListItem.Value)))
                            {
                                score++;
                            }
                        }
                    }
                    sb.Append(string.Join(",", studentAns));
                }
                else
                {
                    RadioButtonList optList = ContentPanel.FindControl($"optList_{question_id}") as RadioButtonList;
                    sb.Append($"{question_id}.{optList.SelectedValue}");
                    if (optList.SelectedValue != "" && answer_id.Contains(int.Parse(optList.SelectedValue)))
                    {
                        score++;
                    }
                }
                worksheet.Add(sb.ToString());
            }

            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.CommandText = "DELETE grade WHERE exam_id=@exam_id AND student_id=@student_id";
                    cmd.Parameters.AddWithValue("@exam_id", exam_id);
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "INSERT INTO [grade] (exam_id, student_id, value, worksheet) VALUES (@exam_id, @student_id, @value, @worksheet);";
                    cmd.Parameters.AddWithValue("@exam_id", exam_id);
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.Parameters.AddWithValue("@value", score);
                    cmd.Parameters.AddWithValue("@worksheet", string.Join(";", worksheet));
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            Response.Redirect($"/Student/Learn/ReviewExam.aspx?exam_id={exam_id}");
        }
    }
}