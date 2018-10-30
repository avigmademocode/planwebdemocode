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
    public class CustOrderData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        string UserID =  HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        private DataSet GetCustOrder(Int64 OrderId,int Type)
        {
            string selectProcedure = string.Empty;
            if (Type == 1)
            {
                selectProcedure = "[GetCustTempOrder]";
            }
            else
            {
                selectProcedure = "[GetCustOrder]";
            }

         
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
                log.logErrorMessage("CustOrderData.GetCustOrder");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }
        public List<dynamic> GetCustOrderList(Int64 OrderId , int Type)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetCustOrder(OrderId,Type);
            var myEnumerable = ds.Tables[0].AsEnumerable();


            List<Order> ord =
                (from item in myEnumerable
                 select new Order
                 {
                     CustId = item.Field<Int64>("CustomerId"),
                     ReferenceNo = item.Field<String>("ReferenceNo"),
                     Department = item.Field<String>("Department"),
                     BranchId = item.Field<Int64>("delivery"),
                     IncoTermId = item.Field<Int64>("IncoTermId"),
                 
                     CityId = item.Field<Int64>("City"),
                     CountryId = item.Field<Int64>("CountryId"),
                     ContactName = item.Field<String>("ContactName"),
                     ContactNum = item.Field<String>("ContactNum"),
                     ContactEmail=item.Field<String>("ContactEmail"),
                    
                 }).ToList();
            objDynamic.Add(ord);


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string shipAddress = ds.Tables[0].Rows[i]["ShippingAddress"].ToString();
                string BillAddress = ds.Tables[0].Rows[i]["BillingAddress"].ToString();
                Array arrshipAddress = shipAddress.Split('#');
                Array arrBillAddress = shipAddress.Split('#');
                objDynamic.Add(arrshipAddress);
                objDynamic.Add(arrBillAddress);
            }
            objDynamic.Add(GetCustOrderAppList(OrderId,Type));
            return objDynamic;
        }
           
        private DataSet GetCustomerOrderDetails(Int64 OrderId)
        {

            string selectProcedure = "[GetCustOrderDetail]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@OrderId",OrderId);

            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("CustOrderData.GetCustomerOrderDetails");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }
        public List<dynamic> GetCustOrderDetailList(Int64 OrderId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetCustomerOrderDetails(OrderId);
            var myEnumerable = ds.Tables[0].AsEnumerable();

            try
            {
                List<OrderDetails> ordtl =
               (from item in myEnumerable
                select new OrderDetails
                {
                    ODID = item.Field<Int64>("ODID"),
                    Serial = item.Field<int>("Serial"),
                     //OrderId = item.Field<Int64>("OrderId");
                     ProductId = item.Field<Int64>("ProductId"),
                    ProductName = item.Field<String>("PartNo"),
                    Qty = item.Field<int>("Qty"),
                    Rate = item.Field<Decimal>("Rate"),
                    Amount = item.Field<Decimal>("Amount")
                }).ToList();
                objDynamic.Add(ordtl);
            }
            catch(Exception ex)
            {

            }

           
           
            return objDynamic;
        }


        private DataSet GetCustomerOrderApprover(Int64 OrderId,int Type)
        {
            string selectProcedure = string.Empty;
            if (Type ==1 )
            {
                selectProcedure = "[GetCustOrderAppTemp]";
            }
            else
            {
                selectProcedure = "[GetCustOrderApp]";
            }
  
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
                log.logErrorMessage("CustOrderData.GetCustOrderApp");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }
        public List<dynamic> GetCustOrderAppList(Int64 OrderId , int Type)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetCustomerOrderApprover(OrderId,Type);
            var myEnumerable = ds.Tables[0].AsEnumerable();


            List<OrderApprover> ordapp =
                (from item in myEnumerable
                 select new OrderApprover
                 {
                    // OAId = item.Field<Int64>("OAId"),
                     Serial = item.Field<int>("Serial"),
                     //OrderId = item.Field<Int64>("OrderId");
                     ApproverName = item.Field<String>("ApproverName"),
                     ApproverTitle = item.Field<String>("ApproverTitle"),
                     ApproverEmail = item.Field<String>("ApproverEmail"),
                     
                 }).ToList();
            objDynamic.Add(ordapp);
            return objDynamic;
        }


        private int SaveCustOrder(Order ord)
        {
            int OrderId = 0;
            string updateProcedure = "[UpdateOrder]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            if (ord.OrderId != 0)
            {
                updateCommand.Parameters.AddWithValue("@OrderId", ord.OrderId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OrderId", 0);
            }
            if (ord.CustId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CustId", ord.CustId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (!string.IsNullOrEmpty(ord.ReferenceNo))
            {
                updateCommand.Parameters.AddWithValue("@ReferenceNo", ord.ReferenceNo);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ReferenceNo", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(ord.Department))
            {
                updateCommand.Parameters.AddWithValue("@Department", ord.Department);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Department", DBNull.Value);
            }
            if (ord.BranchId != 0)
            {
                updateCommand.Parameters.AddWithValue("@BranchId", ord.BranchId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BranchId", 0);
            }
            if (ord.IncoTermId != 0)
            {
                updateCommand.Parameters.AddWithValue("@IncoTermId", ord.IncoTermId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@IncoTermId", 0);
            }
            if (!string.IsNullOrEmpty(ord.ShippingAddress))
            {
                updateCommand.Parameters.AddWithValue("@ShippingAddress", ord.ShippingAddress);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ShippingAddress", DBNull.Value);
            }
            if (ord.CountryId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CountryId ", ord.CountryId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CountryId ", 0);
            }
            if (ord.CityId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CityId", ord.CityId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CityId", 0);
            }
            if (!string.IsNullOrEmpty(ord.BillingAddress))
            {
                updateCommand.Parameters.AddWithValue("@BillingAddress", ord.BillingAddress);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BillingAddress", DBNull.Value);
            }
            
            if (!string.IsNullOrEmpty(ord.ContactName))
            {
                updateCommand.Parameters.AddWithValue("@ContactName", ord.ContactName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ContactName", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(ord.ContactNum))
            {
                updateCommand.Parameters.AddWithValue("@ContactNum", ord.ContactNum);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ContactNum", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(ord.ContactEmail))
            {
                updateCommand.Parameters.AddWithValue("@ContactEmail", ord.ContactEmail);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ContactEmail", DBNull.Value);
            }
            if (ord.UserId!= 0)
            {
                updateCommand.Parameters.AddWithValue("@ModifiedBy", ord.UserId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ModifiedBy", 0);
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
                



                return count;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return OrderId;
            }
            finally
            {
                connection.Close();
            }



        }
        private int SaveCustOrderDetalis(OrderDetails ordtls)
        {
            int count = 0;
            string updateProcedure = "[UpdateCustOrderDetails]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            if (ordtls.ODID != 0)
            {
                updateCommand.Parameters.AddWithValue("@ODID", ordtls.ODID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ODID", 0);
            }


            if (ordtls.Serial != 0)
            {
                updateCommand.Parameters.AddWithValue("@Serial", ordtls.Serial);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Serial", 0);
            }



            if (ordtls.OrderId != 0)
            {
                updateCommand.Parameters.AddWithValue("@OrderId", ordtls.OrderId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OrderId", 0);
            }

            if (ordtls.ProductId != 0)
            {
                updateCommand.Parameters.AddWithValue("@ProductId", ordtls.ProductId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ProductId", 0);
            }
            if (ordtls.Qty != 0)
            {
                updateCommand.Parameters.AddWithValue("@Qty", ordtls.Qty);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Qty", 0);
            }

            if (ordtls.Rate != 0)
            {
                updateCommand.Parameters.AddWithValue("@Rate", ordtls.Rate);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Rate", 0);
            }


            if (ordtls.Amount != 0)
            {
                updateCommand.Parameters.AddWithValue("@Amount", ordtls.Amount);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Amount", 0);
            }



            if (ordtls.TotalOrderAmount != 0)
            {
                updateCommand.Parameters.AddWithValue("@TotalOrderAmount", ordtls.TotalOrderAmount);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@TotalOrderAmount", 0);
            }


            if (ordtls.ModifiedBy != 0)
            {
                updateCommand.Parameters.AddWithValue("@UserID", ordtls.ModifiedBy);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@UserID", 0);
            }

            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

           
            try
            {
            
                connection.Open();
                updateCommand.ExecuteNonQuery();
                if (updateCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
                }
              



                return count;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return count;
            }
            finally
            {
                connection.Close();
            }



        }
        private int SaveCustOrderApprover(OrderApprover ordapp)
        {
            int OrderId = 0;
            int count = 0;
            string updateProcedure = "[UpdateOrderApprover]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (ordapp.OAId!= 0)
            {
                updateCommand.Parameters.AddWithValue("@OAId", ordapp.OAId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OAId", 0);
            }
            if (ordapp.OrderId != 0)
            {
                updateCommand.Parameters.AddWithValue("@OrderId", ordapp.OrderId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OrderId", 0);
            }
           

            if (ordapp.Serial != 0)
            {
                updateCommand.Parameters.AddWithValue("@Serial ", ordapp.Serial);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Serial ", 0);
            }
            if (!string.IsNullOrEmpty(ordapp.ApproverName))
            {
                updateCommand.Parameters.AddWithValue("@ApproverName", ordapp.ApproverName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ApproverName", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(ordapp.ApproverTitle))
            {
                updateCommand.Parameters.AddWithValue("@ApproverTitle", ordapp.ApproverTitle);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ApproverTitle", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(ordapp.ApproverEmail))
            {
                updateCommand.Parameters.AddWithValue("@ApproverEmail", ordapp.ApproverEmail);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ApproverEmail", DBNull.Value);
            }

            if (ordapp.UserId != 0)
            {
                updateCommand.Parameters.AddWithValue("@UserId", ordapp.UserId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@UserId", 0);
            }

            if (!string.IsNullOrEmpty(ordapp.Comments))
            {
                updateCommand.Parameters.AddWithValue("@Comments", ordapp.Comments);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Comments", DBNull.Value);
            }
            if (ordapp.isLoggedin)
            {
                updateCommand.Parameters.AddWithValue("@isLoggedin", ordapp.isLoggedin);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@isLoggedin", 0);
            }

            if (ordapp.isApproved)
            {
                updateCommand.Parameters.AddWithValue("@isApproved", ordapp.isApproved);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@isApproved", 0);
            }

            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            
            try
            {
               
                connection.Open();
                updateCommand.ExecuteNonQuery();
                if (updateCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
                }
                



                return count;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return count;
            }
            finally
            {
                connection.Close();
            }



        }

        public List<dynamic> SaveOrderRequest(CustRequest custreq)
        {

            var OrderIDdet = new List<dynamic>();
            SaveTempOrder SaveTempOrder = new SaveTempOrder();
            Order ord = new Order();
            if (!string.IsNullOrEmpty(custreq.OrderID))
            {
                ord.OrderId = Convert.ToInt64(securityHelper.Decrypt(custreq.OrderID,false));
            }
            else
            {
                ord.OrderId = 0;
            }
           

            if (!string.IsNullOrEmpty(custreq.BranchID))
            {
                ord.BranchId = Convert.ToInt64(custreq.BranchID);
            }
            else
            {
                ord.BranchId = 0;
            }
            if (!string.IsNullOrEmpty(custreq.SCity))
            {
                ord.CityId = Convert.ToInt64(custreq.SCity);
                //ord.CityId = 1;
            }
            else
            {
                ord.CityId = 0;

            }

            if (!string.IsNullOrEmpty(custreq.SCountry))
            {
                ord.CountryId = Convert.ToInt64(custreq.SCountry);
                //ord.CountryId = 1;
            }
            else
            {
                ord.CountryId = 0;

            }
            if (!string.IsNullOrEmpty(custreq.selectedCustomer))
            {
                ord.CustId = Convert.ToInt64(custreq.selectedCustomer);
            }
            else
            {
                ord.CustId = 0;
            }

            ord.Department = custreq.Department;
            if (!string.IsNullOrEmpty(custreq.DeliveryTo))
            {
                ord.IncoTermId = Convert.ToInt64(custreq.DeliveryTo);
            }
            else
            {
                ord.IncoTermId = 0;
            }
            ord.ContactName = custreq.Sname;
            ord.ContactEmail = custreq.Semail;
            ord.ContactNum = custreq.Sphone;

            ord.ReferenceNo = custreq.Refernce;
            ord.BillingAddress = (custreq.BAdd1 + "#" + custreq.BAdd2 + "#" + custreq.BAdd3 + "#" + custreq.BCityName + "#" + custreq.BState + "#" + custreq.BZip + "#" + custreq.BCountryName);
            ord.ShippingAddress = (custreq.SAdd1 + "#" + custreq.SAdd2 + "#" + custreq.SAdd3 + "#" + custreq.SCityName + "#" + custreq.SState + "#" + custreq.SZip + "#" + custreq.SCountryName);
            if (!string.IsNullOrEmpty(UserID))
            {
                ord.UserId = Convert.ToInt64(UserID);
            }


           int cstOrdr  = SaveCustOrder(ord);
            SaveTempOrder.TempOrderID = ord.OrderId;
            if (SaveTempOrder.TempOrderID != 0)
            {
                if (!string.IsNullOrEmpty(custreq.ApproverDetails))
                {


                    var Data = JsonConvert.DeserializeObject<List<ApproverDetail>>(custreq.ApproverDetails);
                    ApproverDetail ApproverDetails = new ApproverDetail();
                    for (int i = 0; i < Data.Count; i++)
                    {
                        ApproverDetails = Data[i];
                        OrderApprover orda = new OrderApprover();
                        orda.OrderId = ord.OrderId;
                        orda.Serial = i + 1; // Need to ask customer
                        orda.UserId = ord.UserId;
                        if (ApproverDetails.AprName != null && ApproverDetails.AprEmail != null)
                        {
                            if (!string.IsNullOrEmpty(ApproverDetails.AprId))
                            {
                                orda.OAId = Convert.ToInt64(orda.OAId);
                                
                            }
                            
                            orda.ApproverName = ApproverDetails.AprName;
                            orda.ApproverTitle = ApproverDetails.AprTitle;
                            orda.ApproverEmail = ApproverDetails.AprEmail;
                            orda.isLoggedin = true;// Need to ask customer
                            orda.isApproved  = true;// Need to ask customer
                            int ordap=SaveCustOrderApprover(orda);
                        }
                    }

                }

            }

            OrderIDdet.Add(SaveTempOrder);
            OrderIDdet.Add(2);

            return OrderIDdet;
        }


        public List<dynamic> SaveOrderDetails(string OrderID, string CustomerID, string SubTotal, CustOrderListDetails strCustOrderDetails)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            OrderDetails OrderDetails = new OrderDetails();
            CustOrderDetails CustOrderDetails = new CustOrderDetails();
            int orddetid = 0;
            string strOrderID = securityHelper.Decrypt(OrderID, false);

            if (!string.IsNullOrEmpty(strCustOrderDetails.CustOrderDetails))
            {


                var Data = JsonConvert.DeserializeObject<List<CustOrderDetails>>(strCustOrderDetails.CustOrderDetails);
                for (int i = 0; i < Data.Count; i++)
                {
                    CustOrderDetails = Data[i];
                    OrderDetails.Serial = i + 1;

                    if (!string.IsNullOrEmpty(strOrderID))
                    {
                        OrderDetails.OrderId = Convert.ToInt64(strOrderID);
                    }

                    if (!string.IsNullOrEmpty(CustOrderDetails.ODID))
                    {
                        OrderDetails.ODID = Convert.ToInt64(CustOrderDetails.ODID);
                    }
                    if (!string.IsNullOrEmpty(CustOrderDetails.ProdID))
                    {
                        OrderDetails.ProductId = Convert.ToInt64(CustOrderDetails.ProdID);
                    }
                    if (!string.IsNullOrEmpty(CustOrderDetails.ProdPrice))
                    {
                        OrderDetails.Rate = Convert.ToDecimal(CustOrderDetails.ProdPrice);
                    }
                    if (!string.IsNullOrEmpty(CustOrderDetails.Quantity))
                    {
                        OrderDetails.Qty = Convert.ToInt32(CustOrderDetails.Quantity);
                    }
                    if (!string.IsNullOrEmpty(CustOrderDetails.TotalPrice))
                    {
                        OrderDetails.Amount = Convert.ToDecimal(CustOrderDetails.TotalPrice);
                    }

                    if (!string.IsNullOrEmpty(SubTotal))
                    {
                        OrderDetails.TotalOrderAmount = Convert.ToDecimal(SubTotal);
                    }
                    if (!string.IsNullOrEmpty(UserID))
                    {
                        OrderDetails.ModifiedBy = Convert.ToInt64(UserID);
                    }
                    SaveCustOrderDetalis(OrderDetails);
                }
                objDynamic.Add(orddetid);
            }
            return objDynamic;

        }

    }
}