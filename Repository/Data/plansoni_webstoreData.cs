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
        /*
        public string ExecuteProcedure( string sp_name)
        {
            SqlCommand cmd = new SqlCommand(connectionString);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = sp_name;

            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    DbParameter dbParameter = cmd.CreateParameter();
                    string name = item.Name;
                    if (!name.StartsWith("@"))
                        name = "@" + name;
                    dbParameter.ParameterName = name;
                    dbParameter.Direction = ParameterDirection.Input;
                    //dbParameter.Size = this.GetSize(item); //if varchar then col length
                    dbParameter.Value = item.Value;
                    cmd.Parameters.Add(dbParameter);
                }
            }

            cmd.ExecuteNonQuery();

            if (parameters != null)
            {
                foreach (var item in parameters)
                {
                    item.Value = cmd.Parameters[item.Name].Value;
                    if (item.Value == DBNull.Value)
                        item.Value = null;
                }
            }
        }

        */
    }
}



 
