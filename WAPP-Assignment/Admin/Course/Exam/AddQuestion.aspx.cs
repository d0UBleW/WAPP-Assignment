using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WAPP_Assignment.Admin.Course.Exam
{
    public partial class AddQuestion : System.Web.UI.Page
    {
        private int exam_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            string exam_id_temp = Request.QueryString["exam_id"];
            if (String.IsNullOrEmpty(exam_id_temp))
            {
                // throw error
                return;
            }
            exam_id = Convert.ToInt32(exam_id_temp);
            if (!IsPostBack)
            {
                int maxSeq = Chapter.GetChapterMaxSeq(exam_id) + 1;
                QueNoTxtBox.Attributes.Add("Max", maxSeq.ToString());
                QueNoRangeValidator.MaximumValue = maxSeq.ToString();
                QueNoRangeValidator.MinimumValue = "1";
                QueNoTxtBox.Text = maxSeq.ToString();
            }

        }

        protected void AddOptBtn_Click(object sender, EventArgs e)
        {

        }
    }
}