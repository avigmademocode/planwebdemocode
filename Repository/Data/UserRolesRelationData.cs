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
    public class UserRolesRelationData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        string UserID = HttpContext.Current.Session["UserId"].ToString();
        Log log = new Log();

        private UserRolesRelationDTO AddUpdateUserRolesRelation(UserRolesRelationDTO userRolesRelationDTO)
        {

            string insertProcedure = "[CreateUserRolesRelation]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (userRolesRelationDTO.URRId != 0)
            {
                insertCommand.Parameters.AddWithValue("@URRId", userRolesRelationDTO.URRId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@URRId", 0);

            }
            if (userRolesRelationDTO.UserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId ", userRolesRelationDTO.UserId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);
            }
            if (userRolesRelationDTO.RoleId != 0)
            {
                insertCommand.Parameters.AddWithValue("@RoleId", userRolesRelationDTO.RoleId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@RoleId", 0);

            }
            if (userRolesRelationDTO.CurUserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CurUserId ", userRolesRelationDTO.CurUserId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CurUserId", 0);
            }

            
            if (userRolesRelationDTO.IsActive)
            {
                insertCommand.Parameters.AddWithValue("@IsActive", userRolesRelationDTO.IsActive);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@IsActive", 0);
            }

            if (userRolesRelationDTO.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", userRolesRelationDTO.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }
           
            insertCommand.Parameters.Add("@URRIdOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@URRIdOut"].Direction = ParameterDirection.Output;

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
                    userRolesRelationDTO.Status = count;
                }
                if (count != 0 && userRolesRelationDTO.URRId == 0)
                {
                    userRolesRelationDTO.URRId = System.Convert.ToInt32(insertCommand.Parameters["@URRIdOut"].Value);
                }





                return userRolesRelationDTO;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("UserRolesRelationData.AddUpdateUserRolesRelation");
                log.logErrorMessage(ex.StackTrace);
                return userRolesRelationDTO;
            }
            finally
            {
                connection.Close();
            }



        }


        public List<dynamic> AddUserRoleRelation(UserRolesRelation UserRolesRelation)
        {
            List<dynamic> objDynamic = new List<dynamic>();

            UserRolesRelationDTO UserRolesRelationDTO = new UserRolesRelationDTO();
            try
            {
                switch (UserRolesRelation.Type)
                {
                     
                    case 2:
                        {
                            var Data = JsonConvert.DeserializeObject<List<UserRolesRelationDTO>>(UserRolesRelation.UserRoledet);
                           

                            for (int i = 0; i < Data.Count; i++)
                            {

                                UserRolesRelationDTO.Type = 1;// Data[i].Type;
                                UserRolesRelationDTO.UserId = UserRolesRelation.UserId; //Data[i].UserId;
                                UserRolesRelationDTO.CurUserId = Convert.ToInt64(UserID); 
                                UserRolesRelationDTO.RoleId = Data[i].RoleId;
                                UserRolesRelationDTO.IsCat= Data[i].IsCat;
                                UserRolesRelationDTO.IsActive = Data[i].IsCat;
                                UserRolesRelationDTO.URRId = Data[i].URRId;
                               
                                AddUpdateUserRolesRelation(UserRolesRelationDTO);

                            }
                            break;
                        }
                }

            }
            catch (Exception ex)
            {


            }


            objDynamic.Add(UserRolesRelationDTO);
            return objDynamic;
        }
    }
}