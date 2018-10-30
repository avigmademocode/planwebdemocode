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
namespace MyProject.Repository.Data
{



    public class BudgetCodeWiseOrderDetailData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
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


        public List<dynamic> GetCustBudgetMastr(Int64 OrderId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetCustBudgetData(OrderId);
            var myEnumerable = ds.Tables[0].AsEnumerable();
           
            List<BudgetCodeMstr> BudgetCodeMstr =
                (from item in myEnumerable
                 select new BudgetCodeMstr
                 {
                     BudgetId = item.Field<Int64>("BudgetId "),
                     BudgetTitle = item.Field<String>("BudgetTitle"),
                     isRequired = item.Field<Boolean>("isRequired"),
                     FldLength = item.Field<int>("FldLength"),
                     Serial = item.Field<int>("Serial"),
                     DependOn = item.Field<Int64>("DependOn")
                 }).ToList();
            objDynamic.Add(BudgetCodeMstr);
            
            var Order = ds.Tables[1].AsEnumerable();
            List<OrdrDetail> OrdrDetail =
               (from item in Order
                select new OrdrDetail
                {
                    ODID = item.Field<Int64>("ODID"),
                    Serial = item.Field<int>("Serial"),
                    ProductId = item.Field<Int64>("ProductId"),
                    ProductName = item.Field<String>("ProductName"),
                    Qty = item.Field<int>("Qty"),
                    Rate = item.Field<Decimal>("Rate"),
                    Amount = item.Field<Decimal>("Amount")

                }).ToList();
            objDynamic.Add(OrdrDetail);




            return objDynamic;
        }







