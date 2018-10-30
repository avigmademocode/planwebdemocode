using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MyProject.Data
{
    public class plansoni_webstoreData
    {
        public static string connectionString;
              //  = "Data Source=DHANUSH\\ADITYA;Initial Catalog=planweb;Integrated Security=SSPI;";

        public static SqlConnection GetConnection()
        {
         connectionString=ConfigurationManager.ConnectionStrings["plansoni_webstoreDBContext"].ToString();

        SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}



 
