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
    public class OrderShipmentCRUD
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();

        private int CRUDOrderShipment(OrderShipmentDTO OrderShipment)
        {
            string insertProcedure = "[CreateOrderShipment]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            int ShipmentId = 0;

            if (OrderShipment.ShipmentId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ShipmentId", OrderShipment.ShipmentId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ShipmentId", 0);
            }
            if (!string.IsNullOrEmpty(OrderShipment.Title))
            {
                insertCommand.Parameters.AddWithValue("@Title", OrderShipment.Title);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Title", DBNull.Value);
            }
            if (OrderShipment.ShipmentDateDB != null)
            {
                insertCommand.Parameters.AddWithValue("@ShipmentDate", OrderShipment.ShipmentDateDB);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ShipmentDate", DBNull.Value);
            }
            if (OrderShipment.OrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderId", OrderShipment.OrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderId", 0);
            }
            if (OrderShipment.isActive)
            {
                insertCommand.Parameters.AddWithValue("@isActive", OrderShipment.isActive);
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
            if (OrderShipment.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", OrderShipment.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }
            if (OrderShipment.DocumentGroupId != 0)
            {
                insertCommand.Parameters.AddWithValue("@DocumentGroupId", OrderShipment.DocumentGroupId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@DocumentGroupId", 0);
            }

            insertCommand.Parameters.Add("@ShipmentIdOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ShipmentIdOut"].Direction = ParameterDirection.Output;

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
                if (count != 0 && OrderShipment.ShipmentId == 0)
                {
                    OrderShipment.ShipmentId = Convert.ToInt32(insertCommand.Parameters["@ShipmentIdOut"].Value);
                }

                return ShipmentId;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return ShipmentId;
            }
            finally
            {
                connection.Close();
            }

        }
        //Order all
        public List<dynamic> AddOrderShipment(OrderShipmentComman OrderShipmentUI)
        {

            OrderShipmentInfoCRUD orderShipmentInfoCRUD = new OrderShipmentInfoCRUD();

            OrderShipmentDetailsCRUD orderShipmentDetailsCRUD = new OrderShipmentDetailsCRUD();


            List<dynamic> ObjDynamic = new List<dynamic>();
            OrderShipmentDTO OrderShipment = new OrderShipmentDTO();

            string strOrderID = securityHelper.Decrypt(OrderShipmentUI.strOrderID, false);


            OrderShipment.Type = OrderShipmentUI.Type;
            OrderShipment.DocumentGroupId = OrderShipmentUI.DocumentGroupId;

            OrderShipment.ShipmentId = OrderShipmentUI.ShipmentId;
            OrderShipment.Title = OrderShipmentUI.Title;
            OrderShipment.ShipmentDateDB = OrderShipmentUI.ShipmentDate;
            OrderShipment.OrderId = Convert.ToInt64(strOrderID);
            OrderShipment.isActive = OrderShipmentUI.isActive;


            CRUDOrderShipment(OrderShipment);

            orderShipmentInfoCRUD.SaveOrdershipmentinfo(OrderShipmentUI, OrderShipment.ShipmentId);

            orderShipmentDetailsCRUD.SaveOrdershipmentDetails(OrderShipmentUI, OrderShipment.ShipmentId);


            ObjDynamic.Add(OrderShipment);
            if (OrderShipmentUI.SendEmail)
            {
                CustRequestData custRequestData = new CustRequestData();
                custRequestData.GetEmailDetails(strOrderID, OrderShipmentUI.SendEmailID,"99");
            }
            return ObjDynamic;

        }

        
    }
}