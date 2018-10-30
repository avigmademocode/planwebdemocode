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
using System.Data.OleDb;
using Microsoft.VisualBasic.FileIO;

namespace MyProject.Repository.Data
{
    public class AddProductData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        string IsPlansonUser = HttpContext.Current.Session["IsPlansonUser"].ToString();

        SecurityHelper securityHelper = new SecurityHelper();
        ProductCategoryData productCategoryData = new ProductCategoryData();
        CustRequestData custRequestData = new CustRequestData();
        string _path = @"c:\\Book1.xls";
        public List<dynamic> InsertExcelRecords(string path,int Tiercount,Int64 intCustomerID,int inttype)
        {
            int count = 0;
            List<dynamic> objDynamic = new List<dynamic>();
            DataInform dataInform = new DataInform();
            try
            {

                //  ExcelConn(_path);  

                //string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Mode=Read;Extended Properties=Excel 12.0;Persist Security Info=False");
                //OleDbConnection Econ = new OleDbConnection(constr);
                //string Query = string.Format("select * from[Sheet1$]");
                //OleDbCommand Ecom = new OleDbCommand(Query, Econ);
                //Econ.Open();

                //DataSet ds = new DataSet();
                //OleDbDataAdapter oda = new OleDbDataAdapter(Query, Econ);
                //Econ.Close();
                //oda.Fill(ds);
                //DataTable Exceldt = ds.Tables[0];
                //foreach (var column in Exceldt.Columns)
                //{
                //    Exceldt.Columns[column.ToString()].ColumnName = column.ToString().Replace(" ", string.Empty).Replace("#", string.Empty);
                //    Exceldt.AcceptChanges();

                //}
                DataTable Exceldt = GetDataTabletFromCSVFile(path);

                dataInform =  InsertDataIntoSQLServerUsingSQLBulkCopy(Exceldt, Tiercount,intCustomerID, inttype);
                objDynamic.Add(dataInform);
                return objDynamic;
            }


            catch (Exception ex)
            {

                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return objDynamic;
            }



        }

