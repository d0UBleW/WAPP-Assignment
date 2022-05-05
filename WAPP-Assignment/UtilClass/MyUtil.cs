using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public static class MyUtil
    {
        public const string defaultThumb = "3041EBCF66C0270BBB172CCDB32C9386F61CC211.svg";

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

        public static void UpdateThumbnailStorage(string dir)
        {
            string defaultFile = dir + defaultThumb;
            string[] currFile = Directory.GetFiles(dir);
            List<string> usedFile = new List<string>();
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT DISTINCT thumbnail FROM course;";
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            usedFile.Add(dir+sdr["thumbnail"].ToString());
                        }
                    }
                    List<string> unusedFile = currFile.Except(usedFile).ToList();
                    foreach (string file in unusedFile)
                    {
                        if (file != defaultFile)
                        {
                            File.Delete(file);
                        }
                    }
                }
                conn.Close();
            }
        }
    }
}