using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Configuration;
using MyProject.Models;
using System.Text;
using System.IO;

namespace MyProject.Repository.Library
{
    public class EmailManager
    {
   
        SmtpClient smtpClient = new SmtpClient();
        MailMessage message = new MailMessage();
        
        Log log = new Log();

        public void AddAttachment(System.IO.Stream memorystream, string filename, string mediatype)
        {
            message.Attachments.Add(new System.Net.Mail.Attachment(memorystream, filename, mediatype));
        }


        //public string GetIGTTemplate(string message)
        //{


        //    string strtemplate = "<table cellspacing= 0 cellpadding= 0 width= 580 border= 0>" +
        //                           "<tbody><tr><td bgcolor= #006699 ><p>  <font color=#FFFFFF>" +
        //                           "<strong>AppConnect CRM System</strong></font></p></td> " +
        //                           "<td bgcolor= #006699><p align= right><font  color= #FFFFFF size= +1 style= font-family:'Times New Roman', Times, serif> " + System.DateTime.Now.ToString("dd-MMM-yyyy") + "</font>" +
        //                           "</p></td></tr><tr height=26><td bgcolor= #e0f1fe colspan= 2 height= 35><p><font  color= #0000CD size= +1 style= font-family:'Times New Roman', Times, serif> Escalation   Alert </font>" +
        //                           "</p></td> </tr><tr bgcolor=#CCCCCC><td colspan= 2><p><font  color= #000000 style= font-size:16px ; font-family:Arial ;font-variant:normal>Alert Details</font></p>" +
        //                           "<p>-----------------------------------------------------------</p></td>" +
        //                           "</tr><tr><td colspan=2 bgcolor=#CCCCCC><p><font  color= #000000  style= font-size:16px ; font-family:Arial ;font-variant:normal >This is a system generated email for CRN No :$CrnNo</font></p>" +
        //                           "<p><font  color= #000000  style=font-size:15px ; font-family:Arial ;font-variant:normal >Reference</font><font  color= #000000  style=font-size:15px ; font-family:Arial ;font-variant:normal > Number &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$Refno</font></p>" +
        //                           "<p><font  color= #000000  style=font-size:15px ; font-family:Arial ;font-variant:normal >Date/Time &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + "</font></td></p> </tr><tr><tr bgcolor=#CCCCCC>" +
        //                           "<p><td colspan= 2><p><font  color= #000000 style=font-size:15px ; font-family:Arial ;font-variant:normal> Please Click on the Link To Access The Complaint</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>" +
        //                           "<p><a href=$email>$email</a></p></td></tr><tr bgcolor=#CCCCCC><td colspan= 2><p><font  color= #000000 size= +0 style= font-style:italic> Please do not respond to this email</font></p></td></tr><tr><td bgcolor= #006699><p>&nbsp;</p></td><td bgcolor= #006699><p align=right><strong>&nbsp;</p></td></tr></tbody></table>";


        //    StringBuilder template = new StringBuilder(strtemplate);
        //    //string[] splitmessage = message.Split(Convert.ToChar("#"));
        //    //template.Replace("$CrnNo", splitmessage[0]).Replace("$email", emaillink).Replace("$Refno", splitmessage[1]);
        //    string strfinaltemplate = template.ToString();
        //    return strfinaltemplate;
        //}


        public int SendEmail(EmailDTO emailDTO )
        {
            try
            {
                smtpClient.Host = ConfigurationManager.AppSettings["smtpServer"].ToString();
                smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
                smtpClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                string fromuserid = ConfigurationManager.AppSettings["smtpUser"].ToString();
                string frompasswd = ConfigurationManager.AppSettings["smtpPass"].ToString();
                emailDTO.From = fromuserid;

                message.From = new MailAddress(emailDTO.From);
                message.To.Add(emailDTO.To);
                message.Subject = emailDTO.Subject;
                //message.IsBodyHtml = emailDTO.IsBodyHtml;
                message.IsBodyHtml = true;
                message.Body = emailDTO.Message + "<br/> <br/>  " + emailDTO.MainBody;
                //smtpClient.Credentials = new NetworkCredential("pritiarora@interactcrm.com", "opa5678");
                smtpClient.Credentials = new NetworkCredential(fromuserid, frompasswd);
                smtpClient.Send(message);

                return 1;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("");
                log.logErrorMessage(ex.StackTrace);
                return 0;
            }
        }

    }
}