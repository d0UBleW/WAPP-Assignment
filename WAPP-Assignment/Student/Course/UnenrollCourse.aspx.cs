using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment.Student.Course
{
    public partial class UnenrollCourse : UtilClass.BaseStudentPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int course_id = GetQueryString("course_id");
            int student_id = Convert.ToInt32(Session["user_id"]);
            StudentC.Unenroll(student_id, course_id);
            RedirectBack();
        }
    }
}