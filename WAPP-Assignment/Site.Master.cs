using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WAPP_Assignment
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public string Location
        {
            get
            {
                return NavLocation.Text;
            }
            set
            {
                NavLocation.Text = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}