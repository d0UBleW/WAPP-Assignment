using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WAPP_Assignment.Admin
{
    public partial class AddExam : System.Web.UI.Page
    {
        private int course_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
            {
                Response.Redirect("~/Home.aspx");
                return;
            }
            if (!(bool)Session["isAdmin"])
            {
                if (!string.IsNullOrEmpty(Request.UrlReferrer.ToString()))
                {
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    Response.Redirect("~/Home.aspx");
                }
                return;
            }
            string course_id_temp = Request.QueryString["course_id"];
            if (string.IsNullOrEmpty(course_id_temp))
            {
                return;
            }
            course_id = int.Parse(course_id_temp);
        }

        protected void AddExBtn_Click(object sender, EventArgs e)
        {
            string title = TitleTxtBox.Text;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO exam (course_id, title) VALUES (@course_id, @title);";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            if (!String.IsNullOrEmpty(Request.UrlReferrer.ToString()))
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
        }
    }
}