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
    public partial class DeleteQuestion : UtilClass.BaseAdminPage
    {
        private int question_id, exam_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            question_id = GetQueryString("question_id");
            Question.DeleteQuestion(question_id);
            RedirectBack();
        }
    }
}