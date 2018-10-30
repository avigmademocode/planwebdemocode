using System;
using System.Data;
using System.Data.SqlClient;
using MyProject.Models;

namespace MyProject.Data
{
    public class MenuData
    {
        public static DataTable MenuSelectAll()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[MenuSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            //selectCommand.Parameters.AddWithValue("@MenuId", clsMenuPara.MenuId);
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
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable MenuSelectForRole(Int64 roleId)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[MenuSelectForRole]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@roleId", roleId);
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
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static bool Add(Menu clsMenu)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string insertProcedure = "[MenuInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@MenuId", clsMenu.MenuId);
            if (clsMenu.MenuName != null)
            {
                insertCommand.Parameters.AddWithValue("@MenuName", clsMenu.MenuName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@MenuName", DBNull.Value);
            }
            insertCommand.Parameters.AddWithValue("@RoleId", clsMenu.RoleId);
            if (clsMenu.IsActive.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", clsMenu.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", DBNull.Value);
            }
            if (clsMenu.CreatedOn.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@CreatedOn", clsMenu.CreatedOn);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CreatedOn", DBNull.Value);
            }
            if (clsMenu.CreatedBy.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@CreatedBy", clsMenu.CreatedBy);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
            }
            if (clsMenu.ModifiedOn.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@ModifiedOn", clsMenu.ModifiedOn);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ModifiedOn", DBNull.Value);
            }
            if (clsMenu.ModifiedBy.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@ModifiedBy", clsMenu.ModifiedBy);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ModifiedBy", DBNull.Value);
            }
            if (clsMenu.DeletedOn.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@DeletedOn", clsMenu.DeletedOn);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@DeletedOn", DBNull.Value);
            }
            if (clsMenu.DeletedBy.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@DeletedBy", clsMenu.DeletedBy);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@DeletedBy", DBNull.Value);
            }
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
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Update(Menu oldMenu,
               Menu newMenu)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string updateProcedure = "[MenuUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewMenuId", newMenu.MenuId);
            if (newMenu.MenuName != null)
            {
                updateCommand.Parameters.AddWithValue("@NewMenuName", newMenu.MenuName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewMenuName", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@NewRoleId", newMenu.RoleId);
            if (newMenu.IsActive.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewIsActive", newMenu.IsActive);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewIsActive", DBNull.Value);
            }
            if (newMenu.CreatedOn.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewCreatedOn", newMenu.CreatedOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewCreatedOn", DBNull.Value);
            }
            if (newMenu.CreatedBy.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewCreatedBy", newMenu.CreatedBy);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewCreatedBy", DBNull.Value);
            }
            if (newMenu.ModifiedOn.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewModifiedOn", newMenu.ModifiedOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewModifiedOn", DBNull.Value);
            }
            if (newMenu.ModifiedBy.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewModifiedBy", newMenu.ModifiedBy);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewModifiedBy", DBNull.Value);
            }
            if (newMenu.DeletedOn.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewDeletedOn", newMenu.DeletedOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewDeletedOn", DBNull.Value);
            }
            if (newMenu.DeletedBy.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewDeletedBy", newMenu.DeletedBy);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewDeletedBy", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@OldMenuId", oldMenu.MenuId);
            if (oldMenu.MenuName != null)
            {
                updateCommand.Parameters.AddWithValue("@OldMenuName", oldMenu.MenuName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldMenuName", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@OldRoleId", oldMenu.RoleId);
            if (oldMenu.IsActive.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldIsActive", oldMenu.IsActive);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldIsActive", DBNull.Value);
            }
            if (oldMenu.CreatedOn.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldCreatedOn", oldMenu.CreatedOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldCreatedOn", DBNull.Value);
            }
            if (oldMenu.CreatedBy.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldCreatedBy", oldMenu.CreatedBy);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldCreatedBy", DBNull.Value);
            }
            if (oldMenu.ModifiedOn.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldModifiedOn", oldMenu.ModifiedOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldModifiedOn", DBNull.Value);
            }
            if (oldMenu.ModifiedBy.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldModifiedBy", oldMenu.ModifiedBy);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldModifiedBy", DBNull.Value);
            }
            if (oldMenu.DeletedOn.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldDeletedOn", oldMenu.DeletedOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldDeletedOn", DBNull.Value);
            }
            if (oldMenu.DeletedBy.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldDeletedBy", oldMenu.DeletedBy);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldDeletedBy", DBNull.Value);
            }
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
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Delete(Menu clsMenu)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string deleteProcedure = "[MenuDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldMenuId", clsMenu.MenuId);
            if (clsMenu.MenuName != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldMenuName", clsMenu.MenuName);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldMenuName", DBNull.Value);
            }
            deleteCommand.Parameters.AddWithValue("@OldRoleId", clsMenu.RoleId);
            if (clsMenu.IsActive.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldIsActive", clsMenu.IsActive);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldIsActive", DBNull.Value);
            }
            if (clsMenu.CreatedOn.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldCreatedOn", clsMenu.CreatedOn);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldCreatedOn", DBNull.Value);
            }
            if (clsMenu.CreatedBy.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldCreatedBy", clsMenu.CreatedBy);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldCreatedBy", DBNull.Value);
            }
            if (clsMenu.ModifiedOn.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldModifiedOn", clsMenu.ModifiedOn);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldModifiedOn", DBNull.Value);
            }
            if (clsMenu.ModifiedBy.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldModifiedBy", clsMenu.ModifiedBy);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldModifiedBy", DBNull.Value);
            }
            if (clsMenu.DeletedOn.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldDeletedOn", clsMenu.DeletedOn);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldDeletedOn", DBNull.Value);
            }
            if (clsMenu.DeletedBy.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldDeletedBy", clsMenu.DeletedBy);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldDeletedBy", DBNull.Value);
            }
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
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}