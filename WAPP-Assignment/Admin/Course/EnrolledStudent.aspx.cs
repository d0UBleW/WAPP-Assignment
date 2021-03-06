using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAPP_Assignment.Admin.Course
{
    public partial class EnrolledStudent : UtilClass.BaseAdminPage
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

        protected void UnenrollBtn_Click(object sender, EventArgs e)
        {
            foreach(GridViewRow row in GridView1.Rows)
            {
                CheckBox cb = row.FindControl("CheckBox1") as CheckBox;
                if (cb.Checked)
                {
                    int student_id = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                    StudentC.Unenroll(student_id, course_id);
                }
            }
            Response.Redirect(Request.UrlReferrer.ToString());
        }
    }
}