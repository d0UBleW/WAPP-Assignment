using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace WAPP_Assignment
{
    public partial class Profile : UtilClass.BaseStudentPage
    {
        private int student_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            student_id = Convert.ToInt32(Session["user_id"]);
            if (userType == "admin")
            {
                student_id = GetQueryString("student_id");
            }
            DataRow studentRow = StudentC.GetStudentData(student_id);
            if (studentRow == null) return;
            ChangePasswdLink.NavigateUrl = $"/Student/ChangePassword.aspx?student_id={student_id}";
            DeleteAccLink.NavigateUrl = $"/Student/DeleteAccount.aspx?student_id={student_id}";
            if (!IsPostBack)
            {
                FullNameTxtBox.Text = studentRow["full_name"].ToString();
                EmailTxtBox.Text = studentRow["email"].ToString();
                ProfileImg.ImageUrl = $"/upload/profile/{studentRow["profile"]}";
                GenderList.SelectedValue = studentRow["gender"].ToString().ToLower();
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            string fullName = MyUtil.SanitizeInput(FullNameTxtBox);
            //string fullName = FullNameTxtBox.Text;
            string email = MyUtil.SanitizeInput(EmailTxtBox);
            //string email = EmailTxtBox.Text;
            string gender = GenderList.SelectedValue;
            string filename = Path.GetFileName(ProfileImg.ImageUrl);
            if (gender == "m")
                filename = MyUtil.defaultMaleProfile;
            else
                filename = MyUtil.defaultFemaleProfile;

            if (ProfileUpload.HasFile)
            {
                string result = MyUtil.ValidateImage(ProfileUpload);
                UploadStatusLbl.Text = result;
                if (result != "Upload success")
                {
                    UploadStatusLbl.ForeColor = System.Drawing.Color.Red;
                    UploadStatusPanel.Visible = true;
                    return;
                }
                string ext = MyUtil.GetUploadExtension(ProfileUpload);
                string sha1sum = MyUtil.ComputeSHA1(ProfileUpload.FileContent);
                filename = sha1sum + ext;
                ProfileUpload.SaveAs(Server.MapPath("~/upload/profile/") + filename);
            }
            UploadStatusPanel.Visible = false;
            ProfileImg.ImageUrl = $"/upload/profile/{filename}";
            using (SqlConnection conn = WAPP_Assignment.DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE student SET full_name=@full_name, email=@email, gender=@gender, profile=@profile WHERE student_id=@student_id;";
                    cmd.Parameters.AddWithValue("@full_name", fullName);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@profile", filename);
                    cmd.Parameters.AddWithValue("@student_id", student_id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            List<string> defaultProfileList = new List<string>();
            defaultProfileList.Add(MyUtil.defaultFemaleProfile);
            defaultProfileList.Add(MyUtil.defaultMaleProfile);
            defaultProfileList.Add(MyUtil.defaultProfile);
            MyUtil.UpdateImageStorage(Server.MapPath("~/upload/profile/"), defaultProfileList, "student", "profile");
        }

        protected void RemoveBtn_Click(object sender, EventArgs e)
        {
            ProfileImg.ImageUrl = $"/upload/profile/{MyUtil.defaultProfile}";
        }
    }
}