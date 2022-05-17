﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace WAPP_Assignment
{
    public partial class MyCourse : UtilClass.BaseStudentPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int student_id = Convert.ToInt32(Session["user_id"].ToString());
            DataTable courseTable = CourseC.GetEnrolledCourseData(student_id);
            foreach (DataRow dr in courseTable.Rows)
            {
                Panel cPanel = new Panel();
                CoursePlaceholder.Controls.Add(cPanel);
                Panel imgPanel = new Panel();
                cPanel.Controls.Add(imgPanel);
                cPanel.CssClass = "container bg-blue";
                Image thumbnail = new Image
                {
                    ImageUrl = "/images/loading.gif",
                    Width = 200,
                    Height = 200,
                };
                thumbnail.Attributes.Add("onload", $"javascript:this.onload=null;this.src='/upload/thumbnail/{dr["thumbnail"]}'");
                imgPanel.Controls.Add(thumbnail);
                HyperLink title = new HyperLink
                {
                    NavigateUrl = $"/ViewCourse.aspx?course_id={dr["course_id"]}",
                    Text = dr["title"].ToString(),
                };
                title.Style.Add("font-size", "36px");
                cPanel.Controls.Add(title);
                Label description = new Label
                {
                    Text = dr["description"].ToString(),
                };
                cPanel.Controls.Add(new Literal { Text = "<br />" } );
                cPanel.Controls.Add(description);
                cPanel.Controls.Add(new Literal { Text = "<br />" } );
                HyperLink viewLink = new HyperLink
                {
                    NavigateUrl = $"/ViewCourse.aspx?course_id={dr["course_id"]}",
                    Text = "View Course",
                };
                cPanel.Controls.Add(viewLink);
                cPanel.Controls.Add(new Literal { Text = "<br />" } );
            }
        }
    }
}