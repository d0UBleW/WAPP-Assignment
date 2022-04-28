using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public partial class Login : System.Web.UI.Page
    {
        private string dbTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            UserTypeRadio_SelectedIndexChanged(sender, e);
        }

        protected void UserTypeRadio_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userType = UserTypeRadio.SelectedValue;
            dbTable = userType;
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = this.UsernameTxtBox.Text;
            string password = this.PasswordTxtBox.Text;

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["iLearnCon"].ConnectionString);
            conn.Open();
            string query = $"SELECT * FROM {dbTable} WHERE username=@username AND password=@password;";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    System.Diagnostics.Debug.WriteLine(dr.ToString());
                }
                this.ErrorLbl.Visible = false;
                string user_id = dt.Rows[0][dbTable+"_id"].ToString();
                this.Session["username"] = username;
                this.Session["user_id"] = user_id;
                Response.Redirect("Dashboard.aspx");
                return;
            }
            else
            {
                this.ErrorLbl.Visible = true;
            }
        }
    }
}