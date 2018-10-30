using System;
using System.Data;
using System.Data.SqlClient;
using MyProject.Models;

namespace MyProject.Data
{
    public class rolesData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[rolesSelectAll]";
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
            string selectProcedure = "[rolesSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Id") {
                selectCommand.Parameters.AddWithValue("@id", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@id", DBNull.Value); }
            if (sField == "Name") {
                selectCommand.Parameters.AddWithValue("@name", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@name", DBNull.Value); }
            if (sField == "Description") {
                selectCommand.Parameters.AddWithValue("@description", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@description", DBNull.Value); }
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

        public static roles Select_Record(roles rolesPara)
        {
            roles roles = new roles();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[rolesSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@id", rolesPara.id);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    roles.id = System.Convert.ToInt32(reader["id"]);
                    roles.name = System.Convert.ToString(reader["name"]);
                    roles.description = System.Convert.ToString(reader["description"]);
                }
                else
                {
                    roles = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return roles;
            }
            finally
            {
                connection.Close();
            }
            return roles;
        }

        public static bool Add(roles roles)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string insertProcedure = "[rolesInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@id", roles.id);
            insertCommand.Parameters.AddWithValue("@name", roles.name);
            insertCommand.Parameters.AddWithValue("@description", roles.description);
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

        public static bool Update(roles oldroles, 
               roles newroles)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string updateProcedure = "[rolesUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@Newid", newroles.id);
            updateCommand.Parameters.AddWithValue("@Newname", newroles.name);
            updateCommand.Parameters.AddWithValue("@Newdescription", newroles.description);
            updateCommand.Parameters.AddWithValue("@Oldid", oldroles.id);
            updateCommand.Parameters.AddWithValue("@Oldname", oldroles.name);
            updateCommand.Parameters.AddWithValue("@Olddescription", oldroles.description);
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

        public static bool Delete(roles roles)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string deleteProcedure = "[rolesDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@Oldid", roles.id);
            deleteCommand.Parameters.AddWithValue("@Oldname", roles.name);
            deleteCommand.Parameters.AddWithValue("@Olddescription", roles.description);
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
 
