﻿using System;
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
        private string DbTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            SetDbTable();
            string username = this.UsernameTxtBox.Text;
            string password = this.PasswordTxtBox.Text;

            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM {DbTable} WHERE username=@username AND password=@password;";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            this.ErrorLbl.Visible = false;
                            string user_id = dt.Rows[0][DbTable+"_id"].ToString();
                            this.Session["username"] = username;
                            this.Session["user_id"] = Convert.ToInt32(user_id);
                            this.Session["isAdmin"] = DbTable == "admin";
                            Response.Redirect("Dashboard.aspx");
                            return;
                        }
                        else
                        {
                            this.ErrorLbl.Visible = true;
                        }
                        conn.Close();
                    }
                }
            }
        }

        protected void SetDbTable()
        {
            string userType = this.UserTypeRadio.SelectedValue;
            DbTable = userType;
        }
    }
}