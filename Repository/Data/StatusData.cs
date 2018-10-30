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
    public class StatusData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();

        private StatusInfo AddStatus(StatusInfo statusInfo)
        {

            string insertProcedure = "[CreateStatus]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;



            if (statusInfo.StatusId != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatusId ", statusInfo.StatusId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatusId ", 0);
            }
            if (!string.IsNullOrEmpty(statusInfo.StatusName))
            {
                insertCommand.Parameters.AddWithValue("@StatusName", statusInfo.StatusName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatusName", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(statusInfo.AltName))
            {
                insertCommand.Parameters.AddWithValue("@AltName", statusInfo.AltName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@AltName", DBNull.Value);
            }
            if (statusInfo.intCustID != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", statusInfo.intCustID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }


            if (statusInfo.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", statusInfo.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }

            if (statusInfo.UserAction != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserAction", statusInfo.UserAction);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserAction ", 0);
            }

            if (statusInfo.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatementType", statusInfo.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatementType", 0);
            }

            if (statusInfo.UserID != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserID", statusInfo.UserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserID", 0);
            }


            if (statusInfo.ShowStatus)
            {
                insertCommand.Parameters.AddWithValue("@ShowStatus", statusInfo.ShowStatus);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ShowStatus", 0);
            }

            insertCommand.Parameters.Add("@StatusIdout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@StatusIdout"].Direction = ParameterDirection.Output;

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
                    statusInfo.Status = count;
                }
                if (count == 1 && statusInfo.StatusId == 0)
                {
                    statusInfo.StatusId = System.Convert.ToInt32(insertCommand.Parameters["@StatusIdout"].Value);
                }
                return statusInfo;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return statusInfo;
            }
            finally
            {
                connection.Close();
            }



        }

        public List<dynamic> AddUpdateStatus(StatusInfo statusInfo)
        {

            List<dynamic> objDynamic = new List<dynamic>();
            GetCustomerStatus GetCustomerStatusDTO = new GetCustomerStatus();
            StatusInfo StatusInfodata = new StatusInfo();
            try
            {
                switch (statusInfo.Type)
                {
                    case 1:
                        {
                            var Data = JsonConvert.DeserializeObject<List<StatusInfo>>(statusInfo.StatusData);
                            for (int i = 0; i < Data.Count; i++)
                            {
                                StatusInfodata = Data[i];
                                if (!string.IsNullOrEmpty(UserID))
                                {
                                    StatusInfodata.UserID = Convert.ToInt64(UserID);
                                }
                                
                                if (StatusInfodata.IsChange && StatusInfodata.IsDelete)
                                {
                                    StatusInfodata.IsActive = false;
                                    StatusInfodata.Type = 3;
                                    AddStatus(StatusInfodata);
                                }
                            }
                            objDynamic.Add(GetStatusData(0));
                            break;
                        }
                    case 2:
                        {
                            var Data = JsonConvert.DeserializeObject<List<GetCustomerStatus>>(statusInfo.CustID);
                            Int64 val = statusInfo.StatusId;

                            for (int i = 0; i < Data.Count; i++)
                            {

                                GetCustomerStatusDTO = Data[i];
                                if (!string.IsNullOrEmpty(UserID))
                                {
                                    StatusInfodata.UserID = Convert.ToInt64(UserID);
                                }
                                StatusInfodata.intCustID = GetCustomerStatusDTO.CustId;
                                if (val != 0)
                                {
                                    StatusInfodata.IsActive = GetCustomerStatusDTO.IsCat;
                                    StatusInfodata.Type = 2;
                                    StatusInfodata.StatusId = val;
                                    StatusInfodata.StatusName = statusInfo.StatusName;
                                    StatusInfodata.AltName = statusInfo.AltName;
                                    StatusInfodata.UserAction = statusInfo.UserAction;
                                    StatusInfodata.ShowStatus = GetCustomerStatusDTO.ShowStatus;
                                    AddStatus(StatusInfodata);
                                    if (StatusInfodata.Status != 1)
                                    {
                                        break;
                                    }

                                }
                                else
                                {
                                    StatusInfodata.IsActive = GetCustomerStatusDTO.IsCat;
                                    StatusInfodata.Type = 1;
                                    StatusInfodata.StatusName = statusInfo.StatusName;
                                    StatusInfodata.AltName = statusInfo.AltName;
                                    StatusInfodata.UserAction = statusInfo.UserAction;
                                    StatusInfodata.ShowStatus = GetCustomerStatusDTO.ShowStatus;
                                    AddStatus(StatusInfodata);
                                    val = StatusInfodata.StatusId;
                                    if (StatusInfodata.Status != 1)
                                    {
                                        break;
                                    }

                                }






                            }
                            objDynamic.Add(StatusInfodata.Status);
                            objDynamic.Add(GetStatusData(0));
                            break;
                        }
                    case 3:
                        {
                            statusInfo.Type = 1;
                            AddStatus(statusInfo);
                            objDynamic.Add(statusInfo.Status);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {


            }
        
            return objDynamic;
        }

      

        private int UpdateStatus(StatusInfo statusInfo)
        {

            string updateProcedure = "[CreateStatus]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;

            int StatusId = 0;


            if (statusInfo.StatusId != 0)
            {
                updateCommand.Parameters.AddWithValue("@StatusId ", statusInfo.StatusId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@StatusId ", 0);
            }
            if (!string.IsNullOrEmpty(statusInfo.StatusName))
            {
                updateCommand.Parameters.AddWithValue("@StatusName", statusInfo.StatusName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@StatusName", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(statusInfo.AltName))
            {
                updateCommand.Parameters.AddWithValue("@AltName", statusInfo.AltName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@AltName", DBNull.Value);
            }


            if (statusInfo.UserAction != 0)
            {
                updateCommand.Parameters.AddWithValue("@UserAction", statusInfo.UserAction);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@UserAction ", 0);
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

                return StatusId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return StatusId;
            }
            finally
            {
                connection.Close();
            }



        }

        public List<dynamic> GetStatusData(Int64 StatusId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetStatusDataMaster(StatusId);
            try
            {
                var myEnumerable = ds.Tables[0].AsEnumerable();
                var myEnumerablecust = ds.Tables[1].AsEnumerable();
                List<StatusInfo> StatusInfo =
                    (from item in myEnumerable
                     select new StatusInfo
                     {
                         StatusId = item.Field<Int64>("StatusId"),
                         StatusName = item.Field<string>("StatusName"),
                         AltName = item.Field<string>("AltName"),
                         UserAction = item.Field<Byte>("UserAction")

                     }).ToList();

                objDynamic.Add(StatusInfo);

                List<GetCustomerStatus> CustomerStatus =
                   (from item in myEnumerablecust
                    select new GetCustomerStatus
                    {
                        CustId = item.Field<Int64>("CustId"),
                        StatusId = item.Field<Int64>("StatusId"),
                        StatusOrder = item.Field<int>("StatusOrder"),
                        IsActive = item.Field<bool>("IsActive"),
                        ShowStatus = item.Field<bool>("ShowStatus")

                    }).ToList();

                objDynamic.Add(CustomerStatus);
            }
            catch (Exception ex)
            {

            }
            return objDynamic;
        }
        public DataSet GetStatusDataMaster(Int64 StatusId)
        {
            string selectProcedure = "[GetStatus]";
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            DataSet ds = new DataSet();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@StatusId", StatusId);
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