        private int AddBudgetCWOrder(BudgetCodeOrderDetail budgetcode)
        {
            int BudgetId = 0;
            string insertProcedure = "[CreatBudgtCodewiseOrdrDtl]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (budgetcode.BudgetOrderSerialId != 0)
            {
                insertCommand.Parameters.AddWithValue("@BudgetOrderSerialId", budgetcode.BudgetOrderSerialId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetOrderSerialId", 0);
            }


            if (budgetcode.BudgetId != 0)
            {
                insertCommand.Parameters.AddWithValue("@BudgetId", budgetcode.BudgetId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetId", 0);
            }
            if (budgetcode.Value != 0)
            {
                insertCommand.Parameters.AddWithValue("@Value", budgetcode.Value);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Value", 0);
            }

            if (budgetcode.Serial)
            {
                insertCommand.Parameters.AddWithValue("@Serial", budgetcode.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial", 0);
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
                }
                if (insertCommand.Parameters["@BudgetIdout"].Value != DBNull.Value)
                {
                    BudgetId = System.Convert.ToInt32(insertCommand.Parameters["@BudgetIdout"].Value);
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
        private int AddBudgetCodeMstr(BudgetCodeMstr budgetmstr)
        {
            int BudgetId = 0;
            string insertProcedure = "[CreatBudgetCodeMaster]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
          

            if (budgetmstr.BudgetId != 0)
            {
                insertCommand.Parameters.AddWithValue("@BudgetId", budgetmstr.BudgetId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetId", 0);
            }
            if (!string.IsNullOrEmpty(budgetmstr.BudgetTitle))
            {
                insertCommand.Parameters.AddWithValue("@BudgetTitle", budgetmstr.BudgetTitle);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetTitle", DBNull.Value);
            }
            if (budgetmstr.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", budgetmstr.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (budgetmstr.isRequired)
            {
                insertCommand.Parameters.AddWithValue("@isRequired", budgetmstr.isRequired);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isRequired", 0);
            }
            if (budgetmstr.FldLength!=0)
            {
                insertCommand.Parameters.AddWithValue("@FldLength", budgetmstr.FldLength);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@FldLength", 0);
            }
            if (budgetmstr.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial ", budgetmstr.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial ", 0);
            }
            if (budgetmstr.DependOn != 0)
            {
                insertCommand.Parameters.AddWithValue("@DependOn ", budgetmstr.DependOn);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@DependOn ", 0);
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
                }
                if (insertCommand.Parameters["@BudgetIdout"].Value != DBNull.Value)
                {
                    BudgetId = System.Convert.ToInt32(insertCommand.Parameters["@BudgetIdout"].Value);
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
      
        private int AddBudgetCodeordrItmDetail(BudgtCodeOrdrItmDtls budgetCOrdrItmDtl)
        {
            int BudgetId = 0;
            string insertProcedure = "[CreateBudgetCodeOrderItemDetails]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;


            if (budgetCOrdrItmDtl.BudgetOrderSerialItemId != 0)
            {
                insertCommand.Parameters.AddWithValue("@BudgetOrderSerialItemId", budgetCOrdrItmDtl.BudgetOrderSerialItemId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetOrderSerialItemId", 0);
            }
            if (budgetCOrdrItmDtl.BudgetOrderSerialItemId != 0)
            {
                insertCommand.Parameters.AddWithValue("@BudgetOrderSerialItemId", budgetCOrdrItmDtl.BudgetOrderSerialItemId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetOrderSerialItemId", 0);
            }
            if (budgetCOrdrItmDtl.ProductId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ProductId", budgetCOrdrItmDtl.ProductId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProductId", 0);
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


            if (budgetCOrdrItmDtl.BudgetCodetoFreight)
            {
                insertCommand.Parameters.AddWithValue("@BudgetCodetoFreight", budgetCOrdrItmDtl.BudgetCodetoFreight);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetCodetoFreight", 0);
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
                }
                if (insertCommand.Parameters["@BudgetIdout"].Value != DBNull.Value)
                {
                    BudgetId = System.Convert.ToInt32(insertCommand.Parameters["@BudgetIdout"].Value);
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
       
        private int SaveBudgetCWOrder(BudgetCodeOrderDetail budgetcode)
        {
            int BudgetId = 0;
            string updateProcedure = "[UpdateBudgetCodewiseOrderDetail]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (budgetcode.BudgetOrderSerialId != 0)
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderSerialId", budgetcode.BudgetOrderSerialId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderSerialId", 0);
            }


            if (budgetcode.BudgetId != 0)
            {
                updateCommand.Parameters.AddWithValue("@BudgetId", budgetcode.BudgetId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetId", 0);
            }
            if (budgetcode.Value != 0)
            {
                updateCommand.Parameters.AddWithValue("@Value", budgetcode.Value);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Value", 0);
            }

            if (budgetcode.Serial)
            {
                updateCommand.Parameters.AddWithValue("@Serial", budgetcode.Serial);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Serial", 0);
            }


            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            updateCommand.Parameters.Add("@BudgetIdout", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@BudgetIdout"].Direction = ParameterDirection.Output;
            try
            {
                int count = 0;
                connection.Open();
                updateCommand.ExecuteNonQuery();
                if (updateCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
                }
                if (updateCommand.Parameters["@BudgetIdout"].Value != DBNull.Value)
                {
                    BudgetId = System.Convert.ToInt32(updateCommand.Parameters["@BudgetIdout"].Value);
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
        private int SaveBudgetCodeMstr(BudgetCodeMstr budgetmstr)
        {
            int BudgetId = 0;
            string updateProcedure = "[UpdateBudgetCodeMaster]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            if (budgetmstr.BudgetId != 0)
            {
                updateCommand.Parameters.AddWithValue("@BudgetId", budgetmstr.BudgetId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetId", 0);
            }
            if (!string.IsNullOrEmpty(budgetmstr.BudgetTitle))
            {
                updateCommand.Parameters.AddWithValue("@BudgetTitle", budgetmstr.BudgetTitle);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetTitle", DBNull.Value);
            }
            if (budgetmstr.CustId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CustId", budgetmstr.CustId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (budgetmstr.isRequired)
            {
                updateCommand.Parameters.AddWithValue("@isRequired", budgetmstr.isRequired);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@isRequired", 0);
            }
            if (budgetmstr.FldLength != 0)
            {
                updateCommand.Parameters.AddWithValue("@FldLength", budgetmstr.FldLength);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@FldLength", 0);
            }
            if (budgetmstr.Serial != 0)
            {
                updateCommand.Parameters.AddWithValue("@Serial ", budgetmstr.Serial);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Serial ", 0);
            }
            if (budgetmstr.DependOn != 0)
            {
                updateCommand.Parameters.AddWithValue("@DependOn ", budgetmstr.DependOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@DependOn ", 0);
            }
            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            updateCommand.Parameters.Add("@BudgetIdout", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@BudgetIdout"].Direction = ParameterDirection.Output;
            try
            {
                int count = 0;
                connection.Open();
                updateCommand.ExecuteNonQuery();
                if (updateCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
                }
                if (updateCommand.Parameters["@BudgetIdout"].Value != DBNull.Value)
                {
                    BudgetId = System.Convert.ToInt32(updateCommand.Parameters["@BudgetIdout"].Value);
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
        private int SaveBudgetCodeordr(BudgetCodeOrder budgetordr)
        {
            int BudgetId = 0;
            string updateProcedure = "[UpdateBudgetCodeOrder]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            if (budgetordr.BudgetOrderId != 0)
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderId", budgetordr.BudgetOrderId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderId", 0);
            }

            if (budgetordr.OrderId != 0)
            {
                updateCommand.Parameters.AddWithValue("@OrderId ", budgetordr.OrderId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OrderId ", 0);
            }
            if (budgetordr.CustId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CustId ", budgetordr.CustId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CustId ", 0);
            }

            if (budgetordr.NoofBudgetCodes != 0)
            {
                updateCommand.Parameters.AddWithValue("@NoofBudgetCodes ", budgetordr.NoofBudgetCodes);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NoofBudgetCodes ", 0);
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


            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            updateCommand.Parameters.Add("@BudgetIdout", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@BudgetIdout"].Direction = ParameterDirection.Output;
            try
            {
                int count = 0;
                connection.Open();
                updateCommand.ExecuteNonQuery();
                if (updateCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
                }
                if (updateCommand.Parameters["@BudgetIdout"].Value != DBNull.Value)
                {
                    BudgetId = System.Convert.ToInt32(updateCommand.Parameters["@BudgetIdout"].Value);
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
        
        private int SaveBudgetCodeWOTotal(BudgetCodewiseOrdrTotl budgetCodeWOTtl)
        {
            int BudgetId = 0;
            string updateProcedure = "[UpdateBudgetCodewiseOrderTotal]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            if (budgetCodeWOTtl.BudgetOrderSerialId != 0)
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderSerialId", budgetCodeWOTtl.BudgetOrderSerialId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderSerialId", 0);
            }
            if (budgetCodeWOTtl.BudgetOrderId != 0)
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderId", budgetCodeWOTtl.BudgetOrderId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetOrderId", 0);
            }


            if (budgetCodeWOTtl.Serial != 0)
            {
                updateCommand.Parameters.AddWithValue("@Serial ", budgetCodeWOTtl.Serial);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Serial ", 0);
            }
            if (budgetCodeWOTtl.Total != 0)
            {
                updateCommand.Parameters.AddWithValue("@Total ", budgetCodeWOTtl.Total);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Total ", 0);
            }



            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            updateCommand.Parameters.Add("@BudgetIdout", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@BudgetIdout"].Direction = ParameterDirection.Output;
            try
            {
                int count = 0;
                connection.Open();
                updateCommand.ExecuteNonQuery();
                if (updateCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
                }
                if (updateCommand.Parameters["@BudgetIdout"].Value != DBNull.Value)
                {
                    BudgetId = System.Convert.ToInt32(updateCommand.Parameters["@BudgetIdout"].Value);
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




      

    }
}
