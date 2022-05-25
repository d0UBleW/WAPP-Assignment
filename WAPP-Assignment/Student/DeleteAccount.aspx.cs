using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WAPP_Assignment.Student
{
    public partial class DeleteAccount : UtilClass.BaseStudentPage
    {
        private int student_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            student_id = Convert.ToInt32(Session["user_id"]);
            if (userType == "admin")
            {
                student_id = GetQueryString("student_id");
            }
            List<int> enrolledCourseID = StudentC.GetEnrolledCourseID(student_id);
            foreach (int courseID in enrolledCourseID)
            {
                StudentC.Unenroll(student_id, courseID);
            }

            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE feed WHERE student_id=@student_id";
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "DELETE student WHERE student_id=@student_id";
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            if (userType == "admin")
            {
                Response.Redirect("~/Admin/StudentData/StudentList.aspx");
                return;
            }
            Response.Redirect("~/Logout.aspx");
        }
    }
}