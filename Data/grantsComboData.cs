using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using MyProject.Models;

namespace MyProject.Data
{

    public class grants_customerData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[grants_customerSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows) {
                    dt.Load(reader); }
                reader.Close();
            }
            catch (SqlException)
            {
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        //public static List<customer> List()
        //{
        //    List<customer> customerList = new List<customer>();
        //    SqlConnection connection = plansoni_webstoreData.GetConnection();
        //    String selectProcedure = "[grants_customerSelect]";
        //    SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader reader = selectCommand.ExecuteReader();
        //        customer customer = new customer();
        //        while (reader.Read())
        //        {
        //            customer = new customer();
        //            customer.CustomerId = System.Convert.ToInt32(reader["CustomerId"]);
        //            customerList.Add(customer);
        //        }
        //        reader.Close();
        //    }
        //    catch (SqlException)
        //    {
        //        return customerList;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    return customerList;
        //}

    }

}

 
