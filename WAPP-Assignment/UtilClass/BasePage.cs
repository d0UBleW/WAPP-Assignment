using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAPP_Assignment.UtilClass
{
    public class BasePage : System.Web.UI.Page
    {
        protected string userType;
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
            {
                userType = "nobody";
                Page.MasterPageFile = "~/SiteAnon.Master";
            }
            else if ((bool)Session["isAdmin"])
            {
                userType = "admin";
                Page.MasterPageFile = "~/SiteAdmin.Master";
            }
            else
            {
                userType = "student";
                Page.MasterPageFile = "~/SiteStudent.Master";
            }
        }
    }
}