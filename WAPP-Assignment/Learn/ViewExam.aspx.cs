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
                Panel qPanel = Exam.DisplayQue(exam_id, questData);
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
            Response.Redirect($"/Learn/ReviewExam.aspx?exam_id={exam_id}");
        }
    }
}