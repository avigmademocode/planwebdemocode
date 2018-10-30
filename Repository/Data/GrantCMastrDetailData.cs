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
using MyProject.Models;
namespace MyProject.Repository.Data
{
    public class GrantCMastrDetailData
    {

        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
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
     /*
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
                     BudgetTitle = item.Field<string>("BudgetTitle"),
                     CustId = item.Field<Int64>("CustId"),
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
                    OrderId = item.Field<Int64>("OrderId"),
                    ProductId = item.Field<Int64>("ProductId"),
                    Qty = item.Field<int>("Qty"),
                    Rate = item.Field<Decimal>("Rate"),
                    Amount = item.Field<Decimal>("Amount")

                }).ToList();
            objDynamic.Add(OrdrDetail);




            return objDynamic;
        }

        */
        private int AddGrntCodeOrdrDetls(GrantCodeOrderDetails grantCODtls)
        {
            int BudgetId = 0;
            string insertProcedure = "[CreatGrantCodeOrderDetails]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;


            if (grantCODtls.GrantOrderSerialId != 0)
            {
                insertCommand.Parameters.AddWithValue("@GrantOrderSerialId", grantCODtls.GrantOrderSerialId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@GrantOrderSerialId", 0);
            }
            if (grantCODtls.GrantId != 0)
            {
                insertCommand.Parameters.AddWithValue("@GrantId", grantCODtls.GrantId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@GrantId", 0);
            }
            if (!string.IsNullOrEmpty(grantCODtls.Value))
            {
                insertCommand.Parameters.AddWithValue("@Value", grantCODtls.Value);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetTitle", DBNull.Value);
            }


            if (grantCODtls.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial ", grantCODtls.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Total ", 0);
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
        private int AddGrntCodeOrdrItmDtls(GrantCodeOrderItemDetails grantCOIDtls)
        {
            int BudgetId = 0;
            string insertProcedure = "[CreatGrantCodeOrderItemDetails]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;


            if (grantCOIDtls.GrantOrderserialItemId != 0)
            {
                insertCommand.Parameters.AddWithValue("@GrantOrderserialItemId", grantCOIDtls.GrantOrderserialItemId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@GrantOrderserialItemId", 0);
            }
            if (grantCOIDtls.GrantOrderSerialId != 0)
            {
                insertCommand.Parameters.AddWithValue("@GrantOrderSerialId", grantCOIDtls.GrantOrderSerialId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@GrantOrderSerialId", 0);
            }
            if (grantCOIDtls.ProductId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ProductId", grantCOIDtls.ProductId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProductId", 0);
            }
            if (grantCOIDtls.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial", grantCOIDtls.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial", 0);
            }

            if (grantCOIDtls.Qty != 0)
            {
                insertCommand.Parameters.AddWithValue("@Qty", grantCOIDtls.Qty);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Qty", 0);
            }

            if (grantCOIDtls.Rate != 0)
            {
                insertCommand.Parameters.AddWithValue("@Rate", grantCOIDtls.Rate);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Rate", 0);
            }

            if (grantCOIDtls.Amount != 0)
            {
                insertCommand.Parameters.AddWithValue("@Amount", grantCOIDtls.Amount);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Amount", 0);
            }

            if (grantCOIDtls.isDelayedTime)
            {
                insertCommand.Parameters.AddWithValue("@isDelayedTime", grantCOIDtls.isDelayedTime);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isDelayedTime", 0);
            }

            if (grantCOIDtls.isTaxAmount)
            {
                insertCommand.Parameters.AddWithValue("@isTaxAmount", grantCOIDtls.isTaxAmount);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isTaxAmount", 0);
            }

            if (!string.IsNullOrEmpty(grantCOIDtls.TaxName))
            {
                insertCommand.Parameters.AddWithValue("@TaxName", grantCOIDtls.TaxName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@TaxName", DBNull.Value);
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
        private int AddGrantCodeOrders(GrantCodeOrders grantCO)
        {
            int BudgetId = 0;
            string insertProcedure = "[CreatGrantCodeOrders]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;


            if (grantCO.GrantOrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@GrantOrderId", grantCO.GrantOrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@GrantOrderId", 0);
            }
            if (grantCO.OrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderId", grantCO.OrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderId", 0);
            }
            if (grantCO.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", grantCO.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (grantCO.NoofGrantCodes != 0)
            {
                insertCommand.Parameters.AddWithValue("@NoofGrantCodes", grantCO.NoofGrantCodes);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@NoofGrantCodes", 0);
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
        private int AddGrantCodeWiseOrderTotal(GrantCodeWiseOrderTotal grantCWOTtl)
        {
            int BudgetId = 0;
            string insertProcedure = "[CreatGrantCodeWiseOrderTotal]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;


            if (grantCWOTtl.GrantOrderSerialId != 0)
            {
                insertCommand.Parameters.AddWithValue("@GrantOrderSerialId", grantCWOTtl.GrantOrderSerialId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@GrantOrderSerialId", 0);
            }
            if (grantCWOTtl.GrantOrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@GrantOrderId", grantCWOTtl.GrantOrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@GrantOrderId", 0);
            }
            if (grantCWOTtl.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial", grantCWOTtl.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial", 0);
            }
            if (grantCWOTtl.Total != 0)
            {
                insertCommand.Parameters.AddWithValue("@Total", grantCWOTtl.Total);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Total", 0);
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


        private int SaveGrntCodeOrdrDetls(GrantCodeOrderDetails grantCODtls)
        {
            int BudgetId = 0;
            string updateProcedure = "[UpdateGrantCodeOrderDetails]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            if (grantCODtls.GrantOrderSerialId != 0)
            {
                updateCommand.Parameters.AddWithValue("@GrantOrderSerialId", grantCODtls.GrantOrderSerialId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@GrantOrderSerialId", 0);
            }
            if (grantCODtls.GrantId != 0)
            {
                updateCommand.Parameters.AddWithValue("@GrantId", grantCODtls.GrantId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@GrantId", 0);
            }
            if (!string.IsNullOrEmpty(grantCODtls.Value))
            {
                updateCommand.Parameters.AddWithValue("@Value", grantCODtls.Value);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetTitle", DBNull.Value);
            }


            if (grantCODtls.Serial != 0)
            {
                updateCommand.Parameters.AddWithValue("@Serial ", grantCODtls.Serial);
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
        private int SaveGrntCodeOrdrItmDtls(GrantCodeOrderItemDetails grantCOIDtls)
        {
            int BudgetId = 0;
            string updateProcedure = "[UpdateGrantCodeOrderItemDetails]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            if (grantCOIDtls.GrantOrderserialItemId != 0)
            {
                updateCommand.Parameters.AddWithValue("@GrantOrderserialItemId", grantCOIDtls.GrantOrderserialItemId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@GrantOrderserialItemId", 0);
            }
            if (grantCOIDtls.GrantOrderSerialId != 0)
            {
                updateCommand.Parameters.AddWithValue("@GrantOrderSerialId", grantCOIDtls.GrantOrderSerialId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@GrantOrderSerialId", 0);
            }
            if (grantCOIDtls.ProductId != 0)
            {
                updateCommand.Parameters.AddWithValue("@ProductId", grantCOIDtls.ProductId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ProductId", 0);
            }
            if (grantCOIDtls.Serial != 0)
            {
                updateCommand.Parameters.AddWithValue("@Serial", grantCOIDtls.Serial);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Serial", 0);
            }

            if (grantCOIDtls.Qty != 0)
            {
                updateCommand.Parameters.AddWithValue("@Qty", grantCOIDtls.Qty);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Qty", 0);
            }

            if (grantCOIDtls.Rate != 0)
            {
                updateCommand.Parameters.AddWithValue("@Rate", grantCOIDtls.Rate);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Rate", 0);
            }

            if (grantCOIDtls.Amount != 0)
            {
                updateCommand.Parameters.AddWithValue("@Amount", grantCOIDtls.Amount);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Amount", 0);
            }

            if (grantCOIDtls.isDelayedTime)
            {
                updateCommand.Parameters.AddWithValue("@isDelayedTime", grantCOIDtls.isDelayedTime);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@isDelayedTime", 0);
            }

            if (grantCOIDtls.isTaxAmount)
            {
                updateCommand.Parameters.AddWithValue("@isTaxAmount", grantCOIDtls.isTaxAmount);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@isTaxAmount", 0);
            }

            if (!string.IsNullOrEmpty(grantCOIDtls.TaxName))
            {
                updateCommand.Parameters.AddWithValue("@TaxName", grantCOIDtls.TaxName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@TaxName", DBNull.Value);
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
        private int SaveGrantCodeOrders(GrantCodeOrders grantCO)
        {
            int BudgetId = 0;
            string updateProcedure = "[UpdateGrantCodeOrders]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            if (grantCO.GrantOrderId != 0)
            {
                updateCommand.Parameters.AddWithValue("@GrantOrderId", grantCO.GrantOrderId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@GrantOrderId", 0);
            }
            if (grantCO.OrderId != 0)
            {
                updateCommand.Parameters.AddWithValue("@OrderId", grantCO.OrderId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OrderId", 0);
            }
            if (grantCO.CustId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CustId", grantCO.CustId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (grantCO.NoofGrantCodes != 0)
            {
                updateCommand.Parameters.AddWithValue("@NoofGrantCodes", grantCO.NoofGrantCodes);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NoofGrantCodes", 0);
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
        private int SaveGrantCodeWiseOrderTotal(GrantCodeWiseOrderTotal grantCWOTtl)
        {
            int BudgetId = 0;
            string updateProcedure = "[UpdateGrantCodeWiseOrderTotal]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            if (grantCWOTtl.GrantOrderSerialId != 0)
            {
                updateCommand.Parameters.AddWithValue("@GrantOrderSerialId", grantCWOTtl.GrantOrderSerialId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@GrantOrderSerialId", 0);
            }
            if (grantCWOTtl.GrantOrderId != 0)
            {
                updateCommand.Parameters.AddWithValue("@GrantOrderId", grantCWOTtl.GrantOrderId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@GrantOrderId", 0);
            }
            if (grantCWOTtl.Serial != 0)
            {
                updateCommand.Parameters.AddWithValue("@Serial", grantCWOTtl.Serial);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Serial", 0);
            }
            if (grantCWOTtl.Total != 0)
            {
                updateCommand.Parameters.AddWithValue("@Total", grantCWOTtl.Total);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Total", 0);
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