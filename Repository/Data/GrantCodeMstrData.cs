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
    public class GrantCodeMstrData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();

        private GrantCodeMster AddGrantCodeMaster(GrantCodeMster grantCodeMster)
        {

            string insertProcedure = "[CreateGrantCodeMaster]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;

            


            if (grantCodeMster.GrantId != 0)
            {
                insertCommand.Parameters.AddWithValue("@GrantId", grantCodeMster.GrantId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@GrantId", 0);
            }
            if (!string.IsNullOrEmpty(grantCodeMster.GrantTitle))
            {
                insertCommand.Parameters.AddWithValue("@GrantTitle", grantCodeMster.GrantTitle);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@GrantTitle", DBNull.Value);
            }
            if (grantCodeMster.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", grantCodeMster.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (grantCodeMster.isRequired)
            {
                insertCommand.Parameters.AddWithValue("@isRequired", grantCodeMster.isRequired);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isRequired", 0);
            }
            if (grantCodeMster.fldlength!= 0)
            {
                insertCommand.Parameters.AddWithValue("@fldlength", grantCodeMster.fldlength);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@fldlength", 0);
            }
            if (grantCodeMster.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial", grantCodeMster.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial", 0);
            }
            if (grantCodeMster.DependOn!= 0)
            {
                insertCommand.Parameters.AddWithValue("@DependOn", grantCodeMster.DependOn);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@DependOn", 0);
            }


            if (grantCodeMster.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", grantCodeMster.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }

            if (grantCodeMster.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatementType", grantCodeMster.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatementType", 0);
            }

            if (grantCodeMster.UserID != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserID", grantCodeMster.UserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserID", 0);
            }
            insertCommand.Parameters.Add("@GrantIdout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@GrantIdout"].Direction = ParameterDirection.Output;

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
                    grantCodeMster.Status = count;
                }
                if (count ==1  && grantCodeMster.GrantId == 0)
                {
                    grantCodeMster.GrantId = System.Convert.ToInt32(insertCommand.Parameters["@GrantIdout"].Value);
                }
                return grantCodeMster;
            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return grantCodeMster;
            }
            finally
            {
                connection.Close();
            }



        }

        public DataSet GrantcodeMaster(Int64 CustId)
        {

            string selectProcedure = "[GetSelectGrantCodeMaster]";
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
        public List<dynamic> AddGrantBudget(GrantBudgeMster grantBudgeMster)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            GrantBudgetCodeMster grantBudgetCodeMster = new GrantBudgetCodeMster();

            switch (grantBudgeMster.value)
            {
                case 1:
                    {

                        GrantCodeMster grantCodeMster = new GrantCodeMster();
                        var Data = JsonConvert.DeserializeObject<List<GrantBudgetCodeMster>>(grantBudgeMster.GrantBudgeMsterlist);
                        for (int i = 0; i < Data.Count; i++)
                        {
                            grantBudgetCodeMster = Data[i];
                            if (grantBudgetCodeMster.Ischange)
                            {
                                if (!String.IsNullOrEmpty(grantBudgetCodeMster.GrantBudgetTitle))
                                {
                                    if (!string.IsNullOrEmpty(UserID))
                                    {
                                        grantCodeMster.UserID = Convert.ToInt64(UserID);
                                    }
                                    grantCodeMster.GrantId = grantBudgetCodeMster.GrantBudgetId;
                                    grantCodeMster.GrantTitle = grantBudgetCodeMster.GrantBudgetTitle;
                                    grantCodeMster.fldlength = grantBudgetCodeMster.fldlength;
                                    grantCodeMster.isRequired = grantBudgetCodeMster.isRequired;
                                    grantCodeMster.Serial = grantBudgetCodeMster.Serial;
                                    grantCodeMster.DependOn = grantBudgetCodeMster.DependOn;
                                    grantCodeMster.IsActive = grantBudgetCodeMster.IsActive;
                                    grantCodeMster.CustId = grantBudgeMster.CustId;

                                    if (grantBudgetCodeMster.GrantBudgetId == 0)
                                    {
                                        grantCodeMster.Type = 1;
                                        grantCodeMster.IsActive = true;
                                    }
                                    else
                                    {
                                        grantCodeMster.Type = 2;
                                    }
                                    if (grantBudgetCodeMster.IsDelete == true)
                                    {
                                        grantCodeMster.Type = 3;
                                        grantCodeMster.IsActive = false;
                                    }

                                    AddGrantCodeMaster(grantCodeMster);
                                    if (grantCodeMster.Status == -99)
                                    {
                                        break;
                                    }

                                }
                                else
                                {

                                }
                            }
                        }
                        objDynamic.Add(grantCodeMster.Status);
                        objDynamic.Add(GetGrantcodeMasterData(grantBudgeMster.CustId));
                        break;
                    }

                case 2:
                    {
                        BudgetCodeMasterDTO budgetCodeMasterDTO = new BudgetCodeMasterDTO();
                        try
                        {
                            var Data = JsonConvert.DeserializeObject<List<GrantBudgetCodeMster>>(grantBudgeMster.GrantBudgeMsterlist);

                            for (int j = 0; j < Data.Count; j++)
                            {
                                grantBudgetCodeMster = Data[j];
                                if (grantBudgetCodeMster.Ischange)
                                {
                                    if (!String.IsNullOrEmpty(grantBudgetCodeMster.GrantBudgetTitle))
                                    {

                                        if (!string.IsNullOrEmpty(UserID))
                                        {
                                            budgetCodeMasterDTO.UserID = Convert.ToInt64(UserID);
                                        }
                                        budgetCodeMasterDTO.BudgetId = grantBudgetCodeMster.GrantBudgetId;
                                        budgetCodeMasterDTO.BudgetTitle = grantBudgetCodeMster.GrantBudgetTitle;
                                        budgetCodeMasterDTO.FldLength = grantBudgetCodeMster.fldlength;
                                        budgetCodeMasterDTO.isRequired = grantBudgetCodeMster.isRequired;
                                        budgetCodeMasterDTO.Serial = grantBudgetCodeMster.Serial;
                                        budgetCodeMasterDTO.DependOn = grantBudgetCodeMster.DependOn;
                                        budgetCodeMasterDTO.IsActive = grantBudgetCodeMster.IsActive;
                                        budgetCodeMasterDTO.CustId = grantBudgeMster.CustId;

                                        if (grantBudgetCodeMster.GrantBudgetId == 0)
                                        {
                                            budgetCodeMasterDTO.Type = 1;
                                            budgetCodeMasterDTO.IsActive = true;
                                        }
                                        else
                                        {
                                            budgetCodeMasterDTO.Type = 2;
                                        }
                                        if (grantBudgetCodeMster.IsDelete == true)
                                        {
                                            budgetCodeMasterDTO.Type = 3;
                                            budgetCodeMasterDTO.IsActive = false;
                                        }

                                        AddBudgetCodeMaster(budgetCodeMasterDTO);
                                        if (budgetCodeMasterDTO.Status == -99)
                                        {
                                            break;
                                        }

                                    }
                                    else
                                    {

                                    }
                                }

                            }
                            objDynamic.Add(budgetCodeMasterDTO.Status);
                            objDynamic.Add(GetBudgetCodeData(grantBudgeMster.CustId));
                           
                        }
                        catch (Exception ex)
                        {

                        }
                        break;
                    }
            }


            
             

            

            return objDynamic;
        }

        private int AddBudgetCodeMaster(BudgetCodeMasterDTO budgetCodeMaster)
        {
            int BudgetId = 0;
            string insertProcedure = "[CreateNewBudgetCodeMaster]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;


            if (budgetCodeMaster.BudgetId != 0)
            {
                insertCommand.Parameters.AddWithValue("@BudgetId", budgetCodeMaster.BudgetId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetId", 0);
            }
            if (!string.IsNullOrEmpty(budgetCodeMaster.BudgetTitle))
            {
                insertCommand.Parameters.AddWithValue("@BudgetTitle", budgetCodeMaster.BudgetTitle);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BudgetTitle", DBNull.Value);
            }
            if (budgetCodeMaster.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", budgetCodeMaster.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (budgetCodeMaster.isRequired)
            {
                insertCommand.Parameters.AddWithValue("@isRequired", budgetCodeMaster.isRequired);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isRequired", 0);
            }
            if (budgetCodeMaster.FldLength != 0)
            {
                insertCommand.Parameters.AddWithValue("@FldLength", budgetCodeMaster.FldLength);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@FldLength", 0);
            }
            if (budgetCodeMaster.Serial != 0)
            {
                insertCommand.Parameters.AddWithValue("@Serial", budgetCodeMaster.Serial);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Serial", 0);
            }
            if (budgetCodeMaster.DependOn != 0)
            {
                insertCommand.Parameters.AddWithValue("@DependOn", budgetCodeMaster.DependOn);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@DependOn", 0);
            }
            if (budgetCodeMaster.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", budgetCodeMaster.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }

            if (budgetCodeMaster.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatementType", budgetCodeMaster.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatementType", 0);
            }

            if (budgetCodeMaster.UserID != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserID", budgetCodeMaster.UserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserID", 0);
            }
            insertCommand.Parameters.Add("@BudgetIdout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@BudgetIdout"].Direction = ParameterDirection.Output;

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
                    budgetCodeMaster.Status = count;
                }
                if (count == 1 && budgetCodeMaster.BudgetId == 0)
                {
                    budgetCodeMaster.BudgetId = System.Convert.ToInt32(insertCommand.Parameters["@BudgetIdout"].Value);
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

        public DataSet GetBudgetCode(Int64 CustId)
        {

            string selectProcedure = "[GetBudgetCodeMaster]";
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


        public List<dynamic> GetBudgetCodeData(Int64 CustId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetBudgetCode(CustId);

            var myEnumerableApr = ds.Tables[0].AsEnumerable();
            List<GrantBudgetCodeMster> BudgetCodeMasterDTO =
               (from item in myEnumerableApr
                select new GrantBudgetCodeMster
                {
                    GrantBudgetId = item.Field<Int64>("BudgetId"),
                    GrantBudgetTitle = item.Field<String>("BudgetTitle"),
                    CustId = item.Field<Int64>("CustId"),
                    isRequired = item.Field<bool>("isRequired"),
                    fldlength = item.Field<int>("FldLength"),
                    Serial = item.Field<int>("Serial"),
                    DependOn = item.Field<Int64>("DependOn"),
                    IsActive = item.Field<bool>("IsActive")


                }).ToList();
            objDynamic.Add(BudgetCodeMasterDTO);

            return objDynamic;
        }
        public List<dynamic> GetGrantcodeMasterData(Int64 CustId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GrantcodeMaster(CustId);
            try
            {
                var myEnumerableApr = ds.Tables[0].AsEnumerable();
                List<GrantBudgetCodeMster> GrantCodeMster =
                   (from item in myEnumerableApr
                    select new GrantBudgetCodeMster
                    {
                        GrantBudgetId = item.Field<Int64>("GrantId"),
                        GrantBudgetTitle = item.Field<String>("GrantTitle"),
                        CustId = item.Field<Int64>("CustId"),
                        isRequired = item.Field<bool>("isRequired"),
                        fldlength = item.Field<int>("fldlength"),
                        Serial = item.Field<int>("Serial"),
                        DependOn = item.Field<Int64>("DependOn"),
                        IsActive = item.Field<bool>("IsActive")


                    }).ToList();
                objDynamic.Add(GrantCodeMster);
            }
            catch(Exception ex)
            { }
           

            return objDynamic;
        }

    }
}