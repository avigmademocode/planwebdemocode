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
    public class CustRequestData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();

        Log log = new Log();
        OrderData orddata = new OrderData();
        CustOrderData custOrderData = new CustOrderData();
        OrderApprovalData orderApprovalData = new OrderApprovalData();
        EmailTemplate emailTemplate = new EmailTemplate();
        EmailFormatDTO emailFormatDTO = new EmailFormatDTO();
        string UserID  = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();
        string strpath = System.Configuration.ConfigurationManager.AppSettings["UploadImagePath"];
        private DataSet GetCustomer(Int64 CustID, int Type)
        {

            string selectProcedure = "[GetCustomerDetails]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@CustId", CustID);
            da.SelectCommand.Parameters.AddWithValue("@Type", Type);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("CustRequestData.GetCustomer");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }


        private DataSet GetCategoryData(Int64 CustID)
        {

            string selectProcedure = "[GetCategory]";
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
                log.logErrorMessage("CustRequestData.GetCategoryData");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }

        private DataSet GetShipAndDeliveryTerms(Int64 CustID)
        {

            string selectProcedure = "[GetCustomerReqDetails]";
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
                log.logErrorMessage("CustRequestData.GetShipAndDeliveryTerms");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }
        private DataTable GetBranch(Int64 CustId, Int64 BranchID)
        {

            string selectProcedure = "[GetCustomerBranch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@CustId", CustId);
            selectCommand.Parameters.AddWithValue("@BranchId", BranchID);
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                log.logErrorMessage("CustRequestData.GetBranch");
                log.logErrorMessage(ex.StackTrace);
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        private DataSet GetProductsData(Int64 CustId, Int64 ProdCatId, int TypeID, string strwhere)
        {

            string selectProcedure = "[GetProducts]";
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@CustId", CustId);
            da.SelectCommand.Parameters.AddWithValue("@ProdCatId", ProdCatId);
            da.SelectCommand.Parameters.AddWithValue("@TypeID", TypeID);
            da.SelectCommand.Parameters.AddWithValue("@whereClause", strwhere);
           
            try
            {
                //connection.Open();
                //SqlDataReader reader = selectCommand.ExecuteReader();
                //if (reader.HasRows)
                //{
                //    dt.Load(reader);
                //}
                // reader.Close();
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("CustRequestData.GetProducts");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }


        public List<dynamic> GetCustMaster(int type)
        {
            Int64 CustID = 0;
            List<dynamic> objDynamic = new List<dynamic>();
            switch (type)
            {
                case 4:
                case 1:
                    {
                        DataSet dt = null;
                        if (!string.IsNullOrEmpty(CustomerID))
                        {
                            CustID = Convert.ToInt64(CustomerID);
                        }
                        if (type == 1)
                        {
                            dt = GetCustomer(CustID, 1);
                        }
                        else if (type == 4)
                        {
                            dt = GetCustomer(CustID, 2);
                        }



                        var myEnumerable = dt.Tables[0].AsEnumerable();

                        List<CustomerInfo> CustomerList =
                            (from item in myEnumerable
                             select new CustomerInfo
                             {
                                 CustId = item.Field<Int64>("CustId"),
                                 CustName = item.Field<string>("CustName"),
                                 UseItemGroups = item.Field<bool>("UseItemGroups")
                             }).ToList();
                        objDynamic.Add(CustomerList);
                        var myEnumerablecity = dt.Tables[1].AsEnumerable();
                        List<CustCity> CustCity =
                           (from item in myEnumerablecity
                            select new CustCity
                            {
                                CityID = item.Field<Int64>("CityID"),
                                CityName = item.Field<string>("CityName")
                            }).ToList();
                        objDynamic.Add(CustCity);

                        var myEnumerablecountry = dt.Tables[2].AsEnumerable();
                        List<CustCountry> CustCountry =
                           (from item in myEnumerablecountry
                            select new CustCountry
                            {
                                CountryId = item.Field<Int64>("CountryId"),
                                CountryName = item.Field<string>("CountryName")
                            }).ToList();
                        objDynamic.Add(CustCountry);
                        break;
                    }

                case 2:
                    {

                        if (!string.IsNullOrEmpty(CustomerID))
                        {
                            CustID = Convert.ToInt64(CustomerID);
                        }
                        DataSet dt = GetCustomer(CustID, 1);
                        var myEnumerable = dt.Tables[0].AsEnumerable();

                        List<CustomerInfo> CustomerList =
                            (from item in myEnumerable
                             select new CustomerInfo
                             {
                                 CustId = item.Field<Int64>("CustId"),
                                 CustName = item.Field<string>("CustName"),
                                 ProdCatId = string.Empty,
                                 ExpDate = string.Empty,
                                 IsActive = string.Empty,
                                 Price = string.Empty,
                                 PCRId = string.Empty


                             }).ToList();
                        objDynamic.Add(CustomerList);

                        break;
                    }

                case 3:
                    {

                        if (!string.IsNullOrEmpty(CustomerID))
                        {
                            CustID = Convert.ToInt64(CustomerID);
                        }
                        DataSet dt = GetCustomer(CustID, 1);
                        var myEnumerable = dt.Tables[0].AsEnumerable();

                        List<CustomerInfo> CustomerList =
                            (from item in myEnumerable
                             select new CustomerInfo
                             {
                                 CustId = (item.Field<Int64>("CustId")),
                                 strCustId = securityHelper.Encrypt(item.Field<Int64>("CustId").ToString(), false),
                                 CustName = item.Field<string>("CustName"),
                                 UseItemGroups = item.Field<bool>("UseItemGroups")


                             }).ToList();
                        objDynamic.Add(CustomerList);

                        break;
                    }
            }


            return objDynamic;
        }


        public List<dynamic> GetShipTo(Int64 custId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetShipAndDeliveryTerms(custId);

            var myEnumerable = ds.Tables[0].AsEnumerable();

            List<CustomerShipTo> CustomerShipToList =
                (from item in myEnumerable
                 select new CustomerShipTo
                 {
                     BranchID = item.Field<Int64>("BranchID"),
                     DisplayName = item.Field<string>("DisplayName")
                 }).ToList();
            objDynamic.Add(CustomerShipToList);
            var myEnumerabledel = ds.Tables[1].AsEnumerable();
            List<CustomerDeliveryTerms> CustomerDeliveryList =
               (from item in myEnumerabledel
                select new CustomerDeliveryTerms
                {
                    IncoTermID = item.Field<Int64>("IncoTermID"),
                    IncoTermInfo = item.Field<string>("IncoTermInfo")
                }).ToList();
            objDynamic.Add(CustomerDeliveryList);


            var myEnumerablelevl = ds.Tables[2].AsEnumerable();
            List<AuthorityLevel> AuthorityLevel =
               (from item in myEnumerablelevl
                select new AuthorityLevel
                {
                    level = item.Field<int>("LOA"),
                    Desg = item.Field<bool>("desg"),
                    RefNoAuto = item.Field<bool>("RefNoAuto")
                }).ToList();
            objDynamic.Add(AuthorityLevel);


            var myEnumerablelevlDetail = ds.Tables[3].AsEnumerable();
            List<ApproverDet> ApproverDetails =
               (from item in myEnumerablelevlDetail
                select new ApproverDet
                {
                    ApproverSerial = item.Field<int>("ApproverSerial"),
                    ApproverNameDisplay= item.Field<string>("ApproverNameDisplay"),
                
                }).ToList();
            objDynamic.Add(ApproverDetails);

            return objDynamic;
        }


        public List<CustomerShipDetials> GetCustDetials(Int64 custId, Int64 BranchID)
        {

            DataTable dt = GetBranch(custId, BranchID);
            var myEnumerable = dt.AsEnumerable();
            List<CustomerShipDetials> CustomerList = null;
            try
            { 
          CustomerList = new List<CustomerShipDetials> 
                (from item in myEnumerable
                 select new CustomerShipDetials
                 {
                     ShipAddress1 = item.Field<string>("BrAdd1"),
                     ShipAddress2 = item.Field<string>("BrAdd2"),
                     ShipAddress3 = item.Field<string>("BrAdd3"),
                     ShipCity = item.Field<Int64>("BrCity"),
                     ShipCityName = item.Field<string>("BrCityName"),
                     ShipState = item.Field<string>("BrState"),
                     ShipCountry = item.Field<Int64>("BrCountry"),
                     ShipCountryName = item.Field<string>("BrCountryName"),
                     ShipPin = item.Field<string>("BrPin"),
                     ContactPerson = item.Field<string>("BrContactPerson"),
                     ShipEmail = item.Field<string>("BrEmailId"),
                     ShipPhoneNo = item.Field<string>("BrContactNo"),
                     BillAddress1 = item.Field<string>("BlAdd1"),
                     BillAddress2 = item.Field<string>("BlAdd2"),
                     BillAddress3 = item.Field<string>("BlAdd3"),
                     BillCity = item.Field<Int64>("BlCity"),
                     BillCityName = item.Field<string>("BICityName"),
                     BillState = item.Field<string>("BlState"),
                     BillCountry = item.Field<Int64>("BlCountry"),
                     BillCountryName = item.Field<string>("BICountryName"),
                     BillPin = item.Field<string>("BlPin"),
                     FullAddress = item.Field<string>("BrFullAddress")
                 }).ToList();
        }

        catch (Exception ex)
            {

            }


            return CustomerList;
        }
        public List<dynamic> SaveOrderRequest(CustRequest custreq)
        {

            var OrderIDdet = new List<dynamic>();
            SaveTempOrder SaveTempOrder = new SaveTempOrder();
            Order ord = new Order();

            if (!string.IsNullOrEmpty(custreq.OrderID))
            {
                ord.OrderId = Convert.ToInt64(custreq.OrderID);
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

            ord.CityName = custreq.SCityName;
            ord.CountryName = custreq.SCountryName;

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
            ord.BillingAddress = (custreq.BAdd1 + "#" + custreq.BAdd2 + "#" + custreq.BAdd3 + "#" + custreq.BCityName + "#" + custreq.BState + "#" + custreq.BZip + "#" + custreq.BCountryName + "#"+ custreq.BCity + "#" + custreq.BCountry);
            ord.ShippingAddress = (custreq.SAdd1 + "#" + custreq.SAdd2 + "#" + custreq.SAdd3 + "#" + custreq.SCityName + "#" + custreq.SState + "#" + custreq.SZip + "#" + custreq.SCountryName + "#" + custreq.SCity + "#" + custreq.SCountry);
            if (!string.IsNullOrEmpty(UserID))
            {
                ord.UserId = Convert.ToInt64(UserID);
            }


            SaveTempOrder = orddata.AddOrder(ord);
            if (ord.OrderId == 0)
            {
                ord.OrderId = SaveTempOrder.TempOrderID;
            }
            else
            {
                SaveTempOrder.TempOrderID = ord.OrderId;
            }
           
            if (SaveTempOrder.TempOrderID != 0)
            {
                if (!string.IsNullOrEmpty(custreq.ApproverDetails))
                {

                    int count = orddata.UpdateApproverDetail(SaveTempOrder.TempOrderID);
                    var Data = JsonConvert.DeserializeObject<List<ApproverDetail>>(custreq.ApproverDetails);
                    ApproverDetail ApproverDetails = new ApproverDetail();
                    for (int i = 0; i < Data.Count; i++)
                    {
                        ApproverDetails = Data[i];
                        OrderApprover orda = new OrderApprover();
                        orda.OrderId = ord.OrderId;
                        orda.Serial = i + 1; // Need to ask customer
                        orda.UserId = ord.UserId;
                        if (ApproverDetails.AprEmail != null)
                        {
                            orda.ApproverName = ApproverDetails.AprName;
                            orda.ApproverTitle = ApproverDetails.AprTitle;
                            orda.ApproverEmail = ApproverDetails.AprEmail;
                            orda.isLoggedin = false;// Need to ask customer
                            orda.isApproved = false;// Need to ask customer
                            orddata.AddApprover(orda);
                        }
                    }

                }

            }

            OrderIDdet.Add(SaveTempOrder);
            OrderIDdet.Add(1);

            return OrderIDdet;
        }





        public List<dynamic> SaveOrderDetails(string OrderID, string CustomerID, string SubTotal, CustOrderListDetails strCustOrderDetails)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            OrderDetails OrderDetails = new OrderDetails();
            CustOrderDetails CustOrderDetails = new CustOrderDetails();
            int orddetid = 0;
            Decimal TotalAmount = 0;
           int count =  orddata.UpdateOrderDetail(Convert.ToInt64(OrderID));
            if (!string.IsNullOrEmpty(strCustOrderDetails.CustOrderDetails))
            {


                var Data = JsonConvert.DeserializeObject<List<CustOrderDetails>>(strCustOrderDetails.CustOrderDetails);
                for (int i = 0; i < Data.Count; i++)
                {
                    CustOrderDetails = Data[i];
                    OrderDetails.Serial = i + 1;

                    if (!string.IsNullOrEmpty(OrderID))
                    {
                        OrderDetails.OrderId = Convert.ToInt64(OrderID);
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
                        TotalAmount = Convert.ToDecimal(SubTotal);
                    }
                    orddetid = orddata.AddOrderDetail(OrderDetails, TotalAmount);
                }
                objDynamic.Add(orddetid);
            }
            return objDynamic;

        }

        public List<dynamic> SaveConfirmOrder(SaveOrder SaveOrder)
        {
            List<dynamic> objDynamic = new List<dynamic>();
          
            Int64 intOrderID = 0, intCustomerID = 0, intUserID = 0, intConfirmOrderID = 0;
            try
            {


                if (!string.IsNullOrEmpty(SaveOrder.strorderID))
                {
                    intOrderID = Convert.ToInt64(SaveOrder.strorderID);
                }
                if (!string.IsNullOrEmpty(SaveOrder.strCustId))
                {
                    intCustomerID = Convert.ToInt64(SaveOrder.strCustId);
                }
                if (!string.IsNullOrEmpty(UserID))
                {
                    intUserID = Convert.ToInt64(UserID);
                }
                intConfirmOrderID = orddata.AddConfirmOrderDetail(intOrderID, intCustomerID, intUserID, SaveOrder.type);

                string strencOrderID = securityHelper.Encrypt(intConfirmOrderID.ToString(), false);
                objDynamic.Add(strencOrderID);
                objDynamic.Add(custOrderData.GetCustOrderDetailList(intConfirmOrderID));
                if (true)
                {

                }
                EmailTemplate emailTemplate = new EmailTemplate();
                EmailFormatDTO emailFormatDTO = GetEmailDetails(intConfirmOrderID.ToString(),string.Empty,string.Empty);
               
               
            }
            catch(Exception ex)
            {

            }
            return objDynamic;

        }

        public EmailFormatDTO GetEmailDetails(String OrderID,String EmailID ,String Status )
        {
            EmailFormatDTO emailFormatDTO = new EmailFormatDTO();
            EmailTemplate emailTemplate = new EmailTemplate();
            List<dynamic> objGetOrderDetails = new List<dynamic>();
            try
            {
                objGetOrderDetails = orderApprovalData.GetOrderDetails(OrderID);
                string AprEmail = string.Empty, AprName = string.Empty, SupportingEmail = string.Empty;
                emailFormatDTO.ReferenceNo = objGetOrderDetails[0][0].ReferenceNo;
                emailFormatDTO.Status = objGetOrderDetails[0][0].StatusID;
                emailFormatDTO.FirstName = objGetOrderDetails[0][0].FirstName;
                emailFormatDTO.LastName = objGetOrderDetails[0][0].LastName;
                emailFormatDTO.CreatedBy = objGetOrderDetails[0][0].UserName;
                emailFormatDTO.AddOffice = objGetOrderDetails[0][0].Department;
                emailFormatDTO.UserId = Convert.ToInt64(UserID);
                emailFormatDTO.OrderId = Convert.ToInt64(OrderID);
                List<ApproverDetails> ApproverDetails = objGetOrderDetails[1];
                for (int i = 0; i < ApproverDetails.Count; i++)
                {
                    if (!string.IsNullOrEmpty(ApproverDetails[i].AprEmail))
                    {


                        if (ApproverDetails.Count == 1)
                        {
                            AprEmail = ApproverDetails[i].AprEmail;
                            AprName = ApproverDetails[i].AprName;
                        }
                        else
                        {
                            AprEmail = AprEmail + ";" + ApproverDetails[i].AprEmail;
                            AprName = AprEmail + " And   " + ApproverDetails[i].AprName;
                        }

                    }
                }
                emailFormatDTO.ApprovedBy = AprName;

                if (objGetOrderDetails[11] != null)
                {
                    List<OrderSupportingEmail> OrderSupportingEmail = objGetOrderDetails[11];

                    for (int i = 0; i < OrderSupportingEmail.Count; i++)
                    {
                        if (OrderSupportingEmail.Count == 1)
                        {
                            SupportingEmail = OrderSupportingEmail[i].email;
                        }
                        else
                        {
                            SupportingEmail = SupportingEmail + "," + OrderSupportingEmail[i].email;

                        }


                    }
                    if (OrderSupportingEmail.Count > 1)
                    {
                        SupportingEmail = SupportingEmail.Substring(1);
                    }
                    //  emailFormatDTO.CC = SupportingEmail;
                }


                Int64 instatus = 0;
                if (!string.IsNullOrEmpty(Status))
                {
                    instatus = Convert.ToInt64(Status);
                }
                else
                {
                    instatus = emailFormatDTO.Status;
                }
                switch (instatus)
                {
                    case 1:
                        {
                            emailFormatDTO.To = objGetOrderDetails[0][0].UserName;
                            emailFormatDTO.CC = SupportingEmail;
                            emailTemplate.NewOrder(emailFormatDTO);
                            break;
                        }
                    case 2:
                        {
                            emailFormatDTO.To = objGetOrderDetails[0][0].UserName;
                            emailFormatDTO.CC = SupportingEmail;
                            emailTemplate.QuoteRequired(emailFormatDTO);
                            break;
                        }
                    case 6:
                        {

                            for (int i = 0; i < ApproverDetails.Count; i++)
                            {
                                if (!string.IsNullOrEmpty(ApproverDetails[i].AprEmail))
                                {
                                    emailFormatDTO.EncOrderApproverNo = securityHelper.Encrypt(ApproverDetails[i].OAID.ToString(), true);

                                    emailFormatDTO.EncOrderNo = securityHelper.Encrypt(OrderID, true);
                                    emailTemplate.RequestsApproval(emailFormatDTO);
                                }
                            }


                            break;
                        }
                    case 8:
                        {
                            emailFormatDTO.To = objGetOrderDetails[0][0].UserName;
                            emailFormatDTO.CC = SupportingEmail;
                            emailTemplate.RequestApproved(emailFormatDTO);
                            break;
                        }
                    case 9:
                        {
                            emailFormatDTO.To = objGetOrderDetails[0][0].UserName;
                            emailFormatDTO.CC = SupportingEmail;
                            emailTemplate.RequestDenied(emailFormatDTO);
                            break;
                        }
                    case 10:
                        {
                            emailFormatDTO.To = objGetOrderDetails[0][0].UserName;
                            emailTemplate.ReviewingOrder(emailFormatDTO);
                            break;
                        }
                    case 99:
                        {
                            emailFormatDTO.To = EmailID;
                            emailTemplate.ShipOrderOrder(emailFormatDTO);
                            break;
                        }
                }
            }
            catch(Exception ex)
            {
                log.logErrorMessage("CustRequestData.GetEmailDetails");
                log.logErrorMessage(ex.StackTrace);
            }
          

            return emailFormatDTO;
        }

        public List<dynamic> GetCategory(Int64 custId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetCategoryData(custId);
            var myEnumerablecat = ds.Tables[0].AsEnumerable();

            List<ProductReqCategory> ProductReqCategory =
                (from item in myEnumerablecat
                 select new ProductReqCategory
                 {
                     CatId = item.Field<Int64>("CatID"),
                     CatName = item.Field<string>("ProdDesc")
                 }).ToList();
            objDynamic.Add(ProductReqCategory);


            var myEnumerableprod = ds.Tables[1].AsEnumerable();
            var myEnumerableprodTier = ds.Tables[2].AsEnumerable();
            try
            {
                List<ProductDetails> ProductDetails =
                    (from item in myEnumerableprod
                     select new ProductDetails
                     {
                         ProdID = item.Field<Int64>("ProductId"),
                         ProdCatID = item.Field<Int64>("ProdCatId"),
                         ProdName = item.Field<string>("Model"),
                         ProdPart = item.Field<string>("PartNo"),
                         ProdPrice = item.Field<Decimal>("Price"),
                         ImageID = item.Field<String>("ImageID"),
                         ImagePath = strpath + "/" + item.Field<String>("ImagePath"),
                         Desc = item.Field<string>("Spec"),
                        TierCount = item.Field<int>("TierCount"),
                         TierData = (from items in myEnumerableprodTier
                                     select new ProductTierPrice
                                     {
                                         ProdID = items.Field<Int64>("ProductId"),
                                         Serial = items.Field<Int64>("Serial"),
                                         Qty = items.Field<int>("Qty"),
                                         Price = items.Field<Decimal>("Price"),

                                     }).ToList()
                                     
                     }).ToList();

                objDynamic.Add(ProductDetails);
            }
            catch(Exception ex)
            {

            }

          



            return objDynamic;
        }

        public List<dynamic> GetProducts(Int64 CustId, Int64 ProdCatId, int TypeId, string strwhere)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetProductsData(CustId, ProdCatId, TypeId, strwhere);

            var myEnumerableprod = ds.Tables[0].AsEnumerable();
           
            switch (TypeId)
            {
                case 1:
                    {
                        var myEnumerableprodTier = ds.Tables[1].AsEnumerable();
                        List<ProductDetails> ProductDetails =
               (from item in myEnumerableprod
                select new ProductDetails
                {
                    ProdID = item.Field<Int64>("ProductId"),
                    ProdCatID = item.Field<Int64>("ProdCatId"),
                    ProdName = item.Field<string>("Model"),
                    ProductName = item.Field<string>("Model"),
                    ProdPart = item.Field<string>("PartNo"),
                    ProdPrice = item.Field<Decimal>("Price"),
                    ImageID = item.Field<String>("ImageID"),
                    ImagePath = strpath +"/" +item.Field<String>("ImagePath"),
                    Desc = item.Field<string>("Spec"),
                    TierData = (from items in myEnumerableprodTier
                                select new ProductTierPrice
                                {
                                    ProdID = items.Field<Int64>("ProductId"),
                                    Serial = items.Field<Int64>("Serial"),
                                    Qty = items.Field<int>("Qty"),
                                    Price = items.Field<Decimal>("Price"),

                                }).ToList()
                }).ToList();
                        objDynamic.Add(ProductDetails);
                        break;
                    }
                case 3:
                case 2:
                    {
                        List<ProductDetails> ProductDetails =
               (from item in myEnumerableprod
                select new ProductDetails
                {
                    strProdID = securityHelper.Encrypt(item.Field<Int64>("ProductId").ToString(),false),
                    ProdName = item.Field<string>("Model"),
                    ProductName = item.Field<string>("Model"),
                    //ProdPrice = item.Field<Decimal>("Price")
                }).ToList();
                        objDynamic.Add(ProductDetails);
                        break;
                    }
            }





            return objDynamic;
        }


        public List<dynamic> GetOrderDetails(Int64 CustId, Int64 OrderID)
        {
            //List<CustOrderDetailsList> ent = new List<CustOrderDetailsList>();
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {
                Int64 intUserId = Convert.ToInt64(UserID);
               
                DataSet ds = orddata.GetOrderDetail(CustId, intUserId, OrderID);
                string ShippingAddress = string.Empty;
                var myEnumerableord = ds.Tables[0].AsEnumerable();

                List<CustOrderDetail> CustOrderDetail =
                   (from item in myEnumerableord
                    select new CustOrderDetail
                    {
                        ReferenceNo = item.Field<String>("ReferenceNo"),
                        Department = item.Field<String>("Department"),
                        TotalOrderAmount = item.Field<Decimal>("TotalOrderAmount"),
                        DsiplayName = item.Field<String>("DisplayName"),
                        CustomerName = item.Field<String>("CustName"),
                        StatusName = item.Field<String>("StatusName"),
                        FirstName = item.Field<String>("FirstName"),
                        LastName = item.Field<String>("LastName"),
                        UserName = item.Field<String>("UserName"),
                        CustName = item.Field<String>("CustName"),
                        HOEmailId = item.Field<String>("HOEmailId"),
                        IncoTermDesc = item.Field<String>("IncoTermDesc"),

                        SEmailId = item.Field<String>("BrEmailId"),
                        SContactPerson = item.Field<String>("BrContactPerson"),
                        SContactNo = item.Field<String>("BrContactNo"),

                    // BEmailId = item.Field<String>("BrEmailId"),
                    // BContactPerson = item.Field<String>("BrContactPerson"),
                    // BContactNo = item.Field<String>("BlContactNo"),

                }).ToList();
                objDynamic.Add(CustOrderDetail);




                var myEnumerableApr = ds.Tables[1].AsEnumerable();

                List<ApproverDetails> ApproverDetails =
                   (from item in myEnumerableApr
                    select new ApproverDetails
                    {
                        AprName = item.Field<String>("ApproverName"),
                        AprTitle = item.Field<String>("ApproverTitle"),
                        AprEmail = item.Field<String>("ApproverEmail")

                    }).ToList();
                objDynamic.Add(ApproverDetails);


                var myEnumerableprod = ds.Tables[2].AsEnumerable();

                List<CustProdDetails> ProductDetails =
                   (from item in myEnumerableprod
                    select new CustProdDetails
                    {
                        ProdID = item.Field<Int64>("ProductId"),
                        PartNo = item.Field<String>("PartNo"),
                        Quantity = item.Field<int>("Qty"),
                        ProdPrice = item.Field<Decimal>("Rate"),
                        TotalPrice = item.Field<Decimal>("Amount")
                    }).ToList();
                objDynamic.Add(ProductDetails);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string shipAddress = ds.Tables[0].Rows[i]["BrAdd1"].ToString();
                    string BillAddress = ds.Tables[0].Rows[i]["BlAdd1"].ToString();
                    Array arrshipAddress = shipAddress.Split('#');
                    Array arrBillAddress = shipAddress.Split('#');
                    objDynamic.Add(arrshipAddress);
                    objDynamic.Add(arrBillAddress);
                }


            }
            catch(Exception ex)
            {

            }


            return objDynamic;
        }

    }


}