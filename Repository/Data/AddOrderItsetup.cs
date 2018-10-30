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
    public class AddOrderItsetup
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        string UserID = HttpContext.Current.Session["UserId"].ToString();

        private DataSet GetCustomerUserTypeDataAndWotkLoc()
        {

            string selectProcedure = "[GetCustomerUserType]";
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            DataSet ds = new DataSet();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;


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

        public List<dynamic> GetCustomerUserTypeDetails()
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetCustomerUserTypeDataAndWotkLoc();
            try
            {
                List<CustomerUserType> CustomerUserType = null;
                if (ds.Tables[0].Rows.Count > 0)
                {

                    var myEnumerablerole = ds.Tables[0].AsEnumerable();
                    CustomerUserType =
                       (from item in myEnumerablerole
                        select new CustomerUserType
                        {
                            UserTypeId = item.Field<Int64>("UserTypeId"),
                            CustId = item.Field<Int64>("CustId"),
                            UserType = item.Field<String>("UserType"),

                        }).ToList();
                    objDynamic.Add(CustomerUserType);


                    var myEnumerableApr = ds.Tables[1].AsEnumerable();

                    List<CustomerWorkLocation> CustomerWorkLocation =
                       (from item in myEnumerableApr
                        select new CustomerWorkLocation
                        {
                            WorkLocationId = item.Field<Int64>("WorkLocationId"),
                            WorkLocation = item.Field<String>("WorkLocation"),
                            Custid = item.Field<Int64>("Custid")

                        }).ToList();
                    objDynamic.Add(CustomerWorkLocation);

                }


            }
            catch (Exception ex)
            {

            }

            return objDynamic;
        }



        //get shipment inf0
        private DataSet GetITShipmentData(Int64 OrderId)
        {

            string selectProcedure = "[GetITsetupshipmentinfo]";
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

        public List<dynamic> GetITsetupShipment(string strOrderID)
        {
         

           
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetITShipmentData(Convert.ToInt64(strOrderID));

            var myEnumerableAprx = ds.Tables[0].AsEnumerable();
            try
            {
                List<ItShipmemntInfo> ItShipmemntInfo =
               (from item in myEnumerableAprx
                select new ItShipmemntInfo
                {
                    Waybill = item.Field<String>("Waybill"),
                    DeliveryDate = item.Field<String>("DeliveryDate"),
                    ShipmentId = item.Field<Int64>("ShipmentId"),
                    ShipmentDate = item.Field<String>("ShipmentDate"),
                    OrderId = item.Field<Int64>("OrderId"),
                    Carrier = item.Field<String>("Carrier"),



                }).ToList();
               objDynamic.Add(ItShipmemntInfo);
                return objDynamic;
            }
            catch (Exception ex)
            {
                return objDynamic;
            }

           
        }




        ///insert data orderItsetup
        ///
        private int CRUDorderItsetup(OrderItsetupDTO orderItsetup)
        {
            string insertProcedure = "[CreateOrderITSetup]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            int ITSetUpId = 0;

            if (orderItsetup.ITSetUpId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ITSetUpId", orderItsetup.ITSetUpId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ITSetUpId", 0);
            }
            if (orderItsetup.OrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderId", orderItsetup.OrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderId", 0);
            }
            if (orderItsetup.ProductId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ProductId", orderItsetup.ProductId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProductId", 0);
            }
            if (orderItsetup.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial", orderItsetup.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial", 0);
            }
            if (!string.IsNullOrEmpty(orderItsetup.UserName))
            {
                insertCommand.Parameters.AddWithValue("@UserName", orderItsetup.UserName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserName", DBNull.Value);
            }
            if (orderItsetup.UserTypeId != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserTypeId", orderItsetup.UserTypeId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserTypeId", 0);
            }
            if (orderItsetup.WorkLocationId != 0)
            {
                insertCommand.Parameters.AddWithValue("@WorkLocationId", orderItsetup.WorkLocationId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@WorkLocationId", 0);
            }
            if (!string.IsNullOrEmpty(orderItsetup.DeliveryLocation))
            {
                insertCommand.Parameters.AddWithValue("@DeliveryLocation", orderItsetup.DeliveryLocation);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@DeliveryLocation", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(orderItsetup.Department))
            {
                insertCommand.Parameters.AddWithValue("@Department", orderItsetup.Department);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Department", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(orderItsetup.Applications))
            {
                insertCommand.Parameters.AddWithValue("@Applications", orderItsetup.Applications);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Applications", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(orderItsetup.SpecialInstructions))
            {
                insertCommand.Parameters.AddWithValue("@SpecialInstructions", orderItsetup.SpecialInstructions);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@SpecialInstructions", DBNull.Value);
            }
            if (orderItsetup.MigrateInfo != 0)
            {
                insertCommand.Parameters.AddWithValue("@MigrateInfo", orderItsetup.MigrateInfo);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@MigrateInfo", 0);
            }
            if (orderItsetup.isActive)
            {
                insertCommand.Parameters.AddWithValue("@isActive", orderItsetup.isActive);
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
            if (orderItsetup.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", orderItsetup.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }



            insertCommand.Parameters.Add("@ITSetUpIdOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ITSetUpIdOut"].Direction = ParameterDirection.Output;

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
                if (count != 0 && orderItsetup.ITSetUpId == 0)
                {
                    orderItsetup.ITSetUpId = Convert.ToInt32(insertCommand.Parameters["@ITSetUpIdOut"].Value);
                }

                return ITSetUpId;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return ITSetUpId;
            }
            finally
            {
                connection.Close();
            }

        }
        //Order IT setup
        public List<dynamic> AddOrderITSetUp(OrderItsetupUI OrderItsetupUI)
        {
            List<dynamic> ObjDynamic = new List<dynamic>();
            OrderItsetupDTO orderItsetupDTO = new OrderItsetupDTO();

            string strOrderID = securityHelper.Decrypt(OrderItsetupUI.strOrderID, false);
          try
            {
            orderItsetupDTO.ITSetUpId = OrderItsetupUI.ITSetUpId;
            orderItsetupDTO.OrderId = Convert.ToInt64(strOrderID);
            orderItsetupDTO.ProductId = OrderItsetupUI.ProductId;
            orderItsetupDTO.Type = OrderItsetupUI.Type;
            orderItsetupDTO.Serial = OrderItsetupUI.Serial;
            orderItsetupDTO.UserName = OrderItsetupUI.UserName;
            orderItsetupDTO.UserTypeId = OrderItsetupUI.UserTypeId;
            orderItsetupDTO.WorkLocationId = OrderItsetupUI.WorkLocationId;
            orderItsetupDTO.DeliveryLocation = OrderItsetupUI.DeliveryLocation;
            orderItsetupDTO.Department = OrderItsetupUI.Department;
            orderItsetupDTO.Applications = OrderItsetupUI.Applications;
            orderItsetupDTO.SpecialInstructions = OrderItsetupUI.SpecialInstructions;
            orderItsetupDTO.MigrateInfo = OrderItsetupUI.MigrateInfo;
            orderItsetupDTO.isActive = OrderItsetupUI.isActive;

            CRUDorderItsetup(orderItsetupDTO);
            ObjDynamic.Add(orderItsetupDTO);
                return ObjDynamic;
          }
          catch (Exception ex)
          {

                return ObjDynamic;
          }
        }
    }
}