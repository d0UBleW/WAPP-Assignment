using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAPP_Assignment.Admin.Course
{
    public partial class Grades : UtilClass.BaseAdminPage
    {
        private int course_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            course_id = GetQueryString("course_id");
            ViewCourseLink.Text = $"Not Found";
            DataTable courseTable = CourseC.GetCourseData(course_id);
            if (courseTable.Rows.Count == 0)
            {
                return;
            }
            ViewCourseLink.NavigateUrl = $"/ViewCourse.aspx?course_id={course_id}";
            ViewCourseLink.Text = $"{courseTable.Rows[0]["title"]}";
        }
    }
}