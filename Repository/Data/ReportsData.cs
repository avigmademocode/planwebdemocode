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
using MyProject.Repository.Security;
using System.Web;

namespace MyProject.Repository.Data
{
    public class ReportsData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        SecurityHelper SecurityHelper = new SecurityHelper();

        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        string IsPlansonUser = HttpContext.Current.Session["IsPlansonUser"].ToString();
        private DataSet GetReports(string strwherecondition)
        {

            string selectProcedure = "[GetCustOrderReport]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            da.SelectCommand.Parameters.AddWithValue("@whereparam", strwherecondition);

            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("OrderApprovalData.GetOrderApp");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }

        public List<dynamic> GetReportData(ReportUI model)
        {
            var StartDate = model.DateStart.ToString("yyyy-MM-dd");
            var EndDate = model.DateEnd.ToString("yyyy-MM-dd");

            List<dynamic> objDynamic = new List<dynamic>();
            try
            {
                string wherecondition = string.Empty, strCustomerID = string.Empty;
                String endday = string.Empty;
                #region comm
                //switch (Convert.ToInt16(model.Endmonth))
                //{
                //    case 1:
                //        {
                //            endday = "31";
                //            break;
                //        }
                //    case 2:
                //        {
                //            endday = "28";
                //            break;
                //        }
                //    case 3:
                //        {
                //            endday = "31";
                //            break;
                //        }
                //    case 4:
                //        {
                //            endday = "30";
                //            break;
                //        }
                //    case 5:
                //        {
                //            endday = "31";
                //            break;
                //        }
                //    case 6:
                //        {
                //            endday = "30";
                //            break;
                //        }
                //    case 7:
                //        {
                //            endday = "31";
                //            break;
                //        }
                //    case 8:
                //        {
                //            endday = "31";
                //            break;
                //        }
                //    case 9:
                //        {
                //            endday = "30";
                //            break;
                //        }
                //    case 10:
                //        {
                //            endday = "31";
                //            break;
                //        }
                //    case 11:
                //        {
                //            endday = "30";
                //            break;
                //        }
                //    case 12:
                //        {
                //            endday = "31";
                //            break;
                //        }
                //}
                #endregion
                if (!string.IsNullOrEmpty(model.CustomerID))
                {
                    wherecondition = "AND ord.CustId =  " + model.CustomerID;
                    if (!string.IsNullOrEmpty(model.StatusID))
                    {
                        wherecondition = wherecondition + "AND  ord.StatusId = ' " + model.StatusID + " '";
                    }

                    wherecondition = wherecondition + "  AND ord.CreatedOn  between '" + StartDate +"'";

                    wherecondition = wherecondition + " AND '" + EndDate + "'";

                }


                DataSet ds = GetReports(wherecondition);

                var myEnumerablecat = ds.Tables[0].AsEnumerable();
                List<ReportsDTO> ReportsDTO =
                    (from item in myEnumerablecat
                     select new ReportsDTO
                     {
                         Request = item.Field<int>("Request"),
                         CountryName = item.Field<string>("CountryName"),
                         TotalOrderAmount = (item.Field<decimal>("TotalOrderAmount")).ToString(), 
                     }).ToList();
                objDynamic.Add(ReportsDTO);

                var myEnumerablezz = ds.Tables[1].AsEnumerable();
                List<ReportsDTOO> ReportsDTOO =
                    (from item in myEnumerablezz
                     select new ReportsDTOO
                     {
                         OrderId = item.Field<Int64>("OrderId"),
                         ReferenceNo = item.Field<string>("ReferenceNo"),
                         Department = item.Field<string>("Department"),
                         CountryName = item.Field<string>("CountryName"),

                         SalesOrderNo = item.Field<string>("SalesOrderNo"),
                         Requesters_Name = item.Field<string>("Requesters_Name"),
                         Requesters_Email = item.Field<string>("Requesters_Email"),
                         BillingCotactEmail = item.Field<string>("BillingCotactEmail"),
                         StatusName = item.Field<string>("StatusName"),

                         CreatedOn = (item.Field<DateTime?>("CreatedOn")).ToString(),
                         LeadTime = (item.Field<int?>("LeadTime")).ToString(),
                         Est_Ship_Date = (item.Field<DateTime?>("Est_Ship_Date")).ToString(),
                         ShipmentDate = (item.Field<DateTime?>("ShipmentDate")).ToString(),
                         FOB = (item.Field<decimal?>("FOB")).ToString(),
                         TaxValue = (item.Field<decimal?>("TaxValue")).ToString(),
                         Feight = (item.Field<decimal?>("Feight")).ToString(),
                         TotalOrderAmount = (item.Field<decimal?>("TotalOrderAmount")).ToString(),

                         Approvedby = item.Field<string>("Approvedby"),
                         AuthorizedOn = (item.Field<DateTime?>("AuthorizedOn")).ToString(),


                     }).ToList();
                objDynamic.Add(ReportsDTOO);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("Reports.SreportsGet");
                log.logErrorMessage(ex.StackTrace);
            }

            return objDynamic;
        }



    }
}