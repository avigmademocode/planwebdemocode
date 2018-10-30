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
    public class ContactUsData
    {
        SqlConnection connection = plansoni_webstoreData.GetConnection();
        Log log = new Log();
        private ContactUsDTO AddUpdateContactUs(ContactUsDTO contactUsDTO)
        {

            string insertProcedure = "[CreateContactUsData]";

            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;




            if (contactUsDTO.ContactId!= 0)
            {
                insertCommand.Parameters.AddWithValue("@ContactId", contactUsDTO.ContactId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ContactId", 0);

            }
            if (contactUsDTO.CustId != 0)
            {
                insertCommand.Parameters.AddWithValue("@CustId", contactUsDTO.CustId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CustId", 0);

            }
            if (contactUsDTO.UserId != 0)
            {
                insertCommand.Parameters.AddWithValue("@UserId", contactUsDTO.UserId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserId", 0);

            }
            if (contactUsDTO.OrderId != 0)
            {
                insertCommand.Parameters.AddWithValue("@OrderId", contactUsDTO.OrderId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@OrderId", 0);

            }
            if (!string.IsNullOrEmpty(contactUsDTO.AddressTo))
            {
                insertCommand.Parameters.AddWithValue("@AddressTo", contactUsDTO.AddressTo);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@AddressTo", 0);
            }
            if (!string.IsNullOrEmpty(contactUsDTO.Subject))
            {
                insertCommand.Parameters.AddWithValue("@Subject", contactUsDTO.Subject);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Subject", 0);
            }
            if (!string.IsNullOrEmpty(contactUsDTO.Contents))
            {
                insertCommand.Parameters.AddWithValue("@Contents", contactUsDTO.Contents);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Contents", 0);
            }

            if (contactUsDTO.Type != 0)
            {
                insertCommand.Parameters.AddWithValue("@Type", contactUsDTO.Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Type", 0);
            }
            insertCommand.Parameters.Add("@ContactIdOut", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ContactIdOut"].Direction = ParameterDirection.Output;

            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;

            try
            {
                int count = 0;
                connection.Open();
                insertCommand.ExecuteNonQuery();


                return contactUsDTO;


            }
            catch (Exception ex)
            {
                log.logErrorMessage("ContactUsData.AddUpdateContactUs");
                log.logErrorMessage(ex.StackTrace);
                return contactUsDTO;
            }
            finally
            {
                connection.Close();
            }

        }
        public int ContactInsert(EmailFormatDTO model)
        {
            ContactUsDTO contactUsDTO = new ContactUsDTO();


            contactUsDTO.CustId = model.CustId;
            contactUsDTO.UserId = model.UserId;
            contactUsDTO.OrderId = model.OrderId;
            contactUsDTO.AddressTo = model.To;
            contactUsDTO.Subject = model.Subject;
            contactUsDTO.Contents = model.Message;
            contactUsDTO.Type = model.Type;


            AddUpdateContactUs(contactUsDTO);
            return 0;
        }
    }
}