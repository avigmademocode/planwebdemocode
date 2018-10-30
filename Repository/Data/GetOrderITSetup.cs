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
    public class GetOrderITSetup
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();




        private DataSet GetGetITSetupSPcallData(Int64 OrderId)
        {
            string selectProcedure = "[GetOrderITsetup]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@OrderId", OrderId);
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("BudgetCodeWiseOrderDetailData.GetCustBudgetData");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }


        public List<dynamic> GetITSetup(string strOrderID)
        {

            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetGetITSetupSPcallData(Convert.ToInt64(strOrderID));

            var myEnumerableApr = ds.Tables[0].AsEnumerable();

            List<OrderItsetupDTO> OrderItsetupDTO =
               (from item in myEnumerableApr
                select new OrderItsetupDTO
                {
                    ITSetUpId = item.Field<Int64>("ITSetUpId"),
                    OrderId = item.Field<Int64>("OrderId"),
                    Serial = item.Field<int>("Serial"),
                    UserName = item.Field<String>("UserName"),
                    UserTypeId = item.Field<Int64>("UserTypeId"),
                    WorkLocationId = item.Field<Int64>("WorkLocationId"),
                    DeliveryLocation = item.Field<String>("DeliveryLocation"),
                    Department = item.Field<String>("Department"),
                    Applications = item.Field<String>("Applications"),
                    SpecialInstructions = item.Field<String>("SpecialInstructions"),
                    MigrateInfo = item.Field<int>("MigrateInfo"),
                    UserType = item.Field<String>("UserType"),
                    WorkLocation = item.Field<String>("WorkLocation"),
                    PartNo = item.Field<String>("PartNo"),
                    ProductId = item.Field<Int64>("ProductId"),
                }).ToList();
            objDynamic.Add(OrderItsetupDTO);



            return objDynamic;
        }







    }
}