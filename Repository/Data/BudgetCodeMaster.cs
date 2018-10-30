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
    public class BudgetCodeMaster
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();


      
        private int UpdateBudgetCodeMaster(BudgetCodeMasterDTO budgetCodeMaster)
        {
            int BudgetId = 0;
            string updateProcedure = "[CreateNewBudgetCodeMaster]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            if (budgetCodeMaster.BudgetId != 0)
            {
                updateCommand.Parameters.AddWithValue("@BudgetId", budgetCodeMaster.BudgetId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetId", 0);
            }
            if (!string.IsNullOrEmpty(budgetCodeMaster.BudgetTitle))
            {
                updateCommand.Parameters.AddWithValue("@BudgetTitle", budgetCodeMaster.BudgetTitle);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@BudgetTitle", DBNull.Value);
            }
            if (budgetCodeMaster.CustId != 0)
            {
                updateCommand.Parameters.AddWithValue("@CustId", budgetCodeMaster.CustId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (budgetCodeMaster.isRequired)
            {
                updateCommand.Parameters.AddWithValue("@isRequired", budgetCodeMaster.isRequired);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@isRequired", 0);
            }
            if (budgetCodeMaster.FldLength != 0)
            {
                updateCommand.Parameters.AddWithValue("@FldLength", budgetCodeMaster.FldLength);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@FldLength", 0);
            }
            if (budgetCodeMaster.Serial != 0)
            {
                updateCommand.Parameters.AddWithValue("@Serial ", budgetCodeMaster.Serial);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Serial ", 0);
            }
            if (budgetCodeMaster.DependOn != 0)
            {
                updateCommand.Parameters.AddWithValue("@DependOn ", budgetCodeMaster.DependOn);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@DependOn ", 0);
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