using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WAPP_Assignment.Student.Course
{
    public partial class ChangePassword : UtilClass.BaseStudentPage
    {
        private int student_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            student_id = Convert.ToInt32(Session["user_id"]);
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            string currPasswd = CurrPasswdTxtBox.Text;
            string newPasswd = NewPasswdTxtBox.Text;
            string currHash = MyUtil.ComputeSHA1(currPasswd);
            string newHash = MyUtil.ComputeSHA1(newPasswd);
            string storedPasswd;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT password FROM student WHERE student_id=@student_id;";
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    storedPasswd = (string)cmd.ExecuteScalar();
                    Label1.Visible = true;
                    if (storedPasswd != currHash)
                    {
                        conn.Close();
                        cmd.Dispose();
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "Current password does not match";
                        return;
                    }
                    Label1.ForeColor = System.Drawing.Color.Green;
                    Label1.Text = "Password changed successfully";
                    cmd.Parameters.Clear();
                    cmd.CommandText = "UPDATE student SET password=@password WHERE student_id=@student_id;";
                    cmd.Parameters.AddWithValue("@password", newHash);
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }
}