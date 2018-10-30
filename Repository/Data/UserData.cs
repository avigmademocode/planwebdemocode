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
    public class UserData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();
        private UserDataDTO AddUserData(UserDataDTO userDataDTO)
        {
            string insertProcedure = "[CreateUserMaster]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;

            int UserId = 0;


            if (userDataDTO.UserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@pkey_UserId", userDataDTO.UserId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@pkey_UserId", 0);
            }

            if (!string.IsNullOrEmpty(userDataDTO.LoginId))
            {
                insertCommand.Parameters.AddWithValue("@LoginId", userDataDTO.LoginId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@LoginId", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(userDataDTO.Pwd))
            {
                insertCommand.Parameters.AddWithValue("@Pwd", userDataDTO.Pwd);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Pwd", DBNull.Value);
            }
            if (!string.IsNullOrEmpty(userDataDTO.UserName))
            {
                insertCommand.Parameters.AddWithValue("@UserName", userDataDTO.UserName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserName", DBNull.Value);
            }

            if (userDataDTO.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", userDataDTO.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);
            }
            if (userDataDTO.BranchId != 0)
            {
                insertCommand.Parameters.AddWithValue("@BranchId", userDataDTO.BranchId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@BranchId", 0);
            }
            if (userDataDTO.IsPlansonUser)
            {
                insertCommand.Parameters.AddWithValue("@IsPlansonUser", userDataDTO.IsPlansonUser);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsPlansonUser", 0);
            }
            if (!string.IsNullOrEmpty(userDataDTO.FirstName))
            {
                insertCommand.Parameters.AddWithValue("@FirstName", userDataDTO.FirstName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@FirstName", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(userDataDTO.LastName))
            {
                insertCommand.Parameters.AddWithValue("@LastName", userDataDTO.LastName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@LastName", DBNull.Value);
            }
            if (userDataDTO.CityId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CityId", userDataDTO.CityId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CityId", 0);
            }
            if (userDataDTO.CountryId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CountryId", userDataDTO.CountryId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CountryId", 0);
            }

            if (!string.IsNullOrEmpty(userDataDTO.Logins))
            {
                insertCommand.Parameters.AddWithValue("@Logins", userDataDTO.Logins);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Logins", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(userDataDTO.Last_Login))
            {
                insertCommand.Parameters.AddWithValue("@Last_Login", userDataDTO.Last_Login);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Last_Login", DBNull.Value);
            }
            if (userDataDTO.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", userDataDTO.IsActive);

            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }
            if (userDataDTO.Locked)
            {
                insertCommand.Parameters.AddWithValue("@Locked", userDataDTO.Locked);

            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Locked", 0);
            }

            if (userDataDTO.UserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", userDataDTO.UserId);

            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
                if (userDataDTO.Type != 0)
                {
                    insertCommand.Parameters.AddWithValue("@Type", userDataDTO.Type);

                }
                else
                {
                    insertCommand.Parameters.AddWithValue("@Type", 0);
                }

                insertCommand.Parameters.Add("@pkey_UserIdOut", System.Data.SqlDbType.Int);
                insertCommand.Parameters["@pkey_UserIdOut"].Direction = ParameterDirection.Output;

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
                        userDataDTO.Status = count;
                    }
                    if (count == 1 && userDataDTO.UserId == 0)
                    {
                        userDataDTO.UserId = System.Convert.ToInt32(insertCommand.Parameters["@pkey_UserIdOut"].Value);
                    }
                    return userDataDTO;


                }
                catch (Exception ex)
                {
                    log.logErrorMessage("");
                    log.logErrorMessage(ex.StackTrace);
                    return userDataDTO;
                }
                finally
                {
                    connection.Close();
                }

            }

         public List<dynamic> AddUserDetailsData(UserDataDTO userDataDTO , int Type , UserDataDetails userDataDetails)
            {
                List<dynamic> objDynamic = new List<dynamic>();
             switch (Type)
            {
                case 1:
                    {
                        Int64 n;
                        bool isNumeric = Int64.TryParse(userDataDTO.strUserId, out n);

                        if (isNumeric)
                        {
                            userDataDTO.UserId = n;

                            AddUserData(userDataDTO);
                            objDynamic.Add(userDataDTO.Status);
                            objDynamic.Add(userDataDTO.UserId);
                            objDynamic.Add(securityHelper.Encrypt(userDataDTO.UserId.ToString(), false));
                            break;

                        }                       
                        else if(!string.IsNullOrEmpty(userDataDTO.strUserId))
                        {
                            userDataDTO.UserId = Convert.ToInt64(securityHelper.Decrypt(userDataDTO.strUserId, false));
                        }
                        AddUserData(userDataDTO);
                        objDynamic.Add(userDataDTO.Status);
                        objDynamic.Add(userDataDTO.UserId);
                        objDynamic.Add(securityHelper.Encrypt(userDataDTO.UserId.ToString(), false));
                        break;
                    }

                case 2:
                    {
                        UserDataDTO DataDTO = new UserDataDTO();
                        var Data = JsonConvert.DeserializeObject<List<UserDataDTO>>(userDataDetails.strUserData);
                        for (int i = 0; i < Data.Count; i++)
                        {
                            DataDTO = Data[i];

                            DataDTO.Type = 4;
                            AddUserData(DataDTO);
                        }
                        
                      
                        objDynamic.Add(DataDTO.Status);
                       
                        break;
                    }
            }

          
            return objDynamic;
            }

            private DataSet GetUserDataDetail(Int64 UserId)
            {

                string selectProcedure = "[GetUserMaster]";
                SqlDataAdapter da = new SqlDataAdapter(selectProcedure, connection);
                DataSet ds = new DataSet();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@userId", UserId);
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

            public List<dynamic> GetUserData(Int64 UserId)
            {
                List<dynamic> objDynamic = new List<dynamic>();
                DataSet ds = GetUserDataDetail(UserId);
                try
                {
                    var myEnumerableApr = ds.Tables[0].AsEnumerable();
                    List<UserDataDTO> userDataDTOs =
                       (from item in myEnumerableApr
                        select new UserDataDTO
                        {

                        //if (item.Field<Int64>("UserId") == DBNull.Value)
                        //{

                        //}
                        //else
                        //{
                        //}

                             UserId = item.Field<Int64>("UserId"),
                            strUserId = securityHelper.Encrypt(item.Field<Int64>("UserId").ToString(), false),
                            LoginId = item.Field<String>("LoginId"),
                            Pwd = item.Field<String>("Pwd"),
                        //confirmPwd = item.Field<String>("confirmPwd"),
                            UserName = item.Field<String>("UserName"),
                            CustId = item.Field<Int64>("CustId"),
                            CityName = item.Field<String>("CityName"),
                            BranchId = item.Field<Int64>("BranchId"),
                            IsPlansonUser = item.Field<Boolean>("IsPlansonUser"),
                            FirstName = item.Field<String>("FirstName"),
                            LastName = item.Field<String>("LastName"),
                            CityId = item.Field<Int64>("CityId"),
                            CountryId = item.Field<Int64>("CountryId"),
                            CountryName = item.Field<String>("CountryName"),
                            //Logins = item.Field<String>("Logins"),
                            Last_Login = item.Field<String>("Last_Login"),
                            Locked = item.Field<Boolean>("Locked"),
                            IsActive = item.Field<Boolean>("IsActive"),


                        }).ToList();
                    objDynamic.Add(userDataDTOs);
                List<CustBranches> custBranches = null;
                if (ds.Tables[1].Rows.Count > 0)
                {


                    var myEnumerablebr = ds.Tables[1].AsEnumerable();
                    custBranches =
                       (from item in myEnumerablebr
                        select new CustBranches
                        {
                            BranchId = item.Field<Int64>("BranchId"),
                            CustId = item.Field<Int64>("CustId"),
                            BrName = item.Field<String>("BrName"),
                            DisplayName = item.Field<String>("DisplayName"),

                        }).ToList();
                    objDynamic.Add(custBranches);
                }
                List<UserRolesDTO> UserRolesDTO = null;
                if (ds.Tables[2].Rows.Count > 0)
                {


                    var myEnumerablerole = ds.Tables[2].AsEnumerable();
                    UserRolesDTO =
                       (from item in myEnumerablerole
                        select new UserRolesDTO
                        {
                            RoleId = item.Field<Int64>("RoleId"),
                            RoleName = item.Field<String>("RoleName"),
                            Description = item.Field<String>("Description"),

                        }).ToList();
                    objDynamic.Add(UserRolesDTO);
                }
                List<UserRolesRelationDTO> UserRolesRelationDTO = null;
                if (ds.Tables[3].Rows.Count > 0)
                {


                    var myEnumerableuserr = ds.Tables[3].AsEnumerable();
                    UserRolesRelationDTO =
                       (from item in myEnumerableuserr
                        select new UserRolesRelationDTO
                        {
                            RoleId = item.Field<Int64>("RoleId"),
                            URRId = item.Field<Int64>("URRId"),
                            UserId = item.Field<Int64>("UserId"),

                        }).ToList();
                    objDynamic.Add(UserRolesRelationDTO);
                }

            }
                catch (Exception ex)
                {

                }

                return objDynamic;
            }





        }
    }
