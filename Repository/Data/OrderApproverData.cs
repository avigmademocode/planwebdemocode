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
using Newtonsoft.Json;
using MyProject.Repository.Security;
using System.Web;

namespace MyProject.Repository.Data
{
    public class OrderApproverData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        SecurityHelper SecurityHelper = new SecurityHelper();

        Log log = new Log();

        private DataSet UpdateStatus(Int64 OrderID, Int64 StatusID, int Type, Int64 LeadTime, Int64 USerID, Int64 IncoID, String Remarks,String SalesOrderNo,Int64 OAID)
        {
            DataSet ds = new DataSet();
            string insertProcedure = "[UpdateChangeStatusData]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            int count = 0;
            if (OrderID != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderId", OrderID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderId", 0);
            }


            if (StatusID != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatusId", StatusID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatusId", 0);
            }
            if (Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }
            if (LeadTime != 0)
            {
                insertCommand.Parameters.AddWithValue("@LeadTime", LeadTime);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@LeadTime", 0);
            }

            if (IncoID != 0)
            {
                insertCommand.Parameters.AddWithValue("@IncoID", IncoID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IncoID", 0);
            }

            if (!string.IsNullOrEmpty(SalesOrderNo))
            {
                insertCommand.Parameters.AddWithValue("@SalesOrderNo", SalesOrderNo);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@SalesOrderNo", DBNull.Value);
            }

            if (USerID != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", USerID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (!string.IsNullOrEmpty(Remarks))
            {
                insertCommand.Parameters.AddWithValue("@Remarks", Remarks);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Remarks", DBNull.Value);
            }
            if (OAID != 0)
            {
                insertCommand.Parameters.AddWithValue("@OAID", OAID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OAID", 0);
            }
            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;


            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                connection.Close();


                connection.Open();
                string selectProcedure = "[GetCurrentCustomerStatus]";
                SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
                SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@orderID", OrderID);

                try
                {
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    log.logErrorMessage("OrderApprovalData.GetOrderApp");
                    log.logErrorMessage(ex.StackTrace);
                    return ds;
                }
                return ds;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("OrderData.UpdateOrderDetail");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }

        }

        private DataSet GetFinalOrderDetail(Int64 OrderID)
        {

            string selectProcedure = "[GetFinalOrderData]";
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            DataSet ds = new DataSet();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@orderID", OrderID);

            DataTable dt = new DataTable();
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("OrderApprovalData.GetFinalOrderDetail");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;

        }


        public EmailFormatDTO GetEmailDetails(String OrderID)
        {
            EmailFormatDTO emailFormatDTO = new EmailFormatDTO();
            EmailTemplate emailTemplate = new EmailTemplate();
            List<dynamic> objGetOrderDetails = new List<dynamic>();
            objGetOrderDetails = GetOrderDetails(OrderID);
            string AprEmail = string.Empty, AprName = string.Empty, SupportingEmail = string.Empty;
            emailFormatDTO.ReferenceNo = objGetOrderDetails[0][0].ReferenceNo;
            emailFormatDTO.Status = objGetOrderDetails[0][0].StatusID;
            emailFormatDTO.FirstName = objGetOrderDetails[0][0].FirstName;
            emailFormatDTO.LastName = objGetOrderDetails[0][0].LastName;
            emailFormatDTO.CreatedBy = objGetOrderDetails[0][0].UserName;
            emailFormatDTO.AddOffice = objGetOrderDetails[0][0].Department;
            emailFormatDTO.EncOrderNo = SecurityHelper.Encrypt(OrderID, false);
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
                        AprEmail = AprEmail + "," + ApproverDetails[i].AprEmail;
                        AprName = AprEmail + " And   " + ApproverDetails[i].AprName;
                    }

                }
            }
            emailFormatDTO.ApprovedBy = AprName;
            if (!string.IsNullOrEmpty(AprEmail))
            {
                emailFormatDTO.To = objGetOrderDetails[0][0].UserName + "," + AprEmail;
            }
            else
            {
                emailFormatDTO.To = objGetOrderDetails[0][0].UserName;
            }

            List<OrderSupportingEmail> OrderSupportingEmail = objGetOrderDetails[11];
            ;
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
            switch (emailFormatDTO.Status)
            {
                case 1:
                    {
                        emailTemplate.NewOrder(emailFormatDTO);
                        break;
                    }
                case 2:
                    {
                        emailTemplate.QuoteRequired(emailFormatDTO);
                        break;
                    }
                case 6:
                    {
                        emailTemplate.RequestsApproval(emailFormatDTO);
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
            }

            return emailFormatDTO;
        }
        public List<dynamic> GetUpdateCustomerStatus(StatusChange model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            Int64 OrderID = 0, StatusID = 0, LeadTime = 0, intUserID = 0, intIncoID = 0,OAID = 0;
            int type = 0;
            String Remarks = string.Empty,SalesOrderNo = string.Empty;
            if (!string.IsNullOrEmpty(SecurityHelper.Decrypt(model.OrderID, false)))
            {
                OrderID = Convert.ToInt64(SecurityHelper.Decrypt(model.OrderID, false));
            }
            if (!string.IsNullOrEmpty(model.Type))
            {
                type = Convert.ToInt32(model.Type);
            }
            if (!string.IsNullOrEmpty(model.ChangedStatus))
            {
                StatusID = Convert.ToInt32(model.ChangedStatus);
            }
            if (!string.IsNullOrEmpty(model.LeadTime))
            {
                LeadTime = Convert.ToInt32(model.LeadTime);
            }
          
            if (!string.IsNullOrEmpty(model.IncoID))
            {
                intIncoID = Convert.ToInt32(model.IncoID);
            }
            if (!string.IsNullOrEmpty(model.ApproverID))
            {

                OAID = Convert.ToInt64(SecurityHelper.Decrypt(model.ApproverID, false));
            }
            Remarks = model.Reason;
            SalesOrderNo = model.SalesOrderNo;
            DataSet ds = UpdateStatus(OrderID, StatusID, type, LeadTime, intUserID, intIncoID, Remarks,SalesOrderNo,OAID);

            string strCurrStatusID = ds.Tables[1].Rows[0]["StatusId"].ToString();

            //DataTable dtstatustable = GetCustStatusDetail(StatusID.ToString(), ds.Tables[0]);
            DataTable dtstatustable = GetCustStatusDetail(strCurrStatusID, ds.Tables[0]);
            var myEnumerablecuststatus = dtstatustable.AsEnumerable();
            List<CustomerStatusDetail> CustomerStatusDetail =
               (from item in myEnumerablecuststatus
                select new CustomerStatusDetail
                {
                    StatusId = item.Field<Int64>("StatusId"),
                    StatusName = item.Field<String>("StatusName"),
                    StatusOrder = item.Field<int>("StatusOrder"),
                    StatusCheck = item.Field<Boolean>("Statuscheck"),

                }).ToList();
            objDynamic.Add(CustomerStatusDetail);
            try
            {
                GetEmailDetails(OrderID.ToString());
            }
            catch(Exception ex)
            {
                
            }
            
            return objDynamic;
        }

        public List<dynamic> SendEmail(EmailFormatDTO emailFormatDTO)
        {
            EmailTemplate emailTemplate = new EmailTemplate();
            List<dynamic> objGetOrderDetails = new List<dynamic>();
            List<dynamic> objDynamic = new List<dynamic>();
            objGetOrderDetails = GetOrderDetails(SecurityHelper.Decrypt(emailFormatDTO.strOrderID, false));
            string AprEmail = string.Empty, AprName = string.Empty, SupportingEmail = string.Empty;
            emailFormatDTO.ReferenceNo = objGetOrderDetails[0][0].ReferenceNo;
            emailFormatDTO.Status = objGetOrderDetails[0][0].StatusID;
            emailFormatDTO.FirstName = objGetOrderDetails[0][0].FirstName;
            emailFormatDTO.LastName = objGetOrderDetails[0][0].LastName;
            emailFormatDTO.CreatedBy = objGetOrderDetails[0][0].UserName;
            emailFormatDTO.AddOffice = objGetOrderDetails[0][0].Department;
            emailFormatDTO.UserId = 0;
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
            emailFormatDTO.To = SupportingEmail;
            emailTemplate.ContactInfo(emailFormatDTO);
            objDynamic.Add(0);
            return objDynamic;
        }

        private DataSet GetOrderStatus()
        {

            string selectProcedure = "[GetOrderStatus]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();


            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("OrderApprovalData.GetOrderApp");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }

        public List<dynamic> GetAllOrderStatus()
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetOrderStatus();
            var myEnumerablecat = ds.Tables[0].AsEnumerable();

            List<StatusDetail> StatusDetail =
                (from item in myEnumerablecat
                 select new StatusDetail
                 {
                     StatusId = item.Field<Int64>("StatusId"),
                     StatusName = item.Field<string>("StatusName"),


                 }).ToList();
            objDynamic.Add(StatusDetail);

            return objDynamic;
        }

        private DataTable GetCustStatusDetail(string status, DataTable dtStatus)
        {
            DataRow[] result = dtStatus.Select("StatusId = '" + status + "'");
            int statusorder = Convert.ToInt32(result[0].ItemArray[2].ToString());
            DataTable finalStatus = dtStatus;
            finalStatus.Columns.Add("Statuscheck", typeof(System.Boolean));

            DataRow[] statusresult = dtStatus.Select("StatusOrder > '" + statusorder + "'");
            foreach (var dr in statusresult)
            {
                dr["Statuscheck"] = 0;
            }

            DataRow[] statusresultcheck = dtStatus.Select("StatusOrder <= '" + statusorder + "'");
            foreach (var dr in statusresultcheck)
            {
                dr["Statuscheck"] = 1;
            }
            return finalStatus;
        }

        public List<dynamic> GetOrderDetails(string strOrderID)
        {
            Int64 OrderID = Convert.ToInt64(strOrderID);
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetFinalOrderDetail(OrderID);
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
                    CustID = item.Field<Int64>("CustId"),

                    SEmailId = item.Field<String>("BrEmailId"),
                    SContactPerson = item.Field<String>("BrContactPerson"),
                    SContactNo = item.Field<String>("BrContactNo"),
                    StatusID = item.Field<Int64>("StatusId")


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
            var myEnumerableprodcount = ds.Tables[3].AsEnumerable();
            List<ProductCount> ProductCount =
               (from item in myEnumerableprodcount
                select new ProductCount
                {
                    ProdCount = item.Field<int>("productcount"),

                }).ToList();
            objDynamic.Add(ProductCount);


            DataTable dtstatustable = GetCustStatusDetail(ds.Tables[0].Rows[0]["StatusId"].ToString(), ds.Tables[5]);


            var myEnumerablecuststatus = dtstatustable.AsEnumerable();
            List<CustomerStatusDetail> CustomerStatusDetail =
               (from item in myEnumerablecuststatus
                select new CustomerStatusDetail
                {
                    StatusId = item.Field<Int64>("StatusId"),
                    StatusName = item.Field<String>("StatusName"),
                    StatusOrder = item.Field<int>("StatusOrder"),
                    StatusCheck = item.Field<Boolean>("Statuscheck"),

                }).ToList();
            objDynamic.Add(CustomerStatusDetail);
            List<CustOrderLog> CustOrderLog = null;
            if (ds.Tables[4].Rows.Count > 0)
            {
                var myEnumerableordLog = ds.Tables[4].AsEnumerable();

                CustOrderLog =
                 (from item in myEnumerableordLog
                  select new CustOrderLog
                  {
                      CurrentStatus = item.Field<String>("CurrentStatus"),
                      NewStatus = item.Field<String>("NewStatus"),
                      FullName = item.Field<String>("FullName"),
                      ActionDate = (item.Field<DateTime>("ActionDate")).ToString("MM/dd/yyyy HH:mm:ss"),
                      Remarks = item.Field<String>("Remarks")
                  }).ToList();


            }
            objDynamic.Add(CustOrderLog);
            int val = 0;
            //if (CustomerID != "2")
            //{
            //    val = 0;
            //}
            DataSet dsstatustable = GetStatusButton(Convert.ToInt64(ds.Tables[0].Rows[0]["StatusId"].ToString()), val);
            var mycuststatus = dsstatustable.Tables[0].AsEnumerable();
            List<ButtonList> ButtonList =
               (from item in mycuststatus
                select new ButtonList
                {
                    ButtonId = item.Field<int>("ButtonId"),
                    IsPlanson = item.Field<Boolean>("IsPlanson"),
                    IsUser = item.Field<Boolean>("IsUser"),

                }).ToList();
            objDynamic.Add(ButtonList);
            List<CustIncoTerm> CustIncoTerm = null;
            if (ds.Tables[6].Rows.Count > 0)
            {
                var myEnumerableInco = ds.Tables[6].AsEnumerable();

                CustIncoTerm =
                (from item in myEnumerableInco
                 select new CustIncoTerm
                 {
                     IncoTermId = item.Field<Int64>("IncoTermId"),
                     IncoTermCode = item.Field<String>("IncoTermCode"),
                     IncoTermDesc = item.Field<String>("IncoTermDesc"),

                 }).ToList();


            }
            objDynamic.Add(CustIncoTerm);
            List<ContactTypes> ContactTypes = null;
            if (ds.Tables[7].Rows.Count > 0)
            {
                var myEnumerablecon = ds.Tables[7].AsEnumerable();

                ContactTypes =
                (from item in myEnumerablecon
                 select new ContactTypes
                 {
                     ContactTypeId = item.Field<Int64>("ContactTypeId"),
                     ContactType = item.Field<String>("ContactType"),
                     OrderIdRequired = item.Field<bool>("OrderIdRequired"),

                 }).ToList();


            }
            objDynamic.Add(ContactTypes);
            List<OrderSupportingEmail> OrderSupportingEmail = null;
            if (ds.Tables[8].Rows.Count > 0)
            {
                var myEnumerableemail = ds.Tables[8].AsEnumerable();

                OrderSupportingEmail =
                (from item in myEnumerableemail
                 select new OrderSupportingEmail
                 {
                     SuppEmailId = item.Field<Int64>("SuppEmailId"),
                     email = item.Field<String>("email"),


                 }).ToList();


            }
            objDynamic.Add(OrderSupportingEmail);
            return objDynamic;
        }


        private DataSet GetStatusButton(Int64 StatusID, int intuser)
        {

            string selectProcedure = "[GetStatusButton]";
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            DataSet ds = new DataSet();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@StatusID", StatusID);
            da.SelectCommand.Parameters.AddWithValue("@IsUserval", intuser);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("OrderApprovalData.GetFinalOrderDetail");
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