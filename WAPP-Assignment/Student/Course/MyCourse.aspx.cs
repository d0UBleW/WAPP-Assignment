using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace WAPP_Assignment
{
    public partial class MyCourse : UtilClass.BaseStudentPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int student_id = Convert.ToInt32(Session["user_id"].ToString());
            List<int> enrolledCourseId = StudentC.GetEnrolledCourseID(student_id);
            foreach (int course_id in enrolledCourseId)
            {
                Panel cPanel = CourseC.DisplayCourse(course_id, userType, enrolledCourseId);
                CoursePlaceholder.Controls.Add(cPanel);
                CoursePlaceholder.Controls.Add(new Literal { Text = "<br />" });
            }
        }
    }
}