        private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return csvData;
        }
    
    private DataInform InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData,int TierCount, Int64 intCustomerID, int inttype)
        {
            int count = 0;
            DataInform dataInform = new DataInform();
            try
            {
                connection.Open();
                Dictionary<string, string> Tablematch = new Dictionary<string, string>();
                using (SqlBulkCopy s = new SqlBulkCopy(connection))
                {

                    string strcolumname = string.Empty;
                    foreach (var column in csvFileData.Columns)
                    {
                        strcolumname = strcolumname.Trim() + column.ToString() + "   varchar (5000) NULL , ";
                    }
                    foreach (var column in csvFileData.Columns)
                    {
                        s.ColumnMappings.Add(column.ToString(), column.ToString());
                    }
                    strcolumname = strcolumname.Remove(strcolumname.Length - 2);

                    string strtemptable = "create table MyTempTable999(" + strcolumname + ")";
                    SqlCommand cmd = new SqlCommand(strtemptable, connection);
                    cmd.CommandTimeout = 180;
                    cmd.ExecuteNonQuery();

                    s.DestinationTableName = "MyTempTable999";
                    s.WriteToServer(csvFileData);
                    connection.Close();
                    dataInform =  AddMultipleProducts("MyTempTable999", TierCount,intCustomerID,inttype);

                }
            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return dataInform;
            }
            finally
            {
                connection.Close();
            }


            return dataInform;
        }

        public List<dynamic> GetProdDataByProductID(string strProductID)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetProdByProductID(Convert.ToInt64(securityHelper.Decrypt(strProductID, false)));//securityHelper.Decrypt(strProductID,false
            var myEnumerableprod = ds.Tables[0].AsEnumerable();

            List<ProductDetail> ProductDetail =
                (from item in myEnumerableprod
                 select new ProductDetail
                 {
                     PartNo = item.Field<String>("PartNo"),
                     Model = item.Field<String>("Model"),
                     ManufacturerId = item.Field<Int64>("ManufacturerId"),
                     ManufacturerDesc = item.Field<String>("ManufacturerDesc"),
                     ProductType = item.Field<String>("ProductType"),
                     ProductTypeId = item.Field<Int64>("ProductTypeId")
                  
                 }).ToList();
            objDynamic.Add(ProductDetail);


            var myEnumerablecustProd = ds.Tables[1].AsEnumerable();

            List<ProductCustDetail> ProductCustDetail =
                (from item in myEnumerablecustProd
                 select new ProductCustDetail
                 {
                     PCRId = item.Field<Int64>("PCRId"),
                     CustName = item.Field<String>("CustName"),
                     CustId = item.Field<Int64>("CustId"),
                     Price = item.Field<Decimal>("Price"),
                     ProdCatDesc = item.Field<String>("ProdCatDesc"),
                     ProdCatId = item.Field<Int64>("ProdCatId"),
                     IsActive = item.Field<bool>("IsActive"),
                     ExpDate = item.Field<String>("EffectiveUpto")

                 }).ToList();
            objDynamic.Add(ProductCustDetail);


            var myEnumerableimagedes = ds.Tables[2].AsEnumerable();

            List<ProductimageDetail> ProductimageDetail =
                (from item in myEnumerableimagedes
                 select new ProductimageDetail
                 {
                     PIID = item.Field<Int64>("PIID"),
                     ImageID = item.Field<String>("ImageID"),
                     ImagePath = item.Field<String>("ImagePath"),
                     Spec = item.Field<String>("Spec")
                 }).ToList();
            objDynamic.Add(ProductimageDetail);
            /////
            var myEnumerableTier = ds.Tables[3].AsEnumerable();

            List<ProductCustomrTierRate> myEnumerableTierAdd =
                (from item in myEnumerableTier
                 select new ProductCustomrTierRate
                 {
                     PCTRId = item.Field<Int64>("PCTRId"),
                     CustId = item.Field<Int64>("CustId"),
                     CustName = item.Field<String>("CustName"),
                     Qty = item.Field<int>("Qty"),
                     Price = item.Field<decimal>("Price"),
                     IsActive = item.Field<Boolean>("IsActive"),
                     StrEffectiveFrom = item.Field<string>("EffectiveFrom"),
                 }).ToList();
            objDynamic.Add(myEnumerableTierAdd);




            objDynamic.Add(IsPlansonUser);
            return objDynamic;


        }



        private DataSet GetProdByProductID(Int64 ProductID)
        {

            string selectProcedure = "[GetProductbyProductID]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@ProductID", ProductID);
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

        private DataInform AddMultipleProducts(string strTableName,int Tiercount, Int64 intCustomerID, int inttype)
        {
            int productID = 0;
            DataInform dataInform = new DataInform();
            string insertProcedure = string.Empty;
            if (inttype == 1)
            {
               insertProcedure = "[InsertMulitpeProductData]";
            }
            else if (inttype == 2)
            {
                insertProcedure = "[InsertMulitpeCategoryData]";
            }

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.CommandTimeout = 180;

            if (intCustomerID != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustID", intCustomerID); // Need to check UI 
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustID", 0);
            }


            if (!string.IsNullOrEmpty(UserID))
            {
                insertCommand.Parameters.AddWithValue("@UserID", Convert.ToInt64(UserID));
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserID", 0);
            }

            if (Tiercount != 0)
            {
                insertCommand.Parameters.AddWithValue("@TierCount", Tiercount);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@TierCount", 0);
            }
            if (!string.IsNullOrEmpty(strTableName))
            {
                insertCommand.Parameters.AddWithValue("@tablename ", strTableName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@tablename ", DBNull.Value);
            }


            insertCommand.Parameters.Add("@ReturnDupValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnDupValue"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;


            try
            {
                int count = 0;
                connection.Open();
                insertCommand.ExecuteNonQuery();
                if (insertCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    dataInform.TotalReccount = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                }
                if (insertCommand.Parameters["@ReturnDupValue"].Value != DBNull.Value)
                {
                    dataInform.TotaProcesscount = System.Convert.ToInt32(insertCommand.Parameters["@ReturnDupValue"].Value);
                }
                dataInform.TotalDupcount =  dataInform.TotalReccount -dataInform.TotaProcesscount;
                return dataInform;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return dataInform;
            }
            finally
            {
                connection.Close();
            }



        }

        public List<dynamic> GetProdData()
        {
            List<dynamic> objDynamic = new List<dynamic>();
            objDynamic.Add(custRequestData.GetCustMaster(2));
            objDynamic.Add(productCategoryData.GetProdCatgry(Convert.ToInt64(CustomerID),1));
            objDynamic.Add(custRequestData.GetProducts(0, 0,3,string.Empty));
            objDynamic.Add(IsPlansonUser);
            return objDynamic;
        }
         
        public List<dynamic> GetProdCategory(Int64 CustID)
        {
            
           return productCategoryData.GetProdCatgry(CustID,1);

            
        }

        public List<dynamic> GetProducts(Productsearch Productsearch)
        {
            string wherecondition = string.Empty, strCustomerID = string.Empty, strCatID = string.Empty;
            string strFinalStatus = string.Empty;
            List<dynamic> objDynamic = new List<dynamic>();
            if (!string.IsNullOrEmpty(Productsearch.CustID))
            {
                strCustomerID = "pcr.CustId =  " + Productsearch.CustID;
            }
            if (!string.IsNullOrEmpty(Productsearch.CatID))
            {
                strCatID = "pcr.ProdCatId =  " + Productsearch.CatID;
            }

            if (!string.IsNullOrEmpty(strCatID))
            {
                //wherecondition =  "  AND  " + strCatID;
                wherecondition = "  " + strCatID;
            }
            if (!string.IsNullOrEmpty(wherecondition))
            {
                if (!string.IsNullOrEmpty(strCustomerID))
                {
                     wherecondition = wherecondition + "  AND  " + strCustomerID;
                    //wherecondition = "  " + strCustomerID;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(strCustomerID))
                {
                    // wherecondition = wherecondition + "  AND  " + strCustomerID;
                    wherecondition = "  " + strCustomerID;
                }
            }


            
            if (string.IsNullOrEmpty(wherecondition))
            {
                return custRequestData.GetProducts(0, 0, 3, wherecondition);
            }
            else
            {
                return custRequestData.GetProducts(0, 0, 2, wherecondition);
            }
         


        }

        public List<dynamic> GetCustProdData()
        {
            List<dynamic> objDynamic = new List<dynamic>();
            objDynamic.Add(custRequestData.GetCustMaster(2));
            objDynamic.Add(productCategoryData.GetProdCatgry(Convert.ToInt64(CustomerID), 2));
            objDynamic.Add(GetManufacturerAndProductTypeDetail());
            objDynamic.Add(IsPlansonUser);
            return objDynamic;
        }


        private DataSet SelectManufacture()
        {

            string selectProcedure = "[GetManufacturerAndProductType]";
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
        public List<dynamic> GetManufacturerAndProductTypeDetail()
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = SelectManufacture();
            var myEnumerable = ds.Tables[0].AsEnumerable();

            List<ManufacturerDet> manufacture =
                (from item in myEnumerable
                 select new ManufacturerDet
                 {
                     ManufacturerId = item.Field<Int64>("ManufacturerId"),
                     ManufacturerDesc = item.Field<String>("ManufacturerDesc"),



                 }).ToList();
            objDynamic.Add(manufacture);

            var myEnumerableprod = ds.Tables[1].AsEnumerable();

            List<ProductTypeDet> product =
                (from item in myEnumerableprod
                 select new ProductTypeDet
                 {
                     ProductTypeId = item.Field<Int64>("ProductTypeId"),
                     ProductType = item.Field<String>("ProductType"),



                 }).ToList();
            objDynamic.Add(product);

            return objDynamic;
        }


        private Int64 AddUpdateProduct(ProductMasterDTO prodmstr)
        {
            Int64 PMId = 0;
            string insertProcedure = "[CreateProductMaster]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
         

            if (prodmstr.ProductId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ProductId", prodmstr.ProductId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProductId", 0);
            }

            if (!string.IsNullOrEmpty(prodmstr.PartNo))
            {
                insertCommand.Parameters.AddWithValue("@PartNo", prodmstr.PartNo);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@PartNo", DBNull.Value);
            }
            if (prodmstr.UserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", prodmstr.UserId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (!string.IsNullOrEmpty(prodmstr.Model))
            {
                insertCommand.Parameters.AddWithValue("@Model", prodmstr.Model);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Model", DBNull.Value);
            }
            if (prodmstr.ManufacturerId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ManufacturerId", prodmstr.ManufacturerId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ManufacturerId", 0);
            }
            if (prodmstr.ProductTypeId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ProductTypeId", prodmstr.ProductTypeId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProductTypeId", 0);
            }
            if (prodmstr.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", prodmstr.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }
            if (prodmstr.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", prodmstr.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }

            if (!string.IsNullOrEmpty(prodmstr.Spec))
            {
                insertCommand.Parameters.AddWithValue("@Spec", prodmstr.Spec);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Spec", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(prodmstr.ImageType))
            {
                insertCommand.Parameters.AddWithValue("@ImageType", prodmstr.ImageType);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ImageType", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(prodmstr.ImageLength))
            {
                insertCommand.Parameters.AddWithValue("@ImageLength", prodmstr.ImageLength);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ImageLength", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(prodmstr.ImageID))
            {
                insertCommand.Parameters.AddWithValue("@ImageID", prodmstr.ImageID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ImageID", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(prodmstr.ImagePath))
            {
                insertCommand.Parameters.AddWithValue("@ImagePath", prodmstr.ImagePath);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }

            insertCommand.Parameters.Add("@ProductIdOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ProductIdOut"].Direction = ParameterDirection.Output;

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
                    if (insertCommand.Parameters["@ProductIdOut"].Value != DBNull.Value)
                    {
                        PMId = System.Convert.ToInt32(insertCommand.Parameters["@ProductIdOut"].Value);
                    }
                    else
                    {
                        PMId = prodmstr.ProductId;                    }
                }


                return PMId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return PMId;
            }
            finally
            {
                connection.Close();
            }



        }

       private Int64 AddUpdateProductCustomer(ProductCustomrRate prodcust)
        {
            Int64 PCRId = 0;
            string insertProcedure = "[CreateProductCustomerRate]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (prodcust.PCRId != 0)
            {
                insertCommand.Parameters.AddWithValue("@PCRId", prodcust.PCRId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@PCRId", 0);
            }

            if (prodcust.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", prodcust.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (prodcust.ProductId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ProductId", prodcust.ProductId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProductId", 0);
            }
            if (prodcust.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial", prodcust.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial", 0);
            }
            if (prodcust.UserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", prodcust.UserId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (prodcust.Price != 0)
            {
                insertCommand.Parameters.AddWithValue("@Price", prodcust.Price);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Price", 0);
            }
            if (prodcust.ProdCatId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ProdCatId", prodcust.ProdCatId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProdCatId", 0);
            }

            if (prodcust.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", prodcust.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }

            if ((prodcust.EffectiveFrom) != null)
            {
                insertCommand.Parameters.AddWithValue("@EffectiveFrom", prodcust.EffectiveFrom);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@EffectiveFrom", DBNull.Value);
            }
            if ((prodcust.EffectiveUpto) != null )
            {
                insertCommand.Parameters.AddWithValue("@EffectiveUpto", prodcust.EffectiveUpto);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@EffectiveUpto", DBNull.Value);
            }

            if (prodcust.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", prodcust.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }


            insertCommand.Parameters.Add("@PCRIdOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@PCRIdOut"].Direction = ParameterDirection.Output;

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
                    if (insertCommand.Parameters["@PCRIdOut"].Value != DBNull.Value)
                    {
                        PCRId = System.Convert.ToInt32(insertCommand.Parameters["@PCRIdOut"].Value);
                    }
                    else
                    {
                        PCRId = prodcust.PCRId;
                    }
                }
               



                return PCRId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return PCRId;
            }
            finally
            {
                connection.Close();
            }



        }

        private int AddUpdateProductCustomerTierRate(ProductCustomrTierRate prodcust)
        {
            int PCTRId = 0;
            string insertProcedure = "[CreateProductCustomerTierRate]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (prodcust.PCTRId != 0)
            {
                insertCommand.Parameters.AddWithValue("@PCTRId", prodcust.PCTRId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@PCTRId", 0);
            }

            if (prodcust.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", prodcust.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (prodcust.ProductId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ProductId", prodcust.ProductId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProductId", 0);
            }
            if (prodcust.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial", prodcust.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial", 0);
            }
            if (prodcust.Qty != 0)
            {
                insertCommand.Parameters.AddWithValue("@Qty", prodcust.Qty);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Qty", 0);
            }
            if (prodcust.UserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", prodcust.UserId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (prodcust.Price != 0)
            {
                insertCommand.Parameters.AddWithValue("@Price", prodcust.Price);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Price", 0);
            }

            if (prodcust.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", prodcust.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }

            if (prodcust.EffectiveFrom != null)
            {
                insertCommand.Parameters.AddWithValue("@EffectiveFrom", prodcust.EffectiveFrom);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@EffectiveFrom", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(prodcust.EffectiveUpto))
            {
                insertCommand.Parameters.AddWithValue("@EffectiveUpto", prodcust.EffectiveUpto);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@EffectiveUpto", DBNull.Value);
            }

            if (prodcust.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", prodcust.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }
           


            insertCommand.Parameters.Add("@PCTRIdOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@PCTRIdOut"].Direction = ParameterDirection.Output;

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

                    if (insertCommand.Parameters["@PCTRIdOut"].Value != DBNull.Value)
                    {
                        PCTRId = System.Convert.ToInt32(insertCommand.Parameters["@PCTRIdOut"].Value);
                    }
                }


                return PCTRId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return PCTRId;
            }
            finally
            {
                connection.Close();
            }



        }


        //here now Tier
        public List<dynamic> SaveTierData(ProductCustomrTierRateUI ProductCustomrTierRateI)
        {
            List<dynamic> objDynamic = new List<dynamic>();

            ProductCustomrTierRate productCustomrTierRate = new ProductCustomrTierRate();

            ProductCustomrTierRateUI productCustomrTierI = new ProductCustomrTierRateUI();

            //int orddetid = 0;

            string strOrderID = securityHelper.Decrypt(ProductCustomrTierRateI.StrProductId, false);
            if (!string.IsNullOrEmpty(ProductCustomrTierRateI.TierDetails))
            {


                var Data = JsonConvert.DeserializeObject<List<ProductCustomrTierRateUI>>(ProductCustomrTierRateI.TierDetails);
                for (int i = 0; i < Data.Count; i++)
                {
                    productCustomrTierI = Data[i];

                    if (productCustomrTierI.PCTRId != 0)
                    {
                        productCustomrTierRate.PCTRId = productCustomrTierI.PCTRId;
                    }
                    if (productCustomrTierI.Qty != 0)
                    {
                        productCustomrTierRate.Qty = productCustomrTierI.Qty;
                    }
                    if (productCustomrTierI.Price != 0)
                    {
                        productCustomrTierRate.Price = productCustomrTierI.Price;
                    }
                    if (productCustomrTierI.IsActive)
                    {
                        productCustomrTierRate.IsActive = productCustomrTierI.IsActive;
                    }
                    if (productCustomrTierI.EffectiveFrom != null)
                    {
                        productCustomrTierRate.EffectiveFrom = DateTime.Parse(productCustomrTierI.EffectiveFrom);
                    }
                   
                    productCustomrTierRate.CustId = ProductCustomrTierRateI.CustId;
                    productCustomrTierRate.ProductId = Convert.ToInt64(strOrderID);
                    productCustomrTierRate.Type = ProductCustomrTierRateI.Type;





                    AddUpdateProductCustomerTierRate(productCustomrTierRate);

                }
                objDynamic.Add(productCustomrTierRate);
            }
            return objDynamic;

        }




        private bool checkData(CustomerInfo customerInfo)
        {
            bool val = true;
            if (string.IsNullOrEmpty(customerInfo.ProdCatId.ToString()))
            {
                val = false;
            }
            else
            {
                return val = true;
            }
            if (string.IsNullOrEmpty(customerInfo.Price.ToString()))
            {
                val = false;
            }
            else
            {
                return val = true;
            }
            if (string.IsNullOrEmpty(customerInfo.IsActive.ToString()))
            {
                val = false;
            }
            else
            {
              return val = true;
            }
          
           
            if (string.IsNullOrEmpty(customerInfo.ExpDate.ToString()))
            {
                val = false;
            }
            else
            {
                return val = true;
            }
            return val;
        }

        public List<dynamic> AddUpdateProducts(List<CustomerInfo> custinfo, ProductMasterDTO productMasterDTO)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            CustomerInfo customerInfo = new CustomerInfo();
            ProductCustomrRate productCustomrRate = new ProductCustomrRate();
            productMasterDTO.UserId = Convert.ToInt64(UserID);
            Int64 ProductId = 0;
            if (!string.IsNullOrEmpty(productMasterDTO.strProductId))
            {
                ProductId = Convert.ToInt64(securityHelper.Decrypt(productMasterDTO.strProductId, false));
                productMasterDTO.ProductId = ProductId;
                productMasterDTO.Type = 2;

            }
            else
            {
                productMasterDTO.Type = 1;
            }
           
            ProductId = AddUpdateProduct(productMasterDTO);
            for (int i = 0; i < custinfo.Count; i++)
            {
                DateTime? dateSample;
                dateSample = null;

                customerInfo = custinfo[i];
                if (checkData(customerInfo))
                {
                    if (!string.IsNullOrEmpty(customerInfo.PCRId.ToString()))
                    {
                        productCustomrRate.PCRId = Convert.ToInt64(customerInfo.PCRId);
                        productCustomrRate.Type = 2;
                    }
                    else
                    {
                        productCustomrRate.PCRId = 0;
                        productCustomrRate.Type = 1;
                    }

                    productCustomrRate.ProductId = ProductId;
                    productCustomrRate.CustId = customerInfo.CustId;
                    if (!string.IsNullOrEmpty(customerInfo.IsActive.ToString()))
                    {
                        productCustomrRate.IsActive = Convert.ToBoolean(customerInfo.IsActive);
                    }
                    if (!string.IsNullOrEmpty(customerInfo.Price.ToString()))
                    {
                        productCustomrRate.Price = Convert.ToDecimal(customerInfo.Price);
                    }
                    if (!string.IsNullOrEmpty(customerInfo.ProdCatId.ToString()))
                    {
                        productCustomrRate.ProdCatId = Convert.ToInt64(customerInfo.ProdCatId);
                    }

                    if (!string.IsNullOrEmpty(customerInfo.ExpDate.ToString()))
                    {
                        productCustomrRate.EffectiveUpto = Convert.ToDateTime(customerInfo.ExpDate);
                    }
                    else
                    {
                        productCustomrRate.EffectiveUpto = dateSample;
                    }
                    productCustomrRate.EffectiveFrom = dateSample;
                  
                    productCustomrRate.Serial = i + 1;
                    productCustomrRate.UserId = Convert.ToInt64(UserID);
                    Int64 pcr = AddUpdateProductCustomer(productCustomrRate);
                }
            }
            objDynamic.Add(securityHelper.Encrypt(ProductId.ToString(),false));
                return objDynamic;
        }

    }
}