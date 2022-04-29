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
    public partial class Register : System.Web.UI.Page
    {
        protected string DbTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            SetDbTable();
            SetUsernameValidPanel(this.UsernameTxtBox.Text);
            if (!IsPostBack)
            {
            }
        }

        protected bool IsUsernameDuplicate(string username)
        {
            string queryExist = $"SELECT * FROM {DbTable} WHERE username=@username;";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["iLearnDBConStr"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(queryExist, conn);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;

        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            SetDbTable();
            string username = this.UsernameTxtBox.Text;
            string password = this.PasswordTxtBox.Text;
            string userType = this.UserTypeRadio.SelectedValue;
            if (IsUsernameDuplicate(username))
            {
                SetUsernameValidPanel(username);
                this.UsernameTxtBox.Focus();
                return;
            }
            string queryInsert = "INSERT INTO " + DbTable;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["iLearnDBConStr"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(queryInsert, conn);
            if (userType == "admin")
            {
                queryInsert += " (username, password) VALUES (@username, @password);";
                cmd.CommandText = queryInsert;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
            }
            else
            {
                string fullName = this.FullNameTxtBox.Text;
                string email = this.EmailTxtBox.Text;
                string gender = this.GenderDropDownList.SelectedValue;
                queryInsert += " (username, password, full_name, email, gender) VALUES (@username, @password, @full_name, @email, @gender);";
                cmd.CommandText = queryInsert;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@full_name", fullName);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@gender", gender);
            }


            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Write("<script>alert('Registration successful!'); window.location.href = 'Login.aspx';</script>");
        }

        protected void UsernameTxtBox_TextChanged(object sender, EventArgs e)
        {
            string username = this.UsernameTxtBox.Text;
            SetDbTable();
            SetUsernameValidPanel(username);
        }

        protected void SetUsernameValidPanel(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                this.UsernameValidPanel.Visible = false;
                return;
            }
            if (IsUsernameDuplicate(username))
            {
                this.UsernameValidLbl.ForeColor = System.Drawing.Color.Red;
                this.UsernameValidLbl.Text = "Username is already taken.";
            }
            else
            {
                this.UsernameValidLbl.ForeColor = System.Drawing.Color.Green;
                this.UsernameValidLbl.Text = "✔";
            }
            this.UsernameValidPanel.Visible = true;
        }

        protected void SetDbTable()
        {
            string userType = this.UserTypeRadio.SelectedValue.ToString();
            DbTable = userType;
        }
    }
}