using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WAPP_Assignment.Admin.Course
{
    public partial class DeleteCourse : UtilClass.BaseAdminPage
    {
        private int course_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            string course_id_temp = Request.QueryString["course_id"];
            if (string.IsNullOrEmpty(course_id_temp))
            {
                return;
            }
            try
            {
                course_id = Convert.ToInt32(course_id_temp);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
        }
    }
}