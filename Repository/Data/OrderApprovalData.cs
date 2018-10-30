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
    public class OrderApprovalData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        SecurityHelper SecurityHelper = new SecurityHelper();

        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        string IsPlansonUser = HttpContext.Current.Session["IsPlansonUser"].ToString();
        private DataSet GetOrderApp(string strwherecondition)
        {

            string selectProcedure = "[CustOrderApprovalList]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@whereparam", strwherecondition);

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

        public List<dynamic> SearchCustomerOrderData(SearchRequestData model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

          
            string wherecondition = string.Empty, strCustomerID = string.Empty, strCountry = string.Empty, strStatus = string.Empty, strCancelOrder = string.Empty;
            string strFinalStatus = string.Empty;
            var Data = JsonConvert.DeserializeObject<List<StatusDetail>>(model.Status);
            if (CustomerID != "2")
            {
                strCustomerID = "ord.CustId =  " + CustomerID;
            }
            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    if (Data[i].StatusChk == "true" || Data[i].StatusChk == "True")
                    {
                        strStatus = strStatus + "  " + Data[i].StatusId.ToString() + "  , ";

                    }
                }
                if (!string.IsNullOrEmpty(strStatus))
                {
                    strFinalStatus = " ord.StatusId  in ( " + strStatus.Substring(0, (strStatus.Trim().Length - 1)) + " ) ";

                }
            }
            if (!string.IsNullOrEmpty(model.StatusID)  && (model.StatusID != "0"))
            {
                strFinalStatus = " ord.StatusId = " + model.StatusID;
            }
            if (!string.IsNullOrEmpty(model.CustomerID))
            {
                strCustomerID = "ord.CustId =  " + model.CustomerID;
            }
            if (!string.IsNullOrEmpty(model.CountryId))
            {
                strCountry = "ord.CountryId =  " + model.CountryId;
            }


            if (!string.IsNullOrEmpty(strCustomerID))
            {
                wherecondition = "  AND  " + strCustomerID;
            }
            if (!string.IsNullOrEmpty(strCountry))
            {
                wherecondition = wherecondition + "  AND  " + strCountry;
            }
            if (!string.IsNullOrEmpty(strFinalStatus))
            {
                wherecondition = wherecondition + "  AND  " + strFinalStatus;
            }
            DataSet ds = GetOrderApp(wherecondition);
            var myEnumerablecat = ds.Tables[0].AsEnumerable();

            List<OrderApprovalDTO> OrderApprovalDTO =
                (from item in myEnumerablecat
                 select new OrderApprovalDTO
                 {
                     OrderId = SecurityHelper.Encrypt(item.Field<Int64>("OrderId").ToString(), false),
                     ReferenceNo = item.Field<string>("ReferenceNo"),
                     Department = item.Field<string>("Department"),
                     CreatedOn = (item.Field<DateTime>("CreatedOn")).ToShortDateString(), //Need to ask customer for format
                     StatusName = item.Field<string>("StatusName"),
                     CountryName = item.Field<string>("CountryName"),
                     CityName = item.Field<string>("CityName"),
                     TotalOrderAmount = item.Field<Decimal>("TotalOrderAmount")
                 }).ToList();
            objDynamic.Add(OrderApprovalDTO);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("OrderApprovalData.SearchCustomerOrderData");
                log.logErrorMessage(ex.StackTrace);
            }

            return objDynamic;
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

        public List<dynamic> GetOrderDetails(string strOrderID)
        {
            
            Int64 OrderID = Convert.ToInt64(strOrderID);
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetFinalOrderDetail(OrderID);
            string ShippingAddress = string.Empty , strPreStatus = string.Empty;
            try
            {
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
                        Feight =   item.Field<Decimal>("Feight"),
                        TaxValue  = item.Field<Decimal>("TaxValue"),
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
                        OAID = item.Field<Int64>("OAId"),
                        AprName = item.Field<String>("ApproverName"),
                        AprTitle = item.Field<String>("ApproverTitle"),
                        AprEmail = item.Field<String>("ApproverEmail"),
                        Comments = item.Field<String>("Comments")

                    }).ToList();
                objDynamic.Add(ApproverDetails);


                var myEnumerableprod = ds.Tables[2].AsEnumerable();

                List<CustProdDetails> ProductDetails =
                   (from item in myEnumerableprod
                    select new CustProdDetails
                    {
                        ODID = item.Field<Int64>("ODID"),
                        ProdID = item.Field<Int64>("ProductId"),
                        PartNo = item.Field<String>("PartNo"),
                        Quantity = item.Field<int>("Qty"),
                        ProdPrice = item.Field<Decimal>("Rate"),
                        TotalPrice = item.Field<Decimal>("Amount"),
                        ItFormRequired = item.Field<Boolean>("ItFormRequired"),
                        LanguageFormRequired = item.Field<Boolean>("LanguageFormRequired"),
                        SoftwareFormRequired = item.Field<Boolean>("SoftwareFormRequired")
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

                if (ds.Tables[9].Rows.Count != 0)
                {
                    strPreStatus = ds.Tables[9].Rows[0]["CurrentStatusId"].ToString();
                }
                DataTable dtstatustable = GetCustStatusDetail(ds.Tables[0].Rows[0]["StatusId"].ToString(), ds.Tables[5], strPreStatus);


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
                int val = 1;
                if (CustomerID != "2")
                {
                    val = 0;
                }
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


                string PlanComm = ds.Tables[0].Rows[0]["PlansonComments"].ToString();
                string[] arrPlanComm = null;
                List<PlansonComment> lstPlansonComment = new List<PlansonComment>();
                if (!string.IsNullOrEmpty(PlanComm))
                {
                    arrPlanComm = PlanComm.Split('#');
                }
                if (arrPlanComm != null)
                {
                    for (int i = 0; i < arrPlanComm.Length; i++)
                    {
                        PlansonComment PlansonComment = new PlansonComment();
                        PlansonComment.Comments = arrPlanComm[i].ToString();
                        lstPlansonComment.Add(PlansonComment);
                    }
                }
               
                objDynamic.Add(lstPlansonComment);

                List<CustomerOrderSetting> CustomerOrderSetting = null;
                if (ds.Tables[10].Rows.Count > 0)
                {
                    var myEnumerableOrdSetting = ds.Tables[10].AsEnumerable();

                    CustomerOrderSetting =
                    (from item in myEnumerableOrdSetting
                     select new CustomerOrderSetting
                     {
                         ReferenceNoAuto = item.Field<Boolean>("ReferenceNoAuto"),
                         UseItemGroups = item.Field<Boolean>("UseItemGroups"),
                         UseItemGroupSeparatedFreight = item.Field<Boolean>("UseItemGroupSeparatedFreight"),
                         RequestNewProducts = item.Field<Boolean>("RequestNewProducts"),
                         Addresses = item.Field<int>("Addresses"),
                         Approver = item.Field<int>("Approver"),
                         LevelofAuthority = item.Field<int>("LevelofAuthority"),
                         Desgination = item.Field<Boolean>("Desgination"),

                     }).ToList();


                }
                objDynamic.Add(CustomerOrderSetting);

                List<OrdFreightDetails> OrdFreightDetails = null;
                if (ds.Tables[11].Rows.Count > 0)
                {
                    var myEnumerableFd = ds.Tables[11].AsEnumerable();

                    OrdFreightDetails =
                    (from item in myEnumerableFd
                     select new OrdFreightDetails
                     {
                         OrderFreightId = item.Field<Int64>("OrderFreightId"),
                         FreightTitle = item.Field<String>("FreightTitle"),
                         Value = item.Field<String>("Value"),
                         isTaxApplicable = item.Field<Boolean>("isTaxApplicable"),
                         isFreightAmount = item.Field<Boolean>("isFreightAmount"),
                         isLeadTime = item.Field<Boolean>("isLeadTime"),
                        

                     }).ToList();


                }
                objDynamic.Add(OrdFreightDetails);

                List<OrderProductSoftwareSetupDTO> lstOrderProductSoftwareSetupDTO = new List<OrderProductSoftwareSetupDTO>();
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    OrderProductSoftwareSetupDTO orderProductSoftwareSetupDTO = new OrderProductSoftwareSetupDTO();

                    orderProductSoftwareSetupDTO.ProductId = Convert.ToInt64(ds.Tables[2].Rows[i]["ProductId"].ToString());
                    orderProductSoftwareSetupDTO.PartNo = ds.Tables[2].Rows[i]["PartNo"].ToString();
                    if (Convert.ToBoolean(ds.Tables[2].Rows[i]["ItFormRequired"].ToString()) ==  true)
                    {
                        var rows = ds.Tables[12].Select("ProductId = '" + orderProductSoftwareSetupDTO.ProductId + "'");
                        List<OrderItsetupDTO> lstOrderItsetupDTO = new List<OrderItsetupDTO>();

                        if (rows.Length != 0)
                        {
                            foreach (var row in rows)
                            {
                                OrderItsetupDTO orderItsetupDTO = new OrderItsetupDTO();

                                orderItsetupDTO.ITSetUpId = Convert.ToInt64(row["ITSetUpId"].ToString());
                                orderItsetupDTO.ProductId = Convert.ToInt64(row["ProductId"].ToString());
                                orderItsetupDTO.UserName = row["UserName"].ToString();
                                orderItsetupDTO.UserType = row["UserType"].ToString();
                                orderItsetupDTO.WorkLocation = row["WorkLocation"].ToString();
                                orderItsetupDTO.DeliveryLocation = row["DeliveryLocation"].ToString();
                                orderItsetupDTO.Applications = row["Applications"].ToString();
                                orderItsetupDTO.SpecialInstructions = row["SpecialInstructions"].ToString();

                                lstOrderItsetupDTO.Add(orderItsetupDTO);

                            }
                        }
                        orderProductSoftwareSetupDTO.DataIt = lstOrderItsetupDTO;
                        lstOrderProductSoftwareSetupDTO.Add(orderProductSoftwareSetupDTO);
                    }


                    }

                objDynamic.Add(lstOrderProductSoftwareSetupDTO);


                List<OrderProductSoftwareSetupDTO> lstOrderProductSoftwareSetupDTO_s = new List<OrderProductSoftwareSetupDTO>();
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    OrderProductSoftwareSetupDTO orderProductSoftwareSetupDTO_s = new OrderProductSoftwareSetupDTO();

                    orderProductSoftwareSetupDTO_s.ProductId = Convert.ToInt64(ds.Tables[2].Rows[i]["ProductId"].ToString());
                    orderProductSoftwareSetupDTO_s.PartNo = ds.Tables[2].Rows[i]["PartNo"].ToString();
                    if (Convert.ToBoolean(ds.Tables[2].Rows[i]["SoftwareFormRequired"].ToString()) == true)
                    {
                        var rows = ds.Tables[13].Select("ProductId = '" + orderProductSoftwareSetupDTO_s.ProductId + "'");
                        List<OrderSoftwareSetupDTO> lstOrderSoftwareSetupDTO = new List<OrderSoftwareSetupDTO>();

                        if (rows.Length != 0)
                        {
                            foreach (var row in rows)
                            {
                                OrderSoftwareSetupDTO OrderSoftwareSetupDTOs = new OrderSoftwareSetupDTO();

                                OrderSoftwareSetupDTOs.SoftwareSetupId = Convert.ToInt64(row["SoftwareSetupId"].ToString());
                                OrderSoftwareSetupDTOs.ProductId = Convert.ToInt64(row["ProductId"].ToString());
                                OrderSoftwareSetupDTOs.UserName = row["UserName"].ToString();
                                OrderSoftwareSetupDTOs.Serial = Convert.ToInt32(row["Serial"].ToString());
                                OrderSoftwareSetupDTOs.UserEmail = row["UserEmail"].ToString();


                                lstOrderSoftwareSetupDTO.Add(OrderSoftwareSetupDTOs);

                            }
                        }
                        orderProductSoftwareSetupDTO_s.DataSS = lstOrderSoftwareSetupDTO;
                        lstOrderProductSoftwareSetupDTO_s.Add(orderProductSoftwareSetupDTO_s);
                    }
                }

                objDynamic.Add(lstOrderProductSoftwareSetupDTO_s);

               



            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
            }

            return objDynamic;
        }

        private DataTable GetCustStatusDetail(string status, DataTable dtStatus,string LastStatus)
        {
            DataRow[] result = dtStatus.Select("StatusId = '" + status + "'");
            int statusorder = Convert.ToInt32(result[0].ItemArray[2].ToString());
            DataTable finalStatus = new DataTable ();
            try
            {
                DataRow[] rem = dtStatus.Select("StatusOrder = 0");
                foreach (DataRow item in rem)
                {
                    dtStatus.Rows.Remove(item);
                }
                dtStatus.AcceptChanges();
                finalStatus = dtStatus;

                if (statusorder != 0 )
              {
                   


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
            }
            else
            {
                DataRow[] lresult = dtStatus.Select("StatusId = '" + LastStatus + "'");
                    if (lresult.Length != 0 )
                    {
                        int laststatusorder = Convert.ToInt32(lresult[0].ItemArray[2].ToString());

                        finalStatus.Columns.Add("Statuscheck", typeof(System.Boolean));

                        DataRow[] statusresult = dtStatus.Select("StatusOrder > '" + laststatusorder + "' And StatusOrder <> 0 ");
                        foreach (var dr in statusresult)
                        {
                            dr["Statuscheck"] = 0;
                        }

                        DataRow[] statusresultcheck = dtStatus.Select("StatusOrder <= '" + laststatusorder + "'And StatusOrder <> 0 ");
                        foreach (var dr in statusresultcheck)
                        {
                            dr["Statuscheck"] = 1;
                        }
                    }
                    
                

            }
            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
            }
            return finalStatus;
        }

        public List<dynamic> GetUpdateCustomerStatus(StatusChange model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            Int64 OrderID = 0, StatusID = 0, LeadTime = 0, intUserID = 0,intIncoID = 0, OAID=0;
            int type = 0;
            String Remarks = string.Empty, SalesOrderNo = string.Empty;
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
            if (!string.IsNullOrEmpty(UserID))
            {
                intUserID = Convert.ToInt32(UserID);
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
            DataSet ds = UpdateStatus(OrderID, StatusID, type, LeadTime, intUserID, intIncoID, Remarks,SalesOrderNo, OAID);



            string strCurrStatusID = ds.Tables[1].Rows[0]["StatusId"].ToString();
            string strlastStatusID = ds.Tables[2].Rows[0]["CurrentStatusId"].ToString();
            //DataTable dtstatustable = GetCustStatusDetail(StatusID.ToString(), ds.Tables[0]);
            DataTable dtstatustable = GetCustStatusDetail(strCurrStatusID, ds.Tables[0], strlastStatusID);

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
                CustRequestData custRequestData = new CustRequestData();
                if (model.Type == "10" && model.SendEmail == true)
                {
                    custRequestData.GetEmailDetails(OrderID.ToString(),string.Empty,string.Empty);
                }
                
             

            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
            }

            
            return objDynamic;
        }

        public List<dynamic> SendEmail(EmailFormatDTO emailFormatDTO)
        {
            EmailTemplate emailTemplate = new EmailTemplate();
            List<dynamic> objGetOrderDetails = new List<dynamic>();
            List<dynamic> objDynamic = new List<dynamic>();
            objGetOrderDetails = GetOrderDetails(SecurityHelper.Decrypt(emailFormatDTO.strOrderID,false));
            string AprEmail = string.Empty, AprName = string.Empty, SupportingEmail = string.Empty;
            emailFormatDTO.ReferenceNo = objGetOrderDetails[0][0].ReferenceNo;
            emailFormatDTO.Status = objGetOrderDetails[0][0].StatusID;
            emailFormatDTO.FirstName = objGetOrderDetails[0][0].FirstName;
            emailFormatDTO.LastName = objGetOrderDetails[0][0].LastName;
            emailFormatDTO.CreatedBy = objGetOrderDetails[0][0].UserName;
            emailFormatDTO.AddOffice = objGetOrderDetails[0][0].Department;
            emailFormatDTO.UserId = Convert.ToInt64(UserID);
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
            private int AddOrderFilesData(OrderFilesData ordrFls)
        {
            int FileId = 0;
            string insertProcedure = "[CreateOrderFiles]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;



            if (ordrFls.DocumentGroupId != 0)
            {
                insertCommand.Parameters.AddWithValue("@DocumentGroupId", ordrFls.DocumentGroupId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@DocumentGroupId", 0);
            }
            if (!string.IsNullOrEmpty(ordrFls.FileName))
            {
                insertCommand.Parameters.AddWithValue("@FileName", ordrFls.FileName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@FileName", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(ordrFls.FileSize))
            {
                insertCommand.Parameters.AddWithValue("@FileSize", ordrFls.FileSize);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@FileSize", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(ordrFls.FileType))
            {
                insertCommand.Parameters.AddWithValue("@FileType", ordrFls.FileType);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@FileType", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(ordrFls.FileLocation))
            {
                insertCommand.Parameters.AddWithValue("@FileLocation", ordrFls.FileLocation);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@FileLocation", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(ordrFls.Description))
            {
                insertCommand.Parameters.AddWithValue("@Description", ordrFls.Description);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Description", DBNull.Value);
            }

            insertCommand.Parameters.Add("@DocIDout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@DocIDout"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            try
            {
                int count = 0;
                connection.Open();
                insertCommand.ExecuteNonQuery();
                if (insertCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    count = Convert.ToInt32(insertCommand.Parameters["@DocIDout"].Value);
                    if (count != 0)
                    {
                        FileId = Convert.ToInt32(insertCommand.Parameters["@DocIDout"].Value);
                    }
                }



                return FileId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return FileId;
            }
            finally
            {
                connection.Close();
            }



        }

        private int UpdateOrderFiles(OrderFilesData ordrFls)
        {
            int FileId = 0;
            string updateProcedure = "[UpdateOrderFiles]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (ordrFls.FileId != 0)
            {
                updateCommand.Parameters.AddWithValue("@FileId", ordrFls.FileId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@FileId", 0);
            }


            if (ordrFls.DocumentGroupId != 0)
            {
                updateCommand.Parameters.AddWithValue("@DocumentGroupId", ordrFls.DocumentGroupId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@DocumentGroupId", 0);
            }
            if (!string.IsNullOrEmpty(ordrFls.FileName))
            {
                updateCommand.Parameters.AddWithValue("@FileName", ordrFls.FileName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@FileName", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(ordrFls.FileSize))
            {
                updateCommand.Parameters.AddWithValue("@FileSize", ordrFls.FileSize);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@FileSize", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(ordrFls.FileType))
            {
                updateCommand.Parameters.AddWithValue("@FileType", ordrFls.FileType);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@FileType", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(ordrFls.FileLocation))
            {
                updateCommand.Parameters.AddWithValue("@FileLocation", ordrFls.FileLocation);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@FileLocation", DBNull.Value);
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



                return FileId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return FileId;
            }
            finally
            {
                connection.Close();
            }



        }


        private int AddOrderFilesDocumentData(OrderFilesDocumentData ordFlsDoc)
        {
            int DocumentGroupId = 0;
            string insertProcedure = "[CreateOrderDocumentGroup]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;



            if (ordFlsDoc.OrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderId", ordFlsDoc.OrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderId", 0);
            }


            if (ordFlsDoc.NoofDocuments != 0)
            {
                insertCommand.Parameters.AddWithValue("@NoofDocuments", ordFlsDoc.NoofDocuments);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@NoofDocuments", 0);
            }

            insertCommand.Parameters.Add("@DocOrderIDout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@DocOrderIDout"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;


            try
            {
                int count = 0;
                connection.Open();
                insertCommand.ExecuteNonQuery();
                if (insertCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    count = Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                    if (count == 1)
                    {
                        DocumentGroupId = Convert.ToInt32(insertCommand.Parameters["@DocOrderIDout"].Value);
                    }
                }



                return DocumentGroupId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return DocumentGroupId;
            }
            finally
            {
                connection.Close();
            }



        }

        private int UpdateOrderFilesDocument(OrderFilesDocumentData ordFlsDoc)
        {
            int DocumentGroupId = 0;
            string updateProcedure = "[UpdateOrderDocumentGroup]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (ordFlsDoc.DocumentGroupId != 0)
            {
                updateCommand.Parameters.AddWithValue("@DocumentGroupId", ordFlsDoc.DocumentGroupId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@DocumentGroupId", 0);
            }


            if (ordFlsDoc.OrderId != 0)
            {
                updateCommand.Parameters.AddWithValue("@OrderId", ordFlsDoc.OrderId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OrderId", 0);
            }
            if (ordFlsDoc.CustId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CustId", ordFlsDoc.CustId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (ordFlsDoc.StatusId != 0)
            {
                updateCommand.Parameters.AddWithValue("@StatusId", ordFlsDoc.StatusId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@StatusId", 0);
            }
            if (ordFlsDoc.NoofDocuments != 0)
            {
                updateCommand.Parameters.AddWithValue("@NoofDocuments", ordFlsDoc.NoofDocuments);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NoofDocuments", 0);
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



                return DocumentGroupId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return DocumentGroupId;
            }
            finally
            {
                connection.Close();
            }


        }

        public int AddOrderFileDocumentmaster(string Orderid, string documents)
        {
            Int64 OrderID = 0;
            int intdocuments = 0;
            int documentgroupid = 0;
            OrderFilesDocumentData OrderFilesDocumentData = new OrderFilesDocumentData();
            if (!string.IsNullOrEmpty(Orderid))
            {
                OrderID = Convert.ToInt64(Orderid);
            }

            if (!string.IsNullOrEmpty(documents))
            {
                intdocuments = Convert.ToInt32(documents);
            }
            OrderFilesDocumentData.OrderId = OrderID;
            OrderFilesDocumentData.NoofDocuments = intdocuments;

            documentgroupid = AddOrderFilesDocumentData(OrderFilesDocumentData);
            return documentgroupid;

        }

        public int AddOrderFiles(OrderFilesData OrderFilesData)
        {
            int ordid = 0;
            ordid = AddOrderFilesData(OrderFilesData);
            return ordid;
        }


        private DataSet GetOrderFileData(Int64 OrderID)
        {

            string selectProcedure = "[GetOrderFilesData]";
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


        public List<dynamic> GetOrderFileDetails(Int64 OrderID)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetOrderFileData(OrderID);
            var myEnumerablecat = ds.Tables[0].AsEnumerable();
            try
            {
                List<OrderFileData> OrderFileData =
                (from item in myEnumerablecat
                 select new OrderFileData
                 {
                     FileId = item.Field<Int64>("FileId"),
                     FileName = item.Field<string>("FileName"),
                     FileLocation = item.Field<string>("FileLocation"),
                     Description = item.Field<string>("Description"),
                     DocumentGroupId = item.Field<Int64>("DocumentGroupId")

                 }).ToList();
                objDynamic.Add(OrderFileData);
            }
            catch(Exception ex)
            {

            }


            

            return objDynamic;
        }

        private DataSet UpdateStatus(Int64 OrderID, Int64 StatusID, int Type, Int64 LeadTime, Int64 USerID, Int64 IncoID, String Remarks, String SalesOrderNo, Int64 OAID)
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

                //CustRequestData custRequestData = new CustRequestData();
                //custRequestData.GetEmailDetails(OrderID.ToString());


                string selectProcedure = "[GetCurrentCustomerStatus]";
                SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
                SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                da.SelectCommand.Parameters.AddWithValue("@orderID", OrderID);
               

                try
                {
                    da.Fill(ds);
                    CustRequestData custRequestData = new CustRequestData();
                    custRequestData.GetEmailDetails(OrderID.ToString(), string.Empty, string.Empty);
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


    }
}