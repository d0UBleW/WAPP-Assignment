using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WAPP_Assignment.Admin.Course
{
    public partial class WebForm1 : UtilClass.BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int course_id = GetQueryString("course_id");
            int student_id = GetQueryString("student_id");
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE rating WHERE course_id=@course_id AND student_id=@student_id;";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            RedirectBack();
        }
    }
}