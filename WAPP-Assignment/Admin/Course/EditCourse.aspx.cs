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
            if (!IsPostBack)
            {
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
            DataTable chapterDataTable = ChapterC.GetCourseChapterData(course_id);
            foreach (DataRow chapterData in chapterDataTable.Rows)
            {
                Panel container = new Panel();
                container.CssClass = "container";
                Label chapTitle = new Label {  Text = $"{chapterData["sequence"]}. {chapterData["title"]}" };
                container.Controls.Add(chapTitle);
                ChapterPlaceholder.Controls.Add(container);
                LinkButton editChapBtn = new LinkButton
                {
                    Text = "Edit Chapter",
                    ID = $"editChapBtn-{chapterData["chapter_id"]}",
                    CssClass = "btn btn-secondary btn-md",
                };
                editChapBtn.Click += new EventHandler(EditChapBtn_Click);
                editChapBtn.Attributes.Add("data-chap-id", chapterData["chapter_id"].ToString());
                LinkButton delChapBtn = new LinkButton
                {
                    Text = "Delete Chapter",
                    ID = $"delChapBtn_{chapterData["chapter_id"]}",
                    CssClass = "btn btn-secondary btn-md",
                };
                delChapBtn.Attributes.Add("data-chap-id", chapterData["chapter_id"].ToString());
                delChapBtn.OnClientClick = "return prompt('Please type in \"Yes, I am sure!\" to proceed') === 'Yes, I am sure!';";
                delChapBtn.Click += new EventHandler(DelChapBtn_Click);
                ChapterPlaceholder.Controls.Add(editChapBtn);
                ChapterPlaceholder.Controls.Add(delChapBtn);
                ChapterPlaceholder.Controls.Add(new Literal { Text = "<br/><br/>" });
            }
            DataTable examTable = ExamC.GetCourseExamData(course_id);
            foreach (DataRow examData in examTable.Rows)
            {
                Panel container = new Panel();
                container.CssClass = "container";
                Label examTitle = new Label {  Text = $"{examData["title"]}" };
                container.Controls.Add(examTitle);
                ExamPlaceholder.Controls.Add(container);
                LinkButton editExamBtn = new LinkButton
                {
                    Text = "Edit Exam",
                    ID = $"editExamBtn-{examData["exam_id"]}",
                    CssClass = "btn btn-secondary btn-md",
                };
                editExamBtn.Click += new EventHandler(EditExamBtn_Click);
                editExamBtn.Attributes.Add("data-exam-id", examData["exam_id"].ToString());
                LinkButton delExamBtn = new LinkButton
                {
                    Text = "Delete Exam",
                    ID = $"delExamBtn_{examData["exam_id"]}",
                    CssClass = "btn btn-secondary btn-md",
                };
                delExamBtn.Attributes.Add("data-exam-id", examData["exam_id"].ToString());
                delExamBtn.OnClientClick = "return prompt('Please type in \"Yes, I am sure!\" to proceed') === 'Yes, I am sure!';";
                delExamBtn.Click += new EventHandler(DelExamBtn_Click);
                ExamPlaceholder.Controls.Add(editExamBtn);
                ExamPlaceholder.Controls.Add(delExamBtn);
                ExamPlaceholder.Controls.Add(new Literal { Text = "<br/><br/>" });
            }
        }

        protected void AddChapBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/Admin/Course/Chapter/AddChapter.aspx?course_id={course_id}");
        }
        protected void EditChapBtn_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            //Regex rg = new Regex(@"editChapBtn-(\d+)");
            //Match match = rg.Match(btn.ID);
            //string chapter_id = match.Groups[1].Value;
            string chapter_id = btn.Attributes["data-chap-id"];
            Response.Redirect($"/Admin/Course/Chapter/EditChapter.aspx?chapter_id={chapter_id}");
        }

        protected void DelChapBtn_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            //Regex rg = new Regex(@"delChapBtn(\d+)");
            //Match match = rg.Match(btn.ID);
            //string chapter_id = match.Groups[1].Value;
            string chapter_id = btn.Attributes["data-chap-id"];
            Response.Redirect($"/Admin/Course/Chapter/DeleteChapter.aspx?chapter_id={chapter_id}");
        }

        protected void EditExamBtn_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            string exam_id = btn.Attributes["data-exam-id"];
            Response.Redirect($"/Admin/Course/Exam/EditExam.aspx?exam_id={exam_id}");
        }

        protected void DelExamBtn_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            string exam_id = btn.Attributes["data-exam-id"];
            Response.Redirect($"/Admin/Course/Exam/DeleteExam.aspx?exam_id={exam_id}");
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            List<string> old_category = CatField.Value.Split(new string[] { "<|>" }, StringSplitOptions.None).ToList();
            string filename = Path.GetFileName(ThumbnailImg.ImageUrl);
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
                    List<string> defaultThumbList = new List<string>();
                    defaultThumbList.Add(MyUtil.defaultThumb);
                    MyUtil.UpdateImageStorage(Server.MapPath("~/upload/thumbnail/"), defaultThumbList, "course", "thumbnail");
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

        protected void AddExBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/Admin/Course/Exam/AddExam.aspx?course_id={course_id}");
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