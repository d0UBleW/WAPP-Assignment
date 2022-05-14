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
using System.Net;
using System.Collections.Specialized;

namespace WAPP_Assignment
{
    public partial class Register : System.Web.UI.Page
    {
        private string DbTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            SetDbTable();
            if (!IsPostBack)
            {
            }
        }

        protected bool IsUsernameDuplicate(string username)
        {
            string queryExist = $"SELECT * FROM {DbTable} WHERE username=@username;";

            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(queryExist, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        conn.Close();
                        if (dt.Rows.Count > 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }

        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            SetDbTable();
            string username = UsernameTxtBox.Text;
            string password = PasswordTxtBox.Text;
            password = MyUtil.ComputeSHA1(password);
            string userType = UserTypeRadio.SelectedValue;
            var client = new MyService();
            if (client.IsUsernameDuplicate(DbTable, username))
            {
                this.UsernameTxtBox.Focus();
                return;
            }
            string queryInsert = "INSERT INTO " + DbTable;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
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
                        queryInsert += " (username, password, full_name, email, gender, profile) VALUES (@username, @password, @full_name, @email, @gender, @profile);";
                        cmd.CommandText = queryInsert;
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@full_name", fullName);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@gender", gender);
                        if (gender == "m")
                            cmd.Parameters.AddWithValue("@profile", "man.png");
                        else
                            cmd.Parameters.AddWithValue("@profile", "girl.png");
                    }

                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Response.Write("<script>alert('Registration successful!'); window.location.href = 'Login.aspx';</script>");
                }
            }
        }

        protected void SetDbTable()
        {
            string userType = this.UserTypeRadio.SelectedValue.ToString();
            DbTable = userType;
        }

    }
}