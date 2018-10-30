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
    
    public class ShipData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();
        private DataSet GetOrderShipmentData(Int64 OrderId)
        {

            string selectProcedure = "[GetOrderShipment]";
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

        private DataSet GetOrderShipmentDetailsData(Int64 ShipmentDetailId)
        {

            string selectProcedure = "[GetOrderShipmentDetails]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@ShipmentId", ShipmentDetailId);
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


        //get new
        private DataSet GetNewOrderShipmentData(Int64 OrderId)
        {

            string selectProcedure = "[GetNewOrderShipmentDetails]";
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




        public List<dynamic> GetOrderShipment(string strOrderID)
        {

            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetOrderShipmentData(Convert.ToInt64(strOrderID));

            var myEnumerableApr = ds.Tables[0].AsEnumerable();
         
            List<OrderShipmentDTO> OrderShipmentDTO =
               (from item in myEnumerableApr
                select new OrderShipmentDTO
                {
                  ShipmentId = item.Field<Int64>("ShipmentId"),
                  Title = item.Field<String>("Title"),
                  ShipmentDate = item.Field<string>("ShipmentDate"),
                  DocumentGroupId = item.Field<Int64>("DocumentGroupId")



                }).ToList();
            objDynamic.Add(OrderShipmentDTO);

          

            return objDynamic;
        }

        public List<dynamic> GetOrderShipmentDetails(Int64 ShipmentDetailId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetOrderShipmentDetailsData(ShipmentDetailId);

            var myEnumerableApr = ds.Tables[0].AsEnumerable();
            var myEnumerablecustApr = ds.Tables[1].AsEnumerable();

            List<OrderShipmentDetailsDTO> OrderShipmentDetailsDTO =
               (from item in myEnumerableApr
                select new OrderShipmentDetailsDTO
                {
                    ShipmentDetailId = item.Field<Int64>("ShipmentDetailId"),
                    ShipmentId = item.Field<Int64>("ShipmentId"),
                    ODID = item.Field<Int64>("ODID"),
                    PartNo = item.Field<string>("PartNo"),
                    Quantity = item.Field<int>("ActualQty"),
                    ToShip = item.Field<int>("ShippedQty"),
                    BalanceQty = item.Field<int>("BalanceQty"),

                }).ToList();
            objDynamic.Add(OrderShipmentDetailsDTO);

            List<OrderShipmentInfoDTO> OrderShipmentInfoDTO =
              (from item in myEnumerablecustApr
               select new OrderShipmentInfoDTO
               {
                   ShipmentInfoId = item.Field<Int64>("ShipmentInfoId"),
                   ShipmentId = item.Field<Int64>("ShipmentId"),
                   CarrierIdx = item.Field<Int64>("CarrierId"),
                   Waybill = item.Field<String>("Waybill"),
                   DeliveryDate = item.Field<string>("DeliveryDate"),



               }).ToList();
            objDynamic.Add(OrderShipmentInfoDTO);

            return objDynamic;
        }






        //get new shipment data
        public List<dynamic> GetNewOrderShipment(string strOrderID)
        {

            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetNewOrderShipmentData(Convert.ToInt64(strOrderID));

            var myEnumerableApr = ds.Tables[0].AsEnumerable();

            List<OrderShipmentDetailsDTO> OrderShipmentDetailsDTO =
               (from item in myEnumerableApr
                select new OrderShipmentDetailsDTO
                {
                    ODID = item.Field<Int64>("ODID"),
                    PartNo = item.Field<String>("PartNo"),
                    Quantity = item.Field<int>("ActualQty"),
                    BalanceQty = item.Field<int>("BalQty")



                }).ToList();
            objDynamic.Add(OrderShipmentDetailsDTO);



            return objDynamic;
        }

    }
}