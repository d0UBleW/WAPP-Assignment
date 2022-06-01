using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAPP_Assignment
{
    public partial class Dashboard : UtilClass.BaseStudentPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<int> enrolledCourseID = null;
            if (userType == "admin")
            {
                AdminPanel.Visible = true;
                CourseCount.Text = CourseC.GetCourseCount().ToString();
                StudentCount.Text = StudentC.GetStudentCount().ToString();
                MaleCount.Text = StudentC.GetStudentCount("m").ToString();
                FemaleCount.Text = StudentC.GetStudentCount("f").ToString();
            }
            else
            {
                StudentPanel.Visible = true;
                int student_id = Convert.ToInt32(Session["user_id"]);
                enrolledCourseID = StudentC.GetEnrolledCourseID(student_id);
                DataTable courseTable = CourseC.GetPopularCourseID();
                int count = Math.Min(courseTable.Rows.Count, 5);
                for (int i = 0; i < count; i++)
                {
                    DataRow dr = courseTable.Rows[i];
                    int course_id = Convert.ToInt32(dr["course_id"]);
                    Panel cPanel = CourseC.DisplayCourse(course_id, userType, enrolledCourseID);
                    StudentGridPanel.Controls.Add(cPanel);
                }
            }
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/Home.aspx");
        }

    }
}