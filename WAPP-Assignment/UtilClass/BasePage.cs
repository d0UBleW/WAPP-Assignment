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

        protected bool CheckQueryString(string param)
        {
            string queryString = Request.QueryString[param];
            if (string.IsNullOrEmpty(queryString))
            {
                return false;
            }
            return true;
        }

        protected int GetQueryString(string param, bool redirect = false)
        {
            if (CheckQueryString(param))
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString[param]);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            if (redirect)
            {
                RedirectBack();
            }
            return -1;
        }

        protected string GetQueryString(string param, bool redirect = false, bool type)
        {
            if (CheckQueryString(param))
            {
                try
                {
                    return Request.QueryString[param];
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            if (redirect)
            {
                RedirectBack();
            }
            return "";
        }

        protected void RedirectBack(string defaultUrl = "~/Home.aspx")
        {
            if (Request.UrlReferrer != null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect(defaultUrl);
            }
        }
    }
}