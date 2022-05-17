using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace WAPP_Assignment
{
    public static class ExamC
    {
        public static DataTable GetCourseExamData(int course_id)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = WAPP_Assignment.DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM exam WHERE course_id=@course_id;";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        adapter.Fill(dataTable);
                    }
                }
                conn.Close();
            }
            return dataTable;
        }

        public static DataTable GetExamData(int exam_id)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = WAPP_Assignment.DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM exam WHERE exam_id=@exam_id;";
                    cmd.Parameters.AddWithValue("@exam_id", exam_id);
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        adapter.Fill(dataTable);
                    }
                }
                conn.Close();
            }
            return dataTable;
        }

        public static Panel DisplayQue(int exam_id, DataRow questData)
        {
            int question_id = int.Parse(questData["question_id"].ToString());
            Panel qPanel = new Panel
            {
                ID = $"qPanel_{question_id}",
            };
            qPanel.Attributes.Add("data-question-id", question_id.ToString());
            Literal question = new Literal
            {
                Text = $"Question {questData["sequence"]} {questData["content"]}",
            };
            qPanel.Controls.Add(question);
            int numOfAnswer = Question.GetAnswerID(question_id).Count;
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
            return qPanel;
        }
    }
}