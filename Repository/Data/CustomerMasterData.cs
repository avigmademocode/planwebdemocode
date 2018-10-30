using System;
using System.Data;
using System.Data.SqlClient;
using MyProject.Models;
using MyProject.Data;
using MyProject.Repository.Library;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web;
using MyProject.Repository.Security;

namespace MyProject.Repository.Data
{
    public class CustomerMasterData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        private CustomerMaster AddCustomerMaster(CustomerMaster customerMaster)
        {

            string insertProcedure = "[CreateUpdateCustMaster]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (customerMaster.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId ", customerMaster.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId ", 0);
            }

            if (!string.IsNullOrEmpty(customerMaster.CustName))
            {
                insertCommand.Parameters.AddWithValue("@CustName", customerMaster.CustName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustName", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(customerMaster.Acronym))
            {
                insertCommand.Parameters.AddWithValue("@Acronym", customerMaster.Acronym);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Acronym", DBNull.Value);
            }


            if (customerMaster.NoofBranches != 0)
            {
                insertCommand.Parameters.AddWithValue("@NoofBranches ", customerMaster.NoofBranches);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@NoofBranches ", 0);
            }
            if (customerMaster.LevelofAuthority != 0)
            {
                insertCommand.Parameters.AddWithValue("@LevelofAuthority", customerMaster.LevelofAuthority);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@LevelofAuthority", 0);
            }
            if (!string.IsNullOrEmpty(customerMaster.Code))
            {
                insertCommand.Parameters.AddWithValue("@Code", customerMaster.Code);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Code", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(customerMaster.Ticker))
            {
                insertCommand.Parameters.AddWithValue("@Ticker", customerMaster.Ticker);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Ticker", DBNull.Value);
            }

            if (customerMaster.InDemo)
            {
                insertCommand.Parameters.AddWithValue("@InDemo", customerMaster.InDemo);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@InDemo", 0);
            }
            if (customerMaster.TieredPricing)
            {
                insertCommand.Parameters.AddWithValue("@TieredPricing", customerMaster.TieredPricing);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@TieredPricing", 0);
            }
            if (!string.IsNullOrEmpty(customerMaster.HOAdd1))
            {
                insertCommand.Parameters.AddWithValue("@HOAdd1", customerMaster.HOAdd1);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOAdd1", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(customerMaster.HOAdd2))
            {
                insertCommand.Parameters.AddWithValue("@HOAdd2", customerMaster.HOAdd2);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOAdd2", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(customerMaster.HOAdd3))
            {
                insertCommand.Parameters.AddWithValue("@HOAdd3", customerMaster.HOAdd3);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOAdd3", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(customerMaster.HOCITY))
            {
                insertCommand.Parameters.AddWithValue("@HOCITY", customerMaster.HOCITY);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOCITY", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(customerMaster.HOState))
            {
                insertCommand.Parameters.AddWithValue("@HOState", customerMaster.HOState);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOState", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(customerMaster.HOCountry))
            {
                insertCommand.Parameters.AddWithValue("@HOCountry", customerMaster.HOCountry);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOCountry", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(customerMaster.HOPin))
            {
                insertCommand.Parameters.AddWithValue("@HOPin", customerMaster.HOPin);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOPin", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(customerMaster.HOFullAddress))
            {
                insertCommand.Parameters.AddWithValue("@HOFullAddress", customerMaster.HOFullAddress);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOFullAddress", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(customerMaster.HOEmailId))
            {
                insertCommand.Parameters.AddWithValue("@HOEmailId", customerMaster.HOEmailId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOEmailId", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(customerMaster.HOContactPerson))
            {
                insertCommand.Parameters.AddWithValue("@HOContactPerson", customerMaster.HOContactPerson);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HOContactPerson", DBNull.Value);
            }
            if (customerMaster.isActive)
            {
                insertCommand.Parameters.AddWithValue("@isActive", customerMaster.isActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isActive", 0);
            }
            if (customerMaster.UserID != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserID ", customerMaster.UserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserID ", 0);
            }

            if (customerMaster.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatementType ", customerMaster.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatementType ", 0);
            }

            insertCommand.Parameters.Add("@CustIdout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@CustIdout"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            try
            {
                int count = 0;
                connection.Open();
                insertCommand.ExecuteNonQuery();
                if (insertCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                    customerMaster.Status = count;
                }
                if (count == 1 && customerMaster.CustId == 0  )
                {
                    customerMaster.CustId = System.Convert.ToInt32(insertCommand.Parameters["@CustIdout"].Value);
                }
                return customerMaster;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return customerMaster;
            }
            finally
            {
                connection.Close();
            }



        }

        private int UpdateCustomerMaster(CustomerMaster customerMaster)
        {

            string updateProcedure = "[CreateCustMaster]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;




            if (customerMaster.CustId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CustId ", customerMaster.CustId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CustId ", 0);
            }
            if (!string.IsNullOrEmpty(customerMaster.CustName))
            {
                updateCommand.Parameters.AddWithValue("@CustName", customerMaster.CustName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CustName", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(customerMaster.Acronym))
            {
                updateCommand.Parameters.AddWithValue("@Acronym", customerMaster.Acronym);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Acronym", DBNull.Value);
            }


            if (customerMaster.NoofBranches != 0)
            {
                updateCommand.Parameters.AddWithValue("@NoofBranches ", customerMaster.NoofBranches);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NoofBranches ", 0);
            }
            if (!string.IsNullOrEmpty(customerMaster.Code))
            {
                updateCommand.Parameters.AddWithValue("@Code", customerMaster.Code);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Code", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(customerMaster.Ticker))
            {
                updateCommand.Parameters.AddWithValue("@Tricker", customerMaster.Ticker);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Tricker", DBNull.Value);
            }

            if (customerMaster.InDemo)
            {
                updateCommand.Parameters.AddWithValue("@InDemo", customerMaster.InDemo);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@InDemo", 0);
            }
            if (customerMaster.TieredPricing)
            {
                updateCommand.Parameters.AddWithValue("@TieredPricing", customerMaster.TieredPricing);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@TieredPricing", 0);
            }
            if (customerMaster.isActive)
            {
                updateCommand.Parameters.AddWithValue("@isActive", customerMaster.isActive);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@isActive", 0);
            }


            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            try
            {
                int count = 0;
                connection.Open();
                updateCommand.ExecuteNonQuery();
                if (updateCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);

                }




                return 0;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return 0;
            }
            finally
            {
                connection.Close();
            }



        }
        private DataSet GetCustMaster(Int64 CustId)
        {

            string selectProcedure = "[GetCustMasterData]";
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            DataSet ds = new DataSet();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CustId", CustId);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;

        }

        public List<dynamic> AddCustMasterData(CustomerMaster customerMaster)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            if (!string.IsNullOrEmpty(UserID))
            {
                customerMaster.UserID = Convert.ToInt64(UserID);
            }
           
            AddCustomerMaster(customerMaster);
            objDynamic.Add(customerMaster.Status);
            objDynamic.Add(customerMaster.CustId);
            objDynamic.Add(customerMaster.CustName);
            objDynamic.Add(GetCustMasterData(0));
            return objDynamic;
        }

            public List<dynamic> GetCustMasterData(Int64 CustId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetCustMaster(CustId);

            var myEnumerableApr = ds.Tables[0].AsEnumerable();
            List<CustomerMaster> CustomerMaster =
               (from item in myEnumerableApr
                select new CustomerMaster
                {
                    CustId = item.Field<Int64>("CustId"),
                    CustName = item.Field<String>("CustName"),
                    Acronym = item.Field<String>("Acronym"),
                    NoofBranches = item.Field<int>("NoofBranches"),
                    Code = item.Field<String>("Code"),
                    Ticker = item.Field<String>("Ticker"),
                    InDemo = item.Field<bool>("InDemo"),
                    TieredPricing= item.Field<bool>("TieredPricing")
             

                }).ToList();
            objDynamic.Add(CustomerMaster);

            return objDynamic;
        }


    }
}