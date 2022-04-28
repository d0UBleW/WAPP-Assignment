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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.UsernameValidPanel.Visible = false;
                UserTypeRadio_SelectedIndexChanged(sender, e);
            }
        }

        protected bool isUsernameDuplicate(string username, string dbTable)
        {
            string queryExist = "SELECT * FROM " + dbTable + " WHERE username=@username;";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["iLearnCon"].ConnectionString);
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

        protected void UserTypeRadio_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userType = UserTypeRadio.SelectedValue;
            if (userType == "admin")
            {
                this.StudentPanel.Visible = false;
            }
            else
            {
                this.StudentPanel.Visible = true;
            }
            string dbTable = userType;
            setUsernameValidPanel(this.UsernameTxtBox.Text, dbTable);
        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            string username = this.UsernameTxtBox.Text;
            string password = this.PasswordTxtBox.Text;
            string userType = this.UserTypeRadio.SelectedValue;
            string dbTable = userType;
            if (isUsernameDuplicate(username, dbTable))
            {
                setUsernameValidPanel(username, dbTable);
                return;
            }
            string queryInsert = "INSERT INTO " + dbTable;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["iLearnCon"].ConnectionString);
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
                string name = this.FullNameTxtBox.Text;
                string email = this.EmailTxtBox.Text;
                string gender = this.GenderDropDownList.SelectedValue;
                queryInsert += " (username, password, name, email, gender) VALUES (@username, @password, @name, @email, @gender);";
                cmd.CommandText = queryInsert;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@gender", gender);
            }


            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Write("<script>alert('Registration successful!'); window.location.href = 'Register.aspx';</script>");
        }

        protected void UsernameTxtBox_TextChanged(object sender, EventArgs e)
        {
            string username = this.UsernameTxtBox.Text;
            System.Diagnostics.Debug.WriteLine(username);
            string userType = this.UserTypeRadio.SelectedValue;
            string dbTable = userType;
            setUsernameValidPanel(username, dbTable);
        }

        protected void setUsernameValidPanel(string username, string dbTable)
        {
            if (String.IsNullOrEmpty(username))
            {
                this.UsernameValidPanel.Visible = false;
                return;
            }
            if (isUsernameDuplicate(username, dbTable))
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
    }
}