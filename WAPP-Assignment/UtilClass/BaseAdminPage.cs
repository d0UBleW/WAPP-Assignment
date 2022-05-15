using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAPP_Assignment.UtilClass
{
    public class BaseAdminPage : UtilClass.BasePage
    {
        protected new void Page_PreInit(object sender, EventArgs e)
        {
            SetUserType();
            SetMasterPageFile();
            if (userType == "nobody")
            {
                Response.Redirect("~/Home.aspx");
                return;
            }
            if (userType == "student")
            {
                if (Request.UrlReferrer != null)
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