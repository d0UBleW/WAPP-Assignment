using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment.Admin.Course.Exam
{
    public partial class DeleteExam : UtilClass.BaseAdminPage
    {
        private int exam_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            exam_id = GetQueryString("exam_id");
            ExamC.DeleteExam(exam_id);
            RedirectBack();
        }
    }
}