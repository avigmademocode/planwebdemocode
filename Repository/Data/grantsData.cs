using System;
using System.Data;
using System.Data.SqlClient;
using MyProject.Models;

namespace MyProject.Data
{
    public class grantsData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[grantsSelectAll]";
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
            string selectProcedure = "[grantsSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Id") {
                selectCommand.Parameters.AddWithValue("@Id", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Id", DBNull.Value); }
            if (sField == "Customer Id") {
                selectCommand.Parameters.AddWithValue("@CustomerId", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CustomerId", DBNull.Value); }
            if (sField == "Pr No") {
                selectCommand.Parameters.AddWithValue("@PrNo", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@PrNo", DBNull.Value); }
            if (sField == "T1") {
                selectCommand.Parameters.AddWithValue("@T1", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@T1", DBNull.Value); }
            if (sField == "Acct Code") {
                selectCommand.Parameters.AddWithValue("@AcctCode", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@AcctCode", DBNull.Value); }
            if (sField == "T3") {
                selectCommand.Parameters.AddWithValue("@T3", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@T3", DBNull.Value); }
            if (sField == "T5") {
                selectCommand.Parameters.AddWithValue("@T5", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@T5", DBNull.Value); }
            if (sField == "T2") {
                selectCommand.Parameters.AddWithValue("@T2", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@T2", DBNull.Value); }
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

        public static grants Select_Record(grants grantsPara)
        {
            grants grants = new grants();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[grantsSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Id", grantsPara.Id);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    grants.Id = System.Convert.ToInt32(reader["Id"]);
                    grants.CustomerId = System.Convert.ToInt32(reader["CustomerId"]);
                    grants.PrNo = reader["PrNo"] is DBNull ? null : reader["PrNo"].ToString();
                    grants.T1 = reader["T1"] is DBNull ? null : reader["T1"].ToString();
                    grants.AcctCode = reader["AcctCode"] is DBNull ? null : reader["AcctCode"].ToString();
                    grants.T3 = reader["T3"] is DBNull ? null : reader["T3"].ToString();
                    grants.T5 = reader["T5"] is DBNull ? null : reader["T5"].ToString();
                    grants.T2 = reader["T2"] is DBNull ? null : reader["T2"].ToString();
                }
                else
                {
                    grants = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return grants;
            }
            finally
            {
                connection.Close();
            }
            return grants;
        }

        public static bool Add(grants grants)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string insertProcedure = "[grantsInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@Id", grants.Id);
            insertCommand.Parameters.AddWithValue("@CustomerId", grants.CustomerId);
            if (grants.PrNo != null) {
                insertCommand.Parameters.AddWithValue("@PrNo", grants.PrNo);
            } else {
                insertCommand.Parameters.AddWithValue("@PrNo", DBNull.Value); }
            if (grants.T1 != null) {
                insertCommand.Parameters.AddWithValue("@T1", grants.T1);
            } else {
                insertCommand.Parameters.AddWithValue("@T1", DBNull.Value); }
            if (grants.AcctCode != null) {
                insertCommand.Parameters.AddWithValue("@AcctCode", grants.AcctCode);
            } else {
                insertCommand.Parameters.AddWithValue("@AcctCode", DBNull.Value); }
            if (grants.T3 != null) {
                insertCommand.Parameters.AddWithValue("@T3", grants.T3);
            } else {
                insertCommand.Parameters.AddWithValue("@T3", DBNull.Value); }
            if (grants.T5 != null) {
                insertCommand.Parameters.AddWithValue("@T5", grants.T5);
            } else {
                insertCommand.Parameters.AddWithValue("@T5", DBNull.Value); }
            if (grants.T2 != null) {
                insertCommand.Parameters.AddWithValue("@T2", grants.T2);
            } else {
                insertCommand.Parameters.AddWithValue("@T2", DBNull.Value); }
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

        public static bool Update(grants oldgrants, 
               grants newgrants)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string updateProcedure = "[grantsUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewId", newgrants.Id);
            updateCommand.Parameters.AddWithValue("@NewCustomerId", newgrants.CustomerId);
            if (newgrants.PrNo != null) {
                updateCommand.Parameters.AddWithValue("@NewPrNo", newgrants.PrNo);
            } else {
                updateCommand.Parameters.AddWithValue("@NewPrNo", DBNull.Value); }
            if (newgrants.T1 != null) {
                updateCommand.Parameters.AddWithValue("@NewT1", newgrants.T1);
            } else {
                updateCommand.Parameters.AddWithValue("@NewT1", DBNull.Value); }
            if (newgrants.AcctCode != null) {
                updateCommand.Parameters.AddWithValue("@NewAcctCode", newgrants.AcctCode);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAcctCode", DBNull.Value); }
            if (newgrants.T3 != null) {
                updateCommand.Parameters.AddWithValue("@NewT3", newgrants.T3);
            } else {
                updateCommand.Parameters.AddWithValue("@NewT3", DBNull.Value); }
            if (newgrants.T5 != null) {
                updateCommand.Parameters.AddWithValue("@NewT5", newgrants.T5);
            } else {
                updateCommand.Parameters.AddWithValue("@NewT5", DBNull.Value); }
            if (newgrants.T2 != null) {
                updateCommand.Parameters.AddWithValue("@NewT2", newgrants.T2);
            } else {
                updateCommand.Parameters.AddWithValue("@NewT2", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldId", oldgrants.Id);
            updateCommand.Parameters.AddWithValue("@OldCustomerId", oldgrants.CustomerId);
            if (oldgrants.PrNo != null) {
                updateCommand.Parameters.AddWithValue("@OldPrNo", oldgrants.PrNo);
            } else {
                updateCommand.Parameters.AddWithValue("@OldPrNo", DBNull.Value); }
            if (oldgrants.T1 != null) {
                updateCommand.Parameters.AddWithValue("@OldT1", oldgrants.T1);
            } else {
                updateCommand.Parameters.AddWithValue("@OldT1", DBNull.Value); }
            if (oldgrants.AcctCode != null) {
                updateCommand.Parameters.AddWithValue("@OldAcctCode", oldgrants.AcctCode);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAcctCode", DBNull.Value); }
            if (oldgrants.T3 != null) {
                updateCommand.Parameters.AddWithValue("@OldT3", oldgrants.T3);
            } else {
                updateCommand.Parameters.AddWithValue("@OldT3", DBNull.Value); }
            if (oldgrants.T5 != null) {
                updateCommand.Parameters.AddWithValue("@OldT5", oldgrants.T5);
            } else {
                updateCommand.Parameters.AddWithValue("@OldT5", DBNull.Value); }
            if (oldgrants.T2 != null) {
                updateCommand.Parameters.AddWithValue("@OldT2", oldgrants.T2);
            } else {
                updateCommand.Parameters.AddWithValue("@OldT2", DBNull.Value); }
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

        public static bool Delete(grants grants)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string deleteProcedure = "[grantsDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldId", grants.Id);
            deleteCommand.Parameters.AddWithValue("@OldCustomerId", grants.CustomerId);
            if (grants.PrNo != null) {
                deleteCommand.Parameters.AddWithValue("@OldPrNo", grants.PrNo);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldPrNo", DBNull.Value); }
            if (grants.T1 != null) {
                deleteCommand.Parameters.AddWithValue("@OldT1", grants.T1);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldT1", DBNull.Value); }
            if (grants.AcctCode != null) {
                deleteCommand.Parameters.AddWithValue("@OldAcctCode", grants.AcctCode);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAcctCode", DBNull.Value); }
            if (grants.T3 != null) {
                deleteCommand.Parameters.AddWithValue("@OldT3", grants.T3);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldT3", DBNull.Value); }
            if (grants.T5 != null) {
                deleteCommand.Parameters.AddWithValue("@OldT5", grants.T5);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldT5", DBNull.Value); }
            if (grants.T2 != null) {
                deleteCommand.Parameters.AddWithValue("@OldT2", grants.T2);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldT2", DBNull.Value); }
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
 
