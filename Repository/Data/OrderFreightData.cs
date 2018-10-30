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
using System.Web;
using MyProject.Repository.Security;
namespace MyProject.Repository.Data
{
    public class OrderFreightData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        OrderApprovalData OrderApprovalData = new OrderApprovalData();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();
        private DataTable GetFreight(Int64 OrderID)
        {

            string selectProcedure = "[GetFreightDetails]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@OrderID", OrderID);
            
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
                log.logErrorMessage("OrderFreightData.GetFreight");
                log.logErrorMessage(ex.StackTrace);
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        private int AddFreightTitleDetail(Int64 OrderID, Int64 FreightTitleID, String strValue)
        {
            
           
            string insertProcedure = "[CreatOrderFreightData]";
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


            if (FreightTitleID != 0)
            {
                insertCommand.Parameters.AddWithValue("@FreightTitleId", FreightTitleID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@FreightTitleId", 0);
            }


            if (!string.IsNullOrEmpty(strValue))
            {
                insertCommand.Parameters.AddWithValue("@Value", strValue);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Value", DBNull.Value);
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
                log.logErrorMessage("OrderData.AddConfirmOrderDetail");
                log.logErrorMessage(ex.StackTrace);
                return count;
            }
            finally
            {
                connection.Close();
            }

        }

        private int UpdateOrderDetail(Int64 OrderID, Int64 USerID,Decimal TotalOrderAmount,Decimal FreigthAmount,Decimal TaxAmount,Int64 intLeadTime)
        {
            string insertProcedure = "[UpdateOrderDetails]";
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


            if (USerID != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserID", USerID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserID", 0);
            }


            if (TotalOrderAmount != 0)
            {
                insertCommand.Parameters.AddWithValue("@TotalOrderAmount", TotalOrderAmount);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@TotalOrderAmount", 0);
            }

            if (FreigthAmount != 0)
            {
                insertCommand.Parameters.AddWithValue("@FreightAmount", FreigthAmount);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@FreightAmount", 0);
            }

            if (TaxAmount != 0)
            {
                insertCommand.Parameters.AddWithValue("@TaxAmount", TaxAmount);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@TaxAmount", 0);
            }
            if (intLeadTime != 0)
            {
                insertCommand.Parameters.AddWithValue("@LeadTime", intLeadTime);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@LeadTime", 0);
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

        public List<dynamic> GetCustFreight(Int64 OrderID)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataTable dt = GetFreight(OrderID);
            var myEnumerable = dt.AsEnumerable();

            List<CustFreightTitle> CustFreightTitle =
                (from item in myEnumerable
                 select new CustFreightTitle
                 {
                     FreightTitleId = item.Field<Int64>("FreightTitleId"),
                     FreightTitle = item.Field<String>("FreightTitle"),
                     isRequired = item.Field<bool>("isRequired"),
                     fldlength = item.Field<int>("fldlength"),
                     fldType = item.Field<String>("fldType"),
                     isFreightAmount = item.Field<bool>("isFreightAmount"),
                     isLeadTime = item.Field<bool>("isLeadTime"),
                     isTaxApplicable = item.Field<bool>("isTaxApplicable"),
                     Serial = item.Field<int>("Serial")

                 }).ToList();
            objDynamic.Add(CustFreightTitle);
            return objDynamic;
        }

        public List<dynamic> GetCustDataFreight(string strOrderID)
        {
            Int64 OrderID = Convert.ToInt64(strOrderID);
            List<dynamic> objDynamic = new List<dynamic>();
            List<dynamic> oborder = new List<dynamic>();
            List<dynamic> objFreight = new List<dynamic>();
            oborder = OrderApprovalData.GetOrderDetails(strOrderID);
            objFreight = GetCustFreight(OrderID);
            objDynamic.Add(oborder);
            objDynamic.Add(objFreight);

            return objDynamic;
        }

        public List<dynamic> AddCustDataFreight(FreightDetails FreightDetails)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            FreightTotal freightTotal = new FreightTotal();
            var Data = JsonConvert.DeserializeObject<List<CustFreightTitle>>(FreightDetails.FreightData);
            CustFreightTitle CustFreightTitle = new CustFreightTitle();
            string strFreightAmount = string.Empty, strTaxAmount = string.Empty  , strLeadTime = string.Empty;
            Decimal FinalAmount = 0;
            Int64 OrderID = 0,intLeadTime = 0;
            if (!string.IsNullOrEmpty(securityHelper.Decrypt(FreightDetails.strOrderID,false)))
            {
                OrderID = Convert.ToInt64(securityHelper.Decrypt(FreightDetails.strOrderID, false));
            }
            for (int i = 0; i < Data.Count; i++)
            {
                CustFreightTitle = Data[i];
                if (CustFreightTitle.isFreightAmount == true)
                {
                    strFreightAmount = CustFreightTitle.Data;
                }

                if (CustFreightTitle.isTaxApplicable == true)
                {
                    strTaxAmount = CustFreightTitle.Data;
                }
                if (CustFreightTitle.isLeadTime == true)
                {
                    strLeadTime = CustFreightTitle.Data;
                    if (!string.IsNullOrEmpty(strLeadTime))
                    {
                        intLeadTime = Convert.ToInt64(strLeadTime);
                    }
                }

                AddFreightTitleDetail(OrderID, CustFreightTitle.FreightTitleId, CustFreightTitle.Data);
            }
            freightTotal = GetTotalAmount(FreightDetails.TotalAmount, strFreightAmount, strTaxAmount);
            int count = UpdateOrderDetail(OrderID, Convert.ToInt64(UserID), freightTotal.TotalAmount, freightTotal.Freight, freightTotal.TaxAmount, intLeadTime);

            EmailTemplate emailTemplate = new EmailTemplate();
            CustRequestData custRequestData = new CustRequestData();
            custRequestData.GetEmailDetails(OrderID.ToString(),string.Empty,string.Empty);

            objDynamic.Add(count);
            objDynamic.Add(FinalAmount);
            return objDynamic;
        }

        private FreightTotal GetTotalAmount(string strTotalAmount,string strFreightAmount,string strTaxAmount)
        {
            Decimal FinalTotal = 0,DecTotalAmount = 0, DecFreightAmount =0 , DecAmount = 0;
            FreightTotal freightTotal = new FreightTotal();
            int intTaxPer = 0;
            if (!string.IsNullOrEmpty(strTotalAmount))
            {
                DecTotalAmount = Convert.ToDecimal(strTotalAmount);
            }
            if (!string.IsNullOrEmpty(strFreightAmount))
            {
                DecFreightAmount = Convert.ToDecimal(strFreightAmount);
                freightTotal.Freight = DecFreightAmount;
            }
            if (!string.IsNullOrEmpty(strTaxAmount))
            {
                intTaxPer = Convert.ToInt32(strTaxAmount);
            }
            DecAmount = DecTotalAmount ;
            if (DecAmount != 0)
            {
               
                Decimal result = ((DecAmount / 100) * intTaxPer);
                freightTotal.TaxAmount = result;
                FinalTotal = DecAmount + result + DecFreightAmount;
                freightTotal.TotalAmount = FinalTotal;
            }
           


            return freightTotal;
        }

    }
}