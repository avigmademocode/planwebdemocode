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
    public class UserRolesData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        private UserRolesDTO AddUpdateUserRoles(UserRolesDTO userRolesDTO)
        {

            string insertProcedure = "[CreateUserRoles]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;




            if (userRolesDTO.RoleId != 0)
            {
                insertCommand.Parameters.AddWithValue("@RoleId", userRolesDTO.RoleId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@RoleId", 0);

            }

            if (!string.IsNullOrEmpty(userRolesDTO.RoleName))
            {
                insertCommand.Parameters.AddWithValue("@RoleName", userRolesDTO.RoleName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@RoleName", 0);
            }
            if (!string.IsNullOrEmpty(userRolesDTO.Description))
            {
                insertCommand.Parameters.AddWithValue("@Description", userRolesDTO.Description);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Description", 0);
            }
            if (userRolesDTO.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", userRolesDTO.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }
            if (userRolesDTO.UserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId ", userRolesDTO.UserId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }

            if (userRolesDTO.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", userRolesDTO.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }
            insertCommand.Parameters.Add("@RoleIdOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@RoleIdOut"].Direction = ParameterDirection.Output;

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
                    userRolesDTO.val = count;
                }
                if (count != 0 && userRolesDTO.RoleId == 0)
                {
                    userRolesDTO.RoleId = System.Convert.ToInt32(insertCommand.Parameters["@RoleIdOut"].Value);
                }

                return userRolesDTO;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("UserRolesData.AddUpdateUserRoles");
                log.logErrorMessage(ex.StackTrace);
                return userRolesDTO;
            }
            finally
            {
                connection.Close();
            }



        }

        private DataSet GetUserRoleDetail()
        {

            string selectProcedure = "[GetUserRoleMaster]";
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

        public List<dynamic> GetUseRolerData()
        {
            List<dynamic> objDynamic = new List<dynamic>();
            DataSet ds = GetUserRoleDetail();
            try
            {
                List<UserRolesDTO> UserRolesDTO = null;
                if (ds.Tables[0].Rows.Count > 0)
                {


                    var myEnumerablerole = ds.Tables[0].AsEnumerable();
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
             

            }
            catch (Exception ex)
            {

            }

            return objDynamic;
        }


        public List<dynamic> SaveUserRoleData(UserRoles  userRoles)
        {
            List<dynamic> objDynamic = new List<dynamic>();


            UserRolesDTO UserRolesDTO = new UserRolesDTO();



            try
            {
               
               
                var Data = JsonConvert.DeserializeObject<List<UserRolesDTO>>(userRoles.RoleDesc);
                for (int i = 0; i < Data.Count; i++)
                {
                    UserRolesDTO = Data[i];
                    if ( !String.IsNullOrEmpty(UserRolesDTO.RoleName))
                    {
                        if (UserRolesDTO.RoleId == 0)
                        {
                            UserRolesDTO.Type = 1;
                        }
                        else
                        {
                            UserRolesDTO.Type = 2;
                            UserRolesDTO.IsActive = true;
                        }

                        if (UserRolesDTO.Ischange == 1)
                        {
                            if (!string.IsNullOrEmpty(UserID))
                            {
                                UserRolesDTO.UserId = Convert.ToInt64(UserID);
                            }
                            if (UserRolesDTO.IsDelete == true)
                            {
                                UserRolesDTO.Type = 3;
                                UserRolesDTO.IsActive = false;
                            }

                            AddUpdateUserRoles(UserRolesDTO);
                            if (UserRolesDTO.val == -99)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        UserRolesDTO.val = -98;
                    }
                }
                objDynamic.Add(UserRolesDTO.val);
                objDynamic.Add(GetUseRolerData());

            }
            catch (Exception ex)
            {


            }


            return objDynamic;
        }

    }
}