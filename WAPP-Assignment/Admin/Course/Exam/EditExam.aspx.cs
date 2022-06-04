using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment.Admin.Course.Exam
{
    public partial class EditExam : UtilClass.BaseAdminPage
    {
        private int exam_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            exam_id = GetQueryString("exam_id");
            DataTable examTable = ExamC.GetExamData(exam_id);
            if (examTable.Rows.Count == 0)
            {
                return;
            }
            DataRow examData = examTable.Rows[0];
            EditLink.NavigateUrl = $"/Admin/Course/Exam/EditExamMenu.aspx?course_id={examData["course_id"]}";
            AddQueLink.NavigateUrl = $"~/Admin/Course/Exam/AddQuestion.aspx?exam_id={exam_id}";
            DataTable courseTable = CourseC.GetCourseData(Convert.ToInt32(examData["course_id"]));
            ViewCourseLink.NavigateUrl = $"/ViewCourse.aspx?course_id={examData["course_id"]}";
            ViewCourseLink.Text = $"{courseTable.Rows[0]["title"]}";
            ExamLbl.Text = $"{examData["title"]}";
            if (!IsPostBack)
            {
                TitleTxtBox.Text = examData["title"].ToString();
                RetakeChkBox.Checked = Convert.ToBoolean(examData["retake"]);
            }
            DataTable questionTable = Question.GetExamQuestion(exam_id);
            foreach (DataRow questData in questionTable.Rows)
            {
                Panel container = new Panel
                {
                    CssClass = "list-group-item list-group-item-action d-flex align-items-center",
                };
                QuePlaceholder.Controls.Add(container);
                Label quest = new Label
                {
                    Text = $"{questData["sequence"]}.",
                    CssClass = "me-auto"
                };
                container.Controls.Add(quest);


                LinkButton delQueBtn = new LinkButton
                {
                    Text = "Delete Question",
                    ID = $"delQueBtn-{questData["question_id"]}",
                    CssClass = "btn btn-outline-danger btn-sm",
                };
                delQueBtn.Attributes.Add("data-question-id", questData["question_id"].ToString());
                delQueBtn.Attributes.Add("data-action", "warn");
                delQueBtn.Click += new EventHandler(DelQueBtn_Click);
                container.Controls.Add(delQueBtn);
            }
        }

        protected void DelQueBtn_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            int question_id = Convert.ToInt32(linkButton.Attributes["data-question-id"]);
            Response.Redirect($"~/Admin/Course/Exam/DeleteQuestion.aspx?question_id={question_id}");
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            string title = MyUtil.SanitizeInput(TitleTxtBox);
            bool retake = RetakeChkBox.Checked;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE exam SET title=@title, retake=@retake WHERE exam_id=@exam_id;";
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@retake", retake.ToString());
                    cmd.Parameters.AddWithValue("@exam_id", exam_id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}
