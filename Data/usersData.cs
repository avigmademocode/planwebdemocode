using System;
using System.Data;
using System.Data.SqlClient;
using MyProject.Models;
using System.Collections.Generic;

namespace MyProject.Data
{
    public class usersData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[UserMasterSelectAll]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            List<UserMaster> lst = new List<UserMaster>();
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

        public static List<UserMaster> SelectAllUsers()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[UserMasterSelectAll]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            List<UserMaster> lst = new List<UserMaster>();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //dt.Load(reader);
                        UserMaster obj = new UserMaster();
                        //lst.Add(new UserMaster
                        //{
                        obj.UserId = Convert.ToInt64(reader["UserId"]);
                        obj.LoginId = Convert.ToString(reader["LoginId"]);
                        obj.Pwd = Convert.ToString(reader["Pwd"]);
                        obj.UserName = Convert.ToString(reader["UserName"]);
                        obj.CustId = Convert.ToInt64(reader["CustId"]);
                        obj.IsPlansonUser = Convert.ToBoolean(reader["IsPlansonUser"]);
                        obj.FirstName = Convert.ToString(reader["FirstName"]);
                        obj.LastName = Convert.ToString(reader["LastName"]);
                        obj.CountryId = Convert.ToInt64(reader["CountryId"]);
                        obj.Logins = Convert.ToString(reader["Logins"]);
                        obj.Last_Login = Convert.ToString(reader["Last_Login"]);
                        obj.Locked = Convert.ToBoolean(reader["Locked"]);
                        obj.IsActive = Convert.ToBoolean(reader["IsActive"]);
                        if (DBNull.Value!= reader["CreatedOn"])// !reader.IsDBNull(reader["CreatedOn"]))
                            obj.CreatedOn = Convert.ToDateTime(reader["CreatedOn"]);
                        if (DBNull.Value != reader["CreatedBy"])
                            obj.CreatedBy = Convert.ToInt64(reader["CreatedBy"]);
                        if (DBNull.Value != reader["ModifiedOn"])
                            obj.ModifiedOn = Convert.ToDateTime(reader["ModifiedOn"]);
                        if (DBNull.Value != reader["ModifiedBy"])
                            obj.ModifiedBy = Convert.ToInt64(reader["ModifiedBy"]);
                        if (DBNull.Value != reader["DeletedOn"])
                            obj.DeletedOn = Convert.ToDateTime(reader["DeletedOn"]);
                        if (DBNull.Value != reader["DeletedBy"])
                            obj.DeletedBy = Convert.ToInt64(reader["DeletedBy"]);

