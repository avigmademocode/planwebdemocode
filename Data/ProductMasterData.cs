using System;
using System.Data;
using System.Data.SqlClient;
using MyProject.Models;

namespace MyProject.Data
{
    public class ProductMasterData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "sp_ppGetProductInfo";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Mode", 1);
            selectCommand.Parameters.AddWithValue("@IsActive", 1);

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
            catch (SqlException ex)
            {
                
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable SelectAllForCustomer(Int64 custid, Int64 prodcatid)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "sp_ppGetProductInfo";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Mode", 2);
            selectCommand.Parameters.AddWithValue("@CustId", custid);
            selectCommand.Parameters.AddWithValue("@ProdCatId", prodcatid);
            selectCommand.Parameters.AddWithValue("@IsActive", 1);


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
            catch (SqlException ex)
            {

                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable SelectCustomer()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "sp_ppGetProductInfo";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Mode", 3);
            selectCommand.Parameters.AddWithValue("@IsActive", 1);

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
            catch (SqlException ex)
            {

                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable SelectCategoryForCustomer(Int64 pcustid)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "sp_ppGetProductInfo";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Mode", 4);
            selectCommand.Parameters.AddWithValue("@CustId", pcustid);
            selectCommand.Parameters.AddWithValue("@IsActive", 1);

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
            catch (SqlException ex)
            {

                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable SearchfrIndex1(string sField, string sCondition, string sValue)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[ProductMasterSearch1]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Product Id")
            {
                selectCommand.Parameters.AddWithValue("@ProductId", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ProductId", DBNull.Value);
            }
            if (sField == "Model")
            {
                selectCommand.Parameters.AddWithValue("@Model", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Model", DBNull.Value);
            }
            selectCommand.Parameters.AddWithValue("@SearchCondition", sCondition);
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
            catch (SqlException)
            {
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable SearchfrIndex2(string custid, string sField, string sCondition, string sValue)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[ProductMasterSearch2]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@CustId", custid);
            if (sField == "Product Id")
            {
                selectCommand.Parameters.AddWithValue("@ProductId", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ProductId", DBNull.Value);
            }
            if (sField == "Model")
            {
                selectCommand.Parameters.AddWithValue("@Model", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Model", DBNull.Value);
            }
            if (sField == "Price")
            {
                selectCommand.Parameters.AddWithValue("@Price", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Price", DBNull.Value);
            }
            if (sField == "Category Id")
            {
                selectCommand.Parameters.AddWithValue("@ProdCatId", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ProdCatId", DBNull.Value);
            }
            if (sField == "Category")
            {
                selectCommand.Parameters.AddWithValue("@ProdCatDesc", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ProdCatDesc", DBNull.Value);
            }

            selectCommand.Parameters.AddWithValue("@SearchCondition", sCondition);
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
            catch (SqlException)
            {
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static DataTable ManufactureList()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "sp_ppGetProductInfo";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Mode", 5);
            

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
            catch (SqlException ex)
            {

                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }
        public static DataTable ProductTypeList()
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "sp_ppGetProductInfo";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Mode", 6);


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
            catch (SqlException ex)
            {

                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool Add(ProductMaster prdMaster )
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string insertProcedure = "[ProductMasterInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@ProductId", prdMaster.ProductId);
            if (prdMaster.Model != null)
            {
                insertCommand.Parameters.AddWithValue("@Model", prdMaster.Model);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Model", DBNull.Value);
            }
            if (prdMaster.Partno != null)
            {
                insertCommand.Parameters.AddWithValue("@Partno", prdMaster.Partno);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Partno", DBNull.Value);
            }
            if (prdMaster.manufacturerId.HasValue==true)
            {
                insertCommand.Parameters.AddWithValue("@manufacturerId", prdMaster.manufacturerId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@manufacturerId", DBNull.Value);
            }
            if (prdMaster.ProductTypeId.HasValue==true)
            {
                if (prdMaster.ProductTypeId == 0)
                {
                    insertCommand.Parameters.AddWithValue("@ProductTypeId", DBNull.Value);
                }
                else
                {
                    insertCommand.Parameters.AddWithValue("@ProductTypeId", prdMaster.ProductTypeId);
                }
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProductTypeId", DBNull.Value);
            }
  
            if (prdMaster.IsActive == true)
            {
                insertCommand.Parameters.AddWithValue("@isActive", 1);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isActive", 1);
            }
            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static ProductMaster Select_Record(Int64 pProductId)
        {
            ProductMaster ProductMaster = new ProductMaster();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[sp_ppGetProductInfo]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Mode", 7);
            selectCommand.Parameters.AddWithValue("@ProductId", pProductId);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    ProductMaster.ProductId = System.Convert.ToInt64(reader["ProductId"]);
                    ProductMaster.Model = reader["Model"] is DBNull ? null : reader["Model"].ToString();
                    ProductMaster.Partno = reader["Partno"] is DBNull ? null : reader["Partno"].ToString();
                    ProductMaster.manufacturerId= reader["manufacturerId"] is DBNull ? null : (Int64?)reader["manufacturerId"];
                    ProductMaster.ProductTypeId = reader["ProductTypeId"] is DBNull ? null : (Int64?)reader["ProductTypeId"];
                    ProductMaster.IsActive = (Boolean)reader["isActive"];
                }
                else
                {
                    ProductMaster = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return ProductMaster;
            }
            finally
            {
                connection.Close();
            }
            return ProductMaster;
        }

        public static ProductMasterDetail Select_Record_Detail(Int64 pProductId)
        {
            ProductMasterDetail ProductMasterDetail = new ProductMasterDetail();
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string selectProcedure = "[sp_ppGetProductInfo]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Mode", 8);
            selectCommand.Parameters.AddWithValue("@ProductId", pProductId);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    
                    ProductMasterDetail.ProductId = System.Convert.ToInt64(reader["ProductId"]);
                    ProductMasterDetail.Model = reader["Model"] is DBNull ? null : reader["Model"].ToString();
                    ProductMasterDetail.Partno = reader["Partno"] is DBNull ? null : reader["Partno"].ToString();
                    ProductMasterDetail.manufacturerdesc = reader["manufacturerdesc"] is DBNull ? null :reader["manufacturerdesc"].ToString();
                    ProductMasterDetail.ProductType = reader["ProductType"] is DBNull ? null :reader["ProductType"].ToString();
                    ProductMasterDetail.IsActive = (Boolean)reader["isActive"];
                }
                else
                {
                    ProductMasterDetail = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return ProductMasterDetail;
            }
            finally
            {
                connection.Close();
            }
            return ProductMasterDetail;
        }

        public static bool Update(ProductMaster oldProductMaster,
       ProductMaster newPrductMaster)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string updateProcedure = "[ProductMasterUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewProductId", newPrductMaster.ProductId);
            if (newPrductMaster.Model != null)
            {
                updateCommand.Parameters.AddWithValue("@NewModel", newPrductMaster.Model);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewModel", DBNull.Value);
            }
            if (newPrductMaster.Partno != null)
            {
                updateCommand.Parameters.AddWithValue("@NewPartno", newPrductMaster.Partno);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewPartno", DBNull.Value);
            }
            if (newPrductMaster.manufacturerId.HasValue == true)
            {
                if (newPrductMaster.manufacturerId == 0)
                {
                    updateCommand.Parameters.AddWithValue("@NewmanufacturerId", DBNull.Value);
                }
                else { 
                updateCommand.Parameters.AddWithValue("@NewmanufacturerId", newPrductMaster.manufacturerId);
                }
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewmanufacturerId", DBNull.Value);
            }
            if (newPrductMaster.ProductTypeId.HasValue == true)
            {
                if (newPrductMaster.ProductTypeId == 0)
                {
                    updateCommand.Parameters.AddWithValue("@NewProductTypeId", DBNull.Value);
                }
                else
                {
                    updateCommand.Parameters.AddWithValue("@NewProductTypeId", newPrductMaster.ProductTypeId);
                }
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewProductTypeId", DBNull.Value);
            }
            if (newPrductMaster.IsActive == true)
            {
                updateCommand.Parameters.AddWithValue("@NewisActive", 1);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewisActive", 0);
            }
            updateCommand.Parameters.AddWithValue("@OldProductId", oldProductMaster.ProductId);
            if (oldProductMaster.Model != null)
            {
                updateCommand.Parameters.AddWithValue("@OldModel", oldProductMaster.Model);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldModel", DBNull.Value);
            }
            if (oldProductMaster.Partno != null)
            {
                updateCommand.Parameters.AddWithValue("@OldPartNo", oldProductMaster.Partno);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldPartNo", DBNull.Value);
            }
            if (oldProductMaster.manufacturerId.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldmanufacturerId", oldProductMaster.manufacturerId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldmanufacturerId", DBNull.Value);
            }
            if (oldProductMaster.ProductTypeId.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldProductTypeId", oldProductMaster.ProductTypeId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldProductTypeId", DBNull.Value);
            }
            if (oldProductMaster.IsActive == true)
            {
                updateCommand.Parameters.AddWithValue("@OldisActive", oldProductMaster.IsActive);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldisActive", DBNull.Value);
            }
            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                updateCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Delete(ProductMaster ProductMaster)
        {
            SqlConnection connection = plansoni_webstoreData.GetConnection();
            string deleteProcedure = "[ProductMasterDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldProductId", ProductMaster.ProductId);
            if (ProductMaster.Model != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldModel", ProductMaster.Model);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldModel", DBNull.Value);
            }
            if (ProductMaster.Partno != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldPartno", ProductMaster.Partno);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldPartno", DBNull.Value);
            }
            if (ProductMaster.manufacturerId.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldmanufacturerId", ProductMaster.manufacturerId);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldmanufacturerId", DBNull.Value);
            }
            if (ProductMaster.ProductTypeId.HasValue == true)
            {
                if (ProductMaster.ProductTypeId == 0)
                {
                    deleteCommand.Parameters.AddWithValue("@OldProductTypeId", DBNull.Value);
                }
                else
                {
                    deleteCommand.Parameters.AddWithValue("@OldProductTypeId", ProductMaster.ProductTypeId);
                }
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldProductTypeId", DBNull.Value);
            }
            if (ProductMaster.IsActive == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldisActive", ProductMaster.IsActive);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldisActive", DBNull.Value);
            }
            deleteCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            deleteCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                deleteCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(deleteCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }




    }
}