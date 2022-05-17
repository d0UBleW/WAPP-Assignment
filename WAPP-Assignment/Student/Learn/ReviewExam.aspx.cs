using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAPP_Assignment.Learn
{
    public partial class ReviewExam : UtilClass.BaseStudentPage
    {
        private int student_id, exam_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            student_id = Convert.ToInt32(Session["user_id"]);

            exam_id = GetQueryString("exam_id");

            DataTable examTable = ExamC.GetExamData(exam_id);
            if (examTable.Rows.Count == 0)
            {
                return;
            }
            TitleLbl.Text = examTable.Rows[0]["title"].ToString();

            bool retake = Convert.ToBoolean(examTable.Rows[0]["retake"]);
            RetakeLink.Visible = false;
            if (retake)
            {
                RetakeLink.Visible = true;
                RetakeLink.NavigateUrl = $"/Student/Learn/ViewExam.aspx?exam_id={exam_id}";
            }

            DataTable examResult = StudentC.GetExamResult(student_id, exam_id);
            if (examResult.Rows.Count == 0)
            {
                return;
            }

            DataTable questionTable = Question.GetExamQuestion(exam_id);
            foreach (DataRow questData in questionTable.Rows)
            {
                Panel qPanel = ExamC.DisplayQue(exam_id, questData);
                ContentPanel.Controls.Add(qPanel);
                ContentPanel.Controls.Add(new Literal { Text = "<br/><br/>" });
            }

            DataRow examResultData = examResult.Rows[0];
            List<string> worksheet = examResultData["worksheet"].ToString().Split(new string[] { ";" }, StringSplitOptions.None).ToList();
            int totalScore = 0;
            List<int> overall_answer_id = new List<int>();
            foreach (string work in worksheet)
            {
                List<string> temp = work.Split(new string[] { "." }, StringSplitOptions.None).ToList();
                int question_id = Convert.ToInt32(temp[0]);
                List<int> answer_id = Question.GetAnswerID(question_id);
                overall_answer_id.AddRange(answer_id);
                totalScore += answer_id.Count;
                Panel qPanel = ContentPanel.FindControl($"qPanel_{question_id}") as Panel;
                List<string> studentAnswer = temp[1].Split(new string[] { "," }, StringSplitOptions.None).ToList();
                string wrongHexColor = "#fee9e9";
                if (answer_id.Count > 1)
                {
                    CheckBoxList optList = qPanel.FindControl($"optList_{question_id}") as CheckBoxList;
                    optList.Enabled = false;
                    foreach (int id in answer_id)
                    {
                        ListItem listItem = optList.Items.FindByValue(id.ToString());
                        //listItem.Text = listItem.Text + " ✔";
                    }
                    foreach (string stuAns in studentAnswer)
                    {
                        if (stuAns != "")
                        {
                            optList.SelectedValue = stuAns;
                            if (!answer_id.Contains(Convert.ToInt32(stuAns)))
                            {
                                ListItem listItem = optList.Items.FindByValue(stuAns);
                                listItem.Text = listItem.Text + " ❌";
                                //qPanel.CssClass = incorrectClass;
                                qPanel.Style.Add("background-color", wrongHexColor);
                            }
                        }
                        else
                        {
                            //qPanel.CssClass = incorrectClass;
                            qPanel.Style.Add("background-color", wrongHexColor);
                        }
                    }
                }
                else
                {
                    RadioButtonList optList = qPanel.FindControl($"optList_{question_id}") as RadioButtonList;
                    optList.Enabled = false;
                    foreach (int id in answer_id)
                    {
                        ListItem listItem = optList.Items.FindByValue(id.ToString());
                        //listItem.Text = listItem.Text + " ✔";
                    }
                    if (studentAnswer[0] != "")
                    {
                        optList.SelectedValue = studentAnswer[0];
                        if (!answer_id.Contains(Convert.ToInt32(studentAnswer[0])))
                        {
                            ListItem listItem = optList.Items.FindByValue(studentAnswer[0]);
                            listItem.Text = listItem.Text + " ❌";
                            //qPanel.CssClass = incorrectClass;
                            qPanel.Style.Add("background-color", wrongHexColor);
                        }
                    }
                    else
                    {
                        //qPanel.CssClass = incorrectClass;
                        qPanel.Style.Add("background-color", wrongHexColor);
                    }
                }
                System.Diagnostics.Debug.WriteLine("");
            }
            ScoreLbl.Text = $"Score: {examResultData["value"]}/{totalScore}";
            foreach (int id in overall_answer_id)
            {
                System.Diagnostics.Debug.WriteLine(id);
            }
            CorrectOptIDField.Value = String.Join(",", overall_answer_id);
        }
    }
}