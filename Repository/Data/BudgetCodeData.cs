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
    public class BudgetCodeData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();
        private DataSet GetCustBudgetData(Int64 OrderId)
        {

            string selectProcedure = "[GetCustBudgetCodeMaster]";
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
        #region comment
        //public List<BudgetCodedetail> GetCustBudgetMastr(Int64 OrderId)
        //{
        //    List<dynamic> objDynamic = new List<dynamic>();
        //    DataSet ds = GetCustBudgetData(OrderId);
        //    var myEnumerablebdg = ds.Tables[0].AsEnumerable();
        //  /*
        //    List<GetBudgetCodeMstr> GetBudgetCodeMstr =
        //       (from item in myEnumerablebdg
        //        select new GetBudgetCodeMstr
        //        {
        //            BudgetId = item.Field<Int64>("BudgetId"),
        //            BudgetTitle = item.Field<String>("BudgetTitle"),
        //            isRequired = item.Field<Boolean>("isRequired"),
        //            FldLength = item.Field<int>("FldLength"),
        //            Serial = item.Field<int>("Serial"),
        //            DependOn = item.Field<Int64>("DependOn")

        //        }).ToList();


        //    objDynamic.Add(GetBudgetCodeMstr);

        //    var Order = ds.Tables[1].AsEnumerable();
        //    List<OrdrDetail> OrdrDetail =
        //       (from item in Order
        //        select new OrdrDetail
        //        {
        //            ODID = item.Field<Int64>("ODID"),
        //            Serial = item.Field<int>("Serial"),
        //            ProductId = item.Field<Int64>("ProductId"),
        //            ProductName = item.Field<String>("ProductName"),
        //            Qty = item.Field<int>("Qty"),
        //            Rate = item.Field<Decimal>("Rate"),
        //            Amount = item.Field<Decimal>("Amount")

        //        }).ToList();
        //    List<object> objectList = GetBudgetCodeMstr.Cast<object>().Concat(OrdrDetail).ToList();
        // */

        //    var GetBudgetCodeMstr = myEnumerablebdg.Select(item => new BudgetCodedetail ()
        //    {
        //        BudgetId = item.Field<Int64>("BudgetId"),
        //        BudgetTitle = item.Field<String>("BudgetTitle"),
        //        isRequired = item.Field<Boolean>("isRequired"),
        //        FldLength = item.Field<int>("FldLength"),
        //        Serial = item.Field<int>("Serial"),
        //        DependOn = item.Field<Int64>("DependOn"),

        //    });


        //    var OrdrDetail = ds.Tables[1].AsEnumerable().Select(item => new BudgetCodedetail()
        //    {

        //        ODID = item.Field<Int64>("ODID"),
        //        Serial = item.Field<int>("Serial"),
        //        ProductId = item.Field<Int64>("ProductId"),
        //        ProductName = item.Field<String>("ProductName"),
        //        Qty = item.Field<int>("Qty"),
        //        Rate = item.Field<Decimal>("Rate"),
        //        Amount = item.Field<Decimal>("Amount")

        //    });
        //    var objectList = GetBudgetCodeMstr.Union(OrdrDetail).ToList();

        //   // objDynamic.Add(objectList);

        //    return objectList;
        //}
        #endregion

        public List<Dictionary<string, string>> GetCustBudgetMastr(Int64 OrderId)

        {
            List<Dictionary<string, string>> objDynamic = new List<Dictionary<string, string>>();
            DataSet ds = GetCustBudgetData(OrderId);
            var myEnumerablebdg = ds.Tables[0].AsEnumerable();

            var columns = ds.Tables[0].AsEnumerable().ToDictionary(x => "budgetid_" + x.Field<long>("BudgetId").ToString(), x => x.Field<string>("BudgetTitle"));
            foreach (var item in ds.Tables[1].AsEnumerable())
            {
                columns.Add("odid_" + item.Field<long>("ODID").ToString(), item.Field<string>("ProductName"));
            }

            var amount = ds.Tables[0].AsEnumerable().ToDictionary(x => "budgetid_" + x.Field<long>("BudgetId").ToString(), x => string.Empty);
            foreach (var item in ds.Tables[1].AsEnumerable())
            {
                amount.Add("odid_" + item.Field<long>("ODID").ToString(), item.Field<Decimal>("Amount").ToString());
            }

            foreach (var itemadd in ds.Tables[3].AsEnumerable())
            {
                if (itemadd.Field<Decimal>("Feight") != 0)
                {
                    columns.Add("odid_Freight_1", "Freight");
                    amount.Add("odid_Freight_" + 1, itemadd.Field<Decimal>("Feight").ToString());
                }
                if (itemadd.Field<Decimal>("TaxValue") != 0)
                {
                    columns.Add("odid_TaxValue_1", "TaxAmount");
                    amount.Add("odid_TaxValue_" + 1, itemadd.Field<Decimal>("TaxValue").ToString());
                }

            }
            columns.Add("Total_1", "Total");
            //foreach (var item in ds.Tables[2].AsEnumerable())
            foreach (var item in ds.Tables[3].AsEnumerable())

            {
                amount.Add("Total_1", item.Field<Decimal>("TotalAmount").ToString());
            }


            var PercentAmount = ds.Tables[0].AsEnumerable().ToDictionary(x => "budgetid_" + x.Field<long>("BudgetId").ToString(), x => string.Empty);
            foreach (var item in ds.Tables[1].AsEnumerable())
            {
                PercentAmount.Add("odid_" + item.Field<long>("ODID").ToString(), "100%");
            }

            foreach (var itemadd in ds.Tables[3].AsEnumerable())
            {
                if (itemadd.Field<Decimal>("Feight") != 0)
                {

                    PercentAmount.Add("odid_Freight_" + 1, "100%");
                }
                if (itemadd.Field<Decimal>("TaxValue") != 0)
                {

                    PercentAmount.Add("odid_TaxValue_" + 1, "100%");
                }

            }
            foreach (var item in ds.Tables[2].AsEnumerable())
            {
                PercentAmount.Add("Total_1", "100%");
            }


            objDynamic.Add(columns);
            objDynamic.Add(amount);
            objDynamic.Add(PercentAmount);
            return objDynamic;
        }
        public List<dynamic> AddCustBudgetMastr(BudgetCodeWiseOrderDetailDTO BudgetCode)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            var Data = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(BudgetCode.BudgetList);
            Int64 OrderID = 0, CustID = 0;
            switch (BudgetCode.Type)
            {
                case 1:
                    {
                        #region Add
                        BudgetCodeOrder budgetordr = new BudgetCodeOrder();
                        if (!string.IsNullOrEmpty(securityHelper.Decrypt((BudgetCode.OrderID), false)))
                        {
                            OrderID = Convert.ToInt64(securityHelper.Decrypt((BudgetCode.OrderID), false));
                        }
                        if (!string.IsNullOrEmpty(CustomerID))
                        {
                            CustID = Convert.ToInt64(CustomerID);
                        }
                        if (!string.IsNullOrEmpty(UserID))
                        {
                            budgetordr.UserID = Convert.ToInt64(UserID);
                        }
                        budgetordr.OrderId = OrderID;
                        budgetordr.CustId = CustID;
                        budgetordr.NoofBudgetCodes = Data.Count;
                        budgetordr.Plansoncomment = BudgetCode.Plansoncomment;
                        budgetordr.Approvercomment = BudgetCode.Approvercomment;
                        budgetordr.Approveremail = BudgetCode.Approveremail;

                        if (!string.IsNullOrEmpty(BudgetCode.BudgetSplit))
                        {
                            budgetordr.BudgetCodeSplitOn = Convert.ToBoolean(Convert.ToInt32(BudgetCode.BudgetSplit));
                        }
                        if (!string.IsNullOrEmpty(BudgetCode.BudgetFreight))
                        {
                            budgetordr.BudgetCodetoFreight = Convert.ToBoolean(Convert.ToInt32(BudgetCode.BudgetFreight));
                        }
                        Int64 BudgetID = AddBudgetCodeordr(budgetordr);

                        var AppData = JsonConvert.DeserializeObject<List<ApproverDetails>>(BudgetCode.Approveremail);
                        for (int j = 0; j < AppData.Count; j++)
                        {
                            ApproverDetails approverDetails = new ApproverDetails();
                            approverDetails = AppData[j];
                            approverDetails.OrderId = OrderID;
                            if (approverDetails.OAID != 0)
                            {
                                SaveCustOrderApprover(approverDetails, Convert.ToInt64(UserID), 2);
                            }
                            else
                            {
                                SaveCustOrderApprover(approverDetails, Convert.ToInt64(UserID), 1);
                            }

                        }

                        for (int i = 0; i < Data.Count; i++)
                        {
                            Dictionary<string, string> BudgetValue = new Dictionary<string, string>();
                            BudgetCodewiseOrdrTotl budgetCodeWOTtl = new BudgetCodewiseOrdrTotl();
                            BudgetValue = Data[i];
                            Decimal BudgetLineTotal = 0;
                            Int64 BudgetSerialID = 0;
                            foreach (String key in BudgetValue.Keys)
                            {
                                if (key.Equals("Total_1"))
                                {
                                    BudgetLineTotal = Convert.ToDecimal(BudgetValue[key]);
                                }
                            }
                            budgetCodeWOTtl.BudgetOrderId = BudgetID;
                            budgetCodeWOTtl.Serial = i + 1;
                            budgetCodeWOTtl.Total = BudgetLineTotal;
                            BudgetSerialID = AddBudgetCodeWOTotal(budgetCodeWOTtl);
                            int Serial = 0;
                            foreach (String key in BudgetValue.Keys)
                            {
                                BudgtCodeOrdrItmDtls budgetCOrdrItmDtl = new BudgtCodeOrdrItmDtls();
                                BudgetCodewiseOrderDetail BudgetCodewiseOrderDetail = new BudgetCodewiseOrderDetail();

                                if (key.Split('_')[0] == "budgetid")
                                {

                                    int intBudgetID = Convert.ToInt32(key.Split('_')[1].ToString());
                                    BudgetCodewiseOrderDetail.BudgetId = intBudgetID;
                                    BudgetCodewiseOrderDetail.BudgetOrderSerialId = BudgetSerialID;
                                    BudgetCodewiseOrderDetail.Serial = Serial;
                                    BudgetCodewiseOrderDetail.Value = BudgetValue[key];
                                    int BudgetIdCodewiseOrdr = AddBudgetCodewiseOrdrDtl(BudgetCodewiseOrderDetail);
                                    Serial = Serial + 1;

                                }
                                if (key.Split('_')[0] == "odid")
                                {
                                    int intODID = 0;
                                    budgetCOrdrItmDtl.IsFreightAmount = false;
                                    if (key.Split('_')[1].ToString() == "Freight")
                                    {
                                        //budgetCOrdrItmDtl.IsFreightAmount = Convert.ToBoolean(Convert.ToInt32(BudgetCode.BudgetFreight)); // Need to ask customer for Logic
                                        budgetCOrdrItmDtl.IsFreightAmount = true; // Need to ask customer for Logic
                                    }
                                  else  if (key.Split('_')[1].ToString() == "TaxValue")
                                    {
                                        budgetCOrdrItmDtl.IsFreightAmount = false; // Need to ask customer for Logic
                                    }
                                    else
                                    {
                                        intODID = Convert.ToInt32(key.Split('_')[1].ToString());
                                    }

                                    if (!string.IsNullOrEmpty(BudgetValue[key]))
                                    {
                                        budgetCOrdrItmDtl.Amount = Convert.ToDecimal(BudgetValue[key]);
                                    }

                                    else
                                    {

                                        budgetCOrdrItmDtl.Amount = 0;
                                    }
                                    budgetCOrdrItmDtl.BudgetOrderSerialId = BudgetSerialID;
                                    budgetCOrdrItmDtl.ProductId = intODID;

                                    int BudgetIdSerialItemId = AddBudgetCodeordrItmDetail(budgetCOrdrItmDtl);



                                }
                            }
                            if (i == (Data.Count - 1))
                            {
                                objDynamic.Add(1);
                            }
                        }
                        #endregion

                        EmailTemplate emailTemplate = new EmailTemplate();
                        CustRequestData custRequestData = new CustRequestData();
                        custRequestData.GetEmailDetails(OrderID.ToString(), string.Empty, string.Empty);
                        break;
                    }


                case 2:
                    {
                        #region Add
                        BudgetCodeOrder budgetordr = new BudgetCodeOrder();
                        if (!string.IsNullOrEmpty(securityHelper.Decrypt((BudgetCode.OrderID), false)))
                        {
                            OrderID = Convert.ToInt64(securityHelper.Decrypt((BudgetCode.OrderID), false));
                        }
                        if (!string.IsNullOrEmpty(CustomerID))
                        {
                            CustID = Convert.ToInt64(CustomerID);
                        }
                        if (!string.IsNullOrEmpty(UserID))
                        {
                            budgetordr.UserID = Convert.ToInt64(UserID);
                        }
                        budgetordr.OrderId = OrderID;
                        budgetordr.CustId = CustID;
                        budgetordr.NoofBudgetCodes = Data.Count;
                        budgetordr.Plansoncomment = BudgetCode.Plansoncomment;
                        budgetordr.Approvercomment = BudgetCode.Approvercomment;
                        budgetordr.Approveremail = BudgetCode.Approveremail;

                        if (!string.IsNullOrEmpty(BudgetCode.BudgetSplit))
                        {
                            budgetordr.BudgetCodeSplitOn = Convert.ToBoolean(Convert.ToInt32(BudgetCode.BudgetSplit));
                        }
                        if (!string.IsNullOrEmpty(BudgetCode.BudgetFreight))
                        {
                            budgetordr.BudgetCodetoFreight = Convert.ToBoolean(Convert.ToInt32(BudgetCode.BudgetFreight));
                        }
                        //Int64 BudgetID = AddBudgetCodeordr(budgetordr);

                        var AppData = JsonConvert.DeserializeObject<List<ApproverDetails>>(BudgetCode.Approveremail);
                        for (int j = 0; j < AppData.Count; j++)
                        {
                            ApproverDetails approverDetails = new ApproverDetails();
                            approverDetails = AppData[j];
                            approverDetails.OrderId = OrderID;
                            if (approverDetails.OAID != 0)
                            {
                                SaveCustOrderApprover(approverDetails, Convert.ToInt64(UserID), 2);
                            }
                            else
                            {
                                SaveCustOrderApprover(approverDetails, Convert.ToInt64(UserID), 1);
                            }

                        }

                        for (int i = 0; i < Data.Count; i++)
                        {
                            Dictionary<string, string> BudgetValue = new Dictionary<string, string>();
                            BudgetCodewiseOrdrTotl budgetCodeWOTtl = new BudgetCodewiseOrdrTotl();
                            BudgetValue = Data[i];
                            Decimal BudgetLineTotal = 0;
                            Int64 BudgetSerialID = 0;
                            foreach (String key in BudgetValue.Keys)
                            {
                                string[] splitkey = key.Split('_');
                                if (splitkey[0].Equals("Total"))
                                {
                                    BudgetSerialID = Convert.ToInt64(splitkey[1]);
                                    BudgetLineTotal = Convert.ToDecimal(BudgetValue[key]);
                                  
                                }
                            }
                            budgetordr.BudgetOrderId = BudgetSerialID;
                            UpdateBudgetCodeordr(budgetordr, BudgetLineTotal);

                 
                       
                            foreach (String key in BudgetValue.Keys)
                            {
                                BudgtCodeOrdrItmDtls budgetCOrdrItmDtl = new BudgtCodeOrdrItmDtls();
                                BudgetCodewiseOrderDetail BudgetCodewiseOrderDetail = new BudgetCodewiseOrderDetail();

                                if (key.Split('_')[0] == "budgetid")
                                {

                                    int intBudgetID = Convert.ToInt32(key.Split('_')[1].ToString());
                                 
                                    BudgetCodewiseOrderDetail.BudgetOrderPkey = intBudgetID;
                                  
                                    BudgetCodewiseOrderDetail.Value = BudgetValue[key];
                                    UpdateBudgetCWOrder(BudgetCodewiseOrderDetail);

                                }
                                if (key.Split('_')[0] == "odid")
                                {
                                    int intODID = 0;
                                    budgetCOrdrItmDtl.IsFreightAmount = false;
                                    if (key.Split('_')[1].ToString() == "Freight")
                                    {
                                        //budgetCOrdrItmDtl.IsFreightAmount = Convert.ToBoolean(Convert.ToInt32(BudgetCode.BudgetFreight)); // Need to ask customer for Logic
                                        budgetCOrdrItmDtl.IsFreightAmount = true; // Need to ask customer for Logic
                                    }
                                  else  if (key.Split('_')[1].ToString() == "TaxValue")
                                    {
                                        budgetCOrdrItmDtl.IsFreightAmount = false; // Need to ask customer for Logic
                                    }
                                    else
                                    {
                                        intODID = Convert.ToInt32(key.Split('_')[1].ToString());
                                    }

                                    if (!string.IsNullOrEmpty(BudgetValue[key]))
                                    {
                                        budgetCOrdrItmDtl.Amount = Convert.ToDecimal(BudgetValue[key]);
                                    }

                                    else
                                    {

                                        budgetCOrdrItmDtl.Amount = 0;
                                    }
                                    budgetCOrdrItmDtl.BudgetOrderSerialItemId = intODID;
                                    UpdateBudgetCodeordrItmDetail(budgetCOrdrItmDtl);

                                  



                                }
                            }
                            if (i == (Data.Count - 1))
                            {
                                objDynamic.Add(1);
                            }
                        }
                        #endregion
                        break;
                    }
            }
         
       

            return objDynamic;
        }

        private ApproverDetails SaveCustOrderApprover(ApproverDetails ordapp, Int64 UserID, int Type)
        {

            int count = 0;
            string updateProcedure = "[UpdateOrderApproverEmail]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (ordapp.OAID != 0)
            {
                updateCommand.Parameters.AddWithValue("@OAId", ordapp.OAID);
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




            if (!string.IsNullOrEmpty(ordapp.AprEmail))
            {
                updateCommand.Parameters.AddWithValue("@ApproverEmail", ordapp.AprEmail);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ApproverEmail", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(ordapp.Comments))
            {
                updateCommand.Parameters.AddWithValue("@Comments", ordapp.Comments);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Comments", DBNull.Value);
            }
            if (UserID != 0)
            {
                updateCommand.Parameters.AddWithValue("@UserId", UserID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@UserId", 0);
            }




            if (Type != 0)
            {
                updateCommand.Parameters.AddWithValue("@Type", Type);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Type", 0);
            }
            updateCommand.Parameters.Add("@OAIdOut", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@OAIdOut"].Direction = ParameterDirection.Output;

            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;


            try
            {

                connection.Open();
                updateCommand.ExecuteNonQuery();
                if (updateCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
                    if (ordapp.OAID == 0 && count != 0)
                    {
                        ordapp.OAID = System.Convert.ToInt64(updateCommand.Parameters["@OAIdOut"].Value);
                    }
                }




                return ordapp;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return ordapp;
            }
            finally
            {
                connection.Close();
            }



        }
        private DataSet GetBudgetOrderProductDetails(Int64 OrderId)
        {

            string selectProcedure = "[GetBudgetOrderProductDetails]";
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

        public List<dynamic> GetViewBudgetProductDetails(string strOrderID)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {
                Int64 OrderID = 0,intSerialID = 0;
                Decimal TotalTax = 0, TotalFiefght = 0, TotalVal = 0;
                OrderID = Convert.ToInt64(securityHelper.Decrypt(strOrderID, false));
                DataSet ds = GetBudgetOrderProductDetails(OrderID);
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    intSerialID = Convert.ToInt64(ds.Tables[2].Rows[i]["BudgetOrderSerialId"].ToString());
                    DataRow[] drbudget = ds.Tables[0].Select("BudgetOrderSerialId = " + intSerialID + "");
                    DataRow[] drProduct = ds.Tables[3].Select("BudgetOrderSerialId = " + intSerialID + "");
                    DataRow[] drFrghtval = ds.Tables[7].Select("BudgetOrderSerialId = " + intSerialID + " And IsFreightAmount = 1");
                    DataRow[] drTaxval = ds.Tables[7].Select("BudgetOrderSerialId = " + intSerialID + " And IsFreightAmount = 0");
                    var columns = drbudget.AsEnumerable().ToDictionary(x => "budgetid_" + x.Field<long>("BudgetOrderPkey").ToString(), x => x.Field<string>("BudgetTitle"));
                    foreach (var item in drProduct.AsEnumerable())
                    {
                        columns.Add("odid_" + item.Field<long>("BudgetOrderSerialItemId").ToString(), item.Field<string>("ProductName"));
                    }
                    foreach (var item in drFrghtval.AsEnumerable())
                    {
                        columns.Add("odid_" + item.Field<long>("BudgetOrderSerialItemId").ToString(), "Freight");
                    }
                    foreach (var item in drTaxval.AsEnumerable())
                    {
                        columns.Add("odid_" + item.Field<long>("BudgetOrderSerialItemId").ToString(), "Tax");
                    }
                    columns.Add("Total_" + intSerialID, "Total"); // Horizontal Total
                    objDynamic.Add(columns);
                   
                    var Data = drbudget.AsEnumerable().ToDictionary(x => "budgetid_" + x.Field<long>("BudgetOrderPkey").ToString(), x => x.Field<string>("Value"));
                   
                    foreach (var item in drProduct.AsEnumerable())
                    {
                        Data.Add("odid_" + item.Field<long>("BudgetOrderSerialItemId").ToString()+ "_"+item.Field<long>("ProductId").ToString(), item.Field<Decimal>("Amount").ToString());
                      
                    }
                    foreach (var item in drFrghtval.AsEnumerable())
                    {
                        Data.Add("TotalFiefght_" + item.Field<long>("BudgetOrderSerialItemId").ToString()+ "_" + item.Field<long>("BudgetOrderSerialItemId").ToString(), item.Field<Decimal>("Amount").ToString());
                        TotalFiefght = TotalFiefght + item.Field<Decimal>("Amount");
                    }
                    foreach (var item in drTaxval.AsEnumerable())
                    {
                        Data.Add("TotalTax_" + item.Field<long>("BudgetOrderSerialItemId").ToString()+ "_" + item.Field<long>("BudgetOrderSerialItemId").ToString(), item.Field<Decimal>("Amount").ToString());
                        TotalTax = TotalTax + item.Field<Decimal>("Amount");
                    }
                    Data.Add("Total_" + intSerialID, ds.Tables[2].Rows[i]["Total"].ToString()); // Horizontal Total
                    if (!string.IsNullOrEmpty(ds.Tables[2].Rows[i]["Total"].ToString()))
                    {
                        TotalVal = TotalVal + Convert.ToDecimal(ds.Tables[2].Rows[i]["Total"].ToString());
                    }
                    objDynamic.Add(Data);
                 
                }
                DataRow[] drbudgettotal = ds.Tables[0].Select("BudgetOrderSerialId = " + intSerialID + "");
                DataRow[] drProducttotal = ds.Tables[3].Select("BudgetOrderSerialId = " + intSerialID + "");

                var total = drbudgettotal.AsEnumerable().ToDictionary(x => "budgetid_" + x.Field<long>("BudgetOrderPkey").ToString(), x =>"");
                foreach (var item in drProducttotal.AsEnumerable())
                {
                    for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                    {
                        if (ds.Tables[6].Rows[i]["ProductId"].ToString() == item.Field<Int64>("ProductId").ToString())
                        {
                            total.Add("odid_" + item.Field<long>("ProductId").ToString(), ds.Tables[6].Rows[i]["Amount"].ToString());
                        }
                    }
                   
                }
                total.Add("TotalFiefght_1", TotalFiefght.ToString());
                total.Add("TotalTax_1", TotalTax.ToString());
                total.Add("TotalVal_1", TotalVal.ToString());

                objDynamic.Add(total);
                var pertotal = drbudgettotal.AsEnumerable().ToDictionary(x => "budgetid_" + x.Field<long>("BudgetOrderPkey").ToString(), x => "");
                foreach (var item in drProducttotal.AsEnumerable())
                {
                    for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                    {
                        if (ds.Tables[6].Rows[i]["ProductId"].ToString() == item.Field<Int64>("ProductId").ToString())
                        {
                            pertotal.Add("odid_"+ item.Field<long>("ProductId").ToString(), "100");
                        }
                    }

                }

                pertotal.Add("TotalFiefght_1", "100");
                pertotal.Add("TotalTax_1", "100");

                objDynamic.Add(pertotal);

                var Finaltotal = drbudgettotal.AsEnumerable().ToDictionary(x => "budgetid_" + x.Field<long>("BudgetOrderPkey").ToString(), x => "");
                foreach (var item in drProducttotal.AsEnumerable())
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if (ds.Tables[1].Rows[i]["ProductId"].ToString() == item.Field<Int64>("ProductId").ToString())
                        {
                            Finaltotal.Add("odid_" +  item.Field<long>("ProductId").ToString(), ds.Tables[1].Rows[i]["Amount"].ToString());
                        }
                    }

                }
                Finaltotal.Add("TotalFiefght_1", TotalFiefght.ToString());
                Finaltotal.Add("TotalTax_1", TotalTax.ToString());
                objDynamic.Add(Finaltotal);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetViewBudgetProductDetails");
                log.logErrorMessage(ex.StackTrace);
               
            }
            return objDynamic;
        }

       public List<dynamic> GetViewBudgetOrderProductDetails(string strOrderID)
        {
            List<dynamic> objDynamic = new List<dynamic>();

            try
            {

           
            Int64 OrderID = 0;
            OrderID = Convert.ToInt64(securityHelper.Decrypt(strOrderID, false));
            DataSet ds = GetBudgetOrderProductDetails(OrderID);

            var myEnumerable = ds.Tables[0].AsEnumerable(); // Grant


            List<BudgetCodeOrderDetail> BudgetCodeOrderDetail =
                (from item in myEnumerable
                 select new BudgetCodeOrderDetail
                 {
                     BudgetOrderPkey = item.Field<Int64>("BudgetOrderPkey"),
                     BudgetOrderSerialId = item.Field<Int64>("BudgetOrderSerialId"),
                     BudgetId = item.Field<Int64>("BudgetId"),
                     BudgetTitle = item.Field<String>("BudgetTitle"),
                     strValue = item.Field<String>("Value"),



                 }).ToList();

            objDynamic.Add(BudgetCodeOrderDetail);





            var myEnumerableser = ds.Tables[2].AsEnumerable();
            List<BudgetCodewiseOrdrTotl> lstBudgetCodewiseOrdrTotl = new List<BudgetCodewiseOrdrTotl>();
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                BudgetCodewiseOrdrTotl BudgetCodewiseOrdrTotl = new BudgetCodewiseOrdrTotl();

                BudgetCodewiseOrdrTotl.BudgetOrderSerialId = Convert.ToInt64(ds.Tables[2].Rows[i]["BudgetOrderSerialId"].ToString());
                BudgetCodewiseOrdrTotl.Serial = Convert.ToInt32(ds.Tables[2].Rows[i]["Serial"].ToString());
                BudgetCodewiseOrdrTotl.Total = Convert.ToDecimal(ds.Tables[2].Rows[i]["Total"].ToString());

                var rows = ds.Tables[3].Select("BudgetOrderSerialId = '" + ds.Tables[2].Rows[i]["BudgetOrderSerialId"].ToString() + "'");
                List<BudgetCodedetail> lstBudgetCodedetail = new List<BudgetCodedetail>();

                if (rows.Length != 0)
                {
                    foreach (var row in rows)
                    {
                        BudgetCodedetail BudgetCodedetail = new BudgetCodedetail();
                        BudgetCodedetail.ProductName = row["ProductName"].ToString();
                        BudgetCodedetail.ProductId = Convert.ToInt64(row["ProductId"].ToString());
                        BudgetCodedetail.Amount = Convert.ToDecimal(row["Amount"]);
                        BudgetCodedetail.Qty = int.Parse(row["Qty"].ToString());
                        BudgetCodedetail.Rate = Convert.ToDecimal(row["Rate"]);
                        BudgetCodedetail.BudgetOrderSerialItemId = Int64.Parse(row["BudgetOrderSerialItemId"].ToString());

                        lstBudgetCodedetail.Add(BudgetCodedetail);
                        // grantSerial.Data = lstgrandata;
                        //lstgrantSerials.Add(grantSerial);

                    }
                }
                else
                {
                    BudgetCodedetail BudgetCodedetail = new BudgetCodedetail();
                    //  List<GrantData> lstgrandata = new List<GrantData>();
                    BudgetCodedetail.ProductName = "";
                    BudgetCodedetail.ProductId = 0;
                    BudgetCodedetail.Amount = 0;
                    BudgetCodedetail.Qty = 1;
                    BudgetCodedetail.Rate = 0;



                    lstBudgetCodedetail.Add(BudgetCodedetail);

                }

                BudgetCodewiseOrdrTotl.Data = lstBudgetCodedetail;
                lstBudgetCodewiseOrdrTotl.Add(BudgetCodewiseOrdrTotl);


            }

            objDynamic.Add(lstBudgetCodewiseOrdrTotl);
            if (ds.Tables.Count > 5)
            {
                List<OrderTotal> OrderTotal =
       (from item in ds.Tables[5].AsEnumerable()
        select new OrderTotal
        {
            TotalOrderAmount = item.Field<Decimal>("TotalOrderAmount"),
        }).ToList();
                objDynamic.Add(OrderTotal);
            }
            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
               
            }
            return objDynamic;
        }


        private int AddBudgetCodeordr(BudgetCodeOrder budgetordr)
        {
            int BudgetId = 0;
            string insertProcedure = "[CreateBudgetCodeOrder]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;




            if (budgetordr.OrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderId ", budgetordr.OrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderId ", 0);
            }
            if (budgetordr.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId ", budgetordr.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId ", 0);
            }

            if (budgetordr.NoofBudgetCodes != 0)
            {
                insertCommand.Parameters.AddWithValue("@NoofBudgetCodes ", budgetordr.NoofBudgetCodes);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@NoofBudgetCodes ", 0);
            }


            if (budgetordr.BudgetCodeSplitOn)
            {
                insertCommand.Parameters.AddWithValue("@BudgetCodeSplitOn", budgetordr.BudgetCodeSplitOn);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetCodeSplitOn", 0);
            }
            if (budgetordr.BudgetCodetoFreight)
            {
                insertCommand.Parameters.AddWithValue("@BudgetCodetoFreight", budgetordr.BudgetCodetoFreight);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetCodetoFreight", 0);
            }

            if (!string.IsNullOrEmpty(budgetordr.Plansoncomment))
            {
                insertCommand.Parameters.AddWithValue("@Plansoncomment", budgetordr.Plansoncomment);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Plansoncomment", DBNull.Value);
            }
            //if (!string.IsNullOrEmpty(budgetordr.Approvercomment))
            //{
            //    insertCommand.Parameters.AddWithValue("@Approvercomment", budgetordr.Approvercomment);
            //}
            //else
            //{
            //    insertCommand.Parameters.AddWithValue("@Approvercomment", DBNull.Value);
            //}
            //if (!string.IsNullOrEmpty(budgetordr.Approveremail))
            //{
            //    insertCommand.Parameters.AddWithValue("@Approveremail", budgetordr.Approvercomment);
            //}
            //else
            //{
            //    insertCommand.Parameters.AddWithValue("@Approveremail", DBNull.Value);
            //}
            if (budgetordr.UserID != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", budgetordr.UserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@BudgetIdout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@BudgetIdout"].Direction = ParameterDirection.Output;
            try
            {
                int count = 0;
                connection.Open();
                insertCommand.ExecuteNonQuery();
                if (insertCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                    if (count != 0)
                    {
                        if (insertCommand.Parameters["@BudgetIdout"].Value != DBNull.Value)
                        {
                            BudgetId = System.Convert.ToInt32(insertCommand.Parameters["@BudgetIdout"].Value);
                        }
                    }
                }




                return BudgetId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return BudgetId;
            }
            finally
            {
                connection.Close();
            }



        }

        private int AddBudgetCodeWOTotal(BudgetCodewiseOrdrTotl budgetCodeWOTtl)
        {
            int BudgetIdSerialID = 0;
            string insertProcedure = "[CreatBudgtCodewiseOrdrTotl]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;


            if (budgetCodeWOTtl.BudgetOrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@BudgetOrderId", budgetCodeWOTtl.BudgetOrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetOrderId", 0);
            }


            if (budgetCodeWOTtl.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial ", budgetCodeWOTtl.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial ", 0);
            }
            if (budgetCodeWOTtl.Total != 0)
            {
                insertCommand.Parameters.AddWithValue("@Total ", budgetCodeWOTtl.Total);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Total ", 0);
            }

            insertCommand.Parameters.Add("@BudgetOrderSerialIdout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@BudgetOrderSerialIdout"].Direction = ParameterDirection.Output;

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
                if (count != 0)
                {


                    if (insertCommand.Parameters["@BudgetOrderSerialIdout"].Value != DBNull.Value)
                    {
                        BudgetIdSerialID = System.Convert.ToInt32(insertCommand.Parameters["@BudgetOrderSerialIdout"].Value);
                    }
                }


                return BudgetIdSerialID;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return BudgetIdSerialID;
            }
            finally
            {
                connection.Close();
            }



        }


        private int AddBudgetCodeordrItmDetail(BudgtCodeOrdrItmDtls budgetCOrdrItmDtl)
        {
            int BudgetIdSerialItemId = 0;
            string insertProcedure = "[CreateBudgetCodeOrderItemDetails]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;



            if (budgetCOrdrItmDtl.BudgetOrderSerialId != 0)
            {
                insertCommand.Parameters.AddWithValue("@BudgetOrderSerialId", budgetCOrdrItmDtl.BudgetOrderSerialId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetOrderSerialId", 0);
            }
            if (budgetCOrdrItmDtl.ProductId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ODID", budgetCOrdrItmDtl.ProductId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ODID", 0);
            }
            if (budgetCOrdrItmDtl.Amount != 0)
            {
                insertCommand.Parameters.AddWithValue("@Amount", budgetCOrdrItmDtl.Amount);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Amount", 0);
            }

            if (budgetCOrdrItmDtl.IsFreightAmount)
            {
                insertCommand.Parameters.AddWithValue("@IsFreightAmount ", budgetCOrdrItmDtl.IsFreightAmount);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsFreightAmount ", 0);
            }




            insertCommand.Parameters.Add("@BudgetOrderSerialItemId", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@BudgetOrderSerialItemId"].Direction = ParameterDirection.Output;

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
                if (count != 0)
                {


                    if (insertCommand.Parameters["@BudgetOrderSerialItemId"].Value != DBNull.Value)
                    {
                        BudgetIdSerialItemId = System.Convert.ToInt32(insertCommand.Parameters["@BudgetOrderSerialItemId"].Value);
                    }
                }


                return BudgetIdSerialItemId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return BudgetIdSerialItemId;
            }
            finally
            {
                connection.Close();
            }



        }


        private int AddBudgetCodewiseOrdrDtl(BudgetCodewiseOrderDetail BudgetCodewiseOrderDetail)
        {
            int BudgetIdCodewiseOrdr = 0;
            string insertProcedure = "[CreatBudgtCodewiseOrdrDtl]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;


            if (BudgetCodewiseOrderDetail.BudgetOrderSerialId != 0)
            {
                insertCommand.Parameters.AddWithValue("@BudgetOrderSerialId", BudgetCodewiseOrderDetail.BudgetOrderSerialId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetOrderSerialId", 0);
            }


            if (BudgetCodewiseOrderDetail.BudgetId != 0)
            {
                insertCommand.Parameters.AddWithValue("@BudgetId", BudgetCodewiseOrderDetail.BudgetId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetId", 0);
            }


            if (!string.IsNullOrEmpty(BudgetCodewiseOrderDetail.Value))
            {
                insertCommand.Parameters.AddWithValue("@Value", BudgetCodewiseOrderDetail.Value);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Value", DBNull.Value);
            }
            if (BudgetCodewiseOrderDetail.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial", BudgetCodewiseOrderDetail.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial", 0);
            }

            insertCommand.Parameters.Add("@BudgetOrderPkey", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@BudgetOrderPkey"].Direction = ParameterDirection.Output;

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
                if (count != 0)
                {


                    if (insertCommand.Parameters["@BudgetOrderPkey"].Value != DBNull.Value)
                    {
                        BudgetIdCodewiseOrdr = System.Convert.ToInt32(insertCommand.Parameters["@BudgetOrderPkey"].Value);
                    }


                }
                return BudgetIdCodewiseOrdr;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return BudgetIdCodewiseOrdr;
            }
            finally
            {
                connection.Close();
            }



        }


        private int UpdateBudgetCodeordr(BudgetCodeOrder budgetordr ,Decimal Total)
        {
            int count = 0;
            string updateProcedure = "[UpdateBudgetCodeOrder]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            if (budgetordr.BudgetOrderId != 0)
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderSerialId", budgetordr.BudgetOrderId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderSerialId", 0);
            }

            if (budgetordr.OrderId != 0)
            {
                updateCommand.Parameters.AddWithValue("@OrderId ", budgetordr.OrderId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OrderId ", 0);
            }
           
            if (budgetordr.NoofBudgetCodes != 0)
            {
                updateCommand.Parameters.AddWithValue("@NoofBudgetCodes ", budgetordr.NoofBudgetCodes);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NoofBudgetCodes ", 0);
            }

            if (Total != 0)
            {
                updateCommand.Parameters.AddWithValue("@Total", Total);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Total", 0);
            }

            if (budgetordr.BudgetCodeSplitOn)
            {
                updateCommand.Parameters.AddWithValue("@BudgetCodeSplitOn", budgetordr.BudgetCodeSplitOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetCodeSplitOn", 0);
            }
            if (budgetordr.BudgetCodetoFreight)
            {
                updateCommand.Parameters.AddWithValue("@BudgetCodetoFreight", budgetordr.BudgetCodetoFreight);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetCodetoFreight", 0);
            }
            if (!string.IsNullOrEmpty(budgetordr.Plansoncomment))
            {
                updateCommand.Parameters.AddWithValue("@Plansoncomment", budgetordr.Plansoncomment);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Plansoncomment", string.Empty);
            }

            if (budgetordr.UserID != 0)
            {
                updateCommand.Parameters.AddWithValue("@UserId", budgetordr.UserID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@UserId", 0);
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

        private int UpdateBudgetCodeordrItmDetail(BudgtCodeOrdrItmDtls budgetCOrdrItmDtl)
        {
            int count = 0;
            string updateProcedure = "[UpdateBudgetCodeOrderItemDetails]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            if (budgetCOrdrItmDtl.BudgetOrderSerialItemId != 0)
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderSerialItemId", budgetCOrdrItmDtl.BudgetOrderSerialItemId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderSerialItemId", 0);
            }
            
            
            if (budgetCOrdrItmDtl.Amount != 0)
            {
                updateCommand.Parameters.AddWithValue("@Amount", budgetCOrdrItmDtl.Amount);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Amount", 0);
            }

            if (budgetCOrdrItmDtl.IsFreightAmount)
            {
                updateCommand.Parameters.AddWithValue("@IsFreightAmount ", budgetCOrdrItmDtl.IsFreightAmount);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@IsFreightAmount ", 0);
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


        private int UpdateBudgetCWOrder(BudgetCodewiseOrderDetail budgetcode)
        {
            int count = 0;
            string updateProcedure = "[UpdateBudgetCodewiseOrderDetail]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (budgetcode.BudgetOrderSerialId != 0)
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderPkey", budgetcode.BudgetOrderPkey);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderPkey", 0);
            }

 
            if (!string.IsNullOrEmpty(budgetcode.Value))
            {
                updateCommand.Parameters.AddWithValue("@Value", budgetcode.Value);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Value", DBNull.Value);
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


    }
}