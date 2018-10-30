using System;
using System.Data;
using System.Data.SqlClient;
using MyProject.Models;

namespace MyProject.Data
{
    public class loginData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[countriesSelectAll]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
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

        public static UserDetails Select_Record(UserDetails userDetpara)
        {
            UserDetails UserDet= new UserDetails();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[loginSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Username", userDetpara.Username);
            selectCommand.Parameters.AddWithValue("@password", userDetpara.password);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    UserDet.id = System.Convert.ToInt32(reader["id"]);
                    UserDet.Username = System.Convert.ToString(reader["Username"]);
                    UserDet.email = System.Convert.ToString(reader["email"]);
                }
                else
                {
                    UserDet = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return UserDet;
            }
            finally
            {
                connection.Close();
            }
            return UserDet;
        }

        public static Intialpage Select_Text()
        {
            Intialpage pageObj = new Intialpage();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[LoginSelectImgText]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
           
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    pageObj.textPart = System.Convert.ToString(reader["TextPart"]);
                    pageObj.imagePath = System.Convert.ToString(reader["ImagePath"]);
                }
                else
                {
                    pageObj = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return pageObj;
            }
            finally
            {
                connection.Close();
            }
            return pageObj;
        }

        public static bool CustomerKeyInsert(string customerCode, string emailKey,string strCustomerKey)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string insertProcedure = "[CustomerKeyInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.AddWithValue("@ForCustomer", customerCode);
            insertCommand.Parameters.AddWithValue("@email", emailKey);
            insertCommand.Parameters.AddWithValue("@CustomerKey", strCustomerKey);
            

            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }
    }
}