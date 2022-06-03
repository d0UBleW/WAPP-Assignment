using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ganss.XSS;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment.Admin.Course.Exam
{
    public partial class AddQuestion : UtilClass.BaseAdminPage
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
            int course_id = Convert.ToInt32(examTable.Rows[0]["course_id"]);
            EditExamLink.NavigateUrl = $"/Admin/Course/Exam/EditExam.aspx?exam_id={exam_id}";
            EditLink.NavigateUrl = $"/Admin/Course/Exam/EditExamMenu.aspx?course_id={course_id}";
            ViewCourseLink.NavigateUrl = $"/ViewCourse.aspx?course_id={course_id}";
            DataTable courseTable = CourseC.GetCourseData(course_id);
            ViewCourseLink.Text = $"{courseTable.Rows[0]["title"]}";
            ExamLbl.Text = $"{examTable.Rows[0]["title"]}";
            if (!IsPostBack)
            {
                int maxSeq = WAPP_Assignment.Question.GetQueMaxSeq(exam_id) + 1;
                QueNoTxtBox.Attributes.Add("Max", maxSeq.ToString());
                QueNoRangeValidator.MaximumValue = maxSeq.ToString();
                QueNoRangeValidator.MinimumValue = "1";
                QueNoTxtBox.Text = maxSeq.ToString();
            }

        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            int maxSeq = Convert.ToInt32(QueNoTxtBox.Attributes["Max"]);
            int seq = Convert.ToInt32(QueNoTxtBox.Text);
            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedTags.Add("iframe");
            sanitizer.AllowedTags.Add("oembed");
            var rawHtml = EditorTxtBox.Text;
            string content = sanitizer.Sanitize(rawHtml);
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    Question.UpdateQueSequence(exam_id, seq, maxSeq);
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO question (exam_id, content, sequence, weight) OUTPUT INSERTED.question_id VALUES (@exam_id, @content, @sequence, 0);";
                    cmd.Parameters.AddWithValue("@exam_id", exam_id);
                    cmd.Parameters.AddWithValue("@content", content);
                    cmd.Parameters.AddWithValue("@sequence", seq);
                    int question_id = (int)cmd.ExecuteScalar();
                    cmd.Parameters.Clear();

                    int answerCount = 0;
                    var formData = Request.Form;
                    foreach (string key in formData.Keys)
                    {
                        if (!key.StartsWith("OptList$")) continue;
                        int isAnswer = 0;
                        if (key.EndsWith("_unchecked"))
                        {
                            isAnswer = 0;
                        }
                        else if (key.EndsWith("_checked"))
                        {
                            isAnswer = 1;
                            answerCount++;
                        }
                        else
                        {
                            continue;
                        }
                        string optContent = formData[key];
                        cmd.CommandText = "INSERT INTO [option] (question_id, content, isAnswer) VALUES (@question_id, @content, @isAnswer);";
                        cmd.Parameters.AddWithValue("@question_id", question_id);
                        cmd.Parameters.AddWithValue("@content", optContent);
                        cmd.Parameters.AddWithValue("@isAnswer", isAnswer);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        cmd.CommandText = "UPDATE question SET weight=@weight WHERE exam_id=@exam_id";
                        cmd.Parameters.AddWithValue("@weight", answerCount);
                        cmd.Parameters.AddWithValue("@exam_id", exam_id);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
                conn.Close();
                RedirectBack();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var data = Request.Form;
            List<string> optKey = new List<string>();
            foreach (string key in data.Keys)
            {
                if (!key.StartsWith("OptTable$")) continue;
                if (key.EndsWith("_checked"))
                optKey.Add(key);
            }
        }
    }
}