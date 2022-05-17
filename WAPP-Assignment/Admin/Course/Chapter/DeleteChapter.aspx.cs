using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment.Admin
{
    public partial class DeleteChapter : UtilClass.BaseAdminPage
    {
        private int chapter_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            chapter_id = GetQueryString("chapter_id");
            RedirectBack();
        }
    }
}