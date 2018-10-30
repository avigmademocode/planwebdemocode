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
    public class GetCarrier
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();

        private DataSet GetCarrierData()
        {

            string selectProcedure = "[GetCarrierType]";
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

        public List<dynamic> GetCarrierDetails()
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetCarrierData();
            try
            {
                List<CarrierDTO> CarrierDTO = null;
                if (ds.Tables[0].Rows.Count > 0)
                {

                    var myEnumerablerole = ds.Tables[0].AsEnumerable();
                    CarrierDTO =
                       (from item in myEnumerablerole
                        select new CarrierDTO
                        {
                            CarrierId = item.Field<Int64>("CarrierId"),
                            Carrier = item.Field<String>("Carrier"),

                        }).ToList();
                    objDynamic.Add(CarrierDTO);
                }


            }
            catch (Exception ex)
            {

            }

            return objDynamic;
        }

    }
}