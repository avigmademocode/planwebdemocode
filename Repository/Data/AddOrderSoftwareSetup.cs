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
    public class AddOrderSoftwareSetup
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        string UserID = HttpContext.Current.Session["UserId"].ToString();

        private int CRUDorderSoftwaresetup(OrderSoftwareSetupDTO orderSofrwaresetup)
        {
            string insertProcedure = "[CreateOrderSoftwareSetup]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            int SoftwareSetupId = 0;

            if (orderSofrwaresetup.SoftwareSetupId != 0)
            {
                insertCommand.Parameters.AddWithValue("@SoftwareSetupId", orderSofrwaresetup.SoftwareSetupId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@SoftwareSetupId", 0);
            }
            if (orderSofrwaresetup.OrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderId", orderSofrwaresetup.OrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderId", 0);
            }
            if (orderSofrwaresetup.ProductId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ProductId", orderSofrwaresetup.ProductId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProductId", 0);
            }
            if (orderSofrwaresetup.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial", orderSofrwaresetup.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial", 0);
            }
            if (!string.IsNullOrEmpty(orderSofrwaresetup.UserName))
            {
                insertCommand.Parameters.AddWithValue("@UserName", orderSofrwaresetup.UserName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserName", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(orderSofrwaresetup.UserEmail))
            {
                insertCommand.Parameters.AddWithValue("@UserEmail", orderSofrwaresetup.UserEmail);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserEmail", DBNull.Value);
            }
            
            if (orderSofrwaresetup.isActive)
            {
                insertCommand.Parameters.AddWithValue("@isActive", orderSofrwaresetup.isActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isActive", 0);
            }
            if (Convert.ToInt64(UserID) != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", Convert.ToInt64(UserID));
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (orderSofrwaresetup.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", orderSofrwaresetup.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }



            insertCommand.Parameters.Add("@SoftwareSetUpIdOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@SoftwareSetUpIdOut"].Direction = ParameterDirection.Output;

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
                if (count != 0 && orderSofrwaresetup.SoftwareSetupId == 0)
                {
                    orderSofrwaresetup.SoftwareSetupId = Convert.ToInt32(insertCommand.Parameters["@SoftwareSetUpIdOut"].Value);
                }

                return SoftwareSetupId;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return SoftwareSetupId;
            }
            finally
            {
                connection.Close();
            }

        }
        //Order Software setup
        public List<dynamic> AddOrderSoftwareSetUp(OrderSoftwareSetupUI OrderSoftwareSetupUI)
        {
            List<dynamic> ObjDynamic = new List<dynamic>();
            OrderSoftwareSetupDTO orderSoftwareSetupDTO = new OrderSoftwareSetupDTO();

            string strOrderID = securityHelper.Decrypt(OrderSoftwareSetupUI.strOrderID, false);
            try
            {
                orderSoftwareSetupDTO.SoftwareSetupId = OrderSoftwareSetupUI.SoftwareSetupId;
                orderSoftwareSetupDTO.OrderId = Convert.ToInt64(strOrderID);
                orderSoftwareSetupDTO.ProductId = OrderSoftwareSetupUI.ProductId;
                orderSoftwareSetupDTO.Type = OrderSoftwareSetupUI.Type;
                orderSoftwareSetupDTO.Serial = OrderSoftwareSetupUI.Serial;
                orderSoftwareSetupDTO.UserName = OrderSoftwareSetupUI.UserName;
                orderSoftwareSetupDTO.UserEmail = OrderSoftwareSetupUI.UserEmail;
                orderSoftwareSetupDTO.isActive = OrderSoftwareSetupUI.isActive;

                CRUDorderSoftwaresetup(orderSoftwareSetupDTO);
                ObjDynamic.Add(orderSoftwareSetupDTO);
                return ObjDynamic;
            }
            catch (Exception ex)
            {

                return ObjDynamic;
            }







        }



        private DataSet GetSoftSetupSPcallData(Int64 OrderId)
        {
            string selectProcedure = "[GetOrderSoftwareSetup]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@OrderId", OrderId);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("BudgetCodeWiseOrderDetailData.GetCustBudgetData");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }


        public List<dynamic> GetSoftSetup(string strOrderID)
        {

            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetSoftSetupSPcallData(Convert.ToInt64(strOrderID));

            var myEnumerableApr = ds.Tables[0].AsEnumerable();
          try { 
            List<OrderSoftwareSetupDTO> OrderSoftwareSetupDTO =
               (from item in myEnumerableApr
                select new OrderSoftwareSetupDTO
                {
                    SoftwareSetupId = item.Field<Int64>("SoftwareSetupId"),
                    //OrderId = item.Field<Int64>("OrderId"),
                    Serial = item.Field<int>("Serial"),
                    UserName = item.Field<String>("UserName"),
                    UserEmail = item.Field<String>("UserEmail"),
                    ProductId = item.Field<Int64>("ProductId"),
                    PartNo = item.Field<String>("PartNo"),
                    
                   
                }).ToList();
            objDynamic.Add(OrderSoftwareSetupDTO);

          }catch(Exception ex)
          {
                return objDynamic;
          }


            return objDynamic;
        }
    }
}