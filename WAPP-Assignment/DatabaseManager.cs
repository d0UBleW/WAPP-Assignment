using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace WAPP_Assignment
{
    public static class DatabaseManager
    {
        public static string ConnectionString = "iLearnDBConStr";
        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);
        }
    }
}