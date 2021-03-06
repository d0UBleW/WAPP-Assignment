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
    public partial class AddCourse : UtilClass.BaseAdminPage
    {
        private int course_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UploadStatusPanel.Visible = false;
            }
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            string filename = "";
            if (ThumbnailUpload.HasFile)
            {
                string result = MyUtil.ValidateImage(ThumbnailUpload);
                UploadStatusLbl.Text = result;
                if (result != "Upload success")
                {
                    UploadStatusLbl.ForeColor = System.Drawing.Color.Red;
                    UploadStatusPanel.Visible = true;
                    return;
                }
                string ext = MyUtil.GetUploadExtension(ThumbnailUpload);
                string sha1sum = MyUtil.ComputeSHA1(ThumbnailUpload.FileContent);
                filename = sha1sum + ext;
                ThumbnailUpload.SaveAs(Server.MapPath("~/upload/thumbnail/") + filename);
                UploadStatusPanel.Visible = false;
            }
            string title = MyUtil.SanitizeInput(TitleTxtBox);
            string description = MyUtil.SanitizeInput(DescTxtBox);
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
                    course_id = (int) cmd.ExecuteScalar();
                    List<string> inputCategories = MyUtil.SanitizeInput(CatField).Split(new string[]{"~|~"}, StringSplitOptions.None).ToList();
                    Category.AddCategory(inputCategories);
                    cmd.Parameters.Clear();
                    cmd.CommandText = "INSERT INTO course_category (course_id, category_id) VALUES (@course_id, (SELECT category_id FROM category WHERE name=@name));";
                    foreach (string cat in inputCategories)
                    {
                        if (string.IsNullOrEmpty(cat)) continue;
                        cmd.Parameters.AddWithValue("@course_id", course_id);
                        cmd.Parameters.AddWithValue("@name", cat);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }
                conn.Close();
                CatField.Value = "";
            }
            Response.Redirect($"~/ViewCourse.aspx?course_id={course_id}");
        }

        [WebMethod]
        public static List<string> SearchCategory(string prefixText, int count)
        {
            return MyAutoComplete.ListCategory(prefixText, count);
        }
    }
}