using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WAPP_Assignment.Student
{
    public partial class Feedback : UtilClass.BaseStudentPage
    {
        private int student_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (userType == "admin")
            {
                SubmitBtn.Enabled = false;
                return;
            }
            student_id = Convert.ToInt32(Session["user_id"]);
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            string content = MyUtil.SanitizeInput(ContentTxtBox);
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO feed (student_id, content) VALUES (@student_id, @content);";
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.Parameters.AddWithValue("@content", content);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            StatusPanel.Visible = true;
            ContentTxtBox.Text = "";
        }
    }
}