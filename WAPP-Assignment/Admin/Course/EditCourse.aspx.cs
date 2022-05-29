using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace WAPP_Assignment.Admin
{
    public partial class EditCourse : UtilClass.BaseAdminPage
    {
        private int course_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            course_id = GetQueryString("course_id");
            DataTable courseDataTable = WAPP_Assignment.CourseC.GetCourseData(course_id);
            if (courseDataTable.Rows.Count == 0)
            {
                Panel1.Visible = false;
                return;
            }
            DataRow courseData = courseDataTable.Rows[0];
            ViewCourseLink.Text = courseData["title"].ToString();
            ViewCourseLink.NavigateUrl = $"/ViewCourse.aspx?course_id={course_id}";
            if (!IsPostBack)
            {
                TitleTxtBox.Text = courseData["title"].ToString();
                DescTxtBox.Text = courseData["description"].ToString();
                ThumbnailImg.ImageUrl = $"/upload/thumbnail/{courseData["thumbnail"]}";
                DataTable categoryDataTable = Category.GetCourseCategoryData(course_id);
                List<string> categoryList = new List<string>();
                foreach (DataRow row in categoryDataTable.Rows)
                {
                    ListItem item = new ListItem { Text = row["name"].ToString() };
                    CatList.Items.Add(item);
                    categoryList.Add(item.Text);
                }
                CatField.Value = string.Join("<|>", categoryList);
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            DataTable categoryTable = Category.GetCourseCategoryData(course_id);
            List<string> old_category = new List<string>();
            foreach (DataRow row in categoryTable.Rows)
            {
                old_category.Add(row["name"].ToString());
            }
            string filename = Path.GetFileName(ThumbnailImg.ImageUrl);
            if (ThumbnailUpload.HasFile)
            {
                string result = MyUtil.ValidateImage(ThumbnailUpload);
                this.UploadStatusLbl.Text = result;
                if (result != "Upload success")
                {
                    this.UploadStatusPanel.Visible = true;
                    return;
                }
                string ext = MyUtil.GetUploadExtension(ThumbnailUpload);
                string sha1sum = MyUtil.ComputeSHA1(ThumbnailUpload.FileContent);
                filename = sha1sum + ext;
                ThumbnailUpload.SaveAs(Server.MapPath("~/upload/thumbnail/") + filename);
                ThumbnailImg.ImageUrl = $"/upload/thumbnail/{filename}";
                this.UploadStatusPanel.Visible = false;
            }
            string title = MyUtil.SanitizeInput(TitleTxtBox);
            //string title = TitleTxtBox.Text;
            string description = MyUtil.SanitizeInput(DescTxtBox);
            //string description = DescTxtBox.Text;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                string query = "UPDATE course SET title=@title, description=@description, thumbnail=@thumbnail WHERE course_id=@course_id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@thumbnail", filename);
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    List<string> inputCategories = CatField.Value.Split(new string[]{"<|>"}, StringSplitOptions.None).ToList();
                    Category.AddNewCategory(inputCategories);
                    List<string> newCategories = inputCategories.Except(old_category).ToList();
                    cmd.CommandText = "INSERT INTO course_category (course_id, category_id) VALUES (@course_id, (SELECT category_id FROM category WHERE name=@name));";
                    foreach (string cat in inputCategories)
                    {
                        if (string.IsNullOrEmpty(cat)) continue;
                        cmd.Parameters.AddWithValue("@course_id", course_id);
                        cmd.Parameters.AddWithValue("@name", cat);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    List<string> removedCategories = old_category.Except(inputCategories).ToList();
                    cmd.CommandText = "DELETE course_category WHERE course_id=@course_id AND category_id=(SELECT category_id FROM category WHERE name=@name);";
                    foreach (string cat in removedCategories)
                    {
                        cmd.Parameters.AddWithValue("@course_id", course_id);
                        cmd.Parameters.AddWithValue("@name", cat);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    Category.UpdateCategory();
                    List<string> defaultThumbList = new List<string>();
                    defaultThumbList.Add(MyUtil.defaultThumb);
                    MyUtil.UpdateImageStorage(Server.MapPath("~/upload/thumbnail/"), defaultThumbList, "course", "thumbnail");
                    old_category = newCategories;
                }
                conn.Close();
            }
            DataTable categoryDataTable = Category.GetCourseCategoryData(course_id);
            CatList.Items.Clear();
            List<string> categoryList = new List<string>();
            foreach (DataRow row in categoryDataTable.Rows)
            {
                ListItem item = new ListItem { Text = row["name"].ToString() };
                CatList.Items.Add(item);
                categoryList.Add(item.Text);
            }
            CatField.Value = string.Join("<|>", categoryList);
        }

        [WebMethod]
        public static List<string> SearchCategory(string prefixText, int count)
        {
            return MyAutoComplete.ListCategory(prefixText, count);
        }

        protected void BackLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ListCourse.aspx");
        }

        protected void RemoveLinkBtn_Click(object sender, EventArgs e)
        {
            ThumbnailImg.ImageUrl = $"/upload/thumbnail/{MyUtil.defaultThumb}";
        }
    }
}