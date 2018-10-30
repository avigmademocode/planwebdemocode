using System;
using System.Data;
using System.Data.SqlClient;
using MyProject.Models;

namespace MyProject.Data
{
    public class countriesData
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

        public static DataTable Search(string sField, string sCondition, string sValue)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[countriesSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Id") {
                selectCommand.Parameters.AddWithValue("@id", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@id", DBNull.Value); }
            if (sField == "Name") {
                selectCommand.Parameters.AddWithValue("@Name", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Name", DBNull.Value); }
            if (sField == "Is Active") {
                selectCommand.Parameters.AddWithValue("@IsActive", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@IsActive", DBNull.Value); }
            selectCommand.Parameters.AddWithValue("@SearchCondition", sCondition);
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

        public static countries Select_Record(countries countriesPara)
        {
            countries countries = new countries();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[countriesSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@id", countriesPara.id);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    countries.id = System.Convert.ToInt32(reader["id"]);
                    countries.Name = System.Convert.ToString(reader["Name"]);
                    countries.IsActive = System.Convert.ToBoolean(reader["IsActive"]);
                }
                else
                {
                    countries = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return countries;
            }
            finally
            {
                connection.Close();
            }
            return countries;
        }

        public static bool Add(countries countries)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string insertProcedure = "[countriesInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@id", countries.id);
            insertCommand.Parameters.AddWithValue("@Name", countries.Name);
            insertCommand.Parameters.AddWithValue("@IsActive", countries.IsActive);
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
        }

        public static bool Update(countries oldcountries, 
               countries newcountries)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string updateProcedure = "[countriesUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@Newid", newcountries.id);
            updateCommand.Parameters.AddWithValue("@NewName", newcountries.Name);
            updateCommand.Parameters.AddWithValue("@NewIsActive", newcountries.IsActive);
            updateCommand.Parameters.AddWithValue("@Oldid", oldcountries.id);
            updateCommand.Parameters.AddWithValue("@OldName", oldcountries.Name);
            updateCommand.Parameters.AddWithValue("@OldIsActive", oldcountries.IsActive);
            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                updateCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
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
        }

        public static bool Delete(countries countries)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string deleteProcedure = "[countriesDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@Oldid", countries.id);
            deleteCommand.Parameters.AddWithValue("@OldName", countries.Name);
            deleteCommand.Parameters.AddWithValue("@OldIsActive", countries.IsActive);
            deleteCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            deleteCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                deleteCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(deleteCommand.Parameters["@ReturnValue"].Value);
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
        }
    }
}
 
