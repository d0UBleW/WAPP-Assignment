using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public static class CourseC
    {
        public static Panel DisplayCourse(int course_id, string userType, List<int> enrolledCourseID = null)
        {
            DataTable courseTable = CourseC.GetCourseData(course_id);
            if (courseTable.Rows.Count == 0) return null;
            DataRow dr = courseTable.Rows[0];
            DataTable courseCatTable = Category.GetCourseCategoryData(course_id);
            Panel cPanel = new Panel();
            Panel imgPanel = new Panel();
            cPanel.CssClass = "container bg-blue course-container";
            //cPanel.ID = $"CourseContainer_{dr["course_id"]}";
            Image thumbnail = new Image
            {
                ImageUrl = "/images/loading.gif",
                Width = 200,
                Height = 200,
            };
            cPanel.Controls.Add(imgPanel);
            thumbnail.Attributes.Add("onload", $"javascript:this.onload=null;this.src='/upload/thumbnail/{dr["thumbnail"]}'");
            imgPanel.Controls.Add(thumbnail);
            HyperLink title = new HyperLink
            {
                NavigateUrl = $"/ViewCourse.aspx?course_id={course_id}",
                Text = dr["title"].ToString(),
                CssClass = "course-title",
                //ID = $"CourseTitle_{dr["course_id"]}",
            };
            title.Style.Add("font-size", "24px");
            cPanel.Controls.Add(title);
            Panel categoryPanel = new Panel
            {
                CssClass = "course-category"
            };
            cPanel.Controls.Add(new Literal { Text = "<br />" });
            cPanel.Controls.Add(categoryPanel);

            foreach (DataRow categoryRow in courseCatTable.Rows)
            {
                Label cat = new Label
                {
                    Text = categoryRow["name"].ToString(),
                    CssClass = "course-category-item badge rounded-pill bg-secondary",
                };
                categoryPanel.Controls.Add(cat);
                // if (courseCatTable.Rows.IndexOf(categoryRow) != courseCatTable.Rows.Count - 1)
                    // categoryPanel.Controls.Add(new Literal { Text = " " });
            }

            Label description = new Label
            {
                Text = dr["description"].ToString(),
            };
            cPanel.Controls.Add(description);
            cPanel.Controls.Add(new Literal { Text = "<br />" });
            double overallRating = CourseC.GetCourseOverallRating(course_id);
            Label rating = new Label
            {
                Text = $"Rating: {overallRating.ToString("0.00")}/5",
        };
            cPanel.Controls.Add(rating);
            cPanel.Controls.Add(new Literal { Text = "<br />" } );
            if (userType == "admin")
            {
                HyperLink editLink = new HyperLink
                {
                    Text = "Edit Course",
                    NavigateUrl = $"/Admin/Course/EditCourse.aspx?course_id={course_id}",
                    CssClass = "btn btn-secondary btn-sm",
                };
                cPanel.Controls.Add(editLink);
                cPanel.Controls.Add(new Literal { Text = "<br />"} );
                cPanel.Controls.Add(new Literal { Text = "<br />"} );
                HyperLink delLink = new HyperLink
                {
                    Text = "Delete Course",
                    NavigateUrl = $"~/Admin/Course/DeleteCourse.aspx?course_id={course_id}",
                    CssClass = "btn btn-secondary btn-sm del-course-link",
                };
                cPanel.Controls.Add(delLink);
                cPanel.Controls.Add(new Literal { Text = "<br />"} );
                cPanel.Controls.Add(new Literal { Text = "<br />"} );
            }
            else if (userType == "student")
            {
                if (enrolledCourseID != null && enrolledCourseID.Contains(course_id)) {
                    HyperLink unenrollLink = new HyperLink
                    {
                        NavigateUrl = $"/Student/Course/UnenrollCourse.aspx?course_id={course_id}",
                        Text = "Unenroll",
                        CssClass = "btn btn-secondary btn-sm unenroll-link",
                    };
                    cPanel.Controls.Add(unenrollLink);
                }
                else
                {
                    HyperLink enrollLink = new HyperLink
                    {
                        Text = "Enroll",
                        NavigateUrl = $"/Student/Course/EnrollCourse.aspx?course_id={course_id}",
                        CssClass = "btn btn-secondary btn-sm",
                    };
                    cPanel.Controls.Add(enrollLink);
                }
                cPanel.Controls.Add(new Literal { Text = "<br />"} );
                cPanel.Controls.Add(new Literal { Text = "<br />"} );
            }
            HyperLink viewLink = new HyperLink
            {
                NavigateUrl = $"/ViewCourse.aspx?course_id={course_id}",
                Text = "View Course",
                CssClass = "btn btn-secondary btn-sm",
            };
            cPanel.Controls.Add(viewLink);
            cPanel.Controls.Add(new Literal { Text = "<br />"} );
            return cPanel;
        }

        public static DataTable GetAllCourseData()
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM course";
                    cmd.Connection = conn;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        DataTable dataTable = new DataTable();
                        sda.Fill(dataTable);
                        conn.Close();
                        return dataTable;
                    }
                }
            }
        }
        public static DataTable GetCourseData(int course_id)
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM course WHERE course_id=@course_id";
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        DataTable dataTable = new DataTable();
                        sda.Fill(dataTable);
                        conn.Close();
                        return dataTable;
                    }
                }
            }
        }

        public static DataTable GetEnrolledCourseData(int student_id)
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM [course] INNER JOIN [enroll] ON course.course_id = enroll.course_id WHERE enroll.student_id=@student_id";
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        DataTable dataTable = new DataTable();
                        sda.Fill(dataTable);
                        conn.Close();
                        return dataTable;
                    }
                }
            }
        }

        public static DataTable GetCourseRating(int course_id)
        {
            DataTable ratingTable = new DataTable();
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM rating WHERE course_id=@course_id;";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(ratingTable);
                    }
                }
                conn.Close();
            }
            return ratingTable;
        }

        public static double GetCourseOverallRating(int course_id)
        {
            double rating = 0;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT ISNULL(AVG(CAST(rating as DECIMAL)), 0) FROM [rating] WHERE course_id=@course_id;";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    var result = cmd.ExecuteScalar();
                    rating = double.Parse(result.ToString());
                }
                conn.Close();
            }
            return rating;
        }
    }
}