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
    public class OrderShipmentInfoCRUD
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();

        private int CRUDOrderShipmentInfo(OrderShipmentInfoDTO OrderShipmentInfo)
        {
            string insertProcedure = "[CreateOrderShipmentInfo]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            int ShipmentInfoId = 0;

            if (OrderShipmentInfo.ShipmentInfoId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ShipmentInfoId", OrderShipmentInfo.ShipmentInfoId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ShipmentInfoId", 0);
            }
            if (OrderShipmentInfo.CarrierId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CarrierId", OrderShipmentInfo.CarrierId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CarrierId", 0);
            }

            if (!string.IsNullOrEmpty(OrderShipmentInfo.Waybill))
            {
                insertCommand.Parameters.AddWithValue("@Waybill", OrderShipmentInfo.Waybill);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Waybill", DBNull.Value);
            }

            if(OrderShipmentInfo.DeliveryDateDB != null)
            {
                insertCommand.Parameters.AddWithValue("@DeliveryDate", OrderShipmentInfo.DeliveryDateDB);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@DeliveryDate", DBNull.Value);
            }
            if (OrderShipmentInfo.ShipmentId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ShipmentId", OrderShipmentInfo.ShipmentId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ShipmentId", 0);
            }
            if (OrderShipmentInfo.isActive)
            {
                insertCommand.Parameters.AddWithValue("@isActive", OrderShipmentInfo.isActive);
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
            if (OrderShipmentInfo.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", OrderShipmentInfo.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }

            insertCommand.Parameters.Add("@ShipmentInfoIdOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ShipmentInfoIdOut"].Direction = ParameterDirection.Output;

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
                if (count != 0 && OrderShipmentInfo.@ShipmentInfoId == 0)
                {
                    OrderShipmentInfo.ShipmentInfoId = Convert.ToInt32(insertCommand.Parameters["@ShipmentInfoIdOut"].Value);
                }

                return ShipmentInfoId;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return ShipmentInfoId;
            }
            finally
            {
                connection.Close();
            }

        }
    

        public List<dynamic> SaveOrdershipmentinfo(OrderShipmentComman strOrderShipmentinfo, Int64 ShipmentId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            
            OrderShipmentInfoDTO orderShipmentInfoDTO = new OrderShipmentInfoDTO();
          
            OrderShipmentComman orderShipmentInfoUI = new OrderShipmentComman();
            int orddetid = 0;

            if (!string.IsNullOrEmpty(strOrderShipmentinfo.Ordershipmentinfo))
            {
                var Data = JsonConvert.DeserializeObject<List<OrderShipmentComman>>(strOrderShipmentinfo.Ordershipmentinfo);
                for (int i = 0; i < Data.Count; i++)
                {
                    orderShipmentInfoUI = Data[i];

                    if(orderShipmentInfoUI.ShipmentInfoId != 0)
                    {
                        orderShipmentInfoDTO.ShipmentInfoId = orderShipmentInfoUI.ShipmentInfoId;
                    }
                    if(orderShipmentInfoUI.CarrierIdx != 0)
                    {
                        orderShipmentInfoDTO.CarrierId = orderShipmentInfoUI.CarrierIdx;
                    }
                    if (!string.IsNullOrEmpty(orderShipmentInfoUI.Waybill))
                    {
                        orderShipmentInfoDTO.Waybill = orderShipmentInfoUI.Waybill;
                    }
                    if (ShipmentId != 0)
                    {
                        orderShipmentInfoDTO.ShipmentId = ShipmentId;
                    }
                    if(orderShipmentInfoUI.DeliveryDate != null)
                    {
                        orderShipmentInfoDTO.DeliveryDateDB = DateTime.Parse(orderShipmentInfoUI.DeliveryDate);
                    }
                    else
                    {
                        orderShipmentInfoDTO.DeliveryDateDB = null;
                    }

                    orderShipmentInfoDTO.isActive = strOrderShipmentinfo.isActive;
                    orderShipmentInfoDTO.Type = strOrderShipmentinfo.Type;

                    CRUDOrderShipmentInfo(orderShipmentInfoDTO);
                }
              objDynamic.Add(orddetid);
            }
            return objDynamic;
        }
    }
}