using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WAPP_Assignment
{
    public partial class Home : System.Web.UI.Page
    {
        public const string navText = "Home";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
            {
                Page.MasterPageFile = "~/SiteAnon.Master";
            }
            else if ((bool)Session["isAdmin"])
            {
                Page.MasterPageFile = "~/SiteAdmin.Master";
            }
            else
            {
                Page.MasterPageFile = "~/SiteStudent.Master";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
