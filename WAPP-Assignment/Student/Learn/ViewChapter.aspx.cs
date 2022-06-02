using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public partial class ViewChapter : UtilClass.BaseStudentPage
    {
        private int chapter_id;
        DataRow chapterRow;
        protected void Page_Load(object sender, EventArgs e)
        {
            chapter_id = GetQueryString("chapter_id");
            int student_id = Convert.ToInt32(Session["user_id"]);
            DataTable dt = ChapterC.GetChapterData(chapter_id);
            if (dt.Rows.Count == 0) return;
            chapterRow = dt.Rows[0];

            int course_id = Convert.ToInt32(chapterRow["course_id"]);
            DataTable courseTable = CourseC.GetCourseData(course_id);
            CourseLink.Text = courseTable.Rows[0]["title"].ToString();
            CourseLink.NavigateUrl = $"/ViewCourse.aspx?course_id={course_id}";
            ChapBreadLit.Text = $"{chapterRow["sequence"]}. {chapterRow["title"]}";

            List<int> enroll_course_id = StudentC.GetEnrolledCourseID(student_id);
            if (!enroll_course_id.Contains(course_id) && userType != "admin")
            {
                if (Request.UrlReferrer != null)
                {
                    Response.Write($"<script>alert('Please enroll prior to viewing the exam'); window.location.href = '{Request.UrlReferrer}'</script>");
                    return;
                }
                Response.Write($"<script>alert('Please enroll prior to viewing the exam'); window.location.href = '/Home.aspx'</script>");
                return;
            }
            DataTable chapterTable = ChapterC.GetCourseChapterData(course_id);
            foreach (DataRow dataRow in chapterTable.Rows)
            {
                HyperLink link = new HyperLink
                {
                    Text = $"{dataRow["sequence"]}. {dataRow["title"]}",
                };
                if (dataRow["chapter_id"].ToString() == chapterRow["chapter_id"].ToString())
                {
                    link.NavigateUrl = "#";
                }
                else
                {
                    link.NavigateUrl = $"/Student/Learn/ViewChapter.aspx?chapter_id={dataRow["chapter_id"]}";
                }
                ChapOutlinePanel.Controls.Add(link);
                ChapOutlinePanel.Controls.Add(new Literal { Text = "<br />" });
            }
            ChapOutlinePanel.Controls.Add(new Literal { Text = "<br />" });

            TitleLtl.Text = chapterRow["title"].ToString();
            Page.Title = chapterRow["title"].ToString();
            ContentPlaceholder.Controls.Add(new Literal { Text = chapterRow["content"].ToString() });

            int chapter_no = Convert.ToInt32(chapterRow["sequence"]);
            int chapter_no_end = ChapterC.GetChapterMaxSeq(course_id);
            if (chapter_no_end == 1)
            {
                PrevBtn.Enabled = false;
                NextBtn.Enabled = false;
            }
            else if (chapter_no == 1)
            {
                PrevBtn.Enabled = false;
                NextBtn.Enabled = true;
            }
            else if (chapter_no == chapter_no_end)
            {
                PrevBtn.Enabled = true;
                NextBtn.Enabled = false;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            int course_id = Convert.ToInt32(chapterRow["course_id"]);
            int seq = Convert.ToInt32(chapterRow["sequence"]);
            int chapter_id = ChapterC.GetNextOrPrevChapterID(course_id, seq, "prev");
            Response.Redirect($"/Student/Learn/ViewChapter.aspx?chapter_id={chapter_id}");
        }

        protected void NextBtn_Click(object sender, EventArgs e)
        {
            int course_id = Convert.ToInt32(chapterRow["course_id"]);
            int seq = Convert.ToInt32(chapterRow["sequence"]);
            int chapter_id = ChapterC.GetNextOrPrevChapterID(course_id, seq, "next");
            Response.Redirect($"/Student/Learn/ViewChapter.aspx?chapter_id={chapter_id}");
        }
    }
}