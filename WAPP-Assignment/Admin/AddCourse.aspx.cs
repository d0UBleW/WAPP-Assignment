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

namespace WAPP_Assignment.Admin
{
    public partial class AddCourse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.UploadStatusPanel.Visible = false;
            }
            System.Diagnostics.Debug.WriteLine(Server.MapPath("~/upload"));
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            string sha1sum = "";
            if (ThumbnailUpload.HasFile)
            {
                try
                {
                    string contentType = ThumbnailUpload.PostedFile.ContentType;
                    System.Diagnostics.Debug.WriteLine(contentType);
                    if (contentType != "image/jpeg" && contentType != "image/png" && contentType != "image/svg+xml")
                    {
                        throw new Exception("Upload status: Only JPEG or PNG file is allowed");
                    }
                    if (ThumbnailUpload.PostedFile.ContentLength > 102400)
                    {
                        throw new Exception("Upload status: Only image lower than 100 KBs is allowed");
                    }
                    string filename = Path.GetFileName(ThumbnailUpload.FileName);
                    sha1sum = ComputeSHA1(ThumbnailUpload.FileContent);
                    ThumbnailUpload.SaveAs(Server.MapPath("~/upload/") + sha1sum);
                }
                catch (Exception ex)
                {
                    this.UploadStatusLbl.Text = ex.Message;
                    this.UploadStatusLbl.ForeColor = System.Drawing.Color.Red;
                    this.UploadStatusPanel.Visible = true;
                    return;
                }
                this.UploadStatusPanel.Visible = false;
            }
            string title = this.TitleTxtBox.Text;
            string description = this.DescTxtBox.Text;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["iLearnDBConStr"].ConnectionString);
            conn.Open();
            string query = "INSERT INTO course (title, description) VALUES (@title, @description);";
            SqlCommand cmd = new SqlCommand(query, conn);
            if (!String.IsNullOrEmpty(sha1sum))
            {
                cmd.CommandText = "INSERT INTO course (title, description, thumbnail) VALUES (@title, @description, @thumbnail);";
                cmd.Parameters.AddWithValue("@thumbnail", sha1sum);
            }
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        protected static string ComputeSHA1(Stream stream)
        {
            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                var hash = sha1.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
    }
}