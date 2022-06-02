using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace WAPP_Assignment.Admin.Course.Chapter
{
    public partial class ListChapter : UtilClass.BaseAdminPage
    {
        private int course_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            course_id = GetQueryString("course_id");
            DataTable courseTable = CourseC.GetCourseData(course_id);
            if (courseTable.Rows.Count == 0)
            {
                return;
            }
            ViewCourseLink.NavigateUrl = $"/ViewCourse.aspx?course_id={course_id}";
            ViewCourseLink.Text = $"{courseTable.Rows[0]["title"]}";
            DataTable chapterDataTable = ChapterC.GetCourseChapterData(course_id);
            foreach (DataRow chapterData in chapterDataTable.Rows)
            {
                Panel container = new Panel
                {
                    CssClass = "container"
                };
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
                delChapBtn.Attributes.Add("data-action", "warn");
                delChapBtn.Click += new EventHandler(DelChapBtn_Click);
                container.Controls.Add(editChapBtn);
                container.Controls.Add(delChapBtn);
                container.Controls.Add(new Literal { Text = "<br/><br/>" });
            }
        }
        protected void EditChapBtn_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            string chapter_id = btn.Attributes["data-chap-id"];
            Response.Redirect($"/Admin/Course/Chapter/EditChapter.aspx?chapter_id={chapter_id}");
        }

        protected void DelChapBtn_Click(object sender, EventArgs e)
        {
            LinkButton btn = sender as LinkButton;
            string chapter_id = btn.Attributes["data-chap-id"];
            Response.Redirect($"/Admin/Course/Chapter/DeleteChapter.aspx?chapter_id={chapter_id}");
        }
        protected void AddChapBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/Admin/Course/Chapter/AddChapter.aspx?course_id={course_id}");
        }

    }
}