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

            Panel colPanel = new Panel
            {
                CssClass = "col-lg-4 course-container d-flex align-items-stretch",
            };

            Panel cPanel = new Panel
            {
                CssClass = "card mb-3 course-card flex-fill",
            };
            colPanel.Controls.Add(cPanel);

            Panel rowInCard = new Panel
            {
                CssClass = "row g-0 flex-grow-1",
            };
            cPanel.Controls.Add(rowInCard);

            Panel imgCol = new Panel
            {
                CssClass = "col-md-4",
            };

            rowInCard.Controls.Add(imgCol);

            Image thumbnail = new Image
            {
                ImageUrl = "/images/loading.gif",
                CssClass = "cover img-fluid rounded-start course-img"
            };
            thumbnail.Attributes.Add("onload", $"javascript:this.onload=null;this.src='/upload/thumbnail/{dr["thumbnail"]}'");
            imgCol.Controls.Add(thumbnail);

            Panel detailCol = new Panel
            {
                CssClass = "col-md-8",
            };
            rowInCard.Controls.Add(detailCol);

            Panel detail = new Panel
            {
                CssClass = "course-detail-container card-body mb-3",
            };
            detailCol.Controls.Add(detail);

            HyperLink title = new HyperLink
            {
                NavigateUrl = $"/ViewCourse.aspx?course_id={course_id}",
                Text = dr["title"].ToString(),
                CssClass = "course-title card-title fs-4 mb-1",
            };
            detail.Controls.Add(title);
            detail.Controls.Add(new Literal { Text = "<br />" });

            double overallRating = CourseC.GetCourseOverallRating(course_id);
            int ratingCount = CourseC.GetCourseRatingCount(course_id);
            Label rating = new Label
            {
                Text = $"Rating: {overallRating:0.00}/5.00 ({ratingCount})",
            };
            detail.Controls.Add(rating);


            Panel categoryPanel = new Panel
            {
                CssClass = "course-category mb-3"
            };
            detail.Controls.Add(categoryPanel);

            foreach (DataRow categoryRow in courseCatTable.Rows)
            {
                Label cat = new Label
                {
                    Text = categoryRow["name"].ToString(),
                    CssClass = "course-category-item badge rounded-pill bg-secondary",
                };
                categoryPanel.Controls.Add(cat);
            }

            Panel linkBtnGroup = new Panel
            {
                CssClass = "btn-group btn-group-md"
            };
            linkBtnGroup.Attributes.Add("role", "group");
            linkBtnGroup.Attributes.Add("aria-label", "Course Button Group");

            if (userType == "admin")
            {
                HyperLink editLink = new HyperLink
                {
                    Text = "Edit Course",
                    NavigateUrl = $"/Admin/Course/EditCourse.aspx?course_id={course_id}",
                    CssClass = "btn btn-outline-primary btn-sm",
                };
                linkBtnGroup.Controls.Add(editLink);
                HyperLink delLink = new HyperLink
                {
                    Text = "Delete Course",
                    NavigateUrl = $"~/Admin/Course/DeleteCourse.aspx?course_id={course_id}",
                    CssClass = "btn btn-outline-danger btn-sm",
                };
                delLink.Attributes.Add("data-action", "warn");
                linkBtnGroup.Controls.Add(delLink);
            }
            else if (userType == "student")
            {
                if (enrolledCourseID != null && enrolledCourseID.Contains(course_id)) {
                    HyperLink unenrollLink = new HyperLink
                    {
                        NavigateUrl = $"/Student/Course/UnenrollCourse.aspx?course_id={course_id}",
                        Text = "Unenroll",
                        CssClass = "btn btn-outline-danger btn-sm",
                    };
                    unenrollLink.Attributes.Add("data-action", "warn");
                    linkBtnGroup.Controls.Add(unenrollLink);
                    colPanel.Attributes.Add("data-enrolled", "true");
                }
                else
                {
                    HyperLink enrollLink = new HyperLink
                    {
                        Text = "Enroll",
                        NavigateUrl = $"/Student/Course/EnrollCourse.aspx?course_id={course_id}",
                        CssClass = "btn btn-outline-primary btn-sm",
                    };
                    linkBtnGroup.Controls.Add(enrollLink);
                    colPanel.Attributes.Add("data-enrolled", "false");
                }
            }
            HyperLink viewLink = new HyperLink
            {
                NavigateUrl = $"/ViewCourse.aspx?course_id={course_id}",
                Text = "View Course",
                CssClass = "btn btn-outline-primary btn-sm",
            };
            linkBtnGroup.Controls.Add(viewLink);
            detail.Controls.Add(linkBtnGroup);
            return colPanel;
        }

        public static DataTable GetAllCourseData()
        {
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM course ORDER BY [title]";
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

        public static int GetCourseRatingCount(int course_id)
        {
            int count;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT COUNT(*) FROM [rating] WHERE course_id=@course_id;";
                    cmd.Parameters.AddWithValue("@course_id", course_id);
                    var result = cmd.ExecuteScalar();
                    count = Convert.ToInt32(result);
                }
                conn.Close();
            }
            return count;
        }

        public static DataTable GetPopularCourseID()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT course.course_id, ISNULL(AVG(CAST(rating AS DECIMAL)), 0) AS rate FROM course FULL JOIN [rating] ON course.course_id = rating.course_id GROUP BY course.course_id ORDER BY rate DESC;";
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.SelectCommand = cmd;
                        adapter.Fill(dataTable);
                    }
                }
                conn.Close();
            }
            return dataTable;
        }

        public static int GetCourseCount()
        {
            int count;
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT COUNT(*) FROM [course];";
                    var result = cmd.ExecuteScalar();
                    count = Convert.ToInt32(result);
                }
                conn.Close();
            }
            return count;
        }
    }
}