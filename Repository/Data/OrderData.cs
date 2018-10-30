using System;
using System.Data;
using System.Data.SqlClient;
using MyProject.Models;
using MyProject.Data;
using MyProject.Repository.Library;
using MyProject.Repository.Security;
using System.Collections.Generic;
namespace MyProject.Repository.Data
{
    public class OrderData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        public SaveTempOrder AddOrder(Order order)
        {
            SaveTempOrder SaveTempOrder = new SaveTempOrder();
            int orderId = 0;
            string insertProcedure = "[CreatNewOrderTemp]";
            if (order.OrderId == 0)
            {
                 insertProcedure = "[CreatNewOrderTemp]";
            }
            else
            {
                insertProcedure = "[UpdateNewOrderTemp]";
            }
            
              SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (order.OrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderID", order.OrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderID", 0);
            }

            if (!string.IsNullOrEmpty(order.ReferenceNo))
            {
                insertCommand.Parameters.AddWithValue("@ReferenceNo", order.ReferenceNo);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ReferenceNo", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(order.Department))
            {
                insertCommand.Parameters.AddWithValue("@Department", order.Department);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Department", DBNull.Value);
            }
            if (order.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", order.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (order.UserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", order.UserId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (order.BranchId != 0)
            {
                insertCommand.Parameters.AddWithValue("@BranchId", order.BranchId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BranchId", 0);
            }
            if (order.IncoTermId != 0)
            {
                insertCommand.Parameters.AddWithValue("@IncoTermId", order.IncoTermId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IncoTermId", 0);
            }

            if (order.StatusId != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatusId", order.StatusId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatusId", 0);
            }

            if (!string.IsNullOrEmpty(order.ShippingAddress))
            {
                insertCommand.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ShippingAddress", DBNull.Value);
            }
            if (order.CountryId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CountryId", order.CountryId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CountryId", 0);
            }

            if (!string.IsNullOrEmpty(order.CountryName))
            {
                insertCommand.Parameters.AddWithValue("@CountryName", order.CountryName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CountryName", DBNull.Value);
            }
            if (order.CityId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CityId", order.CityId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CityId", 0);
            }

            if (!string.IsNullOrEmpty(order.CityName))
            {
                insertCommand.Parameters.AddWithValue("@CityName", order.CityName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CityName", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(order.BillingAddress))
            {
                insertCommand.Parameters.AddWithValue("@BillingAddress", order.BillingAddress);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BillingAddress", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(order.CustomerComments))
            {
                insertCommand.Parameters.AddWithValue("@CustomerComments", order.CustomerComments);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustomerComments", DBNull.Value);
            }

            if (order.Feight != 0)
            {
                insertCommand.Parameters.AddWithValue("@Feight", order.Feight);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Feight", 0);
            }

            if (order.IsSalesOrder != false)
            {
                insertCommand.Parameters.AddWithValue("@IsSalesOrder", order.IsSalesOrder);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsSalesOrder", 0);
            }
            if (order.SalesOrderNo != null)
            {
                insertCommand.Parameters.AddWithValue("@SalesOrderNo", order.SalesOrderNo);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@SalesOrderNo", DBNull.Value);
            }

            if (order.ContactName != null)
            {
                insertCommand.Parameters.AddWithValue("@ContactName", order.ContactName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ContactName", DBNull.Value);
            }

            if (order.ContactNum != null)
            {
                insertCommand.Parameters.AddWithValue("@ContactNum", order.ContactNum);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ContactNum", DBNull.Value);
            }

            if (order.ContactEmail != null)
            {
                insertCommand.Parameters.AddWithValue("@ContactEmail", order.ContactEmail);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ContactEmail", DBNull.Value);
            }
            if (order.isClosed != false)
            {
                insertCommand.Parameters.AddWithValue("@isClosed", order.isClosed);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isClosed", 0);
            }
            if (order.isDeleted != false)
            {
                insertCommand.Parameters.AddWithValue("@isDeleted", order.isDeleted);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isDeleted", 0);
            }

            if (!string.IsNullOrEmpty(order.DeletedReason))
            {
                insertCommand.Parameters.AddWithValue("@DeletedReason", order.isDeleted);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@DeletedReason", DBNull.Value);
            }
            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@OrderIDout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@OrderIDout"].Direction = ParameterDirection.Output;
            try
            {
                int count = 0;
                connection.Open();
                insertCommand.ExecuteNonQuery();
                if (insertCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                     count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                    SaveTempOrder.RetVal = count;
                }
                if (order.OrderId == 0 )
                {
                    if (insertCommand.Parameters["@OrderIDout"].Value != DBNull.Value)
                    {
                        orderId = System.Convert.ToInt32(insertCommand.Parameters["@OrderIDout"].Value);
                        SaveTempOrder.TempOrderID = orderId;
                    }
                }
               
              


                return SaveTempOrder;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("OrderData.AddOrder");
                log.logErrorMessage(ex.StackTrace);
                return SaveTempOrder;
            }
            finally
            {
                connection.Close();
            }
        }

        public int AddApprover(OrderApprover orderOrderApprover)
        {
            int orderAId = 0;
            // string insertProcedure = "[CreatOrderApprover]";
            string insertProcedure = "[CreatOrderApproverTemp]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (orderOrderApprover.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial", orderOrderApprover.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial", 0);
            }

            if (orderOrderApprover.OrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderId", orderOrderApprover.OrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderId", 0);
            }
            if (!string.IsNullOrEmpty(orderOrderApprover.ApproverName))
            {
                insertCommand.Parameters.AddWithValue("@ApproverName", orderOrderApprover.ApproverName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ApproverName", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(orderOrderApprover.ApproverTitle))
            {
                insertCommand.Parameters.AddWithValue("@ApproverTitle", orderOrderApprover.ApproverTitle);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ApproverTitle", DBNull.Value);
            }


            if (!string.IsNullOrEmpty(orderOrderApprover.ApproverEmail))
            {
                insertCommand.Parameters.AddWithValue("@ApproverEmail", orderOrderApprover.ApproverEmail);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ApproverEmail", DBNull.Value);
            }

            if (orderOrderApprover.UserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", orderOrderApprover.UserId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (!string.IsNullOrEmpty(orderOrderApprover.Comments))
            {
                insertCommand.Parameters.AddWithValue("@Comments", orderOrderApprover.Comments);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Comments", DBNull.Value);
            }


            if (orderOrderApprover.isLoggedin != false)
            {
                insertCommand.Parameters.AddWithValue("@isLoggedin", orderOrderApprover.isLoggedin);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isLoggedin", 0);
            }

            if (orderOrderApprover.isApproved != false)
            {
                insertCommand.Parameters.AddWithValue("@isApproved", orderOrderApprover.isApproved);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isApproved", 0);
            }

            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@OAIdout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@OAIdout"].Direction = ParameterDirection.Output;


            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                orderAId = System.Convert.ToInt32(insertCommand.Parameters["@OAIdout"].Value);


                return orderAId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("OrderData.AddApprover");
                log.logErrorMessage(ex.StackTrace);
                return orderAId;
            }
            finally
            {
                connection.Close();
            }

        }


        public int AddOrderDetail(OrderDetails OrderDetails, Decimal Totalamount)
        {
            int orderDId = 0;
            // string insertProcedure = "[CreateOrderDetails]";
            string insertProcedure = "[CreateOrderDetailsTemp]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (OrderDetails.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial", OrderDetails.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial", 0);
            }


            if (OrderDetails.OrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderId", OrderDetails.OrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderId", 0);
            }


            if (OrderDetails.ProductId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ProductId", OrderDetails.ProductId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProductId", 0);
            }

            if (OrderDetails.Qty != 0)
            {
                insertCommand.Parameters.AddWithValue("@Qty", OrderDetails.Qty);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Qty", 0);
            }

            if (OrderDetails.Rate != 0)
            {
                insertCommand.Parameters.AddWithValue("@Rate", OrderDetails.Rate);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Rate", 0);
            }
            if (OrderDetails.Amount != 0)
            {
                insertCommand.Parameters.AddWithValue("@Amount", OrderDetails.Amount);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Amount", 0);
            }

            if (OrderDetails.ModifiedBy != 0)
            {
                OrderDetails.ModifiedBy = 1;
                insertCommand.Parameters.AddWithValue("@ModifiedBy", OrderDetails.ModifiedBy);
            }
            else
            {
                OrderDetails.ModifiedBy = 1;
                insertCommand.Parameters.AddWithValue("@ModifiedBy", 0);
            }


            if (Totalamount != 0)
            {
                insertCommand.Parameters.AddWithValue("@TotalOrderAmount", Totalamount);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@TotalOrderAmount", 0);
            }

            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@ODIDout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ODIDout"].Direction = ParameterDirection.Output;


            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                orderDId = System.Convert.ToInt32(insertCommand.Parameters["@ODIDout"].Value);


                return orderDId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("OrderData.AddOrderDetail");
                log.logErrorMessage(ex.StackTrace);
                return orderDId;
            }
            finally
            {
                connection.Close();
            }

        }


        public int UpdateOrderDetail(Int64 OrderID)
        {
            int count = 0;
          
            string insertProcedure = "[UpdateTempOrderDetailsData]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (OrderID != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderId", OrderID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderId", 0);
            }



            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

           


            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
               

                return count;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("OrderData.UpdateOrderDetail");
                log.logErrorMessage(ex.StackTrace);
                return count;
            }
            finally
            {
                connection.Close();
            }

        }





        public int UpdateApproverDetail(Int64 OrderID)
        {
            int count = 0;

            string insertProcedure = "[UpdateOrderApproverTemp]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (OrderID != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderId", OrderID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderId", 0);
            }



            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;




            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);


                return count;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("OrderData.UpdateOrderDetail");
                log.logErrorMessage(ex.StackTrace);
                return count;
            }
            finally
            {
                connection.Close();
            }

        }

        public int AddConfirmOrderDetail(Int64 OrderID,Int64 CustID,Int64 UserID,int Type)
        {
            int orderDId = 0;
            // string insertProcedure = "[CreateOrderDetails]";
            string insertProcedure = "[InsertOrder]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (OrderID != 0)
            {
                insertCommand.Parameters.AddWithValue("@TempOrderID", OrderID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@TempOrderID", 0);
            }


            if (CustID != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", CustID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }


            if (UserID != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", UserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }

            if (Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }



            insertCommand.Parameters.Add("@OrderIDout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@OrderIDout"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;


            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                orderDId = System.Convert.ToInt32(insertCommand.Parameters["@OrderIDout"].Value);


                return orderDId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("OrderData.AddConfirmOrderDetail");
                log.logErrorMessage(ex.StackTrace);
                return orderDId;
            }
            finally
            {
                connection.Close();
            }

        }

        public DataSet GetOrderDetail(Int64 CustId, Int64 UserId, Int64 OrderID)
        {

            string selectProcedure = "[GetOrderData]";
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            DataSet ds = new DataSet();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@orderID", OrderID);
            da.SelectCommand.Parameters.AddWithValue("@CustId", CustId);
            da.SelectCommand.Parameters.AddWithValue("@UserId", UserId); 
            DataTable dt = new DataTable();
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("CustRequestData.GetOrderDetail");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
          
        }

       
    }
}