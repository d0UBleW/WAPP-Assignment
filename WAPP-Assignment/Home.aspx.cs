using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WAPP_Assignment
{
    public partial class Home : UtilClass.BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (userType != "nobody")
            {
                JumboLink.NavigateUrl = "~/ListCourse.aspx";
                JumboLink.Text = "Learn";
            }
        }
    }
}
