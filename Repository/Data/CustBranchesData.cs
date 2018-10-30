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
    public class CustBranchesData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();

        private int AddCustomerBranches(CustBrnches custBrnches)
        {
          
            string insertProcedure = "[CreatCustBranches]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            int BranchId = 0;

            if (custBrnches.BranchId != 0)
            {
                insertCommand.Parameters.AddWithValue("@BranchId", custBrnches.BranchId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BranchId", 0);
            }
            if (custBrnches.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", custBrnches.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (!string.IsNullOrEmpty(custBrnches.BrName))
            {
                insertCommand.Parameters.AddWithValue("@BrName", custBrnches.BrName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrName", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(custBrnches.DisplayName))
            {
                insertCommand.Parameters.AddWithValue("@DisplayName", custBrnches.DisplayName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@DisplayName", DBNull.Value);
            }
            if (custBrnches.BIsHeadOffice)
            {
                insertCommand.Parameters.AddWithValue("@IsHeadOffice", custBrnches.BIsHeadOffice);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsHeadOffice", 0);
            }
            if (custBrnches.PreSetAddress)
            {
                insertCommand.Parameters.AddWithValue("@PreSetAddress", custBrnches.PreSetAddress);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@PreSetAddress", 0);
            }
            if (custBrnches.HideBillingAddress)
            {
                insertCommand.Parameters.AddWithValue("@HideBillingAddress", custBrnches.HideBillingAddress);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@HideBillingAddress", 0);
            }
            if (!string.IsNullOrEmpty(custBrnches.BrAdd1))
            {
                insertCommand.Parameters.AddWithValue("@BrAdd1", custBrnches.BrAdd1);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrAdd1", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(custBrnches.BrAdd2))
            {
                insertCommand.Parameters.AddWithValue("@BrAdd2", custBrnches.BrAdd2);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrAdd2", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(custBrnches.BrAdd3))
            {
                insertCommand.Parameters.AddWithValue("@BrAdd3", custBrnches.BrAdd3);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrAdd3", DBNull.Value);
            }
            if (custBrnches.BrCity != 0)
            {
                insertCommand.Parameters.AddWithValue("@BrCity", custBrnches.BrCity);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrCity", 0);
            }
            if (!string.IsNullOrEmpty(custBrnches.BrCityName))
            {
                insertCommand.Parameters.AddWithValue("@BrCityName", custBrnches.BrCityName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrCityName", DBNull.Value);
            }



            if (!string.IsNullOrEmpty(custBrnches.BrState))
            {
                insertCommand.Parameters.AddWithValue("@BrState", custBrnches.BrState);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrState", DBNull.Value);
            }
            if (custBrnches.BrCountry != 0)
            {
                insertCommand.Parameters.AddWithValue("@BrCountry", custBrnches.BrCountry);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrCountry",0);
            }
            if (!string.IsNullOrEmpty(custBrnches.BrCountryName))
            {
                insertCommand.Parameters.AddWithValue("@BrCountryName", custBrnches.BrCountryName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrCountryName", DBNull.Value);
            }


            if (!string.IsNullOrEmpty(custBrnches.BrPin))
            {
                insertCommand.Parameters.AddWithValue("@BrPin", custBrnches.BrPin);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrPin", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(custBrnches.BrEmailId))
            {
                insertCommand.Parameters.AddWithValue("@BrEmailId", custBrnches.BrEmailId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrEmailId", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(custBrnches.BrContactNo))
            {
                insertCommand.Parameters.AddWithValue("@BrContactNo", custBrnches.BrContactNo);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrContactNo", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(custBrnches.BrContactPerson))
            {
                insertCommand.Parameters.AddWithValue("@BrContactPerson", custBrnches.BrContactPerson);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrContactPerson", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(custBrnches.BrFullAddress))
            {
                insertCommand.Parameters.AddWithValue("@BrFullAddress", custBrnches.BrFullAddress);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BrFullAddress", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(custBrnches.BlAdd1))
            {
                insertCommand.Parameters.AddWithValue("@BlAdd1", custBrnches.BlAdd1);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BlAdd1", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(custBrnches.BlAdd2))
            {
                insertCommand.Parameters.AddWithValue("@BlAdd2", custBrnches.BlAdd2);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BlAdd2", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(custBrnches.BlAdd3))
            {
                insertCommand.Parameters.AddWithValue("@BlAdd3", custBrnches.BlAdd3);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BlAdd3", DBNull.Value);
            }

            if (custBrnches.BlCity != 0 )
            {
                insertCommand.Parameters.AddWithValue("@BlCity", custBrnches.BlCity);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BlCity", 0);
            }
            if (!string.IsNullOrEmpty(custBrnches.BICityName))
            {
                insertCommand.Parameters.AddWithValue("@BlCityName", custBrnches.BICityName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BlCityName", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(custBrnches.BlState))
            {
                insertCommand.Parameters.AddWithValue("@BlState", custBrnches.BlState);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BlState", DBNull.Value);
            }
            if (custBrnches.BlCountry != 0)
            {
                insertCommand.Parameters.AddWithValue("@BlCountry", custBrnches.BlCountry);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BlCountry", 0);
            }
            if (!string.IsNullOrEmpty(custBrnches.BillCountryName))
            {
                insertCommand.Parameters.AddWithValue("@BlCountryName", custBrnches.BillCountryName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BlCountryName", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(custBrnches.BlPin))
            {
                insertCommand.Parameters.AddWithValue("@BlPin", custBrnches.BlPin);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BlPin", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(custBrnches.BlEmailId))
            {
                insertCommand.Parameters.AddWithValue("@BlEmailId", custBrnches.BlEmailId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BlEmailId", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(custBrnches.BlContactNo))
            {
                insertCommand.Parameters.AddWithValue("@BlContactNo", custBrnches.BlContactNo);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BlContactNo", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(custBrnches.BlContactPerson))
            {
                insertCommand.Parameters.AddWithValue("@BlContactPerson", custBrnches.BlContactPerson);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BlContactPerson", DBNull.Value);
            }
            if (custBrnches.Needs_Delivery_Term)
            {
                insertCommand.Parameters.AddWithValue("@Needs_Delivery_Term", custBrnches.Needs_Delivery_Term);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Needs_Delivery_Term", 0);
            }
            if (custBrnches.Needs_Fee_Warning)
            {
                insertCommand.Parameters.AddWithValue("@Needs_Fee_Warning", custBrnches.Needs_Fee_Warning);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Needs_Fee_Warning", 0);
            }
            if (!string.IsNullOrEmpty(custBrnches.Fee_Warning))
            {
                insertCommand.Parameters.AddWithValue("@Fee_Warning", custBrnches.Fee_Warning);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Fee_Warning", DBNull.Value);
            }
            if (custBrnches.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", custBrnches.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", DBNull.Value);
            }
            if (custBrnches.UserID != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", custBrnches.UserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (custBrnches.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", custBrnches.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type",0);
            }

            insertCommand.Parameters.Add("@BranchIdout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@BranchIdout"].Direction = ParameterDirection.Output;

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
                }
                if (count != 0 && custBrnches.BranchId == 0)
                {
                    custBrnches.BranchId = Convert.ToInt32(insertCommand.Parameters["@BranchIdout"].Value);
                }

                return BranchId;
               // return custBrnches;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return BranchId;
                //return custBrnches;
            }
            finally
            {
                connection.Close();
            }

        }
        //customer branches
        public List<dynamic> AddCustomerBranchesx(CustomerBranches customerBranches)
        {
            List<dynamic> ObjDynamic = new List<dynamic>();
            CustBrnches custBranches = new CustBrnches();
            custBranches.BranchId = customerBranches.BranchId;
            custBranches.CustId = customerBranches.CustomerId;
            custBranches.BrName = customerBranches.BranchName;
            custBranches.DisplayName = customerBranches.DisplayName;
            custBranches.BIsHeadOffice = customerBranches.HeadOffice;
            custBranches.PreSetAddress = customerBranches.PreSetAddress;
            custBranches.BrAdd1 = customerBranches.BrAdd1;
            custBranches.BrAdd2 = customerBranches.BrAdd2;
            custBranches.BrAdd3 = customerBranches.BrAdd3;
            if (!string.IsNullOrEmpty(customerBranches.BrCity))
            {
                custBranches.BrCity = Int64.Parse(customerBranches.BrCity);
            }
            custBranches.BrCityName = customerBranches.BrCityName;
            custBranches.BrState = customerBranches.BrState;
            if (!string.IsNullOrEmpty(customerBranches.BrCountry))
            {
                custBranches.BrCountry = Int64.Parse(customerBranches.BrCountry);
            }
            custBranches.BrCountryName = customerBranches.BrCountryName;
            custBranches.BrPin = customerBranches.Brpin;
            custBranches.BrEmailId = customerBranches.BrEmail;
            custBranches.BrContactNo = customerBranches.BrContact; 
            custBranches.BrContactPerson = customerBranches.BrConName;
            custBranches.BrFullAddress = customerBranches.BrFullAddress;
            custBranches.BlAdd1 = customerBranches.BIAdd1;
            custBranches.BlAdd2 = customerBranches.BIAdd2;
            custBranches.BlAdd3 = customerBranches.BIAdd3;
            if (!string.IsNullOrEmpty(customerBranches.BICity))
            {
                custBranches.BlCity = Int64.Parse(customerBranches.BICity);
            }
            custBranches.BICityName = customerBranches.BICityName;
            custBranches.BlState = customerBranches.BlState;
            if (!string.IsNullOrEmpty(customerBranches.BlCountry))
            {
                custBranches.BlCountry = Int64.Parse(customerBranches.BlCountry);
            }
            custBranches.BillCountryName = customerBranches.BillCountryName;
            custBranches.BlPin = customerBranches.BIpin;
            custBranches.BlEmailId = customerBranches.BIEmail;
            custBranches.BlContactNo = customerBranches.BIContact;
            custBranches.BlContactPerson = customerBranches.BIConName;
            custBranches.HideBillingAddress = customerBranches.HideBillingAddress;
            custBranches.Needs_Delivery_Term = customerBranches.NeedDelivery;
            custBranches.Needs_Fee_Warning = customerBranches.NeedWarning;
            custBranches.Fee_Warning = customerBranches.FeeWarning;
            custBranches.IsActive = true; // delete this line <--
            

            custBranches.Type = customerBranches.Type;
            if (!string.IsNullOrEmpty(UserID))
            {
                custBranches.UserID =  Convert.ToInt64(UserID);
            }
            AddCustomerBranches(custBranches);
            //ObjDynamic.Add(custBranches.COSId);
            ObjDynamic.Add(custBranches);
            return ObjDynamic;

        }
        private DataSet GetCustomerBranches(Int64 CustID)
        {

            string selectProcedure = "[GetCustBranches]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@CustId", CustID);
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

        public List<dynamic> GetCustDetials(Int64 custId)
        {
            List<dynamic> ObjDynamic = new List<dynamic>();
            DataSet ds = GetCustomerBranches(custId);
            var myEnumerable = ds.Tables[0].AsEnumerable();
            try
            {
                List<CustomerBranches> CustomerList =
                    (from item in myEnumerable
                     select new CustomerBranches
                     {
                         BranchId = item.Field<Int64>("BranchId"),
                         CustomerId = item.Field<Int64>("CustId"),
                         BranchName = item.Field<string>("BrName"),
                         DisplayName = item.Field<string>("DisplayName"),
                         HeadOffice = item.Field<bool>("IsHeadOffice"),
                         PreSetAddress = item.Field<bool>("PreSetAddress"),
                         HideBillingAddress = item.Field<bool>("HideBillingAddress"),
                         BrAdd1 = item.Field<string>("BrAdd1"),
                         BrAdd2 = item.Field<string>("BrAdd2"),
                         BrAdd3 = item.Field<string>("BrAdd3"),
                         intBrCity = item.Field<Int64>("BrCityId"),
                         BrCityName = item.Field<string>("BrCity"),
                         BrState = item.Field<string>("BrState"),
                         intBrCountry = item.Field<Int64>("BrCountryId"),
                         BrCountryName = item.Field<string>("BrCountry"),
                         Brpin = item.Field<string>("BrPin"),
                         BrConName = item.Field<string>("BrContactPerson"),
                         BrEmail = item.Field<string>("BrEmailId"),
                         BrContact = item.Field<string>("BrContactNo"),
                         BrFullAddress = item.Field<string>("BrFullAddress"),
                         BIAdd1 = item.Field<string>("BlAdd1"),
                         BIAdd2 = item.Field<string>("BlAdd2"),
                         BIAdd3 = item.Field<string>("BlAdd3"),
                         intBlCity = item.Field<Int64>("BlCityId"),
                         BICityName = item.Field<string>("BlCity"),
                         BlState = item.Field<string>("BlState"),
                         intBlCountry = item.Field<Int64>("BlCountryId"),
                         BillCountryName = item.Field<string>("BlCountry"),
                         BIpin = item.Field<string>("BlPin"),
                         BIEmail = item.Field<string>("BlEmailId"),
                         BIContact = item.Field<string>("BlContactNo"),
                         BIConName = item.Field<string>("BlContactPerson"),
                         NeedDelivery = item.Field<bool>("Needs_Delivery_Term"),
                         NeedWarning = item.Field<bool>("Needs_Fee_Warning"),
                         FeeWarning = item.Field<string>("Fee_Warning")

                     }).ToList();
                ObjDynamic.Add(CustomerList);
            }
            catch(Exception ex)
            {

            }
           
            return ObjDynamic;
        }
    }
}