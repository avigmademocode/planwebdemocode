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
    public class OrderShipmentDetailsCRUD
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();

        private int CRUDOrderDetails(OrderShipmentDetailsDTO OrderShipmentDetails)
        {
            string insertProcedure = "[CreateOrderShipmentDetails]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            int ShipmentDetailId = 0;

            if (OrderShipmentDetails.ShipmentDetailId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ShipmentDetailId", OrderShipmentDetails.ShipmentDetailId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ShipmentDetailId", 0);
            }
            if (OrderShipmentDetails.ShipmentId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ShipmentId", OrderShipmentDetails.ShipmentId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ShipmentId", 0);
            }
            if (OrderShipmentDetails.ODID != 0)
            {
                insertCommand.Parameters.AddWithValue("@ODID", OrderShipmentDetails.ODID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ODID", 0);
            }
            if (OrderShipmentDetails.ActualQty != 0)
            {
                insertCommand.Parameters.AddWithValue("@ActualQty", OrderShipmentDetails.ActualQty);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ActualQty", 0);
            }
            if (OrderShipmentDetails.ShippedQty != 0)
            {
                insertCommand.Parameters.AddWithValue("@ShippedQty", OrderShipmentDetails.ShippedQty);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ShippedQty", 0);
            }
            if (OrderShipmentDetails.BalanceQty != 0)
            {
                insertCommand.Parameters.AddWithValue("@BalanceQty", OrderShipmentDetails.BalanceQty);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BalanceQty", 0);
            }
            if (OrderShipmentDetails.isActive)
            {
                insertCommand.Parameters.AddWithValue("@isActive", OrderShipmentDetails.isActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isActive", 0);
            }
            if (Convert.ToInt64(UserID) != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", UserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (OrderShipmentDetails.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", OrderShipmentDetails.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }

            insertCommand.Parameters.Add("@ShipmentDetailIdOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ShipmentDetailIdOut"].Direction = ParameterDirection.Output;

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
                if (count != 0 && OrderShipmentDetails.ShipmentDetailId == 0)
                {
                    OrderShipmentDetails.ShipmentDetailId = Convert.ToInt32(insertCommand.Parameters["@ShipmentDetailIdOut"].Value);
                }

                return ShipmentDetailId;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return ShipmentDetailId;
            }
            finally
            {
                connection.Close();
            }

        }

        public List<dynamic> SaveOrdershipmentDetails(OrderShipmentComman strOrderShipmentDtls, Int64 ShipmentId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
           
            OrderShipmentDetailsDTO orderShipmentDetailsDTO = new OrderShipmentDetailsDTO();
         
            OrderShipmentComman orderShipmentInfoUI = new OrderShipmentComman();
            int orddetid = 0;

            //string strOrderID = securityHelper.Decrypt(strOrderShipmentDtls.strOrderID, false);
            if (!string.IsNullOrEmpty(strOrderShipmentDtls.Ordershipmentinfo))
            {


                var Data = JsonConvert.DeserializeObject<List<OrderShipmentComman>>(strOrderShipmentDtls.ShipmentDetails);
                for (int i = 0; i < Data.Count; i++)
                {
                    orderShipmentInfoUI = Data[i];
                     
                    if(orderShipmentInfoUI.ShipmentDetailId != 0)
                    {
                        orderShipmentDetailsDTO.ShipmentDetailId = orderShipmentInfoUI.ShipmentDetailId;
                    }

                    if (ShipmentId != 0)
                    {
                        orderShipmentDetailsDTO.ShipmentId = ShipmentId;
                    }

                    if (orderShipmentInfoUI.ODID != 0)
                    {
                        orderShipmentDetailsDTO.ODID = orderShipmentInfoUI.ODID;
                    }

                    if (orderShipmentInfoUI.Quantity != 0)
                    {
                        orderShipmentDetailsDTO.ActualQty = orderShipmentInfoUI.Quantity;
                    }

                    
                    orderShipmentDetailsDTO.ShippedQty = orderShipmentInfoUI.ToShip;

                    orderShipmentDetailsDTO.BalanceQty = orderShipmentInfoUI.BalanceQty;

                   

                    orderShipmentDetailsDTO.isActive = strOrderShipmentDtls.isActive;
                    orderShipmentDetailsDTO.Type = strOrderShipmentDtls.Type;

                    CRUDOrderDetails(orderShipmentDetailsDTO);

                }
                objDynamic.Add(orddetid);
            }
            return objDynamic;

        }
    }
}