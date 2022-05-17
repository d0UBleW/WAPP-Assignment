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
            if (!IsPostBack)
            {
                DataTable examTable = ExamC.GetExamData(exam_id);
                TitleTxtBox.Text = examTable.Rows[0]["title"].ToString();
                RetakeChkBox.Checked = Convert.ToBoolean(examTable.Rows[0]["retake"]);
            }
            DataTable questionTable = Question.GetExamQuestion(exam_id);
            foreach (DataRow questData in questionTable.Rows)
            {
                Label quest = new Label
                {
                    Text = $"{questData["sequence"]}",
                };
                LinkButton editQueBtn = new LinkButton
                {
                    Text = "Edit Question",
                    ID = $"editQueBtn-{questData["question_id"]}",
                    CssClass = "btn btn-secondary btn-md",
                };
                editQueBtn.Click += new EventHandler(EditQueBtn_Click);
                editQueBtn.Attributes.Add("data-question-id", questData["question_id"].ToString());
                LinkButton delQueBtn = new LinkButton
                {
                    Text = "Delete Question",
                    ID = $"delQueBtn-{questData["question_id"]}",
                    CssClass = "btn btn-secondary btn-md",
                };
                delQueBtn.Attributes.Add("data-question-id", questData["question_id"].ToString());
                delQueBtn.OnClientClick = "return confirm('Are you sure?');";
                delQueBtn.Click += new EventHandler(DelQueBtn_Click);
                QuePlaceholder.Controls.Add(quest);
                QuePlaceholder.Controls.Add(editQueBtn);
                QuePlaceholder.Controls.Add(new Literal { Text = "<br/>" });
            }
        }

        protected void EditQueBtn_Click(object sender, EventArgs e)
        {
        }

        protected void DelQueBtn_Click(object sender, EventArgs e)
        {
        }

        protected void AddQueBtnLink_Click(object sender, EventArgs e)
        {
            Response.Redirect($"~/Admin/Course/Exam/AddQuestion.aspx?exam_id={exam_id}");
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            string title = TitleTxtBox.Text;
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