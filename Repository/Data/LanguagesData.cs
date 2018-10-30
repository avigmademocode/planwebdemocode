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
    public class LanguagesData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        


        private LanguagesDTO AddLanguages(LanguagesDTO languages)
        {
            
            string insertProcedure = "[CreateLanguages]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;


            if (languages.LanguageId != 0)
            {
                insertCommand.Parameters.AddWithValue("@LanguageId", languages.LanguageId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@LanguageId", 0);
            }

            if (!string.IsNullOrEmpty(languages.LanguageName))
            {
                insertCommand.Parameters.AddWithValue("@LanguageName", languages.LanguageName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@LanguagesName", DBNull.Value);
            }
            if (languages.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustID", languages.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustID", 0);
            }

            if (languages.UserID != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserID", languages.UserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserID", 0);
            }
            if (languages.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", languages.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }
            if (languages.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatementType", languages.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatementType", 0);
            }

            insertCommand.Parameters.Add("@LanguageIdout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@LanguageIdout"].Direction = ParameterDirection.Output;

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
                    languages.Status = count;
                }
                if (count != 0 && languages.LanguageId == 0)
                {
                    languages.LanguageId = System.Convert.ToInt32(insertCommand.Parameters["@LanguageIdout"].Value);
                }
                return languages;
            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return languages;
            }
            finally
            {
                connection.Close();
            }



        }
        private int UpdateLanguages(LanguagesDTO languages)
        {
            int LanguageId = 0;
            string updateProcedure = "[CreateLanguages]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            if (languages.LanguageId != 0)
            {
                updateCommand.Parameters.AddWithValue("@LanguageId", languages.LanguageId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@LanguageId", 0);
            }
            if (!string.IsNullOrEmpty(languages.LanguageName))
            {
                updateCommand.Parameters.AddWithValue("@LanguageName", languages.LanguageName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@LanguagesName", DBNull.Value);
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

                return LanguageId;
            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return LanguageId;
            }
            finally
            {
                connection.Close();
            }



        }

        private DataSet GetLanguages(Int64 LanguageId)
        {

            string selectProcedure = "[GetLanguages]";
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            DataSet ds = new DataSet();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@LanguageId",LanguageId);
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

        public List<dynamic> AddLanguagesData(LanguagesDTO languagesDetail)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            #region comment
            //var Data = JsonConvert.DeserializeObject<List<LanguagesDTO>>(languagesDetail.Languagesdet);
            //for (int i = 0; i < Data.Count; i++)
            //{
            //    languages = Data[i];
            //    if (!String.IsNullOrEmpty(languages.LanguageName))
            //    {
            //        if (languages.IsChange)
            //        {
            //            if (!string.IsNullOrEmpty(UserID))
            //            {
            //                languages.UserID = Convert.ToInt64(UserID);
            //            }
            //            if (languages.LanguageId != 0 )
            //            {
            //                languages.Type = 2;
            //            }

            //            if (languages.IsDelete == true)
            //            {
            //                languages.Type = 3;
            //                languages.IsActive = false;
            //            }

            //            AddLanguages(languages);
            //            if (languages.Status == -99)
            //            {
            //                objDynamic.Add(languages.Status);
            //                break;
            //            }
            //            else
            //            {
            //                objDynamic.Add(languages.Status);
            //            }
            //        }

            //    }

            // }

            #endregion
            CustLanguages custLanguages = new CustLanguages();
            LanguagesDTO languages = new LanguagesDTO();
           
            

            try
            {
                switch (languagesDetail.Type)
                {
                    case 1:
                        {
                           
                            var Data = JsonConvert.DeserializeObject<List<LanguagesDTO>>(languagesDetail.Languagesdet);
                            for (int i = 0; i < Data.Count; i++)
                            {
                                languages = Data[i];
                                if (languages.IsChange && languages.IsDelete)
                                {
                                    languages.IsActive = false;
                                    languages.Type = 3;
                                    if (!string.IsNullOrEmpty(UserID))
                                    {
                                        languages.UserID = Convert.ToInt64(UserID);
                                    }
                                    AddLanguages(languages);
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            var Data = JsonConvert.DeserializeObject<List<CustLanguages>>(languagesDetail.Customerdet);
                            Int64 val = languagesDetail.LanguageId;

                            for (int i = 0; i < Data.Count; i++)
                            {

                                custLanguages = Data[i];
                                languages.CustId = custLanguages.CustId;
                                languages.LanguageName = languagesDetail.LanguageName;
                                if (!string.IsNullOrEmpty(UserID))
                                {
                                    languages.UserID = Convert.ToInt64(UserID);
                                }
                                if (val != 0)
                                {
                                    languages.IsActive = custLanguages.IsCat;
                                    languages.Type = 2;
                                    languages.LanguageId = val;

                                    AddLanguages(languages);

                                }
                                else
                                {
                                    languages.IsActive = custLanguages.IsCat;
                                    languages.Type = 1;
                                    languages.LanguageId = val;

                                    AddLanguages(languages);
                                    val = languages.LanguageId;
                                    if (languages.Status != 1)
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


            //AddLanguages(languages);
            objDynamic.Add(languages.Status);
            objDynamic.Add(GetLanguagesData(0));
            return objDynamic;
        }


        public List<dynamic> GetLanguagesData(Int64 LanguageId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetLanguages(LanguageId);

            var myEnumerableApr = ds.Tables[0].AsEnumerable();
            var myEnumerablecustApr = ds.Tables[1].AsEnumerable();
            List<LanguagesDTO> LanguagesDTO =
               (from item in myEnumerableApr
                select new LanguagesDTO
                {
                    LanguageId = item.Field<Int64>("LanguageId"),
                    LanguageName = item.Field<String>("LanguageName"),
            


                }).ToList();
            objDynamic.Add(LanguagesDTO);

            List<CustLanguages> CustLanguages =
              (from item in myEnumerablecustApr
               select new CustLanguages
               {
                   LanguageId = item.Field<Int64>("LanguageId"),
                   CustId = item.Field<Int64>("CustId"),



               }).ToList();
            objDynamic.Add(CustLanguages);

            return objDynamic;
        }

    }
}