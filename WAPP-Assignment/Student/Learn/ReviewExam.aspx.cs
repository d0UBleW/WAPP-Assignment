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
            if (userType == "admin")
            {
                student_id = 0;
            }

            exam_id = GetQueryString("exam_id");

            DataTable examTable = ExamC.GetExamData(exam_id);
            if (examTable.Rows.Count == 0)
            {
                return;
            }
            TitleLbl.Text = examTable.Rows[0]["title"].ToString();

            bool retake = Convert.ToBoolean(examTable.Rows[0]["retake"]);
            RetakePanel.Visible = false;
            if (retake && userType == "student")
            {
                RetakePanel.Visible = true;
                RetakeLink.NavigateUrl = $"/Student/Learn/ViewExam.aspx?exam_id={exam_id}";
            }

            DataTable examResult = StudentC.GetExamResult(student_id, exam_id);
            if (examResult.Rows.Count == 0 && student_id != 0)
            {
                return;
            }

            DataTable questionTable = Question.GetExamQuestion(exam_id);
            foreach (DataRow questData in questionTable.Rows)
            {
                Panel qPanel = ExamC.DisplayQue(exam_id, questData, false);
                ContentPanel.Controls.Add(qPanel);
                ContentPanel.Controls.Add(new Literal { Text = "<br/><br/>" });
            }

            DataRow examResultData = null;
            List<string> worksheet = null;
            Dictionary<int, List<string>> worksheetDict = null;
            if (userType == "student")
            {
                examResultData = examResult.Rows[0];
                worksheet = examResultData["worksheet"].ToString().Split(new string[] { ";" }, StringSplitOptions.None).ToList();
                worksheetDict = new Dictionary<int, List<string>>();
                foreach (string work in worksheet)
                {
                    List<string> temp = work.Split(new string[] { "." }, StringSplitOptions.None).ToList();
                    int question_id = Convert.ToInt32(temp[0]);
                    List<string> studentAnswer = temp[1].Split(new string[] { "," }, StringSplitOptions.None).ToList();
                    worksheetDict.Add(question_id, studentAnswer);
                }
            }

            int totalScore = 0;
            List<int> overall_answer_id = new List<int>();
            foreach (DataRow questData in questionTable.Rows)
            {
                int question_id = Convert.ToInt32(questData["question_id"]);
                List<int> answer_id = Question.GetAnswerID(question_id);
                overall_answer_id.AddRange(answer_id);
                totalScore += answer_id.Count;
                Panel qPanel = ContentPanel.FindControl($"qPanel_{question_id}") as Panel;
                string wrongHexColor = "#fee9e9";
                if (userType == "admin") continue;
                if (!worksheetDict.ContainsKey(question_id))
                {
                    qPanel.Style.Add("background-color", wrongHexColor);
                    continue;
                }
                List<string> studentAnswer = worksheetDict[question_id];
                if (answer_id.Count > 1)
                {
                    CheckBoxList optList = qPanel.FindControl($"optList_{question_id}") as CheckBoxList;
                    if (studentAnswer.Count != answer_id.Count)
                    {
                        qPanel.Style.Add("background-color", wrongHexColor);
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
                            }
                        }
                    }
                }
                else
                {
                    RadioButtonList optList = qPanel.FindControl($"optList_{question_id}") as RadioButtonList;
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
            }
            if (userType == "student")
                ScoreLbl.Text = $"Score: {examResultData["value"]}/{totalScore}";

            CorrectOptIDField.Value = String.Join(",", overall_answer_id);
        }
    }
}