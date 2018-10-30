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
    public class SupportingEmailData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        string CustomerID = HttpContext.Current.Session["CustId"].ToString();
        SecurityHelper securityHelper = new SecurityHelper();


        private SupportingEmail AddSupportingEmail(SupportingEmail supportingEmail)
        {

            string insertProcedure = "[CreateSupportingEmails]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;

            if (supportingEmail.SuppEmailId != 0)
            {
                insertCommand.Parameters.AddWithValue("@SuppEmailId", supportingEmail.SuppEmailId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@SuppEmailId", 0);
            }
            if (supportingEmail.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", supportingEmail.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (!string.IsNullOrEmpty(supportingEmail.email))
            {
                insertCommand.Parameters.AddWithValue("@email", supportingEmail.email);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@email", DBNull.Value);
            }
            if (supportingEmail.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@isActive", supportingEmail.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@isActive", 0);
            }

            if (supportingEmail.UserID != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserID", supportingEmail.UserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserID", 0);
            }

            if (supportingEmail.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatementType", supportingEmail.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatementType", 0);
            }
            insertCommand.Parameters.Add("@SuppEmailIdIntOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@SuppEmailIdIntOut"].Direction = ParameterDirection.Output;

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
                    supportingEmail.Status = count;

                }
                if (count == 1 && supportingEmail.SuppEmailId == 0)
                {
                    supportingEmail.SuppEmailId = System.Convert.ToInt32(insertCommand.Parameters["@SuppEmailIdIntOut"].Value);
                }
                return supportingEmail;
            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return supportingEmail;
            }
            finally
            {
                connection.Close();
            }



        }

        private DataSet GetSupportingEmail(Int64 CustId)
        {

            string selectProcedure = "[GetSupportingEmails]";
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

        public List<dynamic> GetSupportingEmailData(Int64 CustId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetSupportingEmail(CustId);
            try
            {
                var myEnumerableApr = ds.Tables[0].AsEnumerable();
                var myEnumerablecust = ds.Tables[1].AsEnumerable();
                List<SupportingEmail> SupportingEmail =
                   (from item in myEnumerableApr
                    select new SupportingEmail
                    {
                        SuppEmailId = item.Field<Int64>("SuppEmailId"),
                        email = item.Field<String>("email")



                    }).ToList();

                objDynamic.Add(SupportingEmail);



                List<CustEmail> CustEmail =
                  (from item in myEnumerablecust
                   select new CustEmail
                   {
                       SuppEmailId = item.Field<Int64>("SuppEmailId"),
                       CustId = item.Field<Int64>("CustId"),



                   }).ToList();
                objDynamic.Add(CustEmail);
            }
            catch (Exception ex)
            {
            }


            return objDynamic;
        }
        #region comment
        //public List<dynamic> AddUpdateSupportingEmailData(SupportingEmailDTO supportingEmailDTO)
        //{

        //    List<dynamic> objDynamic = new List<dynamic>();
        //    SupportingEmail SupportingEmail = new SupportingEmail();


        //    try
        //    {
        //        switch (supportingEmailDTO.Type)
        //        {
        //            case 1:
        //                {
        //                    var Data = JsonConvert.DeserializeObject<List<SupportingEmail>>(supportingEmailDTO.EmailData);
        //                    for (int i = 0; i < Data.Count; i++)
        //                    {
        //                        SupportingEmail = Data[i];
        //                        if (!string.IsNullOrEmpty(UserID))
        //                        {
        //                            SupportingEmail.UserID = Convert.ToInt64(UserID);
        //                        }
        //                        SupportingEmail.CustId = supportingEmailDTO.CustId;
        //                        if (SupportingEmail.Ischange == 1)
        //                        {
        //                            if (SupportingEmail.SuppEmailId == 0)
        //                            {
        //                                SupportingEmail.Type = 1;
        //                                SupportingEmail.IsActive = true;
        //                            }
        //                            else
        //                            {
        //                                SupportingEmail.Type = 2;
        //                                SupportingEmail.IsActive = true;

        //                            }
        //                            if (SupportingEmail.IsDelete)
        //                            {
        //                                SupportingEmail.Type = 3;
        //                                SupportingEmail.IsActive = false;
        //                            }

        //                            AddSupportingEmail(SupportingEmail);

        //                        }




        //                    }
        //                    break;
        //                }


        //        }

        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    objDynamic.Add(SupportingEmail.Status);
        //    objDynamic.Add(GetSupportingEmailData(supportingEmailDTO.CustId));
        //    return objDynamic;
        //}
        #endregion

        public List<dynamic> AddUpdateSupportingEmailData(SupportingEmailDTO supportingEmailDTO)
        {
            List<dynamic> objDynamic = new List<dynamic>();
           
           
            SupportingEmail supportingEmail = new SupportingEmail();
            CustEmail custEmail = new CustEmail();


            try
            {
                switch (supportingEmailDTO.Type)
                {
                    case 1:
                        {

                            var Data = JsonConvert.DeserializeObject<List<SupportingEmail>>(supportingEmailDTO.EmailData);
                            for (int i = 0; i < Data.Count; i++)
                            {
                                supportingEmail = Data[i];
                                if (supportingEmail.IsChange && supportingEmail.IsDelete)
                                {
                                    supportingEmail.IsActive = false;
                                    supportingEmail.Type = 3;
                                    if (!string.IsNullOrEmpty(UserID))
                                    {
                                        supportingEmail.UserID = Convert.ToInt64(UserID);
                                    }
                                    AddSupportingEmail(supportingEmail);
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            var Data = JsonConvert.DeserializeObject<List<CustEmail>>(supportingEmailDTO.Customerdet);
                            Int64 val = supportingEmailDTO.SuppEmailId;

                            for (int i = 0; i < Data.Count; i++)
                            {

                                custEmail = Data[i];
                                supportingEmail.CustId = custEmail.CustId;
                                supportingEmail.email = supportingEmailDTO.Email;
                                if (!string.IsNullOrEmpty(UserID))
                                {
                                    supportingEmail.UserID = Convert.ToInt64(UserID);
                                }
                                if (val != 0)
                                {
                                    supportingEmail.IsActive = custEmail.IsCat;
                                    supportingEmail.Type = 2;
                                    supportingEmail.SuppEmailId = val;

                                    AddSupportingEmail(supportingEmail);
                                    if (supportingEmail.Status != 1)
                                    {
                                        break;
                                    }

                                }
                                else
                                {
                                    supportingEmail.IsActive = custEmail.IsCat;
                                    supportingEmail.Type = 1;
                                    supportingEmail.SuppEmailId = val;

                                    AddSupportingEmail(supportingEmail);
                                    val = supportingEmail.SuppEmailId;
                                    if (supportingEmail.Status != 1)
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


           
               objDynamic.Add(supportingEmail.Status);
               objDynamic.Add(GetSupportingEmailData(supportingEmailDTO.CustId));
            return objDynamic;
        }
    }
    }