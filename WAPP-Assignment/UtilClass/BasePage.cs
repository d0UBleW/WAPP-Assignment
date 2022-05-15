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
            SetUserType();
            SetMasterPageFile();
        }

        protected void SetUserType()
        {
            if (Session["user_id"] == null)
            {
                userType = "nobody";
            }
            else if ((bool)Session["isAdmin"])
            {
                userType = "admin";
            }
            else
            {
                userType = "student";
            }
        }

        protected void SetMasterPageFile()
        {
            if (userType == "nobody")
            {
                Page.MasterPageFile = "~/SiteAnon.Master";
            }
            else if (userType == "admin")
            {
                Page.MasterPageFile = "~/SiteAdmin.Master";
            }
            else
            {
                Page.MasterPageFile = "~/SiteStudent.Master";
            }
        }
    }
}