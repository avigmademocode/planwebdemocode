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
    public class ManufacturerData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();

      

        private Manufacturer AddManufacture(Manufacturer manufacturer)
        {
            int ManufacturerId = 0;
            string insertProcedure = "[CreateManufacturer]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (manufacturer.ManufacturerId != 0)
            {
                insertCommand.Parameters.AddWithValue("@ManufacturerId", manufacturer.ManufacturerId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ManufacturerId", 0);
            }
            if (!string.IsNullOrEmpty(manufacturer.ManufacturerDesc))
            {
                insertCommand.Parameters.AddWithValue("@ManufacturerDesc", manufacturer.ManufacturerDesc);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ManufacturerDesc", DBNull.Value);
            }
            if (manufacturer.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", manufacturer.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }
            if (manufacturer.UserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", manufacturer.UserId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (manufacturer.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", manufacturer.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }



            insertCommand.Parameters.Add("@ManufacturerIdOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ManufacturerIdOut"].Direction = ParameterDirection.Output;

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
                    manufacturer.val = count;
                }
                if (insertCommand.Parameters["@ManufacturerIdOut"].Value != DBNull.Value)
                {
                    ManufacturerId = System.Convert.ToInt32(insertCommand.Parameters["@ManufacturerIdOut"].Value);
                    manufacturer.ManufacturerId = ManufacturerId;
                }



                return manufacturer;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("ManufacturerData.AddManufacture");
                log.logErrorMessage(ex.StackTrace);
                return manufacturer;
            }
            finally
            {
                connection.Close();
            }



        }
        public List<dynamic> AddManufacture(ManufacturerDetail  manufacturerDetail)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            Manufacturer manf = new Manufacturer();
            var Data = JsonConvert.DeserializeObject<List<Manufacturer>>(manufacturerDetail.Manufacturerdet);
            for (int i = 0; i < Data.Count; i++)
            {
                manf = Data[i];
                if (!String.IsNullOrEmpty(manf.ManufacturerDesc))
                {


                    if (manf.Ischange == 1)
                    {
                        if (!string.IsNullOrEmpty(UserID))
                        {
                            manf.UserId = Convert.ToInt64(UserID);
                        }
                        if (manf.IsDelete == true)
                        {
                            manf.Type = 3;
                            manf.IsActive = false;
                        }

                        AddManufacture(manf);
                        if (manf.val == -99)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    manf.val = -98;
                }
            }

            DataSet ds = GetManufactureDetail();
            var myEnumerable = ds.Tables[0].AsEnumerable();

            List<Manufacturer> manufacture =
                (from item in myEnumerable
                 select new Manufacturer
                 {
                     ManufacturerId = item.Field<Int64>("ManufacturerId"),
                     ManufacturerDesc = item.Field<String>("ManufacturerDesc"),
                     IsActive = item.Field<Boolean>("IsActive"),
                     Type = 2


                 }).ToList();
            objDynamic.Add(manufacture);
            objDynamic.Add(manf.val);

            return objDynamic;
        }
        private DataSet GetManufactureDetail()
        {

            string selectProcedure = "[GetManufacturer]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                log.logErrorMessage("ManufacturerData.GetManufacture");
                log.logErrorMessage(ex.StackTrace);
                return ds;
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }
        public List<dynamic> GetManufacturer()
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetManufactureDetail();
            var myEnumerable = ds.Tables[0].AsEnumerable();

            List<Manufacturer> manufacture =
                (from item in myEnumerable
                 select new Manufacturer
                 {
                     ManufacturerId = item.Field<Int64>("ManufacturerId"),
                     ManufacturerDesc = item.Field<String>("ManufacturerDesc"),
                     IsActive = item.Field<Boolean>("IsActive"),
                     Type = 2
                     

                 }).ToList();
            objDynamic.Add(manufacture);

            return objDynamic;
        }

       
    }
}