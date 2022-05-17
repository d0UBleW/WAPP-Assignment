using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment.Admin
{
    public partial class DeleteCourse : UtilClass.BaseAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int course_id = GetQueryString("course_id");
            DataTable courseTable = CourseC.GetCourseData(course_id);
            if (courseTable.Rows.Count == 0)
            {
                return;
            }

            DataTable chapTable = ChapterC.GetCourseChapterData(course_id);
            foreach (DataRow dr in chapTable.Rows)
            {
                int chapter_id = Convert.ToInt32(dr["chapter_id"]);
                ChapterC.DeleteChapter(chapter_id);
            }

            DataTable examTable = ExamC.GetCourseExamData(course_id);
            foreach (DataRow exam in examTable.Rows)
            {
                int exam_id = Convert.ToInt32(exam["exam_id"]);
                ExamC.DeleteExam(exam_id);
            }

            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE course_category WHERE course_id=@course_id;";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    Category.UpdateCategory();

                    cmd.CommandText = "DELETE rating WHERE course_id=@course_id;";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "DELETE enroll WHERE course_id=@course_id;";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "DELETE course WHERE course_id=@course_id;";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                conn.Close();
            }
            RedirectBack();
        }
    }
}