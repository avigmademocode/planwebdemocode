using System;
using System.Data;
using System.Data.SqlClient;
using MyProject.Models;

namespace MyProject.Data
{
    public class UserMasterData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[UserMasterSelectAll]";
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
            string selectProcedure = "[UserMasterSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "User Id") {
                selectCommand.Parameters.AddWithValue("@UserId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@UserId", DBNull.Value); }
            if (sField == "Login Id") {
                selectCommand.Parameters.AddWithValue("@LoginId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@LoginId", DBNull.Value); }
            if (sField == "Pwd") {
                selectCommand.Parameters.AddWithValue("@Pwd", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Pwd", DBNull.Value); }
            if (sField == "User Name") {
                selectCommand.Parameters.AddWithValue("@UserName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@UserName", DBNull.Value); }
            if (sField == "Is Planson User") {
                selectCommand.Parameters.AddWithValue("@IsPlansonUser", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@IsPlansonUser", DBNull.Value); }
            if (sField == "First Name") {
                selectCommand.Parameters.AddWithValue("@FirstName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@FirstName", DBNull.Value); }
            if (sField == "Last Name") {
                selectCommand.Parameters.AddWithValue("@LastName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@LastName", DBNull.Value); }
            if (sField == "Locked") {
                selectCommand.Parameters.AddWithValue("@Locked", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Locked", DBNull.Value); }
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

        public static UserMaster Select_Record(UserMaster UserMasterPara)
        {
            UserMaster UserMaster = new UserMaster();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[UserMasterSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@UserId", UserMasterPara.UserId);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    UserMaster.UserId = System.Convert.ToInt32(reader["UserId"]);
                    UserMaster.LoginId = System.Convert.ToString(reader["LoginId"]);
                    UserMaster.Pwd = System.Convert.ToString(reader["Pwd"]);
                    UserMaster.UserName = reader["UserName"] is DBNull ? null : reader["UserName"].ToString();
                    UserMaster.IsPlansonUser = (Boolean)reader["IsPlansonUser"];
                    UserMaster.FirstName = reader["FirstName"] is DBNull ? null : reader["FirstName"].ToString();
                    UserMaster.LastName = reader["LastName"] is DBNull ? null : reader["LastName"].ToString();
                    UserMaster.Locked = (Boolean)reader["Locked"];
                    UserMaster.IsActive =(Boolean)reader["IsActive"];
                }
                else
                {
                    UserMaster = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return UserMaster;
            }
            finally
            {
                connection.Close();
            }
            return UserMaster;
        }

        public static bool Add(UserMaster UserMaster)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string insertProcedure = "[UserMasterInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@UserId", UserMaster.UserId);
            insertCommand.Parameters.AddWithValue("@LoginId", UserMaster.LoginId);
            insertCommand.Parameters.AddWithValue("@Pwd", UserMaster.Pwd);
            if (UserMaster.UserName != null) {
                insertCommand.Parameters.AddWithValue("@UserName", UserMaster.UserName);
            } else {
                insertCommand.Parameters.AddWithValue("@UserName", DBNull.Value); }
            if (UserMaster.IsPlansonUser == true) {
                insertCommand.Parameters.AddWithValue("@IsPlansonUser", UserMaster.IsPlansonUser);
            } else {
                insertCommand.Parameters.AddWithValue("@IsPlansonUser", DBNull.Value); }
            if (UserMaster.FirstName != null) {
                insertCommand.Parameters.AddWithValue("@FirstName", UserMaster.FirstName);
            } else {
                insertCommand.Parameters.AddWithValue("@FirstName", DBNull.Value); }
            if (UserMaster.LastName != null) {
                insertCommand.Parameters.AddWithValue("@LastName", UserMaster.LastName);
            } else {
                insertCommand.Parameters.AddWithValue("@LastName", DBNull.Value); }
            if (UserMaster.Locked== true) {
                insertCommand.Parameters.AddWithValue("@Locked", UserMaster.Locked);
            } else {
                insertCommand.Parameters.AddWithValue("@Locked", DBNull.Value); }
            if (UserMaster.IsActive == true) {
                insertCommand.Parameters.AddWithValue("@IsActive", UserMaster.IsActive);
            } else {
                insertCommand.Parameters.AddWithValue("@IsActive", DBNull.Value); }
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

        public static bool Update(UserMaster oldUserMaster, 
               UserMaster newUserMaster)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string updateProcedure = "[UserMasterUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewUserId", newUserMaster.UserId);
            updateCommand.Parameters.AddWithValue("@NewLoginId", newUserMaster.LoginId);
            updateCommand.Parameters.AddWithValue("@NewPwd", newUserMaster.Pwd);
            if (newUserMaster.UserName != null) {
                updateCommand.Parameters.AddWithValue("@NewUserName", newUserMaster.UserName);
            } else {
                updateCommand.Parameters.AddWithValue("@NewUserName", DBNull.Value); }
            if (newUserMaster.IsPlansonUser == true) {
                updateCommand.Parameters.AddWithValue("@NewIsPlansonUser", newUserMaster.IsPlansonUser);
            } else {
                updateCommand.Parameters.AddWithValue("@NewIsPlansonUser", DBNull.Value); }
            if (newUserMaster.FirstName != null) {
                updateCommand.Parameters.AddWithValue("@NewFirstName", newUserMaster.FirstName);
            } else {
                updateCommand.Parameters.AddWithValue("@NewFirstName", DBNull.Value); }
            if (newUserMaster.LastName != null) {
                updateCommand.Parameters.AddWithValue("@NewLastName", newUserMaster.LastName);
            } else {
                updateCommand.Parameters.AddWithValue("@NewLastName", 0); }
            if (newUserMaster.Locked == true) {
                updateCommand.Parameters.AddWithValue("@NewLocked", 1);
            } else {
                updateCommand.Parameters.AddWithValue("@NewLocked", 0); }
            if (newUserMaster.IsActive == true) {
                updateCommand.Parameters.AddWithValue("@NewIsActive", 1);
            } else {
                updateCommand.Parameters.AddWithValue("@NewIsActive", 0); }
            updateCommand.Parameters.AddWithValue("@OldUserId", oldUserMaster.UserId);
            updateCommand.Parameters.AddWithValue("@OldLoginId", oldUserMaster.LoginId);
            updateCommand.Parameters.AddWithValue("@OldPwd", oldUserMaster.Pwd);
            if (oldUserMaster.UserName != null) {
                updateCommand.Parameters.AddWithValue("@OldUserName", oldUserMaster.UserName);
            } else {
                updateCommand.Parameters.AddWithValue("@OldUserName", DBNull.Value); }
            if (oldUserMaster.IsPlansonUser == true) {
                updateCommand.Parameters.AddWithValue("@OldIsPlansonUser", oldUserMaster.IsPlansonUser);
            } else {
                updateCommand.Parameters.AddWithValue("@OldIsPlansonUser", DBNull.Value); }
            if (oldUserMaster.FirstName != null) {
                updateCommand.Parameters.AddWithValue("@OldFirstName", oldUserMaster.FirstName);
            } else {
                updateCommand.Parameters.AddWithValue("@OldFirstName", DBNull.Value); }
            if (oldUserMaster.LastName != null) {
                updateCommand.Parameters.AddWithValue("@OldLastName", oldUserMaster.LastName);
            } else {
                updateCommand.Parameters.AddWithValue("@OldLastName", DBNull.Value); }
            if (oldUserMaster.Locked == true) {
                updateCommand.Parameters.AddWithValue("@OldLocked", 1);
            } else {
                updateCommand.Parameters.AddWithValue("@OldLocked", 0); }
            if (oldUserMaster.IsActive == true) {
                updateCommand.Parameters.AddWithValue("@OldIsActive",1);
            } else {
                updateCommand.Parameters.AddWithValue("@OldIsActive", 0); }
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

        public static bool Delete(UserMaster UserMaster)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string deleteProcedure = "[UserMasterDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldUserId", UserMaster.UserId);
            deleteCommand.Parameters.AddWithValue("@OldLoginId", UserMaster.LoginId);
            deleteCommand.Parameters.AddWithValue("@OldPwd", UserMaster.Pwd);
            if (UserMaster.UserName != null) {
                deleteCommand.Parameters.AddWithValue("@OldUserName", UserMaster.UserName);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldUserName", DBNull.Value); }
            if (UserMaster.IsPlansonUser == true) {
                deleteCommand.Parameters.AddWithValue("@OldIsPlansonUser", UserMaster.IsPlansonUser);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldIsPlansonUser", DBNull.Value); }
            if (UserMaster.FirstName != null) {
                deleteCommand.Parameters.AddWithValue("@OldFirstName", UserMaster.FirstName);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldFirstName", DBNull.Value); }
            if (UserMaster.LastName != null) {
                deleteCommand.Parameters.AddWithValue("@OldLastName", UserMaster.LastName);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldLastName", DBNull.Value); }
            if (UserMaster.Locked == true) {
                deleteCommand.Parameters.AddWithValue("@OldLocked", UserMaster.Locked);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldLocked", DBNull.Value); }
            if (UserMaster.IsActive == true) {
                deleteCommand.Parameters.AddWithValue("@OldIsActive", UserMaster.IsActive);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldIsActive", DBNull.Value); }
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
 
