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
using System.Collections;
namespace MyProject.Repository.Data
{
    public class ProductCategoryData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();

        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();

        private ProductCatgry AddProductCategory(ProductCatgry proCat)
        {
            Int64 ProdCatId = 0;
            string insertProcedure = "[CreateProductCategory]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (proCat.ProdCatId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ProdCatId", proCat.ProdCatId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProdCatId", 0);
            }

            if (proCat.CustID != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustID", proCat.CustID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustID", 0);
            }

            if (!string.IsNullOrEmpty(proCat.ProdCatDesc))
            {
                insertCommand.Parameters.AddWithValue("@ProdCatDesc", proCat.ProdCatDesc);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProdCatDesc", DBNull.Value);
            }
            if (proCat.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", proCat.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }
            if (proCat.UserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", proCat.UserId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (proCat.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", proCat.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }


            insertCommand.Parameters.Add("@ProdCatIdOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ProdCatIdOut"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@Prodcount", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@Prodcount"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;


            try
            {
                int count = 0, prodcount = 0;
                connection.Open();
                insertCommand.ExecuteNonQuery();
                if (insertCommand.Parameters["@ReturnValue"].Value != DBNull.Value)
                {
                    count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                }
                if (insertCommand.Parameters["@Prodcount"].Value != DBNull.Value)
                {
                    proCat.count = System.Convert.ToInt32(insertCommand.Parameters["@Prodcount"].Value);
                }
                if (count != 0)
                {
                    if (insertCommand.Parameters["@ProdCatIdOut"].Value != DBNull.Value)
                    {
                        proCat.ProdCatId = System.Convert.ToInt32(insertCommand.Parameters["@ProdCatIdOut"].Value);
                    }
                }


                return proCat;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return proCat;
            }
            finally
            {
                connection.Close();
            }



        }
        public List<dynamic> AddProdCatgry(ProductCategoryDetail productCategoryDetail)
        {

            List<dynamic> objDynamic = new List<dynamic>();
            ProductCatgry productCatgry = new ProductCatgry();
            ProdcatCust prodcatCust = new ProdcatCust();
            int value = 0;
            switch (productCategoryDetail.Type)
            {
                case "1":
                    {
                        var Data = JsonConvert.DeserializeObject<List<ProductCatgry>>(productCategoryDetail.Productcategorydet);

                        for (int i = 0; i < Data.Count; i++)
                        {

                            productCatgry = Data[i];
                            if (productCatgry.Ischange == 1)
                            {

                                if (!string.IsNullOrEmpty(UserID))
                                {
                                    productCatgry.UserId = Convert.ToInt64(UserID);
                                }

                                if (!string.IsNullOrEmpty(productCategoryDetail.CustID))
                                {
                                    productCatgry.CustID = Convert.ToInt64(productCategoryDetail.CustID);
                                }
                                if (productCatgry.IsDelete == true)
                                {
                                    productCatgry.Type = 3;
                                    productCatgry.IsActive = false;
                                }
                                AddProductCategory(productCatgry);
                            }

                        }
                        break;
                    }

                case "2":
                    {

                        var Data = JsonConvert.DeserializeObject<List<ProdcatCust>>(productCategoryDetail.CustID);
                        Int64 val = Int64.Parse(productCategoryDetail.CatID);

                        for (int i = 0; i < Data.Count; i++)
                        {
                            ProdcatCust prodcatCustnew = new ProdcatCust();
                            prodcatCustnew = Data[i];
                            if (val != 0)
                            {
                                productCatgry.Type = 5;
                                productCatgry.ProdCatId = val;
                            }
                            else
                            {
                                productCatgry.Type = 1;
                                productCatgry.IsActive = true;
                            }


                            if (!string.IsNullOrEmpty(UserID))
                            {
                                productCatgry.UserId = Convert.ToInt64(UserID);
                            }
                            productCatgry.IsActive = prodcatCustnew.IsCat;
                            productCatgry.CustID = (prodcatCustnew.CustId);
                            productCatgry.ProdCatDesc = productCategoryDetail.Productcategorydet;

                             AddProductCategory(productCatgry);
                            val = productCatgry.ProdCatId;
                            if (productCatgry.count == -99)
                            {
                                value = productCatgry.count;
                            }


                        }
                        break;
                    }
            }


            DataSet ds = GetProductCatDetail(0, 2);

            List<ProductCatgry> prodCatmaster =
                      (from item in ds.Tables[0].AsEnumerable()
                       select new ProductCatgry
                       {
                           ProdCatId = item.Field<Int64>("ProdCatId"),
                           ProdCatDesc = item.Field<String>("ProdCatDesc"),
                           IsActive = item.Field<bool>("IsActive"),
                           Type = 2,


                       }).ToList();
            objDynamic.Add(prodCatmaster);


            List<ProductCatgry> prodCat =
                   (from item in ds.Tables[1].AsEnumerable()
                    select new ProductCatgry
                    {
                        ProdCatId = item.Field<Int64>("ProdCatId"),
                        ProdCatDesc = item.Field<String>("ProdCatDesc"),
                        IsActive = item.Field<bool>("IsActive"),
                        CustID = item.Field<Int64>("CustId"),


                    }).ToList();
            objDynamic.Add(prodCat);

            objDynamic.Add(value);

            return objDynamic;
        }

        private DataSet GetProductCatDetail(Int64 CustID, int TypeID)
        {

            string selectProcedure = "[GetProductCategory]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CustID", CustID);
            da.SelectCommand.Parameters.AddWithValue("@TypeID", TypeID);
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
        public List<dynamic> GetProdCatgry(Int64 CustID, int intTypeID)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetProductCatDetail(CustID, intTypeID);
            var myEnumerable = ds.Tables[0].AsEnumerable();
            if (intTypeID == 1)
            {
                List<ProductCatgry> prodCat =
             (from item in myEnumerable
              select new ProductCatgry
              {
                  ProdCatId = item.Field<Int64>("ProdCatId"),
                  ProdCatDesc = item.Field<String>("ProdCatDesc"),
                  IsActive = item.Field<bool>("IsActive"),
                  Type = 2,


              }).ToList();
                objDynamic.Add(prodCat);
            }
            else if (intTypeID == 2)
            {

                List<ProductCatgry> prodCatmaster =
                          (from item in ds.Tables[0].AsEnumerable()
                           select new ProductCatgry
                           {
                               ProdCatId = item.Field<Int64>("ProdCatId"),
                               ProdCatDesc = item.Field<String>("ProdCatDesc"),
                               IsActive = item.Field<bool>("IsActive"),
                               Type = 2,


                           }).ToList();
                objDynamic.Add(prodCatmaster);


                List<ProductCatgry> prodCat =
                       (from item in ds.Tables[1].AsEnumerable()
                        select new ProductCatgry
                        {
                            ProdCatId = item.Field<Int64>("ProdCatId"),
                            ProdCatDesc = item.Field<String>("ProdCatDesc"),
                            IsActive = item.Field<bool>("IsActive"),
                            CustID = item.Field<Int64>("CustId"),


                        }).ToList();
                objDynamic.Add(prodCat);



            }


            return objDynamic;
        }


    }

}
