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
    public partial class ViewChapter : UtilClass.BasePage
    {
        private int chapter_id;
        DataRow chapterRow;
        protected void Page_Load(object sender, EventArgs e)
        {
            string chapter_id_temp = Request.QueryString["chapter_id"];
            if (userType == "nobody")
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            if (string.IsNullOrEmpty(chapter_id_temp))
            {
                return;
            }
            try
            {
                chapter_id = int.Parse(chapter_id_temp);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
            int student_id = Convert.ToInt32(Session["user_id"]);
            DataTable dt = Chapter.GetChapterData(chapter_id);
            if (dt.Rows.Count == 0) return;
            chapterRow = dt.Rows[0];

            int course_id = Convert.ToInt32(chapterRow["course_id"]);
            List<int> enroll_course_id = Student.GetEnrolledCourseID(student_id);
            if (!enroll_course_id.Contains(course_id) && userType != "admin")
            {
                Response.Write($"<script>alert('Please enroll prior to viewing the chapter'); window.location.href = '{Request.UrlReferrer}'</script>");
                return;
            }
            DataTable chapterTable = Chapter.GetCourseChapterData(course_id);
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
                    link.NavigateUrl = $"/Learn/ViewChapter.aspx?chapter_id={dataRow["chapter_id"]}";
                }
                TOCPanel.Controls.Add(link);
                TOCPanel.Controls.Add(new Literal { Text = "<br />" });
            }
            TOCPanel.Controls.Add(new Literal { Text = "<br />" });

            TitleLbl.Text = chapterRow["title"].ToString();
            Page.Title = chapterRow["title"].ToString();
            ContentPlaceholder.Controls.Add(new Literal { Text = chapterRow["content"].ToString() });

            int chapter_no = Convert.ToInt32(chapterRow["sequence"]);
            int chapter_no_end = Chapter.GetChapterMaxSeq(course_id);
            if (chapter_no_end == 1)
            {
                PrevBtn.Visible = false;
                NextBtn.Visible = false;
            }
            else if (chapter_no == 1)
            {
                PrevBtn.Visible = false;
                NextBtn.Visible = true;
            }
            else if (chapter_no == chapter_no_end)
            {
                PrevBtn.Visible = true;
                NextBtn.Visible = false;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            int course_id = Convert.ToInt32(chapterRow["course_id"]);
            int seq = Convert.ToInt32(chapterRow["sequence"]);
            int chapter_id = Chapter.GetNextOrPrevChapterID(course_id, seq, "prev");
            Response.Redirect($"/Learn/ViewChapter.aspx?chapter_id={chapter_id}");
        }

        protected void NextBtn_Click(object sender, EventArgs e)
        {
            int course_id = Convert.ToInt32(chapterRow["course_id"]);
            int seq = Convert.ToInt32(chapterRow["sequence"]);
            int chapter_id = Chapter.GetNextOrPrevChapterID(course_id, seq, "next");
            Response.Redirect($"/Learn/ViewChapter.aspx?chapter_id={chapter_id}");
        }
    }
}