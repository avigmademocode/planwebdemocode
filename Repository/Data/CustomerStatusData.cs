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
    public class CustomerStatusData
    { 
    
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();
       
        
        private CustomerStatus AddCustomerStatus(CustomerStatus customerStatus)
        {

            string insertProcedure = "[CreateCustomerStatus]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;

            


            if (customerStatus.CustStatusId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustStatusId ", customerStatus.CustStatusId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustStatusId ", 0);
            }
            if (customerStatus.CustId!= 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId ", customerStatus.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId ", 0);
            }
            if (customerStatus.StatusId != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatusId ", customerStatus.StatusId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatusId ", 0);
            }
            if (customerStatus.StatusOrder != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatusOrder ", customerStatus.StatusOrder);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatusOrder", 0);
            }


            if (customerStatus.UserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId ", customerStatus.UserId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (customerStatus.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", customerStatus.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }

            if (customerStatus.StatementType != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatementType", customerStatus.StatementType);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatementType", 0);
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
                    customerStatus.Status = count;

                }

                return customerStatus;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return customerStatus;
            }
            finally
            {
                connection.Close();
            }



        }
        private int UpdateCustomerStatus(CustomerStatus customerStatus)
        {

            string updateProcedure = "[CreateCustomerStatus]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;

            int CustStatusId = 0;


            if (customerStatus.CustStatusId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CustStatusId ", customerStatus.CustStatusId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CustStatusId ", 0);
            }
            if (customerStatus.CustId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CustId ", customerStatus.CustId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CustId ", 0);
            }
            if (customerStatus.StatusId != 0)
            {
                updateCommand.Parameters.AddWithValue("@StatusId ", customerStatus.StatusId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@StatusId ", 0);
            }
            if (customerStatus.StatusOrder != 0)
            {
                updateCommand.Parameters.AddWithValue("@StatusOrder ", customerStatus.StatusOrder);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@StatusOrder", 0);
            }
            if (customerStatus.IsActive)
            {
                updateCommand.Parameters.AddWithValue("@IsActive", customerStatus.IsActive);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@IsAtive ", 0);
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

                return CustStatusId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return CustStatusId;
            }
            finally
            {
                connection.Close();
            }



        }
        private DataSet GetCustomerStatus(Int64 CustId)
        {

            string selectProcedure = "[GetCustomerStatus]";
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            DataSet ds = new DataSet();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CustId", CustId);
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


        private DataSet GetAllCustomerActiveStatus()
        {

            string selectProcedure = "[GetAllCustomerActiveStatus]";
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            DataSet ds = new DataSet();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
          
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


        public List<dynamic> GetAllCustomerActiveStatusData()
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetAllCustomerActiveStatus();
            try
            {
                var myEnumerableApr = ds.Tables[0].AsEnumerable();
                List<CustomerStatus> CustomerStatus =
                   (from item in myEnumerableApr
                    select new CustomerStatus
                    {
                        StatusId = item.Field<Int64>("StatusId"),
                        StatusName = item.Field<String>("StatusName"),
                        StatusOrder = item.Field<int>("StatusOrder"),
                        CustId = item.Field<Int64>("CustId"),


                    }).ToList();

                objDynamic.Add(CustomerStatus);
            }
            catch (Exception ex)
            { }


            return objDynamic;
        }



        public List<dynamic> GetCustomerStatusData(Int64 CustId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetCustomerStatus(CustId);
            try
            {
                var myEnumerableApr = ds.Tables[0].AsEnumerable();
                List<CustomerStatus> CustomerStatus =
                   (from item in myEnumerableApr
                    select new CustomerStatus
                    {
                        StatusId = item.Field<Int64>("StatusId"),
                        StatusName = item.Field<String>("StatusName"),
                        StatusOrder = item.Field<int>("StatusOrder")


                    }).ToList();

                objDynamic.Add(CustomerStatus);
            }
            catch(Exception ex)
            { }
           

            return objDynamic;
        }


        public List<dynamic> AddUpdateCustStatusData(GetCustomerStatusData getCustomerStatusData)
        {

            List<dynamic> objDynamic = new List<dynamic>();
            GetCustomerStatus GetCustomerStatusDTO = new GetCustomerStatus();
            CustomerStatus customerStatus = new CustomerStatus();
            StatusInfo StatusInfodata = new StatusInfo();
            
            try
            {
                switch (getCustomerStatusData.Type)
                {
                    case 2:
                        {
                            var Data = JsonConvert.DeserializeObject<List<GetCustomerStatus>>(getCustomerStatusData.CustStatusData);
                            for (int i = 0; i < Data.Count; i++)
                            {
                                GetCustomerStatusDTO = Data[i];
                                customerStatus.CustId = getCustomerStatusData.CustId;
                                customerStatus.StatusId = GetCustomerStatusDTO.StatusId;
                                customerStatus.StatusOrder = GetCustomerStatusDTO.StatusOrder;
                                if (GetCustomerStatusDTO.IsDelete)
                                {
                                    customerStatus.IsActive = false;
                                }
                                else
                                {
                                    customerStatus.IsActive = true;
                                }
                                
                                customerStatus.StatementType = getCustomerStatusData.Type;
                                if (!string.IsNullOrEmpty(UserID))
                                {
                                    customerStatus.UserId = Convert.ToInt64(UserID);
                                }
                                
                                AddCustomerStatus(customerStatus);
                                getCustomerStatusData.Status = customerStatus.Status;
                                
                                if (customerStatus.Status == -99)
                                {

                                    break;
                                }

                            }
                            break;
                        }

                    case 1:
                        {
                            StatusInfo statusInfo = new StatusInfo();
                            StatusData statusData = new StatusData();
                            statusInfo.intCustID = getCustomerStatusData.CustId;
                            statusInfo.AltName = getCustomerStatusData.AltName;
                            statusInfo.StatusName = getCustomerStatusData.StatusName;
                            statusInfo.IsActive = true;
                            statusInfo.UserAction = getCustomerStatusData.UserAction;
                            statusInfo.ShowStatus = getCustomerStatusData.ShowStatus;
                            statusInfo.Type = 3;
                            var data = statusData.AddUpdateStatus(statusInfo);
                            getCustomerStatusData.Status = data[0];
                            if (data[0] == -99 )
                            {
                                 
                                break;
                            }

                            
                            break;
                        }
                }

            }
            catch (Exception ex)
            {


            }
           
            objDynamic.Add(getCustomerStatusData.Status);
            objDynamic.Add(GetCustomerStatusData(getCustomerStatusData.CustId));
            return objDynamic;
        }


       
    }

}