                        lst.Add(obj);
                           
                        
                    }
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return lst;
            }
            finally
            {
                connection.Close();
            }
            return lst;
        }

        public static DataTable Search(string sField, string sCondition, string sValue)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[UserMasterSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Id")
            {
                selectCommand.Parameters.AddWithValue("@id", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@id", DBNull.Value);
            }
            if (sField == "Email")
            {
                selectCommand.Parameters.AddWithValue("@email", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@email", DBNull.Value);
            }
            if (sField == "Username")
            {
                selectCommand.Parameters.AddWithValue("@username", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@username", DBNull.Value);
            }
            if (sField == "Password")
            {
                selectCommand.Parameters.AddWithValue("@password", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@password", DBNull.Value);
            }
            if (sField == "Logins")
            {
                selectCommand.Parameters.AddWithValue("@logins", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@logins", DBNull.Value);
            }
            if (sField == "Last Login")
            {
                selectCommand.Parameters.AddWithValue("@last_login", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@last_login", DBNull.Value);
            }
            if (sField == "Customer Id")
            {
                selectCommand.Parameters.AddWithValue("@CustomerId", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@CustomerId", DBNull.Value);
            }
            if (sField == "Field Office Id")
            {
                selectCommand.Parameters.AddWithValue("@FieldOfficeId", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@FieldOfficeId", DBNull.Value);
            }
            if (sField == "First Name")
            {
                selectCommand.Parameters.AddWithValue("@FirstName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@FirstName", DBNull.Value);
            }
            if (sField == "Last Name")
            {
                selectCommand.Parameters.AddWithValue("@LastName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@LastName", DBNull.Value);
            }
            if (sField == "City")
            {
                selectCommand.Parameters.AddWithValue("@CountryId", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@CountryId", DBNull.Value);
            }
            if (sField == "Country Id")
            {
                selectCommand.Parameters.AddWithValue("@Name", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Name", DBNull.Value);
            }
            selectCommand.Parameters.AddWithValue("@SearchCondition", sCondition);
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

        public static users Select_Record(users usersPara)
        {
            users users = new users();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[usersSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@id", usersPara.id);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    users.id = System.Convert.ToInt32(reader["id"]);
                    users.email = System.Convert.ToString(reader["email"]);
                    users.username = System.Convert.ToString(reader["username"]);
                    users.password = System.Convert.ToString(reader["password"]);
                    users.logins = System.Convert.ToInt32(reader["logins"]);
                    users.last_login = reader["last_login"] is DBNull ? null : (Int32?)reader["last_login"];
                    users.CustomerId = reader["CustomerId"] is DBNull ? null : (Int32?)reader["CustomerId"];
                    users.FieldOfficeId = reader["FieldOfficeId"] is DBNull ? null : (Int32?)reader["FieldOfficeId"];
                    users.FirstName = System.Convert.ToString(reader["FirstName"]);
                    users.LastName = System.Convert.ToString(reader["LastName"]);
                    users.City = System.Convert.ToString(reader["City"]);
                    users.CountryId = System.Convert.ToInt32(reader["CountryId"]);
                }
                else
                {
                    users = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return users;
            }
            finally
            {
                connection.Close();
            }
            return users;
        }

        public static bool Add(users users)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string insertProcedure = "[usersInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@id", users.id);
            insertCommand.Parameters.AddWithValue("@email", users.email);
            insertCommand.Parameters.AddWithValue("@username", users.username);
            insertCommand.Parameters.AddWithValue("@password", users.password);
            insertCommand.Parameters.AddWithValue("@logins", users.logins);
            if (users.last_login.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@last_login", users.last_login);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@last_login", DBNull.Value);
            }
            if (users.CustomerId.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@CustomerId", users.CustomerId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustomerId", DBNull.Value);
            }
            if (users.FieldOfficeId.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@FieldOfficeId", users.FieldOfficeId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@FieldOfficeId", DBNull.Value);
            }
            insertCommand.Parameters.AddWithValue("@FirstName", users.FirstName);
            insertCommand.Parameters.AddWithValue("@LastName", users.LastName);
            insertCommand.Parameters.AddWithValue("@City", users.City);
            insertCommand.Parameters.AddWithValue("@CountryId", users.CountryId);
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

        public static bool Update(users oldusers,
               users newusers)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string updateProcedure = "[usersUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@Newid", newusers.id);
            updateCommand.Parameters.AddWithValue("@Newemail", newusers.email);
            updateCommand.Parameters.AddWithValue("@Newusername", newusers.username);
            updateCommand.Parameters.AddWithValue("@Newpassword", newusers.password);
            updateCommand.Parameters.AddWithValue("@Newlogins", newusers.logins);
            if (newusers.last_login.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@Newlast_login", newusers.last_login);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Newlast_login", DBNull.Value);
            }
            if (newusers.CustomerId.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewCustomerId", newusers.CustomerId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewCustomerId", DBNull.Value);
            }
            if (newusers.FieldOfficeId.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewFieldOfficeId", newusers.FieldOfficeId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewFieldOfficeId", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@NewFirstName", newusers.FirstName);
            updateCommand.Parameters.AddWithValue("@NewLastName", newusers.LastName);
            updateCommand.Parameters.AddWithValue("@NewCity", newusers.City);
            updateCommand.Parameters.AddWithValue("@NewCountryId", newusers.CountryId);
            updateCommand.Parameters.AddWithValue("@Oldid", oldusers.id);
            updateCommand.Parameters.AddWithValue("@Oldemail", oldusers.email);
            updateCommand.Parameters.AddWithValue("@Oldusername", oldusers.username);
            updateCommand.Parameters.AddWithValue("@Oldpassword", oldusers.password);
            updateCommand.Parameters.AddWithValue("@Oldlogins", oldusers.logins);
            if (oldusers.last_login.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@Oldlast_login", oldusers.last_login);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Oldlast_login", DBNull.Value);
            }
            if (oldusers.CustomerId.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldCustomerId", oldusers.CustomerId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldCustomerId", DBNull.Value);
            }
            if (oldusers.FieldOfficeId.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldFieldOfficeId", oldusers.FieldOfficeId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldFieldOfficeId", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@OldFirstName", oldusers.FirstName);
            updateCommand.Parameters.AddWithValue("@OldLastName", oldusers.LastName);
            updateCommand.Parameters.AddWithValue("@OldCity", oldusers.City);
            updateCommand.Parameters.AddWithValue("@OldCountryId", oldusers.CountryId);
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

        public static bool Delete(users users)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string deleteProcedure = "[usersDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@Oldid", users.id);
            deleteCommand.Parameters.AddWithValue("@Oldemail", users.email);
            deleteCommand.Parameters.AddWithValue("@Oldusername", users.username);
            deleteCommand.Parameters.AddWithValue("@Oldpassword", users.password);
            deleteCommand.Parameters.AddWithValue("@Oldlogins", users.logins);
            if (users.last_login.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@Oldlast_login", users.last_login);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@Oldlast_login", DBNull.Value);
            }
            if (users.CustomerId.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldCustomerId", users.CustomerId);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldCustomerId", DBNull.Value);
            }
            if (users.FieldOfficeId.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldFieldOfficeId", users.FieldOfficeId);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldFieldOfficeId", DBNull.Value);
            }
            deleteCommand.Parameters.AddWithValue("@OldFirstName", users.FirstName);
            deleteCommand.Parameters.AddWithValue("@OldLastName", users.LastName);
            deleteCommand.Parameters.AddWithValue("@OldCity", users.City);
            deleteCommand.Parameters.AddWithValue("@OldCountryId", users.CountryId);
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

        public static bool UserNewInsert(users objNewUser, decimal keyvalidhr)
        {
            //string firstName, string lastName, string city, string country, string email, string autokey, string password
            //Customers clsCustMaster = new Customers();

            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string insertProcedure = "[UserRegisterationInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            //insertCommand.Parameters.AddWithValue("@id", users.id);
            insertCommand.Parameters.AddWithValue("@firstName", objNewUser.FirstName);
            insertCommand.Parameters.AddWithValue("@lastName", objNewUser.LastName);
            insertCommand.Parameters.AddWithValue("@city", objNewUser.City);
            insertCommand.Parameters.AddWithValue("@CountryId", objNewUser.Country);
            insertCommand.Parameters.AddWithValue("@email", objNewUser.email);
            insertCommand.Parameters.AddWithValue("@autokey", objNewUser.Key);
            insertCommand.Parameters.AddWithValue("@password", objNewUser.password);
            insertCommand.Parameters.AddWithValue("@keyvalidhr", keyvalidhr);

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


    }
}

