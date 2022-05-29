using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Text;
using Ganss.XSS;

namespace WAPP_Assignment
{
    public static class MyUtil
    {
        public const string defaultThumb = "46ECB7BBD7D75AEA077A274C3B192E21534366E2.svg";
        public const string defaultMaleProfile = "man.png";
        public const string defaultFemaleProfile = "girl.png";
        public const string defaultProfile = "default.svg";

        public static string ValidateImage(FileUpload file)
        {
            string contentType = file.PostedFile.ContentType;
            System.Diagnostics.Debug.WriteLine(contentType);
            string[] allowed = {"jpeg", "jpg", "png", "svg+xml"};
            bool valid = false;
            foreach (string allowedItem in allowed)
            {
                if (contentType == $"image/{allowedItem}")
                {
                    valid = true;
                    break;
                }
            }
            if (!valid)
            {
                return "Upload status: Only JPEG, JPG, PNG, or SVG file is allowed";
            }
            if (file.PostedFile.ContentLength > 409600)
            {
                return "Upload status: Only image lower than 400 KBs is allowed";
            }
            return "Upload success";
        }

        public static string GetUploadExtension(FileUpload file)
        {
            string ext = "";
            switch (file.PostedFile.ContentType)
            {
                case "image/jpeg":
                    ext = ".jpeg";
                    break;
                case "image/jpg":
                    ext = ".jpg";
                    break;
                case "image/png":
                    ext = ".png";
                    break;
                case "image/svg+xml":
                    ext = ".svg";
                    break;
            }
            return ext;
        }

        public static string ComputeSHA1(Stream stream)
        {
            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                var hash = sha1.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

        public static string ComputeSHA1(string str)
        {
            byte[] byteArr = Encoding.ASCII.GetBytes(str);
            MemoryStream ms = new MemoryStream(byteArr);
            using (var sha1 = System.Security.Cryptography.SHA1.Create())
            {
                var hash = sha1.ComputeHash(ms);
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }

        public static void UpdateImageStorage(string dir, List<string> defaultImg, string tbl, string col)
        {
            List<string> defaultFile = defaultImg;
            string[] currFile = Directory.GetFiles(dir);
            List<string> usedFile = new List<string>();
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $"SELECT DISTINCT {col} FROM {tbl};";
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            usedFile.Add(dir+sdr[col].ToString());
                        }
                    }
                    List<string> unusedFile = currFile.Except(usedFile).ToList();
                    foreach (string file in unusedFile)
                    {
                        string temp = Path.GetFileName(file);
                        if (!defaultFile.Contains(temp))
                        {
                            File.Delete(file);
                        }
                    }
                }
                conn.Close();
            }
        }

        public static string SanitizeInput(TextBox txtBox)
        {
            var sanitizer = new HtmlSanitizer();
            string sanitized = sanitizer.Sanitize(txtBox.Text);
            return sanitized;
        }

        public static string SanitizeInput(HiddenField field)
        {
            var sanitizer = new HtmlSanitizer();
            string sanitized = sanitizer.Sanitize(field.Value);
            return sanitized;
        }
    }
}