using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public partial class EnrollCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["course_id"]) ||
                Session["user_id"] == null ||
                (bool)Session["isAdmin"])
            {
                return;
            }
            int student_id = Convert.ToInt32(Session["user_id"]);
            int course_id = int.Parse(Request.QueryString["course_id"]);
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO [enroll] (student_id, course_id) VALUES (@student_id, @course_id)";
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            if (!string.IsNullOrEmpty(Request.UrlReferrer.ToString()))
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
        }
    }
}