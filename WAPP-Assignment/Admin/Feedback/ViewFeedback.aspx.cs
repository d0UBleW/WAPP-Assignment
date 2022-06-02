using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WAPP_Assignment.Admin.Feedback
{
    public partial class ViewFeedback : UtilClass.BaseAdminPage
    {
        private int feed_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            feed_id = GetQueryString("feed_id");
            DataTable feedTable = UtilClass.FeedbackC.GetFeedbackData(feed_id);
            if (feedTable.Rows.Count == 0)
            {
                return;
            }
            DataRow feedData = feedTable.Rows[0];
            SubjectLit.Text = $"{feedData["subject"]}";
            ContentTxtBox.Text = $"{feedData["content"]}";
            int student_id = Convert.ToInt32(feedData["student_id"]);
            DataRow studentData = StudentC.GetStudentData(student_id);
            ProfileImg.ImageUrl = $"/upload/profile/{studentData["profile"]}";
            UsernameLbl.Text = $"{studentData["username"]}";

            DeleteLink.NavigateUrl = $"/Admin/Feedback/DeleteFeedback.aspx?feed_id={feed_id}";
        }
    }
}