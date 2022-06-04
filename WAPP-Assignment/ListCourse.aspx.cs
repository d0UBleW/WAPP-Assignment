using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public partial class ListCourse : UtilClass.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EnrollmentDiv.Visible = false;
            List<int> enrolledCourseID = null;
            if (userType == "student")
            {
                EnrollmentDiv.Visible = true;
                enrolledCourseID = StudentC.GetEnrolledCourseID(Convert.ToInt32(Session["user_id"]));
            }
            DataTable dt = CourseC.GetAllCourseData();
            foreach (DataRow dr in dt.Rows)
            {
                int course_id = Convert.ToInt32(dr["course_id"]);
                Panel cPanel = CourseC.DisplayCourse(course_id, userType, enrolledCourseID);
                CoursePlaceholder.Controls.Add(cPanel);
            }
        }

        [WebMethod]
        public static List<string> SearchCategory(string prefixText, int count)
        {
            return MyAutoComplete.ListCategory(prefixText, count);
        }

        [WebMethod]
        public static List<string> SearchTitle(string prefixText, int count)
        {
            return MyAutoComplete.ListCourseTitle(prefixText, count);
        }
    }
}