using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace WAPP_Assignment.Admin
{
    public partial class AddChapter : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["iLearnDBConStr"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void AddBtnASP_Click(object sender, EventArgs e)
        {
            //var data = new Literal { Text = dataField.Value };
            //TestPanel.Controls.Add(data);
            string course_id = Request.QueryString["course_id"];
            if (String.IsNullOrEmpty(course_id))
            {
                // throw error
                return;
            }
            string title = TitleTxtBox.Text;
            string content = dataField.Value;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO chapter (course_id, title, content) VALUES (@course_id, @title, @content);";
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@course_id", course_id);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}