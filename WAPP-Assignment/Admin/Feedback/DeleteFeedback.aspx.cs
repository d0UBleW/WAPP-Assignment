using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WAPP_Assignment.Admin.Feedback
{
    public partial class DeleteFeedback : UtilClass.BaseAdminPage
    {
        private int feed_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            feed_id = GetQueryString("feed_id");
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE feed WHERE feed_id=@feed_id;";
                    cmd.Parameters.AddWithValue("@feed_id", feed_id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            Response.Redirect("~/Admin/Feedback/FeedbackList.aspx");
        }
    }
}