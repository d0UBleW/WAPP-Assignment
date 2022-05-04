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
    public partial class EditCourse : System.Web.UI.Page
    {
        private int course_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            string course_id_temp = Request.QueryString["course_id"];
            if (String.IsNullOrEmpty(course_id_temp))
            {
                return;
            }
            course_id = Convert.ToInt32(course_id_temp);
            if (!IsPostBack)
            {
                DataTable courseDataTable = WAPP_Assignment.Course.GetCourseData(course_id);
                if (courseDataTable.Rows.Count == 0)
                {
                    return;
                }
                DataRow courseData = courseDataTable.Rows[0];
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
            DataTable chapterDataTable = Chapter.GetCourseChapterData(course_id);
            StringBuilder sb = new StringBuilder();
            foreach (DataRow chapterData in chapterDataTable.Rows)
            {
                sb.AppendLine("<div class=\"container\">");
                sb.AppendLine($"<h3>{chapterData["sequence"]}. {chapterData["title"]}</h3>");
                sb.AppendLine("</div>");
                ChapterPlaceholder.Controls.Add(new Literal { Text = sb.ToString() });
                sb.Clear();
                Button editChapBtn = new Button
                {
                    Text = "Edit Chapter",
                    ID = $"editChapBtn-{chapterData["chapter_id"]}",
                };
                editChapBtn.Click += new EventHandler(EditChapBtn_Click);
                editChapBtn.Attributes.Add("data-chap-id", chapterData["chapter_id"].ToString());
                //Button delChapBtn = new Button
                //{
                //    Text = "Delete Chapter",
                //    ID = $"delChapBtn{chapterData["chapter_id"]}",
                //    UseSubmitBehavior = false,
                //    CausesValidation = false,
                //};
                sb.AppendLine($"<button type=\"button\" id=\"delChapBtn-{chapterData["chapter_id"]}\" data-chap-id=\"{chapterData["chapter_id"]}\">Delete Chapter</button>");
                ChapterPlaceholder.Controls.Add(editChapBtn);
                ChapterPlaceholder.Controls.Add(new Literal { Text = sb.ToString() });
                ChapterPlaceholder.Controls.Add(new Literal { Text = "<br/><br/>" });
                sb.Clear();
            }
        }

        protected void AddChapBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/Admin/Course/Chapter/AddChapter.aspx?course_id={course_id}");
        }
        protected void EditChapBtn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            //Regex rg = new Regex(@"editChapBtn-(\d+)");
            //Match match = rg.Match(btn.ID);
            //string chapter_id = match.Groups[1].Value;
            string chapter_id = btn.Attributes["data-chap-id"];
            Response.Redirect($"/Admin/Course/Chapter/EditChapter.aspx?chapter_id={chapter_id}");
        }

        protected void DelChapBtn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Regex rg = new Regex(@"delChapBtn(\d+)");
            Match match = rg.Match(btn.ID);
            string chapter_id = match.Groups[1].Value;
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            string filename = Path.GetFileName(ThumbnailImg.ImageUrl);
            List<string> old_category = CatField.Value.Split(new string[] { "<|>" }, StringSplitOptions.None).ToList();
            if (ThumbnailUpload.HasFile)
            {
                string result = MyUtil.ValidateImage(ThumbnailUpload);
                this.UploadStatusLbl.Text = result;
                if (result != "Upload success")
                {
                    this.UploadStatusLbl.ForeColor = System.Drawing.Color.Red;
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
            string title = TitleTxtBox.Text;
            string description = DescTxtBox.Text;
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
                    MyUtil.UpdateThumbnailStorage(Server.MapPath("~/upload/thumbnail/"));
                    old_category = newCategories;
                }
                conn.Close();
            }
        }

        [WebMethod]
        public static List<string> SearchCategory(string prefixText, int count)
        {
            return MyAutoComplete.ListCategory(prefixText, count);
        }

        protected void RemoveBtn_Click(object sender, EventArgs e)
        {
            ThumbnailImg.ImageUrl = $"/upload/thumbnail/{MyUtil.defaultThumb}";
        }

        protected void AddExBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/Admin/Course/Exam/AddExam.aspx?course_id={course_id}");
        }
    }
}