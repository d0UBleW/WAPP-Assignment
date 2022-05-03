using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public partial class ViewChapter : System.Web.UI.Page
    {
        private int chapter_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            string chapter_id_temp = Request.QueryString["chapter_id"];
            if (string.IsNullOrEmpty(chapter_id_temp))
            {
                return;
            }
            try
            {
                chapter_id = int.Parse(chapter_id_temp);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
            DataTable dt = Chapter.GetChapterData(chapter_id);
            DataRow dr = dt.Rows[0];
            TitleLbl.Text = dr["title"].ToString();
            ContentPlaceholder.Controls.Add(new Literal { Text = dr["content"].ToString() });
        }
    }
}