using System;
using System.Data;
using System.Data.SqlClient;
using MyProject.Models;

namespace MyProject.Data
{
    public class customersData
    {
        public static Customers Select_Record(Customers clsCustMasterPara)
        {
            Customers clsCustMaster = new Customers();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[CustMasterSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@CustId", clsCustMasterPara.CustId);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    clsCustMaster.CustId = System.Convert.ToInt32(reader["CustId"]);
                    clsCustMaster.CustName = reader["CustName"] is DBNull ? null : reader["CustName"].ToString();
                    clsCustMaster.Acronym = reader["Acronym"] is DBNull ? null : reader["Acronym"].ToString();
                    clsCustMaster.NoofBranches = reader["NoofBranches"] is DBNull ? null : (Int32?)reader["NoofBranches"];
                    clsCustMaster.LevelofAuthority = reader["LevelofAuthority"] is DBNull ? null : (Int32?)reader["LevelofAuthority"];
                    clsCustMaster.Code = reader["Code"] is DBNull ? null : reader["Code"].ToString();
                    clsCustMaster.Ticker = reader["Ticker"] is DBNull ? null : reader["Ticker"].ToString();
                    clsCustMaster.InDemo = reader["InDemo"] is DBNull ? null : (Boolean?)reader["InDemo"];
                    clsCustMaster.TieredPricing = reader["TieredPricing"] is DBNull ? null : (Boolean?)reader["TieredPricing"];
                    clsCustMaster.HOAdd1 = reader["HOAdd1"] is DBNull ? null : reader["HOAdd1"].ToString();
                    clsCustMaster.HOAdd2 = reader["HOAdd2"] is DBNull ? null : reader["HOAdd2"].ToString();
                    clsCustMaster.HOAdd3 = reader["HOAdd3"] is DBNull ? null : reader["HOAdd3"].ToString();
                    clsCustMaster.HOCITY = reader["HOCITY"] is DBNull ? null : reader["HOCITY"].ToString();
                    clsCustMaster.HOState = reader["HOState"] is DBNull ? null : reader["HOState"].ToString();
                    clsCustMaster.HOCountry = reader["HOCountry"] is DBNull ? null : reader["HOCountry"].ToString();
                    clsCustMaster.HOPin = reader["HOPin"] is DBNull ? null : reader["HOPin"].ToString();
                    clsCustMaster.HOFullAddress = reader["HOFullAddress"] is DBNull ? null : reader["HOFullAddress"].ToString();
                    clsCustMaster.HOEmailId = reader["HOEmailId"] is DBNull ? null : reader["HOEmailId"].ToString();
                    clsCustMaster.HOContactPerson = reader["HOContactPerson"] is DBNull ? null : reader["HOContactPerson"].ToString();
                    clsCustMaster.isActive = reader["isActive"] is DBNull ? null : (Boolean?)reader["isActive"];
                    clsCustMaster.CreatedOn = reader["CreatedOn"] is DBNull ? null : (DateTime?)reader["CreatedOn"];
                    clsCustMaster.CreatedBy = reader["CreatedBy"] is DBNull ? null : (Int64?)reader["CreatedBy"];
                    clsCustMaster.ModifiedOn = reader["ModifiedOn"] is DBNull ? null : (DateTime?)reader["ModifiedOn"];
                    clsCustMaster.ModifiedBy = reader["ModifiedBy"] is DBNull ? null : (Int64?)reader["ModifiedBy"];
                    clsCustMaster.DeletedOn = reader["DeletedOn"] is DBNull ? null : (DateTime?)reader["DeletedOn"];
                    clsCustMaster.DeletedBy = reader["DeletedBy"] is DBNull ? null : (Int64?)reader["DeletedBy"];
                }
                else
                {
                    clsCustMaster = null;
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
            return clsCustMaster;
        }

        public static bool Add(Customers clsCustMaster)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string insertProcedure = "[CustMasterInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@CustId", clsCustMaster.CustId);
            if (clsCustMaster.CustName != null)
            {
                insertCommand.Parameters.AddWithValue("@CustName", clsCustMaster.CustName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustName", DBNull.Value);
            }
            if (clsCustMaster.Acronym != null)
            {
                insertCommand.Parameters.AddWithValue("@Acronym", clsCustMaster.Acronym);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Acronym", DBNull.Value);
            }
            if (clsCustMaster.NoofBranches.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@NoofBranches", clsCustMaster.NoofBranches);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@NoofBranches", DBNull.Value);
            }
            if (clsCustMaster.LevelofAuthority.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@LevelofAuthority", clsCustMaster.LevelofAuthority);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@LevelofAuthority", DBNull.Value);
            }
            if (clsCustMaster.Code != null)
            {
                insertCommand.Parameters.AddWithValue("@Code", clsCustMaster.Code);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Code", DBNull.Value);
            }
            if (clsCustMaster.Ticker != null)
            {
                insertCommand.Parameters.AddWithValue("@Ticker", clsCustMaster.Ticker);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Ticker", DBNull.Value);
            }
            if (clsCustMaster.InDemo.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@InDemo", clsCustMaster.InDemo);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@InDemo", DBNull.Value);
            }
            if (clsCustMaster.TieredPricing.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@TieredPricing", clsCustMaster.TieredPricing);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@TieredPricing", DBNull.Value);
            }
            if (clsCustMaster.HOAdd1 != null)
            {
                insertCommand.Parameters.AddWithValue("@HOAdd1", clsCustMaster.HOAdd1);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOAdd1", DBNull.Value);
            }
            if (clsCustMaster.HOAdd2 != null)
            {
                insertCommand.Parameters.AddWithValue("@HOAdd2", clsCustMaster.HOAdd2);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOAdd2", DBNull.Value);
            }
            if (clsCustMaster.HOAdd3 != null)
            {
                insertCommand.Parameters.AddWithValue("@HOAdd3", clsCustMaster.HOAdd3);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOAdd3", DBNull.Value);
            }
            if (clsCustMaster.HOCITY != null)
            {
                insertCommand.Parameters.AddWithValue("@HOCITY", clsCustMaster.HOCITY);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOCITY", DBNull.Value);
            }
            if (clsCustMaster.HOState != null)
            {
                insertCommand.Parameters.AddWithValue("@HOState", clsCustMaster.HOState);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOState", DBNull.Value);
            }
            if (clsCustMaster.HOCountry != null)
            {
                insertCommand.Parameters.AddWithValue("@HOCountry", clsCustMaster.HOCountry);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOCountry", DBNull.Value);
            }
            if (clsCustMaster.HOPin != null)
            {
                insertCommand.Parameters.AddWithValue("@HOPin", clsCustMaster.HOPin);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOPin", DBNull.Value);
            }
            if (clsCustMaster.HOFullAddress != null)
            {
                insertCommand.Parameters.AddWithValue("@HOFullAddress", clsCustMaster.HOFullAddress);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOFullAddress", DBNull.Value);
            }
            if (clsCustMaster.HOEmailId != null)
            {
                insertCommand.Parameters.AddWithValue("@HOEmailId", clsCustMaster.HOEmailId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOEmailId", DBNull.Value);
            }
            if (clsCustMaster.HOContactPerson != null)
            {
                insertCommand.Parameters.AddWithValue("@HOContactPerson", clsCustMaster.HOContactPerson);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOContactPerson", DBNull.Value);
            }
            if (clsCustMaster.isActive.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@isActive", clsCustMaster.isActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isActive", DBNull.Value);
            }
            if (clsCustMaster.CreatedOn.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@CreatedOn", clsCustMaster.CreatedOn);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CreatedOn", DBNull.Value);
            }
            if (clsCustMaster.CreatedBy.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@CreatedBy", clsCustMaster.CreatedBy);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
            }
            if (clsCustMaster.ModifiedOn.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@ModifiedOn", clsCustMaster.ModifiedOn);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ModifiedOn", DBNull.Value);
            }
            if (clsCustMaster.ModifiedBy.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@ModifiedBy", clsCustMaster.ModifiedBy);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ModifiedBy", DBNull.Value);
            }
            if (clsCustMaster.DeletedOn.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@DeletedOn", clsCustMaster.DeletedOn);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@DeletedOn", DBNull.Value);
            }
            if (clsCustMaster.DeletedBy.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@DeletedBy", clsCustMaster.DeletedBy);
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

        public static bool Update(Customers oldCustMaster,
               Customers newCustMaster)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string updateProcedure = "[CustMasterUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewCustId", newCustMaster.CustId);
            if (newCustMaster.CustName != null)
            {
                updateCommand.Parameters.AddWithValue("@NewCustName", newCustMaster.CustName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewCustName", DBNull.Value);
            }
            if (newCustMaster.Acronym != null)
            {
                updateCommand.Parameters.AddWithValue("@NewAcronym", newCustMaster.Acronym);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewAcronym", DBNull.Value);
            }
            if (newCustMaster.NoofBranches.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewNoofBranches", newCustMaster.NoofBranches);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewNoofBranches", DBNull.Value);
            }
            if (newCustMaster.LevelofAuthority.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewLevelofAuthority", newCustMaster.LevelofAuthority);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewLevelofAuthority", DBNull.Value);
            }
            if (newCustMaster.Code != null)
            {
                updateCommand.Parameters.AddWithValue("@NewCode", newCustMaster.Code);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewCode", DBNull.Value);
            }
            if (newCustMaster.Ticker != null)
            {
                updateCommand.Parameters.AddWithValue("@NewTicker", newCustMaster.Ticker);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewTicker", DBNull.Value);
            }
            if (newCustMaster.InDemo.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewInDemo", newCustMaster.InDemo);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewInDemo", DBNull.Value);
            }
            if (newCustMaster.TieredPricing.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewTieredPricing", newCustMaster.TieredPricing);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewTieredPricing", DBNull.Value);
            }
            if (newCustMaster.HOAdd1 != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHOAdd1", newCustMaster.HOAdd1);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHOAdd1", DBNull.Value);
            }
            if (newCustMaster.HOAdd2 != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHOAdd2", newCustMaster.HOAdd2);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHOAdd2", DBNull.Value);
            }
            if (newCustMaster.HOAdd3 != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHOAdd3", newCustMaster.HOAdd3);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHOAdd3", DBNull.Value);
            }
            if (newCustMaster.HOCITY != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHOCITY", newCustMaster.HOCITY);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHOCITY", DBNull.Value);
            }
            if (newCustMaster.HOState != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHOState", newCustMaster.HOState);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHOState", DBNull.Value);
            }
            if (newCustMaster.HOCountry != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHOCountry", newCustMaster.HOCountry);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHOCountry", DBNull.Value);
            }
            if (newCustMaster.HOPin != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHOPin", newCustMaster.HOPin);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHOPin", DBNull.Value);
            }
            if (newCustMaster.HOFullAddress != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHOFullAddress", newCustMaster.HOFullAddress);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHOFullAddress", DBNull.Value);
            }
            if (newCustMaster.HOEmailId != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHOEmailId", newCustMaster.HOEmailId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHOEmailId", DBNull.Value);
            }
            if (newCustMaster.HOContactPerson != null)
            {
                updateCommand.Parameters.AddWithValue("@NewHOContactPerson", newCustMaster.HOContactPerson);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewHOContactPerson", DBNull.Value);
            }
            if (newCustMaster.isActive.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewisActive", newCustMaster.isActive);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewisActive", DBNull.Value);
            }
            if (newCustMaster.CreatedOn.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewCreatedOn", newCustMaster.CreatedOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewCreatedOn", DBNull.Value);
            }
            if (newCustMaster.CreatedBy.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewCreatedBy", newCustMaster.CreatedBy);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewCreatedBy", DBNull.Value);
            }
            if (newCustMaster.ModifiedOn.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewModifiedOn", newCustMaster.ModifiedOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewModifiedOn", DBNull.Value);
            }
            if (newCustMaster.ModifiedBy.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewModifiedBy", newCustMaster.ModifiedBy);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewModifiedBy", DBNull.Value);
            }
            if (newCustMaster.DeletedOn.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewDeletedOn", newCustMaster.DeletedOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewDeletedOn", DBNull.Value);
            }
            if (newCustMaster.DeletedBy.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewDeletedBy", newCustMaster.DeletedBy);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewDeletedBy", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@OldCustId", oldCustMaster.CustId);
            if (oldCustMaster.CustName != null)
            {
                updateCommand.Parameters.AddWithValue("@OldCustName", oldCustMaster.CustName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldCustName", DBNull.Value);
            }
            if (oldCustMaster.Acronym != null)
            {
                updateCommand.Parameters.AddWithValue("@OldAcronym", oldCustMaster.Acronym);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldAcronym", DBNull.Value);
            }
            if (oldCustMaster.NoofBranches.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldNoofBranches", oldCustMaster.NoofBranches);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldNoofBranches", DBNull.Value);
            }
            if (oldCustMaster.LevelofAuthority.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldLevelofAuthority", oldCustMaster.LevelofAuthority);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldLevelofAuthority", DBNull.Value);
            }
            if (oldCustMaster.Code != null)
            {
                updateCommand.Parameters.AddWithValue("@OldCode", oldCustMaster.Code);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldCode", DBNull.Value);
            }
            if (oldCustMaster.Ticker != null)
            {
                updateCommand.Parameters.AddWithValue("@OldTicker", oldCustMaster.Ticker);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldTicker", DBNull.Value);
            }
            if (oldCustMaster.InDemo.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldInDemo", oldCustMaster.InDemo);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldInDemo", DBNull.Value);
            }
            if (oldCustMaster.TieredPricing.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldTieredPricing", oldCustMaster.TieredPricing);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldTieredPricing", DBNull.Value);
            }
            if (oldCustMaster.HOAdd1 != null)
            {
                updateCommand.Parameters.AddWithValue("@OldHOAdd1", oldCustMaster.HOAdd1);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldHOAdd1", DBNull.Value);
            }
            if (oldCustMaster.HOAdd2 != null)
            {
                updateCommand.Parameters.AddWithValue("@OldHOAdd2", oldCustMaster.HOAdd2);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldHOAdd2", DBNull.Value);
            }
            if (oldCustMaster.HOAdd3 != null)
            {
                updateCommand.Parameters.AddWithValue("@OldHOAdd3", oldCustMaster.HOAdd3);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldHOAdd3", DBNull.Value);
            }
            if (oldCustMaster.HOCITY != null)
            {
                updateCommand.Parameters.AddWithValue("@OldHOCITY", oldCustMaster.HOCITY);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldHOCITY", DBNull.Value);
            }
            if (oldCustMaster.HOState != null)
            {
                updateCommand.Parameters.AddWithValue("@OldHOState", oldCustMaster.HOState);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldHOState", DBNull.Value);
            }
            if (oldCustMaster.HOCountry != null)
            {
                updateCommand.Parameters.AddWithValue("@OldHOCountry", oldCustMaster.HOCountry);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldHOCountry", DBNull.Value);
            }
            if (oldCustMaster.HOPin != null)
            {
                updateCommand.Parameters.AddWithValue("@OldHOPin", oldCustMaster.HOPin);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldHOPin", DBNull.Value);
            }
            if (oldCustMaster.HOFullAddress != null)
            {
                updateCommand.Parameters.AddWithValue("@OldHOFullAddress", oldCustMaster.HOFullAddress);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldHOFullAddress", DBNull.Value);
            }
            if (oldCustMaster.HOEmailId != null)
            {
                updateCommand.Parameters.AddWithValue("@OldHOEmailId", oldCustMaster.HOEmailId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldHOEmailId", DBNull.Value);
            }
            if (oldCustMaster.HOContactPerson != null)
            {
                updateCommand.Parameters.AddWithValue("@OldHOContactPerson", oldCustMaster.HOContactPerson);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldHOContactPerson", DBNull.Value);
            }
            if (oldCustMaster.isActive.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldisActive", oldCustMaster.isActive);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldisActive", DBNull.Value);
            }
            if (oldCustMaster.CreatedOn.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldCreatedOn", oldCustMaster.CreatedOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldCreatedOn", DBNull.Value);
            }
            if (oldCustMaster.CreatedBy.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldCreatedBy", oldCustMaster.CreatedBy);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldCreatedBy", DBNull.Value);
            }
            if (oldCustMaster.ModifiedOn.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldModifiedOn", oldCustMaster.ModifiedOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldModifiedOn", DBNull.Value);
            }
            if (oldCustMaster.ModifiedBy.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldModifiedBy", oldCustMaster.ModifiedBy);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldModifiedBy", DBNull.Value);
            }
            if (oldCustMaster.DeletedOn.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldDeletedOn", oldCustMaster.DeletedOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldDeletedOn", DBNull.Value);
            }
            if (oldCustMaster.DeletedBy.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldDeletedBy", oldCustMaster.DeletedBy);
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

        public static bool Delete(Customers clsCustMaster)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string deleteProcedure = "[CustMasterDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldCustId", clsCustMaster.CustId);
            if (clsCustMaster.CustName != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldCustName", clsCustMaster.CustName);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldCustName", DBNull.Value);
            }
            if (clsCustMaster.Acronym != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldAcronym", clsCustMaster.Acronym);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldAcronym", DBNull.Value);
            }
            if (clsCustMaster.NoofBranches.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldNoofBranches", clsCustMaster.NoofBranches);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldNoofBranches", DBNull.Value);
            }
            if (clsCustMaster.LevelofAuthority.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldLevelofAuthority", clsCustMaster.LevelofAuthority);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldLevelofAuthority", DBNull.Value);
            }
            if (clsCustMaster.Code != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldCode", clsCustMaster.Code);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldCode", DBNull.Value);
            }
            if (clsCustMaster.Ticker != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldTicker", clsCustMaster.Ticker);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldTicker", DBNull.Value);
            }
            if (clsCustMaster.InDemo.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldInDemo", clsCustMaster.InDemo);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldInDemo", DBNull.Value);
            }
            if (clsCustMaster.TieredPricing.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldTieredPricing", clsCustMaster.TieredPricing);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldTieredPricing", DBNull.Value);
            }
            if (clsCustMaster.HOAdd1 != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldHOAdd1", clsCustMaster.HOAdd1);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldHOAdd1", DBNull.Value);
            }
            if (clsCustMaster.HOAdd2 != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldHOAdd2", clsCustMaster.HOAdd2);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldHOAdd2", DBNull.Value);
            }
            if (clsCustMaster.HOAdd3 != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldHOAdd3", clsCustMaster.HOAdd3);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldHOAdd3", DBNull.Value);
            }
            if (clsCustMaster.HOCITY != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldHOCITY", clsCustMaster.HOCITY);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldHOCITY", DBNull.Value);
            }
            if (clsCustMaster.HOState != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldHOState", clsCustMaster.HOState);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldHOState", DBNull.Value);
            }
            if (clsCustMaster.HOCountry != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldHOCountry", clsCustMaster.HOCountry);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldHOCountry", DBNull.Value);
            }
            if (clsCustMaster.HOPin != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldHOPin", clsCustMaster.HOPin);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldHOPin", DBNull.Value);
            }
            if (clsCustMaster.HOFullAddress != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldHOFullAddress", clsCustMaster.HOFullAddress);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldHOFullAddress", DBNull.Value);
            }
            if (clsCustMaster.HOEmailId != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldHOEmailId", clsCustMaster.HOEmailId);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldHOEmailId", DBNull.Value);
            }
            if (clsCustMaster.HOContactPerson != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldHOContactPerson", clsCustMaster.HOContactPerson);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldHOContactPerson", DBNull.Value);
            }
            if (clsCustMaster.isActive.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldisActive", clsCustMaster.isActive);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldisActive", DBNull.Value);
            }
            if (clsCustMaster.CreatedOn.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldCreatedOn", clsCustMaster.CreatedOn);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldCreatedOn", DBNull.Value);
            }
            if (clsCustMaster.CreatedBy.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldCreatedBy", clsCustMaster.CreatedBy);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldCreatedBy", DBNull.Value);
            }
            if (clsCustMaster.ModifiedOn.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldModifiedOn", clsCustMaster.ModifiedOn);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldModifiedOn", DBNull.Value);
            }
            if (clsCustMaster.ModifiedBy.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldModifiedBy", clsCustMaster.ModifiedBy);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldModifiedBy", DBNull.Value);
            }
            if (clsCustMaster.DeletedOn.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldDeletedOn", clsCustMaster.DeletedOn);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldDeletedOn", DBNull.Value);
            }
            if (clsCustMaster.DeletedBy.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldDeletedBy", clsCustMaster.DeletedBy);
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