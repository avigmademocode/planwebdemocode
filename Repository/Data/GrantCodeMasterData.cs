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
    public class GrantCodeMasterData
    {

        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();

        private DataSet GetGrantCodeMasterData(Int64 OrderID)
        {

            string selectProcedure = "[GetGrantCodeMaster]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@OrderID", OrderID);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }

        private DataSet GetGrantOrderData(Int64 OrderID)
        {

            string selectProcedure = "[GetGrantOrderDetails]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@OrderID", OrderID);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }

        private DataSet GetGrantOrderProductDetails(Int64 OrderID)
        {

            string selectProcedure = "[GetGrantOrderProductDetails]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@OrderID", OrderID);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }

        public List<dynamic> GetGrantOrderProductDetails(string strOrderID)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            Int64 OrderID = 0;
            OrderID = Convert.ToInt64(securityHelper.Decrypt(strOrderID, false));
            DataSet ds = GetGrantOrderProductDetails(OrderID);

            var myEnumerable = ds.Tables[0].AsEnumerable(); // Grant


            List<GrantDetail> GrantDetail =
                (from item in myEnumerable
                 select new GrantDetail
                 {
                     GrantIdOrderID = item.Field<Int64>("GrantIdOrderID"),
                     GrantOrderSerialId = item.Field<Int64>("GrantOrderSerialId"),
                     GrantId = item.Field<Int64>("GrantId"),
                     GrantTitle = item.Field<String>("GrantTitle"),
                     Value = item.Field<String>("Value"),



                 }).ToList();

            objDynamic.Add(GrantDetail);





            var myEnumerableser = ds.Tables[2].AsEnumerable();
            List<GrantSerial> lstgrantSerials = new List<GrantSerial>();
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                GrantSerial grantSerial = new GrantSerial();

                grantSerial.GrantOrderSerial = Convert.ToInt64(ds.Tables[2].Rows[i]["GrantOrderSerialId"].ToString());
                grantSerial.Serial = Convert.ToInt32(ds.Tables[2].Rows[i]["Serial"].ToString());
                grantSerial.Total = Convert.ToDecimal(ds.Tables[2].Rows[i]["Total"].ToString());
                var rows = ds.Tables[3].Select("GrantOrderSerialId = '" + ds.Tables[2].Rows[i]["GrantOrderSerialId"].ToString() + "'");
                List<GrantData> lstgrandata = new List<GrantData>();

                if (rows.Length != 0)
                {
                    foreach (var row in rows)
                    {
                        GrantData grantData = new GrantData();
                        grantData.ProductName = row["ProductName"].ToString();
                        grantData.ProductID = Convert.ToInt64(row["ProductId"].ToString());
                        grantData.SelAmount = Convert.ToDecimal(row["Amount"]);
                        grantData.SelQty = int.Parse(row["Qty"].ToString());
                        grantData.SelRate = Convert.ToDecimal(row["Rate"]);
                        grantData.ProductIDTemp = Convert.ToInt64(row["ProductId"].ToString());
                        grantData.SelAmountTemp = Convert.ToDecimal(row["Amount"]);
                        grantData.SelQtyTemp = int.Parse(row["Qty"].ToString());
                        grantData.SelRateTemp = Convert.ToDecimal(row["Rate"]);
                        grantData.GrantOrderserialItemId = Int64.Parse(row["GrantOrderserialItemId"].ToString());

                        grantData.Items = (from items in ds.Tables[1].AsEnumerable() // Prod
                                           select new GrantOrdrDetail
                                           {
                                               ODID = items.Field<Int64>("ODID"),
                                               ProductId = items.Field<Int64>("ProductId"),
                                               ProductName = items.Field<String>("ProdName"),
                                               Qty = items.Field<int>("Qty"),
                                               Rate = items.Field<Decimal>("Rate"),
                                               Amount = items.Field<Decimal>("Amount"),

                                           }).ToList();
                        lstgrandata.Add(grantData);
                        // grantSerial.Data = lstgrandata;
                        //lstgrantSerials.Add(grantSerial);

                    }
                }
                else
                {
                    GrantData grantData = new GrantData();
                    //  List<GrantData> lstgrandata = new List<GrantData>();
                    grantData.ProductName = "";
                    grantData.ProductID = 0;
                    grantData.SelAmount = 0;
                    grantData.SelQty = 1;
                    grantData.SelRate = 0;


                    grantData.Items = (from items in ds.Tables[1].AsEnumerable() // Prod
                                       select new GrantOrdrDetail
                                       {
                                           ODID = items.Field<Int64>("ODID"),
                                           ProductId = items.Field<Int64>("ProductId"),
                                           ProductName = items.Field<String>("ProdName"),
                                           Qty = items.Field<int>("Qty"),
                                           Rate = items.Field<Decimal>("Rate"),
                                           Amount = items.Field<Decimal>("Amount"),

                                       }).ToList();
                    lstgrandata.Add(grantData);

                }

                grantSerial.Data = lstgrandata;
                lstgrantSerials.Add(grantSerial);


            }

            objDynamic.Add(lstgrantSerials);

            List<OrderTotal> OrderTotal =
          (from item in ds.Tables[5].AsEnumerable()
           select new OrderTotal
           {
               TotalOrderAmount = item.Field<Decimal>("TotalOrderAmount"),



           }).ToList();
            objDynamic.Add(OrderTotal);
            return objDynamic;
        }

        public List<dynamic> GetViewGrantOrderProductDetails(string strOrderID)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            Int64 OrderID = 0;
            OrderID = Convert.ToInt64(securityHelper.Decrypt(strOrderID, false));
            DataSet ds = GetGrantOrderProductDetails(OrderID);

            var myEnumerable = ds.Tables[0].AsEnumerable(); // Grant


            List<GrantDetail> GrantDetail =
                (from item in myEnumerable
                 select new GrantDetail
                 {
                     GrantIdOrderID = item.Field<Int64>("GrantIdOrderID"),
                     GrantOrderSerialId = item.Field<Int64>("GrantOrderSerialId"),
                     GrantId = item.Field<Int64>("GrantId"),
                     GrantTitle = item.Field<String>("GrantTitle"),
                     Value = item.Field<String>("Value"),



                 }).ToList();

            objDynamic.Add(GrantDetail);





            var myEnumerableser = ds.Tables[2].AsEnumerable();
            List<GrantSerial> lstgrantSerials = new List<GrantSerial>();
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                GrantSerial grantSerial = new GrantSerial();

                grantSerial.GrantOrderSerial = Convert.ToInt64(ds.Tables[2].Rows[i]["GrantOrderSerialId"].ToString());
                grantSerial.Serial = Convert.ToInt32(ds.Tables[2].Rows[i]["Serial"].ToString());
                grantSerial.Total = Convert.ToDecimal(ds.Tables[2].Rows[i]["Total"].ToString());
                var rows = ds.Tables[3].Select("GrantOrderSerialId = '" + ds.Tables[2].Rows[i]["GrantOrderSerialId"].ToString() + "'");
                List<GrantData> lstgrandata = new List<GrantData>();

                if (rows.Length != 0)
                {
                    foreach (var row in rows)
                    {
                        GrantData grantData = new GrantData();
                        grantData.ProductName = row["ProductName"].ToString();
                        grantData.ProductID = Convert.ToInt64(row["ProductId"].ToString());
                        grantData.SelAmount = Convert.ToDecimal(row["Amount"]);
                        grantData.SelQty = int.Parse(row["Qty"].ToString());
                        grantData.SelRate = Convert.ToDecimal(row["Rate"]);
                        grantData.GrantOrderserialItemId = Int64.Parse(row["GrantOrderserialItemId"].ToString());

                        lstgrandata.Add(grantData);
                        // grantSerial.Data = lstgrandata;
                        //lstgrantSerials.Add(grantSerial);

                    }
                }
                else
                {
                    GrantData grantData = new GrantData();
                    //  List<GrantData> lstgrandata = new List<GrantData>();
                    grantData.ProductName = "";
                    grantData.ProductID = 0;
                    grantData.SelAmount = 0;
                    grantData.SelQty = 1;
                    grantData.SelRate = 0;



                    lstgrandata.Add(grantData);

                }

                grantSerial.Data = lstgrandata;
                lstgrantSerials.Add(grantSerial);


            }

            objDynamic.Add(lstgrantSerials);
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

            return objDynamic;
        }

        public List<dynamic> GetGrantCodeMastr(Int64 OrderID)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetGrantCodeMasterData(OrderID);
            var myEnumerable = ds.Tables[0].AsEnumerable();

            List<GrantCodeMaster> grantCodeMstr =
                (from item in myEnumerable
                 select new GrantCodeMaster
                 {
                     GrantId = item.Field<Int64>("GrantId"),
                     GrantTitle = item.Field<string>("GrantTitle"),
                     isRequired = item.Field<Boolean>("isRequired"),
                     fldlength = item.Field<int>("fldlength"),
                     Serial = item.Field<int>("Serial"),
                     DependOn = item.Field<Int64>("DependOn"),
                     Data = string.Empty
                 }).ToList();
            objDynamic.Add(grantCodeMstr);


            return objDynamic;
        }

        public List<dynamic> SaveGrantCodeOrders(GrantCodeMasterDTO grantCodeMasterDTO)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            Int64 OrderID = 0;
            OrderID = Convert.ToInt64(securityHelper.Decrypt(grantCodeMasterDTO.OrderID, false));
            var Data = JsonConvert.DeserializeObject<List<RootObject>>(grantCodeMasterDTO.GrantCodeMaster);
            RootObject rootObject = new RootObject();
            for (int i = 0; i < Data.Count; i++)
            {
                List<Datum> datum = new List<Datum>();
                GrantCodeOrderItemDetails grantCodeOrderItemDetails = new GrantCodeOrderItemDetails();
                rootObject = Data[i];
                datum = rootObject.Data;
                foreach (var item in datum)
                {
                    if (item.SelectedItem != null)
                    {
                        var ODID = item.SelectedItem.Split(',');
                        var productId = item.Items.Find(x => x.ODID == Convert.ToInt32(ODID[0])).ProductId;

                        grantCodeOrderItemDetails.ProductId = productId;
                        grantCodeOrderItemDetails.GrantOrderSerialId = rootObject.GrantOrderSerial;
                        grantCodeOrderItemDetails.Serial = i + 1;
                        grantCodeOrderItemDetails.Rate = Convert.ToDecimal(ODID[1]);
                        grantCodeOrderItemDetails.Qty = Convert.ToInt32(item.SelQty);
                        grantCodeOrderItemDetails.Amount = Convert.ToDecimal(item.SelAmount);
                        grantCodeOrderItemDetails.Total = Convert.ToDecimal(item.SelSubTotal); ;
                        grantCodeOrderItemDetails.isTaxAmount = false; //Need to ask
                        grantCodeOrderItemDetails.isDelayedTime = true;//Need to ask
                        grantCodeOrderItemDetails.TaxName = "Test";//Need to ask

                        grantCodeOrderItemDetails.GrantOrderserialItemId = AddGrntCodeOrdrItmDtls(grantCodeOrderItemDetails);
                    }
                }
            }

            objDynamic.Add("1");
            return objDynamic;
        }
        public List<dynamic> AddGrantCodeOrders(GrantCodeMasterDTO grantCodeMasterDTO)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            var Data = JsonConvert.DeserializeObject<List<GrantCodeMaster>>(grantCodeMasterDTO.GrantCodeMaster);
            Int64 OrderID = 0, CustID = 0, GrantOrderSerialId = 0;
            OrderID = Convert.ToInt64(securityHelper.Decrypt(grantCodeMasterDTO.OrderID, false));
            CustID = Convert.ToInt64(CustomerID);
            GrantCodeOrders grantCodeOrders = new GrantCodeOrders();
            GrantCodeMaster grantCodeMaster = new GrantCodeMaster();
            GrantCodeWiseOrderTotal grantCodeWiseOrderTotal = new GrantCodeWiseOrderTotal();

            grantCodeOrders.OrderId = OrderID;
            grantCodeOrders.CustId = CustID;
            grantCodeOrders.NoofGrantCodes = Data.Count;
            AddGrantCodeOrders(grantCodeOrders);

            grantCodeWiseOrderTotal.GrantOrderId = grantCodeOrders.GrantOrderId;
            grantCodeWiseOrderTotal.Serial = grantCodeOrders.NoofGrantCodes;

            GrantOrderSerialId = AddGrantCodeWiseOrderTotal(grantCodeWiseOrderTotal);
            for (int i = 0; i < Data.Count; i++)
            {
                grantCodeMaster = Data[i];
                GrantCodeOrderDetails grantCodeOrderDetails = new GrantCodeOrderDetails();
                grantCodeOrderDetails.GrantOrderSerialId = GrantOrderSerialId;
                grantCodeOrderDetails.Serial = i + 1;
                grantCodeOrderDetails.Value = grantCodeMaster.Data;
                grantCodeOrderDetails.GrantId = grantCodeMaster.GrantId;
                AddGrntCodeOrdrDetls(grantCodeOrderDetails);

            }




            return GrantCodeOrderDetails(grantCodeMasterDTO.OrderID);
        }

        public List<dynamic> GrantCodeOrderDetails(string strOrderID)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            Int64 OrderID = 0;
            OrderID = Convert.ToInt64(securityHelper.Decrypt(strOrderID, false));
            DataSet ds = GetGrantOrderData(OrderID);

            var myEnumerable = ds.Tables[0].AsEnumerable();
            var myEnumerableord = ds.Tables[1].AsEnumerable();

            List<GrantDetail> GrantDetail =
                (from item in myEnumerable
                 select new GrantDetail
                 {
                     GrantIdOrderID = item.Field<Int64>("GrantIdOrderID"),
                     GrantOrderSerialId = item.Field<Int64>("GrantOrderSerialId"),
                     GrantId = item.Field<Int64>("GrantId"),
                     GrantTitle = item.Field<String>("GrantTitle"),
                     Value = item.Field<String>("Value"),



                 }).ToList();

            objDynamic.Add(GrantDetail);



            List<GrantData> lstgrandata = new List<GrantData>();

            var myEnumerableser = ds.Tables[2].AsEnumerable();
            GrantData grantData = new GrantData();
            grantData.SelAmount = 0;
            grantData.SelRate = 0;
            grantData.SelQty = 1;
            grantData.SelSubTotal = 0;
            grantData.Items = (from items in ds.Tables[1].AsEnumerable()
                               select new GrantOrdrDetail
                               {
                                   ODID = items.Field<Int64>("ODID"),
                                   ProductId = items.Field<Int64>("ProductId"),
                                   ProductName = items.Field<String>("ProdName"),
                                   Qty = items.Field<int>("Qty"),
                                   Rate = items.Field<Decimal>("Rate"),
                                   Amount = items.Field<Decimal>("Amount"),

                               }).ToList();
            lstgrandata.Add(grantData);

            List<GrantSerial> GrantSerial =
                (from item in myEnumerableser
                 select new GrantSerial
                 {
                     GrantOrderSerial = item.Field<Int64>("GrantOrderSerialId"),
                     Serial = item.Field<int>("Serial"),
                     Data = lstgrandata,


                 }).ToList();
            objDynamic.Add(GrantSerial);

            List<OrderTotal> OrderTotal =
                (from item in ds.Tables[4].AsEnumerable()
                 select new OrderTotal
                 {
                     TotalOrderAmount = item.Field<Decimal>("TotalOrderAmount"),



                 }).ToList();
            objDynamic.Add(OrderTotal);

            return objDynamic;
        }

        private GrantCodeOrders AddGrantCodeOrders(GrantCodeOrders grantCO)
        {
            Int64 GrantCodeID = 0;
            int numGrantcode = 0;
            string insertProcedure = "[CreatGrantCodeOrders]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;



            if (grantCO.OrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderId", grantCO.OrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderId", 0);
            }


            insertCommand.Parameters.Add("@NoofGrantCodes", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@NoofGrantCodes"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@GrantOrderId", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@GrantOrderId"].Direction = ParameterDirection.Output;

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
                    if (insertCommand.Parameters["@GrantOrderId"].Value != DBNull.Value)
                    {
                        GrantCodeID = System.Convert.ToInt32(insertCommand.Parameters["@GrantOrderId"].Value);
                    }

                    if (insertCommand.Parameters["@NoofGrantCodes"].Value != DBNull.Value)
                    {
                        numGrantcode = System.Convert.ToInt32(insertCommand.Parameters["@NoofGrantCodes"].Value);
                    }
                }
                grantCO.GrantOrderId = GrantCodeID;
                grantCO.NoofGrantCodes = numGrantcode;



                return grantCO;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return grantCO;
            }
            finally
            {
                connection.Close();
            }



        }
        private Int64 AddGrantCodeWiseOrderTotal(GrantCodeWiseOrderTotal grantCWOTtl)
        {
            Int64 GrantOrderSerialId = 0;
            string insertProcedure = "[CreatGrantCodeWiseOrderTotal]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;



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

            insertCommand.Parameters.Add("@GrantOrderSerialId", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@GrantOrderSerialId"].Direction = ParameterDirection.Output;

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
                    if (insertCommand.Parameters["@GrantOrderSerialId"].Value != DBNull.Value)
                    {
                        GrantOrderSerialId = System.Convert.ToInt32(insertCommand.Parameters["@GrantOrderSerialId"].Value);
                    }
                }




                return GrantOrderSerialId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return GrantOrderSerialId;
            }
            finally
            {
                connection.Close();
            }



        }

        private Int64 AddGrntCodeOrdrDetls(GrantCodeOrderDetails grantCODtls)
        {
            Int64 GrntCodeOrdrDetls = 0;
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
                insertCommand.Parameters.AddWithValue("@Value", DBNull.Value);
            }


            if (grantCODtls.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial", grantCODtls.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial ", 0);
            }

            insertCommand.Parameters.Add("@GrantIdOrderID", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@GrantIdOrderID"].Direction = ParameterDirection.Output;

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
                    if (insertCommand.Parameters["@GrantIdOrderID"].Value != DBNull.Value)
                    {
                        GrntCodeOrdrDetls = System.Convert.ToInt32(insertCommand.Parameters["@GrantIdOrderID"].Value);
                    }
                }




                return GrntCodeOrdrDetls;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return GrntCodeOrdrDetls;
            }
            finally
            {
                connection.Close();
            }



        }

        private Int64 AddGrntCodeOrdrItmDtls(GrantCodeOrderItemDetails grantCOIDtls)
        {
            int GrantCodeOrderItemDetailsID = 0;
            string insertProcedure = "[CreatGrantCodeOrderItemDetails]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;



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
            if (grantCOIDtls.Total != 0)
            {
                insertCommand.Parameters.AddWithValue("@Total", grantCOIDtls.Total);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Total", 0);
            }
            insertCommand.Parameters.Add("@GrantOrderserialItemId", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@GrantOrderserialItemId"].Direction = ParameterDirection.Output;

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
                    if (insertCommand.Parameters["@GrantOrderserialItemId"].Value != DBNull.Value)
                    {
                        GrantCodeOrderItemDetailsID = System.Convert.ToInt32(insertCommand.Parameters["@GrantOrderserialItemId"].Value);
                    }
                }




                return GrantCodeOrderItemDetailsID;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return GrantCodeOrderItemDetailsID;
            }
            finally
            {
                connection.Close();
            }



        }

    }
}