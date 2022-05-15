using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAPP_Assignment.Admin.Course.Exam
{
    public partial class EditExam : UtilClass.BasePage
    {
        private int exam_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (userType == "nobody")
            {
                Response.Redirect("~/Home.aspx");
                return;
            }
            if (userType == "student")
            {
                if (!string.IsNullOrEmpty(Request.UrlReferrer.ToString()))
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    Response.Redirect("~/Home.aspx");
                }
                return;
            }
            string exam_id_temp = Request.QueryString["exam_id"];
            if (string.IsNullOrEmpty(exam_id_temp))
            {
                MainPanel.Visible = false;
                return;
            }
            exam_id = int.Parse(exam_id_temp);
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
    }
}