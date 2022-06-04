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
            course_id = GetQueryString("course_id");
            int student_id = Convert.ToInt32(Session["user_id"]);
            StudentC.Enroll(student_id, course_id);
            Response.Redirect($"~/ViewCourse.aspx?course_id={course_id}");
        }
    }
}