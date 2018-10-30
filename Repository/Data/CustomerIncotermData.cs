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
    public class CustomerIncotermData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        private int AddCustomerIncoTerm(CustomerIncoTerm customerIncoTerm)
        {

            string insertProcedure = "[CreateCustomerIncoterm]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;

            int CIId = 0;


            if (customerIncoTerm.CIId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CIId", customerIncoTerm.CIId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CIId", 0);
            }
            if (customerIncoTerm.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", customerIncoTerm.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (customerIncoTerm.IncoTermId != 0)
            {
                insertCommand.Parameters.AddWithValue("@IncoTermId", customerIncoTerm.IncoTermId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IncoTermId", 0);
            }
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

                return CIId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return CIId;
            }
            finally
            {
                connection.Close();
            }



        }
        private int UpdateCustomerIncoTerm(CustomerIncoTerm customerIncoTerm)
        {

            string updateProcedure = "[CreateCustomerIncoterm]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;

            int CIId = 0;


            if (customerIncoTerm.CIId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CIId", customerIncoTerm.CIId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CIId", 0);
            }
            if (customerIncoTerm.CustId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CustId", customerIncoTerm.CustId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (customerIncoTerm.IncoTermId != 0)
            {
                updateCommand.Parameters.AddWithValue("@IncoTermId", customerIncoTerm.IncoTermId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@IncoTermId", 0);
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

                return CIId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return CIId;
            }
            finally
            {
                connection.Close();
            }



        }
        public DataSet GetCustomerIncoTerm(Int64 CIId)
        {

            string selectProcedure = "[GetCustomerIncoterm]";
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            DataSet ds = new DataSet();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CIId", CIId);
            DataTable dt = new DataTable();
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
    }
}