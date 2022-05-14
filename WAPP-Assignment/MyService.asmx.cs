using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    /// <summary>
    /// Summary description for MyService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MyService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public bool IsUsernameDuplicate(string tbl, string username)
        {
            int count;
            string queryExist = $"SELECT COUNT(*) FROM {tbl} WHERE username=@username;";
            using (SqlConnection conn = DatabaseManager.CreateConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(queryExist, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    var result = cmd.ExecuteScalar();
                    count = Convert.ToInt32(result);
                }
            }
            return count > 0;
        }
    }
}
