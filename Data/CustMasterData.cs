using System;
using System.Data;
using System.Data.SqlClient;
using MyProject.Models;

namespace MyProject.Data
{
    public class CustMasterData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[CustMasterSelectAll]";
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
            string selectProcedure = "[CustMasterSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Cust Id") {
                selectCommand.Parameters.AddWithValue("@CustId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CustId", DBNull.Value); }
            if (sField == "Cust Name") {
                selectCommand.Parameters.AddWithValue("@CustName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CustName", DBNull.Value); }
            if (sField == "Acronym") {
                selectCommand.Parameters.AddWithValue("@Acronym", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Acronym", DBNull.Value); }
            if (sField == "Noof Branches") {
                selectCommand.Parameters.AddWithValue("@NoofBranches", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@NoofBranches", DBNull.Value); }
            if (sField == "Levelof Authority") {
                selectCommand.Parameters.AddWithValue("@LevelofAuthority", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@LevelofAuthority", DBNull.Value); }
            if (sField == "Code")
            {
                selectCommand.Parameters.AddWithValue("@Code", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Code", DBNull.Value);
            }
            if (sField == "Ticker") {
                selectCommand.Parameters.AddWithValue("@Ticker", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Ticker", DBNull.Value); }
            if (sField == "In Demo") {
                selectCommand.Parameters.AddWithValue("@InDemo", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@InDemo", DBNull.Value); }
            if (sField == "Tiered Pricing") {
                selectCommand.Parameters.AddWithValue("@TieredPricing", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@TieredPricing", DBNull.Value); }
            if (sField == "Is Active") {
                selectCommand.Parameters.AddWithValue("@isActive", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@isActive", DBNull.Value); }
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

        public static CustMaster Select_Record(Int64 pCustId)
        {
            CustMaster CustMaster = new CustMaster();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[CustMasterSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@CustId", pCustId);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    CustMaster.CustId = System.Convert.ToInt64(reader["CustId"]);
                    CustMaster.CustName = reader["CustName"] is DBNull ? null : reader["CustName"].ToString();
                    CustMaster.Acronym = reader["Acronym"] is DBNull ? null : reader["Acronym"].ToString();
                    CustMaster.NoofBranches = reader["NoofBranches"] is DBNull ? null : (Int32?)reader["NoofBranches"];
                    CustMaster.LevelofAuthority = reader["LevelofAuthority"] is DBNull ? null : (Int32?)reader["LevelofAuthority"];
                    CustMaster.Code = reader["Code"] is DBNull ? null : reader["Code"].ToString();
                    CustMaster.Ticker = reader["Ticker"] is DBNull ? null : reader["Ticker"].ToString();
                    CustMaster.InDemo =  (Boolean)reader["InDemo"];
                    CustMaster.TieredPricing = (Boolean)reader["TieredPricing"];
                    CustMaster.isActive = (Boolean)reader["isActive"];
                }
                else
                {
                    CustMaster = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return CustMaster;
            }
            finally
            {
                connection.Close();
            }
            return CustMaster;
        }

        public static bool Add(CustMaster CustMaster)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string insertProcedure = "[CustMasterInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@CustId", CustMaster.CustId);
            if (CustMaster.CustName != null) {
                insertCommand.Parameters.AddWithValue("@CustName", CustMaster.CustName);
            } else {
                insertCommand.Parameters.AddWithValue("@CustName", DBNull.Value); }
            if (CustMaster.Acronym != null) {
                insertCommand.Parameters.AddWithValue("@Acronym", CustMaster.Acronym);
            } else {
                insertCommand.Parameters.AddWithValue("@Acronym", DBNull.Value); }
            if (CustMaster.NoofBranches.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@NoofBranches", CustMaster.NoofBranches);
            } else {
                insertCommand.Parameters.AddWithValue("@NoofBranches", DBNull.Value); }
            if (CustMaster.LevelofAuthority.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@LevelofAuthority", CustMaster.LevelofAuthority);
            } else {
                insertCommand.Parameters.AddWithValue("@LevelofAuthority", DBNull.Value); }
            if (CustMaster.Code != null)
            {
                insertCommand.Parameters.AddWithValue("@Code", CustMaster.Ticker);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Code", DBNull.Value);
            }

            if (CustMaster.Ticker != null) {
                insertCommand.Parameters.AddWithValue("@Ticker", CustMaster.Ticker);
            } else {
                insertCommand.Parameters.AddWithValue("@Ticker", DBNull.Value); }
            if (CustMaster.InDemo == true) {
                insertCommand.Parameters.AddWithValue("@InDemo", 1);
            } else {
                insertCommand.Parameters.AddWithValue("@InDemo",0); }
            if (CustMaster.TieredPricing == true) {
                insertCommand.Parameters.AddWithValue("@TieredPricing", 1);
            } else {
                insertCommand.Parameters.AddWithValue("@TieredPricing", 0); }
            if (CustMaster.isActive == true) {
                insertCommand.Parameters.AddWithValue("@isActive", 1);
            } else {
                insertCommand.Parameters.AddWithValue("@isActive", 1); }
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

        public static bool Update(CustMaster oldCustMaster, 
               CustMaster newCustMaster)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string updateProcedure = "[CustMasterUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewCustId", newCustMaster.CustId);
            if (newCustMaster.CustName != null) {
                updateCommand.Parameters.AddWithValue("@NewCustName", newCustMaster.CustName);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCustName", DBNull.Value); }
            if (newCustMaster.Acronym != null) {
                updateCommand.Parameters.AddWithValue("@NewAcronym", newCustMaster.Acronym);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAcronym", DBNull.Value); }
            if (newCustMaster.NoofBranches.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewNoofBranches", newCustMaster.NoofBranches);
            } else {
                updateCommand.Parameters.AddWithValue("@NewNoofBranches", DBNull.Value); }
            if (newCustMaster.LevelofAuthority.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewLevelofAuthority", newCustMaster.LevelofAuthority);
            } else {
                updateCommand.Parameters.AddWithValue("@NewLevelofAuthority", DBNull.Value); }
            if (newCustMaster.Code != null)
            {
                updateCommand.Parameters.AddWithValue("@NewCode", newCustMaster.Code);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewCode", DBNull.Value);
            }
            if (newCustMaster.Ticker != null) {
                updateCommand.Parameters.AddWithValue("@NewTicker", newCustMaster.Ticker);
            } else {
                updateCommand.Parameters.AddWithValue("@NewTicker", DBNull.Value); }
            if (newCustMaster.InDemo == true) {
                updateCommand.Parameters.AddWithValue("@NewInDemo", 1);
            } else {
                updateCommand.Parameters.AddWithValue("@NewInDemo", 0); }
            if (newCustMaster.TieredPricing == true) {
                updateCommand.Parameters.AddWithValue("@NewTieredPricing", 1);
            } else {
                updateCommand.Parameters.AddWithValue("@NewTieredPricing", 0); }
            if (newCustMaster.isActive == true) {
                updateCommand.Parameters.AddWithValue("@NewisActive", 1);
            } else {
                updateCommand.Parameters.AddWithValue("@NewisActive", 0); }
            updateCommand.Parameters.AddWithValue("@OldCustId", oldCustMaster.CustId);
            if (oldCustMaster.CustName != null) {
                updateCommand.Parameters.AddWithValue("@OldCustName", oldCustMaster.CustName);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCustName", DBNull.Value); }
            if (oldCustMaster.Acronym != null) {
                updateCommand.Parameters.AddWithValue("@OldAcronym", oldCustMaster.Acronym);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAcronym", DBNull.Value); }
            if (oldCustMaster.NoofBranches.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldNoofBranches", oldCustMaster.NoofBranches);
            } else {
                updateCommand.Parameters.AddWithValue("@OldNoofBranches", DBNull.Value); }
            if (oldCustMaster.LevelofAuthority.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldLevelofAuthority", oldCustMaster.LevelofAuthority);
            } else {
                updateCommand.Parameters.AddWithValue("@OldLevelofAuthority", DBNull.Value); }
            if (oldCustMaster.Code != null)
            {
                updateCommand.Parameters.AddWithValue("@OldCode", oldCustMaster.Code);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldCode", DBNull.Value);
            }

            if (oldCustMaster.Ticker != null) {
                updateCommand.Parameters.AddWithValue("@OldTicker", oldCustMaster.Ticker);
            } else {
                updateCommand.Parameters.AddWithValue("@OldTicker", DBNull.Value); }
            if (oldCustMaster.InDemo == true) {
                updateCommand.Parameters.AddWithValue("@OldInDemo", oldCustMaster.InDemo);
            } else {
                updateCommand.Parameters.AddWithValue("@OldInDemo", DBNull.Value); }
            if (oldCustMaster.TieredPricing == true) {
                updateCommand.Parameters.AddWithValue("@OldTieredPricing", oldCustMaster.TieredPricing);
            } else {
                updateCommand.Parameters.AddWithValue("@OldTieredPricing", DBNull.Value); }
            if (oldCustMaster.isActive == true) {
                updateCommand.Parameters.AddWithValue("@OldisActive", oldCustMaster.isActive);
            } else {
                updateCommand.Parameters.AddWithValue("@OldisActive", DBNull.Value); }
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

        public static bool Delete(CustMaster CustMaster)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string deleteProcedure = "[CustMasterDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldCustId", CustMaster.CustId);
            if (CustMaster.CustName != null) {
                deleteCommand.Parameters.AddWithValue("@OldCustName", CustMaster.CustName);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCustName", DBNull.Value); }
            if (CustMaster.Acronym != null) {
                deleteCommand.Parameters.AddWithValue("@OldAcronym", CustMaster.Acronym);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAcronym", DBNull.Value); }
            if (CustMaster.NoofBranches.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldNoofBranches", CustMaster.NoofBranches);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldNoofBranches", DBNull.Value); }
            if (CustMaster.LevelofAuthority.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldLevelofAuthority", CustMaster.LevelofAuthority);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldLevelofAuthority", DBNull.Value); }
            if (CustMaster.Code != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldCode", CustMaster.Code);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldCode", DBNull.Value);
            }
            if (CustMaster.Ticker != null) {
                deleteCommand.Parameters.AddWithValue("@OldTicker", CustMaster.Ticker);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldTicker", DBNull.Value); }
            if (CustMaster.InDemo == true) {
                deleteCommand.Parameters.AddWithValue("@OldInDemo", CustMaster.InDemo);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldInDemo", DBNull.Value); }
            if (CustMaster.TieredPricing == true) {
                deleteCommand.Parameters.AddWithValue("@OldTieredPricing", CustMaster.TieredPricing);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldTieredPricing", DBNull.Value); }
            if (CustMaster.isActive == true) {
                deleteCommand.Parameters.AddWithValue("@OldisActive", CustMaster.isActive);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldisActive", DBNull.Value); }
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


        public static DataTable CustomerSelect_All()
        {
            Customers clsCustMaster = new Customers();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[CustMasterSelectAll]";
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

        public static DataTable CountrySelect_All()
        {
            Customers clsCustMaster = new Customers();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[CountrySelectAll]";
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

    }
}
 
