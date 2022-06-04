using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WAPP_Assignment.Admin
{
    public partial class AddExam : UtilClass.BaseAdminPage
    {
        private int course_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            course_id = GetQueryString("course_id");
            DataTable courseTable = CourseC.GetCourseData(course_id);
            if (courseTable.Rows.Count == 0)
            {
                AddExBtn.Visible = false;
                return;
            }
            ViewCourseLink.Text = $"{courseTable.Rows[0]["title"]}";
            ViewCourseLink.NavigateUrl = $"/ViewCourse.aspx?course_id={course_id}";
            EditLink.NavigateUrl = $"/Admin/Course/Exam/EditExamMenu.aspx?course_id={course_id}";
        }

        protected void AddExBtn_Click(object sender, EventArgs e)
        {
            string title = MyUtil.SanitizeInput(TitleTxtBox);
            bool retake = RetakeChkBox.Checked;
            int exam_id;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO exam (course_id, title, retake) OUTPUT INSERTED.exam_id VALUES (@course_id, @title, @retake);";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@retake", retake.ToString());
                    exam_id = (int) cmd.ExecuteScalar();
                }
                conn.Close();
            }
            Response.Redirect($"~/Admin/Course/Exam/EditExam.aspx?exam_id={exam_id}");
        }
    }
}
