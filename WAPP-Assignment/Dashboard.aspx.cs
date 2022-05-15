using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WAPP_Assignment
{
    public partial class Dashboard : UtilClass.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (userType == "nobody")
            {
                Response.Redirect("/Login.aspx");
                return;
            }
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/Home.aspx");
        }

    }
}