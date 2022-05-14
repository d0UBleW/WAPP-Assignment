using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WAPP_Assignment.Admin.Course.Exam
{
    public partial class EditExam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
            {
                Response.Redirect("~/Home.aspx");
                return;
            }
            if (!(bool)Session["isAdmin"])
            {
                if (!string.IsNullOrEmpty(Request.UrlReferrer.ToString()))
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    Response.Redirect("~/Home.aspx");
                }
                return;
            }
        }
    }
}