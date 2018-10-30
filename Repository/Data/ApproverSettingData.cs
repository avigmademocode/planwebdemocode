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
   


    public class ApproverSettingData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();

        private DataSet GetApprover(Int64 CustId)
        {

            string selectProcedure = "[GetCustomerApprover]";
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


        public List<dynamic> GetApproverData(Int64 CustId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetApprover(CustId);
            try
            {
                var myEnumerableApr = ds.Tables[0].AsEnumerable();
                List<ApproverSettingDTO> ApproverSettingDTO =
                   (from item in myEnumerableApr
                    select new ApproverSettingDTO
                    {
                        CustApproverId = item.Field<Int64>("CustApproverId"),
                        ApproverNameDisplay = item.Field<String>("ApproverNameDisplay"),
                        ApproverSerial = item.Field<int>("ApproverSerial"),
                        LevelofAuthority = item.Field<int>("LevelofAuthority")


                    }).ToList();

                objDynamic.Add(ApproverSettingDTO);
            }
            catch (Exception ex)
            { }


            return objDynamic;
        }

        private ApproverSettingDTO AddUpdateApprover(ApproverSettingDTO approverSettingDTO)
        {

            string insertProcedure = "[CreateCustomerApproverSetting]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;




            if (approverSettingDTO.CustApproverId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustApproverId ", approverSettingDTO.CustApproverId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustApproverId ", 0);
            }
            if (approverSettingDTO.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId ", approverSettingDTO.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId ", 0);
            }
            if (approverSettingDTO.ApproverSerial != 0)
            {
                insertCommand.Parameters.AddWithValue("@ApproverSerial ", approverSettingDTO.ApproverSerial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ApproverSerial ", 0);
            }
            if (!string.IsNullOrEmpty(approverSettingDTO.ApproverNameDisplay) )
            {
                insertCommand.Parameters.AddWithValue("@ApproverNameDisplay ", approverSettingDTO.ApproverNameDisplay);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ApproverNameDisplay", 0);
            }


            if (approverSettingDTO.UserID != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId ", approverSettingDTO.UserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (approverSettingDTO.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", approverSettingDTO.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }

            if (approverSettingDTO.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatementType", approverSettingDTO.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatementType", 0);
            }
            insertCommand.Parameters.Add("@CustApproverIdout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@CustApproverIdout"].Direction = ParameterDirection.Output;

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
                    approverSettingDTO.Status = count;

                }
                if (count != 0 && approverSettingDTO.CustApproverId == 0)
                {
                    approverSettingDTO.CustApproverId = System.Convert.ToInt32(insertCommand.Parameters["@CustApproverIdout"].Value);
                }

                return approverSettingDTO;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("ApproverSettingData.AddUpdateApprover");
                log.logErrorMessage(ex.StackTrace);
                return approverSettingDTO;
            }
            finally
            {
                connection.Close();
            }



        }

        public List<dynamic> AddUpdateApproverData(ApproverSetting approverSetting)
        {

            List<dynamic> objDynamic = new List<dynamic>();
            ApproverSettingDTO approverSettingDTO = new ApproverSettingDTO();
 

            try
            {
                switch (approverSetting.Type)
                {
                    case 1:
                        {
                            var Data = JsonConvert.DeserializeObject<List<ApproverSettingDTO>>(approverSetting.ApproverData);
                            for (int i = 0; i < Data.Count; i++)
                            {
                                approverSettingDTO = Data[i];
                                if (!string.IsNullOrEmpty(UserID))
                                {
                                    approverSettingDTO.UserID = Convert.ToInt64(UserID);
                                }
                                approverSettingDTO.CustId = approverSetting.CustId;
                                if (approverSettingDTO.Ischange == 1)
                                {
                                    if (approverSettingDTO.CustApproverId == 0)
                                    {
                                        approverSettingDTO.Type = 1;
                                        approverSettingDTO.IsActive = true;
                                    }
                                    else
                                    {
                                        approverSettingDTO.Type = 2;
                                        approverSettingDTO.IsActive = true;

                                    }
                                    if (approverSettingDTO.IsDelete)
                                    {
                                        approverSettingDTO.Type = 3;
                                        approverSettingDTO.IsActive = false;
                                    }

                                    AddUpdateApprover(approverSettingDTO);
                                    if (approverSetting.Status == -99)
                                    {
                                        break;
                                    }
                                }




                            }
                            break;
                        }

                     
                }

            }
            catch (Exception ex)
            {


            }
            objDynamic.Add(approverSetting.Status);
            objDynamic.Add(GetApproverData(approverSetting.CustId));
            return objDynamic;
        }


    }
}