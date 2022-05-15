using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAPP_Assignment.UtilClass
{
    public class BaseStudentPage : UtilClass.BasePage
    {
        protected new void Page_PreInit(object sender, EventArgs e)
        {
            SetUserType();
            SetMasterPageFile();
            if (userType == "nobody")
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            if (userType == "admin")
            {
                Response.Redirect("~/Home.aspx");
                return;
            }
        }
    }
}