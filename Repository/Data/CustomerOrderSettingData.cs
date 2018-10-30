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
    public class CustomerOrderSettingData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
       // string UserID = HttpContext.Current.Session["UserId"].ToString();
       // string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();
        private CustomerOrderSetting AddUpdateCustomerOrderSetting(CustomerOrderSetting customerOrderSettingDTO)
        {
            
            string insertProcedure = "[CreatCustomerOrderSetting]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            
            if (customerOrderSettingDTO.COSId != 0)
            {
                insertCommand.Parameters.AddWithValue("@COSId", customerOrderSettingDTO.COSId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@COSId", 0);
            }
            if (customerOrderSettingDTO.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", customerOrderSettingDTO.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }

            if (customerOrderSettingDTO.ReferenceNoAuto)
            {
                insertCommand.Parameters.AddWithValue("@ReferenceNoAuto", customerOrderSettingDTO.ReferenceNoAuto);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ReferenceNoAuto", 0);
            }
            if (customerOrderSettingDTO.UseItemGroups)
            {
                insertCommand.Parameters.AddWithValue("@UseItemGroups", customerOrderSettingDTO.UseItemGroups);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UseItemGroups", 0);
            }
            if (customerOrderSettingDTO.UseItemGroupSeparatedFreight)
            {
                insertCommand.Parameters.AddWithValue("@UseItemGroupSeparatedFreight", customerOrderSettingDTO.UseItemGroupSeparatedFreight);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UseItemGroupSeparatedFreight", 0);
            }
            if (customerOrderSettingDTO.RequestNewProducts)
            {
                insertCommand.Parameters.AddWithValue("@RequestNewProducts", customerOrderSettingDTO.RequestNewProducts);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@RequestNewProducts", 0);
            }
            if (customerOrderSettingDTO.Desgination)
            {
                insertCommand.Parameters.AddWithValue("@Desgination", customerOrderSettingDTO.Desgination);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Desgination", 0);
            }
            if (customerOrderSettingDTO.Addresses != 0)
            {
                insertCommand.Parameters.AddWithValue("@Addresses", customerOrderSettingDTO.Addresses);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Addresses", 0);
            }
            if (customerOrderSettingDTO.Approver != 0)
            {
                insertCommand.Parameters.AddWithValue("@Approver", customerOrderSettingDTO.Approver);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Approver", 0);
            }
            if (customerOrderSettingDTO.LevelofAuthority != 0)
            {
                insertCommand.Parameters.AddWithValue("@LevelofAuthority", customerOrderSettingDTO.LevelofAuthority);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@LevelofAuthority", 0);
            }
            if (customerOrderSettingDTO.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", customerOrderSettingDTO.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }
            insertCommand.Parameters.Add("@COSIdout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@COSIdout"].Direction = ParameterDirection.Output;

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

                if (count != 0 && customerOrderSettingDTO.COSId == 0)
                {
                    customerOrderSettingDTO.COSId = Convert.ToInt32(insertCommand.Parameters["@COSIdout"].Value);
                }

                return customerOrderSettingDTO;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return customerOrderSettingDTO;
            }
            finally
            {
                connection.Close();
            }



        }
        public List<dynamic> GetCustomerSettingDetails(Int64 CustID)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetCustSettingOrder(CustID);
            var myEnumerable = ds.Tables[0].AsEnumerable();

            List<CustomerOrderSetting> CustomerOrderSetting =
                (from item in myEnumerable
                 select new CustomerOrderSetting
                 {
                     COSId = item.Field<Int64>("COSId"),
                      CustId = item.Field<Int64>("CustId"),
                     ReferenceNoAuto = item.Field<Boolean>("ReferenceNoAuto"),
                     UseItemGroups = item.Field<Boolean>("UseItemGroups"),
                     UseItemGroupSeparatedFreight = item.Field<Boolean>("UseItemGroupSeparatedFreight"),
                     RequestNewProducts = item.Field<Boolean>("RequestNewProducts"),
                     Desgination = item.Field<Boolean>("Desgination"),
                     Addresses =  item.Field<int>("Addresses"),
                     Approver = item.Field<int>("Approver"),
                     LevelofAuthority =  item.Field<int>("LevelofAuthority") + 1 ,
                     Type = 2
                 }).ToList();
            objDynamic.Add(CustomerOrderSetting);

            return objDynamic;
        }

        private DataSet GetCustSettingOrder(Int64 CustID)
        {

            string selectProcedure = "[GetCustSettingOrder]";
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

        public List<dynamic> GetCustSettingOrderDetails(Int64 CustID)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetCustSettingOrder(CustID);
            var myEnumerablecat = ds.Tables[0].AsEnumerable();
            try
            {
                List<CustomerSettings> CustomerSettings =
                    (from item in myEnumerablecat
                     select new CustomerSettings
                     {
                         CustomerId = item.Field<Int64>("CustId"),
                         Reference = item.Field<bool>("ReferenceNoAuto"),
                         UserItem = item.Field<bool>("UseItemGroups"),

                         UserItemSp = item.Field<bool>("UseItemGroupSeparatedFreight"),
                         RequestNew = item.Field<bool>("RequestNewProducts"),
                         Desgination = item.Field<bool>("Desgination"),

                         Addresses = item.Field<int>("Addresses"),
                         Approver = item.Field<int>("Approver"),
                         No_Approver = item.Field<int>("LevelofAuthority") +1 ,

                     }).ToList();
                objDynamic.Add(CustomerSettings);
            }
            catch(Exception ex)
            { }
            return objDynamic;
        }
            public List<dynamic> AddUpdateCustomerSetting(CustomerSettings customerSettings)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            CustomerOrderSetting customerOrderSetting = new CustomerOrderSetting();
            customerOrderSetting.CustId = customerSettings.CustomerId;
            customerOrderSetting.ReferenceNoAuto = customerSettings.Reference;
            customerOrderSetting.Addresses = customerSettings.Addresses;
            
            customerOrderSetting.UseItemGroups = customerSettings.UserItem;
            customerOrderSetting.UseItemGroupSeparatedFreight = customerSettings.UserItemSp;
            customerOrderSetting.RequestNewProducts = customerSettings.RequestNew;
            switch(customerSettings.Approver)
            {
                case 0:
                    {
                        customerOrderSetting.Approver = 0;
                        customerOrderSetting.LevelofAuthority = 0;
                        break;
                    }

                    case 1:
                    {
                        customerOrderSetting.Approver = 1;
                        customerOrderSetting.LevelofAuthority = 1;
                        break;
                    }

                case 2:
                    {
                        customerOrderSetting.Approver = customerSettings.No_Approver;
                        customerOrderSetting.LevelofAuthority = customerSettings.No_Approver - 1;
                        break;
                    }
            }

            customerOrderSetting.Type = 1;
            customerOrderSetting = AddUpdateCustomerOrderSetting(customerOrderSetting);
            objDynamic.Add(customerOrderSetting.COSId);





            return objDynamic;
        }
            private int UpdateCustomerOrderSetting(CustomerOrderSetting customerOrderSettingDTO)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            Log log = new Log();
            string updateProcedure = "[UpdateCustomerOrderSetting]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            int COSId = 0;
            if (customerOrderSettingDTO.COSId != 0)
            {
                updateCommand.Parameters.AddWithValue("@COSId", customerOrderSettingDTO.COSId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@COSId", 0);
            }
            if (customerOrderSettingDTO.CustId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CustId", customerOrderSettingDTO.CustId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CustId", 0);
            }

            if (customerOrderSettingDTO.ReferenceNoAuto)
            {
                updateCommand.Parameters.AddWithValue("@ReferenceNoAuto ", customerOrderSettingDTO.ReferenceNoAuto);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ReferenceNoAuto", 0);
            }
            if (customerOrderSettingDTO.UseItemGroups)
            {
                updateCommand.Parameters.AddWithValue("@UseItemGroups", customerOrderSettingDTO.UseItemGroups);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@UseItemGroups ", 0);
            }
            if (customerOrderSettingDTO.UseItemGroupSeparatedFreight)
            {
                updateCommand.Parameters.AddWithValue("@UseItemGroupSeparatedFreight ", customerOrderSettingDTO.UseItemGroupSeparatedFreight);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@UseItemGroupSeparatedFreight ", 0);
            }
            if (customerOrderSettingDTO.RequestNewProducts)
            {
                updateCommand.Parameters.AddWithValue("@RequestNewProducts ", customerOrderSettingDTO.RequestNewProducts);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@RequestNewProducts ", 0);
            }
            if (customerOrderSettingDTO.Desgination)
            {
                updateCommand.Parameters.AddWithValue("@Desgination ", customerOrderSettingDTO.Desgination);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Desgination ", 0);
            }
            if (customerOrderSettingDTO.Addresses != 0)
            {
                updateCommand.Parameters.AddWithValue("@Addresses ", customerOrderSettingDTO.Addresses);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Addresses ", 0);
            }
            if (customerOrderSettingDTO.Approver != 0)
            {
                updateCommand.Parameters.AddWithValue("@Approver ", customerOrderSettingDTO.Approver);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Approver ", 0);
            }
            if (customerOrderSettingDTO.LevelofAuthority != 0)
            {
                updateCommand.Parameters.AddWithValue("@LevelofAuthority ", customerOrderSettingDTO.LevelofAuthority);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@LevelofAuthority ", 0);
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


                return COSId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return COSId;
            }
            finally
            {
                connection.Close();
            }



        }
    }
}