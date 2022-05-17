using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WAPP_Assignment
{
    public partial class EnrollCourse : UtilClass.BaseStudentPage
    {
        private int course_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: Enroll student from admin
            course_id = GetQueryString("course_id");
            DataTable courseTable = CourseC.GetCourseData(course_id);
            if (courseTable.Rows.Count == 0)
            {
                RedirectBack();
                return;
            }
            int student_id = Convert.ToInt32(Session["user_id"]);
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO [enroll] (student_id, course_id) VALUES (@student_id, @course_id)";
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            RedirectBack();
        }
    }
}