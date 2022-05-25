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