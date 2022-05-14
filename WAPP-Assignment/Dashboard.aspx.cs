﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WAPP_Assignment
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
            {
                Response.Redirect("/Login.aspx");
                return;
            }
            if ((bool)Session["isAdmin"])
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
            if (Session["user_id"] == null)
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