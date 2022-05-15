using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAPP_Assignment.Learn
{
    public partial class ViewExam : UtilClass.BasePage
    {
        private int exam_id;
        private int student_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (userType == "nobody")
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            if (userType == "admin")
            {
                return;
            }
            string exam_id_temp = Request.QueryString["exam_id"].ToString();
            if (string.IsNullOrEmpty(exam_id_temp))
            {
                return;
            }
            try
            {
                exam_id = Convert.ToInt32(exam_id_temp);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
            student_id = Convert.ToInt32(Session["user_id"]);
            DataTable examTable = Exam.GetExamData(exam_id);
            if (examTable.Rows.Count == 0)
            {
                return;
            }
            DataRow examData = examTable.Rows[0];
            TitleLbl.Text = examData["title"].ToString();

            DataTable questionTable = Question.GetExamQuestion(exam_id);
            foreach (DataRow questData in questionTable.Rows)
            {
                int question_id = int.Parse(questData["question_id"].ToString());
                Panel qPanel = new Panel
                {
                    ID = $"qPanel_{question_id}",
                };
                qPanel.Attributes.Add("data-question-id", question_id.ToString());
                ContentPanel.Controls.Add(qPanel);
                Literal question = new Literal
                {
                    Text = $"Question {questData["sequence"]} {questData["content"]}",
                };
                qPanel.Controls.Add(question);
                int numOfAnswer = Question.GetNumOfAnswer(question_id);
                DataTable optTable = Question.GetQuestionOption(question_id);
                if (numOfAnswer > 1)
                {
                    CheckBoxList optList = new CheckBoxList
                    {
                        ID = $"optList_{question_id}",
                    };
                    optList.Attributes.Add("data-question-id", question_id.ToString()); 
                    foreach (DataRow optData in optTable.Rows)
                    {
                        ListItem optListItem = new ListItem
                        {
                            Text = optData["content"].ToString(),
                            Value = optData["option_id"].ToString(),
                        };
                        optList.Items.Add(optListItem);
                    }
                    qPanel.Controls.Add(optList);
                }
                else
                {
                    RadioButtonList optList = new RadioButtonList
                    {
                        ID = $"optList_{question_id}",
                    };
                    optList.Attributes.Add("data-question-id", question_id.ToString()); 
                    foreach (DataRow optData in optTable.Rows)
                    {
                        ListItem optListItem = new ListItem
                        {
                            Text = optData["content"].ToString(),
                            Value = optData["option_id"].ToString(),
                        };
                        optList.Items.Add(optListItem);
                    }
                    qPanel.Controls.Add(optList);
                }
                ContentPanel.Controls.Add(new Literal { Text = "<br/><br/>" });
            }
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            DataTable questionTable = Question.GetExamQuestion(exam_id);
            foreach (DataRow questData in questionTable.Rows)
            {
                int question_id = int.Parse(questData["question_id"].ToString());
                int numOfAnswer = Question.GetNumOfAnswer(question_id);
                if (numOfAnswer > 1)
                {
                    CheckBoxList optList = ContentPanel.FindControl($"optList_{questData["question_id"]}") as CheckBoxList;
                    foreach (ListItem optListItem in optList.Items)
                    {
                        if (optListItem.Selected)
                        System.Diagnostics.Debug.WriteLine(optListItem.Value);
                    }
                }
                else
                {
                    RadioButtonList optList = ContentPanel.FindControl($"optList_{questData["question_id"]}") as RadioButtonList;
                    System.Diagnostics.Debug.WriteLine(optList.SelectedValue);
                }
                System.Diagnostics.Debug.WriteLine("abc");
            }
        }
    }
}