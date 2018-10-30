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
    public class IncoTermData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        private IncoTermDTO AddIncoTerm(IncoTermDTO incoTerm)
        {

            string insertProcedure = "[CreateIncoTerm]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;

            int IncoTermId = 0;


            if (incoTerm.IncoTermId != 0)
            {
                insertCommand.Parameters.AddWithValue("@IncoTermId", incoTerm.IncoTermId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IncoTermId", 0);
            }

            if (incoTerm.intCustID != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustID", incoTerm.intCustID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustID", 0);
            }
            if (!string.IsNullOrEmpty(incoTerm.IncoTermCode))
            {
                insertCommand.Parameters.AddWithValue("@IncoTermCode", incoTerm.IncoTermCode);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IncoTermCode", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(incoTerm.IncoTermDesc))
            {
                insertCommand.Parameters.AddWithValue("@IncoTermDesc", incoTerm.IncoTermDesc);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IncoTermDesc", DBNull.Value);
            }

            if (incoTerm.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@StatementType", incoTerm.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@StatementType", 0);
            }
            if (incoTerm.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", incoTerm.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }


            insertCommand.Parameters.Add("@IncoTermIdout", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@IncoTermIdout"].Direction = ParameterDirection.Output;

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
                    incoTerm.Status = count;
                }
                if (count == 1 && incoTerm.IncoTermId == 0)
                {
                    incoTerm.IncoTermId = System.Convert.ToInt32(insertCommand.Parameters["@IncoTermIdout"].Value);
                }
                return incoTerm;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return incoTerm;
            }
            finally
            {
                connection.Close();
            }



        }
        private int UpdateIncoTerm(IncoTermDTO incoTerm)
        {

            string updateProcedure = "[CreateIncoTerm]";

            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;

            int IncoTermId = 0;


            if (incoTerm.IncoTermId != 0)
            {
                updateCommand.Parameters.AddWithValue("@IncoTermId", incoTerm.IncoTermId);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@IncoTermId", 0);
            }
            if (!string.IsNullOrEmpty(incoTerm.IncoTermCode))
            {
                updateCommand.Parameters.AddWithValue("@IncoTermCode", incoTerm.IncoTermCode);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@IncoTermCode", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(incoTerm.IncoTermDesc))
            {
                updateCommand.Parameters.AddWithValue("@IncoTermDesc", incoTerm.IncoTermDesc);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@IncoTermDesc", DBNull.Value);
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

                return IncoTermId;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return IncoTermId;
            }
            finally
            {
                connection.Close();
            }



        }
        private DataSet GetIncoTermDetail(Int64 IncoTermId)
        {

            string selectProcedure = "[GetIncoTerm]";
            SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
            DataSet ds = new DataSet();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@CustID", IncoTermId);
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
        public List<dynamic> GetCustIncoTermDetail(Int64 IncoTermId)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetIncoTermDetail(IncoTermId);

            var myEnumerableApr = ds.Tables[0].AsEnumerable();
            List<IncoTermDTO> IncoTermDTO =
               (from item in myEnumerableApr
                select new IncoTermDTO
                {
                    IncoTermId = item.Field<Int64>("IncoTermId"),
                    IncoTermCode = item.Field<String>("IncoTermCode"),
                    IncoTermDesc = item.Field<String>("IncoTermDesc")

                }).ToList();
            objDynamic.Add(IncoTermDTO);

            var myEnumerable = ds.Tables[1].AsEnumerable();
            List<CustIncoTermDTO> CustIncoTermDTO =
               (from item in myEnumerable
                select new CustIncoTermDTO
                {
                    IncoTermId = item.Field<Int64>("IncoTermId"),
                    CustId = item.Field<Int64>("CustId"),


                }).ToList();
            objDynamic.Add(CustIncoTermDTO);


            return objDynamic;
        }
        public List<dynamic> AddCustIncoTerm(IncoTermDTO incoTermDTO)
        {

            List<dynamic> objDynamic = new List<dynamic>();
            CustIncoTermDTO custIncoTermDTO = new CustIncoTermDTO();
            IncoTermDTO incoTermDTOdata = new IncoTermDTO();
            switch (incoTermDTO.Type)
            {
                case 1:
                    {
                        var Data = JsonConvert.DeserializeObject<List<IncoTermDTO>>(incoTermDTO.IncoTermData);
                        for (int i = 0; i < Data.Count; i++)
                        {
                            incoTermDTOdata = Data[i];
                            if (incoTermDTOdata.IsChange && incoTermDTOdata.IsDelete)
                            {
                                incoTermDTOdata.IsActive = false;
                                incoTermDTOdata.Type = 3;
                                AddIncoTerm(incoTermDTOdata);
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        var Data = JsonConvert.DeserializeObject<List<CustIncoTermDTO>>(incoTermDTO.CustID);
                        Int64 val = incoTermDTO.IncoTermId;

                        for (int i = 0; i < Data.Count; i++)
                        {

                            custIncoTermDTO = Data[i];
                            incoTermDTO.intCustID = custIncoTermDTO.CustId;
                            if (val != 0)
                            {
                                incoTermDTO.IsActive = custIncoTermDTO.IsCat;
                                incoTermDTO.Type = 2;
                                incoTermDTO.IncoTermId = val;
                                AddIncoTerm(incoTermDTO);

                                if (incoTermDTO.Status != 1)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                incoTermDTO.IsActive = custIncoTermDTO.IsCat;
                                incoTermDTO.Type = 1;
                                AddIncoTerm(incoTermDTO);
                                val = incoTermDTO.IncoTermId;

                                if (incoTermDTO.Status != 1)
                                {
                                    break;
                                }

                            }






                        }
                        break;
                    }
            }

            
            objDynamic.Add(GetCustIncoTermDetail(0));
            objDynamic.Add(incoTermDTO.Status);
            return objDynamic;
        }
    }
}