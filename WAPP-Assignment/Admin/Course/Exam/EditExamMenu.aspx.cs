using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAPP_Assignment.Admin.Course.Exam
{
    public partial class EditExamMenu : UtilClass.BaseAdminPage
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
            DataTable examTable = ExamC.GetCourseExamData(course_id);
            foreach (DataRow examData in examTable.Rows)
            {
                Panel container = new Panel
                {
                    CssClass = "list-group-item list-group-item-action d-flex align-items-center"
                };
                Label examTitle = new Label
                {
                    Text = $"{examData["title"]}",
                    CssClass = "me-auto",
                };
                container.Controls.Add(examTitle);
                ExamPlaceholder.Controls.Add(container);
                Panel btnGroup = new Panel
                {
                    CssClass = "btn-group",
                };
                btnGroup.Attributes.Add("role", "group");
                btnGroup.Attributes.Add("aria-label", "Edit and delete chapter button group");
                LinkButton editExamBtn = new LinkButton
                {
                    Text = "Edit Exam",
                    ID = $"editExamBtn-{examData["exam_id"]}",
                    CssClass = "btn btn-outline-primary btn-sm",
                };
                editExamBtn.Click += new EventHandler(EditExamBtn_Click);
                editExamBtn.Attributes.Add("data-exam-id", examData["exam_id"].ToString());
                LinkButton delExamBtn = new LinkButton
                {
                    Text = "Delete Exam",
                    ID = $"delExamBtn_{examData["exam_id"]}",
                    CssClass = "btn btn-outline-danger btn-sm",
                };
                delExamBtn.Attributes.Add("data-exam-id", examData["exam_id"].ToString());
                delExamBtn.Attributes.Add("data-action", "warn");
                delExamBtn.Click += new EventHandler(DelExamBtn_Click);
                btnGroup.Controls.Add(editExamBtn);
                btnGroup.Controls.Add(delExamBtn);
                container.Controls.Add(btnGroup);
            }
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

        protected void AddExBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/Admin/Course/Exam/AddExam.aspx?course_id={course_id}");
        }
    }
}