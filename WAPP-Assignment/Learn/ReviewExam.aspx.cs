using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAPP_Assignment.Learn
{
    public partial class ReviewExam : UtilClass.BasePage
    {
        private int student_id, exam_id;
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
            student_id = Convert.ToInt32(Session["user_id"]);

            string exam_id_temp = Request.QueryString["exam_id"];
            if (string.IsNullOrEmpty(exam_id_temp))
            {
                return;
            }
            exam_id = Convert.ToInt32(exam_id_temp);
            DataTable examTable = Exam.GetExamData(exam_id);
            if (examTable.Rows.Count == 0)
            {
                return;
            }
            TitleLbl.Text = examTable.Rows[0]["title"].ToString();

            DataTable questionTable = Question.GetExamQuestion(exam_id);
            foreach (DataRow questData in questionTable.Rows)
            {
                Panel qPanel = Exam.DisplayQue(exam_id, questData);
                ContentPanel.Controls.Add(qPanel);
                ContentPanel.Controls.Add(new Literal { Text = "<br/><br/>" });
            }

            DataTable examResult = Student.GetExamResult(student_id, exam_id);
            if (examResult.Rows.Count == 0)
            {
                return;
            }
            DataRow examResultData = examResult.Rows[0];
            List<string> worksheet = examResultData["worksheet"].ToString().Split(new string[] { ";" }, StringSplitOptions.None).ToList();
            int totalScore = 0;
            foreach (string work in worksheet)
            {
                List<string> temp = work.Split(new string[] { "." }, StringSplitOptions.None).ToList();
                int question_id = Convert.ToInt32(temp[0]);
                List<int> answer_id = Question.GetAnswerID(question_id);
                totalScore += answer_id.Count;
                Panel qPanel = ContentPanel.FindControl($"qPanel_{question_id}") as Panel;
                List<string> studentAnswer = temp[1].Split(new string[] { "," }, StringSplitOptions.None).ToList();
                if (answer_id.Count > 1)
                {
                    CheckBoxList optList = qPanel.FindControl($"optList_{question_id}") as CheckBoxList;
                    optList.Enabled = false;
                    foreach (int id in answer_id)
                    {
                        ListItem listItem = optList.Items.FindByValue(id.ToString());
                        listItem.Text = listItem.Text + " ✔";
                    }
                    foreach (string stuAns in studentAnswer)
                    {
                        if (stuAns != "")
                            optList.SelectedValue = stuAns;
                    }
                }
                else
                {
                    RadioButtonList optList = qPanel.FindControl($"optList_{question_id}") as RadioButtonList;
                    optList.Enabled = false;
                    foreach (int id in answer_id)
                    {
                        ListItem listItem = optList.Items.FindByValue(id.ToString());
                        listItem.Text = listItem.Text + " ✔";
                    }
                    if (studentAnswer[0] != "")
                        optList.SelectedValue = studentAnswer[0];
                }
                System.Diagnostics.Debug.WriteLine("");
            }
            ScoreLbl.Text = $"Score: {examResultData["value"]}/{totalScore}";
        }
    }
}