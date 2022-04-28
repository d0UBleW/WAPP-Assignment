using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WAPP_Assignment
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserTypeRadio_SelectedIndexChanged(sender, e);
            }
        }

        protected void UserTypeRadio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UserTypeRadio.SelectedValue == "admin")
            {
                StudentPanel.Visible = false;
            }
            else
            {
                StudentPanel.Visible = true;
            }
        }
    }
}