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
using System.IO;
using System.Web.Services;

namespace WAPP_Assignment.Admin
{
    public partial class AddCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            if (!(bool)Session["isAdmin"])
            {
                // Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                return;
            }
            if (!IsPostBack)
            {
                this.UploadStatusPanel.Visible = false;
            }
            System.Diagnostics.Debug.WriteLine(Server.MapPath("~/upload"));
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            string filename = "";
            if (ThumbnailUpload.HasFile)
            {
                string contentType = ThumbnailUpload.PostedFile.ContentType;
                try
                {
                    System.Diagnostics.Debug.WriteLine(contentType);
                    string[] allowed = {"jpeg", "jpg", "png", "svg+xml"};
                    bool valid = false;
                    foreach (string allowedItem in allowed)
                    {
                        if (contentType == $"image/{allowedItem}")
                        {
                            valid = true;
                            break;
                        }
                    }
                    if (!valid)
                    {
                        throw new Exception("Upload status: Only JPEG, JPG, PNG, or SVG file is allowed");
                    }
                    if (ThumbnailUpload.PostedFile.ContentLength > 102400)
                    {
                        throw new Exception("Upload status: Only image lower than 100 KBs is allowed");
                    }
                }
                catch (Exception ex)
                {
                    this.UploadStatusLbl.Text = ex.Message;
                    this.UploadStatusLbl.ForeColor = System.Drawing.Color.Red;
                    this.UploadStatusPanel.Visible = true;
                    return;
                }
                string ext = "";
                switch (contentType)
                {
                    case "image/jpeg":
                        ext = ".jpeg";
                        break;
                    case "image/jpg":
                        ext = ".jpg";
                        break;
                    case "image/png":
                        ext = ".png";
                        break;
                    case "image/svg+xml":
                        ext = ".svg";
                        break;
                }
                string sha1sum = ComputeSHA1(ThumbnailUpload.FileContent);
                filename = sha1sum + ext;
                ThumbnailUpload.SaveAs(Server.MapPath("~/upload/") + filename);
                this.UploadStatusPanel.Visible = false;
            }
            string title = this.TitleTxtBox.Text;
            string description = this.DescTxtBox.Text;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                string query = "INSERT INTO course (title, description) OUTPUT INSERTED.course_id VALUES (@title, @description);";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!String.IsNullOrEmpty(filename))
                    {
                        cmd.CommandText = "INSERT INTO course (title, description, thumbnail) OUTPUT INSERTED.course_id VALUES (@title, @description, @thumbnail);";
                        cmd.Parameters.AddWithValue("@thumbnail", filename);
                    }
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@description", description);
                    int course_id = (int) cmd.ExecuteScalar();
                    AddCategory();
                    List<string> inputCategories = CatField.Value.Split(new string[]{"<|>"}, StringSplitOptions.None).ToList();
                    cmd.CommandText = "INSERT INTO course_category (course_id, category_id) VALUES (@course_id, (SELECT category_id FROM category WHERE name=@name));";
                    foreach (string cat in inputCategories)
                    {
                        cmd.Parameters.AddWithValue("@course_id", course_id);
                        cmd.Parameters.AddWithValue("@name", cat);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
            }
        }

        protected static string ComputeSHA1(Stream stream)
        {
            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                var hash = sha1.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

        protected void AddCategory()
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                string query = "SELECT name FROM category;";
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    List<string> currCategories = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            currCategories.Add(sdr["name"].ToString());
                        }
                    }
                    List<string> inputCategories = CatField.Value.Split(new string[]{"<|>"}, StringSplitOptions.None).ToList();
                    List<string> newCategories = inputCategories.Except(currCategories).ToList();
                    if (newCategories.Count == 0)
                    {
                        conn.Close();
                        return;
                    }
                    cmd.CommandText = "INSERT INTO category (name) VALUES (@name);";
                    foreach (string category in newCategories)
                    {
                        cmd.Parameters.AddWithValue("@name", category);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    conn.Close();
                }
            }
        }

        [WebMethod]
        public static List<string> SearchCategory(string prefixText, int count)
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    conn.Open();
                    cmd.CommandText = "SELECT name FROM category WHERE name LIKE @SearchText + '%'";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    List<string> categories = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            categories.Add(sdr["name"].ToString());
                        }
                    }
                    conn.Close();
                    return categories;
                }
            }
        }
    }
}