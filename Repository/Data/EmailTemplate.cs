using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net;
using System.Configuration;
using MyProject.Models;
using System.Net.Mail;
using MyProject.Repository.Library;

namespace MyProject.Repository.Data
{
    public class EmailTemplate
    {
        SmtpClient smtpClient = new SmtpClient();
        MailMessage message = new MailMessage();
        EmailManager EmailManager = new EmailManager();
        EmailDTO emailDTO = new EmailDTO();
        ContactUsData contactUsData = new ContactUsData();
        Log log = new Log();

        //Freight Quoted for Req. CUS03569
        public EmailFormatDTO GetFreightQuoted(EmailFormatDTO model)
        {

            string strval = System.Configuration.ConfigurationManager.AppSettings["EmailLink"];
            // model.ReferenceNo = "CUS03569";
            model.Link = strval + "?redirect=request%2Ffinalize%3Fid%3D4092";


            StringBuilder sb = new StringBuilder();

            string strtemplate = "<p>Freight charges have been quoted for your request <b> " + model.ReferenceNo + " </b> . The final step is to assign budget codes to your request. </p> <br/><br/>";
            sb.Append(strtemplate);

            string link = "<a href='" + model.Link + "' target ='_blank'> Click here to assign budget codes </a>  <br/><br/>";
            sb.Append(link);

            string thanks = "<p>Many thanks, </p>  <br/><br/><p> Your Planson Team  </p>";
            sb.Append(thanks);


            string strfinaltemplate = sb.ToString();
            model.Subject = "Request created Ref. No. " + model.ReferenceNo + " with request details";
            model.Message = strfinaltemplate;


            emailDTO.Subject = model.Subject;
            emailDTO.Message = model.Message;
            emailDTO.To = model.To;
            emailDTO.CC = model.CC;
            emailDTO.BCC = model.BCC;
            emailDTO.From = model.From;
            emailDTO.IsBodyHtml = model.IsBodyHtml;
            emailDTO.MainBody = strfinaltemplate;


            int val = EmailManager.SendEmail(emailDTO);

               contactUsData.ContactInsert(model); //for table datastore

            model.StatusEmail = val;
            return model;
        }


        //Raphael Lokeny Requests Your Approval for Req. CUS03569 - Planson Partner Portal
        public EmailFormatDTO RequestsApproval(EmailFormatDTO model)
        {
            
                 string strval = System.Configuration.ConfigurationManager.AppSettings["ApproveEmailLink"];
            //  model.Link = "https://www.plansonintl.com/portal/review/answer1?id=4092&key=2f94967c8b58e50e9e18663f294d95c7";
            model.Link = strval + "RequestOrder?Oval="+model.EncOrderNo+"&AppId="+model.EncOrderApproverNo;
            StringBuilder sb = new StringBuilder();

            string strtemplate = "<p>You are listed as the approving user for a request made on the Planson Partner Portal. Please use the following link to review the request and submit your  </p> <br/><br/>";
            sb.Append(strtemplate);

            string link = "<p> decision:<a href='" + model.Link + "' target ='_blank'> " + (model.Link) + " </a>  </p> <br/><br/>";
            sb.Append(link);

            string thanks = "<p>Thank you, </p>  <br/><br/><p> Your Planson Team  </p>";
            sb.Append(thanks);


            string strfinaltemplate = sb.ToString();
            model.Message = strfinaltemplate;
            model.Subject = "Request pending for Approval for  Ref. No. " + model.ReferenceNo + " with request details";

            emailDTO.Subject = model.Subject;
            emailDTO.Message = model.Message;
            emailDTO.To = model.To;
            emailDTO.CC = model.CC;
            emailDTO.BCC = model.BCC;
            emailDTO.From = model.From;
            emailDTO.IsBodyHtml = model.IsBodyHtml;
            emailDTO.MainBody = strfinaltemplate;


            int val = EmailManager.SendEmail(emailDTO);
            contactUsData.ContactInsert(model); //for table datastore

            model.StatusEmail = val;
            return model;
        }

        //Your Request CUS03561 was Approved - Planson Customer Portal
        public EmailFormatDTO RequestApproved(EmailFormatDTO model)
        {
            //model.ReferenceNo = "CUS03561";
            //model.Link = "https://www.plansonintl.com/portal/request/view?id=4084";
            //model.ApprovedBy = "accountspayable@mercycorps.org";


            StringBuilder sb = new StringBuilder();

            string strtemplate = "<p>Your request with reference <b> " + model.ReferenceNo + " </b> has been approved by the Financial Reviewer and is now being processed as an order. No further action is required at this time. You can view your order at any time by clicking <a href='" + model.Link + "' target ='_blank'>here</a>   </p> <br/><br/>";
            sb.Append(strtemplate);

            string link = "Approved by: <a href='mailto:" + model.ApprovedBy + "' target ='_blank'> accountspayable@mercycorps.org </a>  <br/><br/>";
            sb.Append(link);

            string order = "<p>Thank you for this order </p>";
            sb.Append(order);

            string thanks = "<p>Regards, </p>  <br/><br/><p> Your Planson Team  </p>";
            sb.Append(thanks);


            string strfinaltemplate = sb.ToString();
            model.Message = strfinaltemplate;
            model.Subject = "Request  Approved for  Ref. No. " + model.ReferenceNo + " with request details";
            emailDTO.Subject = model.Subject;
            emailDTO.Message = model.Message;
            emailDTO.To = model.To;
            emailDTO.CC = model.CC;
            emailDTO.BCC = model.BCC;
            emailDTO.From = model.From;
            emailDTO.IsBodyHtml = model.IsBodyHtml;
            emailDTO.MainBody = strfinaltemplate;



            int val = EmailManager.SendEmail(emailDTO);
            contactUsData.ContactInsert(model); //for table datastore

            model.StatusEmail = val;
            return model;
        }


        public EmailFormatDTO RequestDenied(EmailFormatDTO model)
        {
            //model.ReferenceNo = "CUS03561";
            //model.Link = "https://www.plansonintl.com/portal/request/view?id=4084";
            //model.ApprovedBy = "accountspayable@mercycorps.org";


            StringBuilder sb = new StringBuilder();

            string strtemplate = "<p>Your request with reference <b> " + model.ReferenceNo + " </b> has been Denied by the Financial Reviewer and is now being processed as an order. No further action is required at this time. You can view your order at any time by clicking <a href='" + model.Link + "' target ='_blank'>here</a>   </p> <br/><br/>";
            sb.Append(strtemplate);

            string link = "Denied by: <a href='mailto:" + model.ApprovedBy + "' target ='_blank'> accountspayable@mercycorps.org </a>  <br/><br/>";
            sb.Append(link);

            string order = "<p>Thank you for this order </p>";
            sb.Append(order);

            string thanks = "<p>Regards, </p>  <br/><br/><p> Your Planson Team  </p>";
            sb.Append(thanks);


            string strfinaltemplate = sb.ToString();
            model.Message = strfinaltemplate;
            model.Subject = "Request  Denied for  Ref. No. " + model.ReferenceNo + " with request details";
            emailDTO.Subject = model.Subject;
            emailDTO.Message = model.Message;
            emailDTO.To = model.To;
            emailDTO.CC = model.CC;
            emailDTO.BCC = model.BCC;
            emailDTO.From = model.From;
            emailDTO.IsBodyHtml = model.IsBodyHtml;
            emailDTO.MainBody = strfinaltemplate;



            int val = EmailManager.SendEmail(emailDTO);
            contactUsData.ContactInsert(model); //for table datastore
            model.StatusEmail = val;
            return model;
        }
        //New Order (Req. CUS03561) - Planson Customer Portal
        public EmailFormatDTO NewOrder(EmailFormatDTO model)
        {
            //  model.ReferenceNo = "CUS03561";
            //model.Link = "https://www.plansonintl.com/portal/request/view?id=4084";
            //model.AddOffice = "Cambridge, United States";
            //model.CreatedBy = "sphinizy@mercycorps.org";
            //model.ApprovedBy = "accountspayable@mercycorps.org";

            StringBuilder sb = new StringBuilder();

            string strtemplate = "<p>A new Planson Customer Portal Request has been approved to become an order. </p> <br/><br/>";
            sb.Append(strtemplate);



            string link = "<p> Reference: : <a href='" + model.Link + "' target ='_blank'> <b> " + model.ReferenceNo + " </b> </a>  <br/><br/> </p>";
            sb.Append(link);

            string office = "<p> Office: " + model.AddOffice + " </p>";
            sb.Append(office);

            string CreatedBy = "<p> Created By: " + model.CreatedBy + " </p>";
            sb.Append(CreatedBy);

            string ApprovedBy = "<p>  Approved By: " + model.ApprovedBy + " </p>";

            string notice = "<p  style='align-items:center' >Notice Of Shipment </p>  <br/><br/>";
            sb.Append(notice);

            string thanks = "<p>Regards, </p> <p> Your Planson Team  </p>";
            sb.Append(thanks);

            model.Subject = "Request created Ref. No.  "+ model.ReferenceNo +"  with request details";
            string strfinaltemplate = sb.ToString();

            model.Message = strfinaltemplate;

            emailDTO.Subject = model.Subject;
            emailDTO.Message = model.Message;
            emailDTO.To = model.To;
            emailDTO.CC = model.CC;
            emailDTO.BCC = model.BCC;
            emailDTO.From = model.From;
            emailDTO.IsBodyHtml = model.IsBodyHtml;
            emailDTO.MainBody = strfinaltemplate;


            int val = EmailManager.SendEmail(emailDTO);
            contactUsData.ContactInsert(model); //for table datastore

            model.StatusEmail = val;
            return model;
        }




        //Your Order # SLS23914 / CUS03543 Has Been Delivered - Please Submit GRN
        public EmailFormatDTO Delivered(EmailFormatDTO model)
        {
            //model.OrderNo = "SLS23914 / CUS03543";
            //model.Link = "https://www.plansonintl.com/portal/request/view?id=4066";
            //model.Name = "Daniel Thomas";
            //model.CreatedBy = "sphinizy@mercycorps.org";
            //model.ApprovedBy = "accountspayable@mercycorps.org";

            StringBuilder sb = new StringBuilder();

            string ord = "<p> Order #   " + model.ReferenceNo + " </p>";
            sb.Append(ord);

            string Nam = "<p> Dear  " + model.Name + " </p>";
            sb.Append(Nam);

            string start = "<p>I hope this mail finds you well.</p>";
            sb.Append(start);

            string strtemplate = "<p>Your order has been delivered. Please confirm that all goods have been received and are complete and correct. Then submit your GRN by clicking  <a href='" + model.Link + "' target ='_blank'> <b> here </b> </a> </p> <br/><br/>";
            sb.Append(strtemplate);



            string link = "<p> Reference: : <a href='" + model.Link + "' target ='_blank'> <b> " + model.ReferenceNo + " </b> </a>  <br/><br/> </p>";
            sb.Append(link);

            string questions = "<p> Please contact us if you have any questions. </p>";
            sb.Append(questions);


            string thanks = "<p>Thank you. </p>";
            sb.Append(thanks);


            string strfinaltemplate = sb.ToString();
            model.Message = strfinaltemplate;

            emailDTO.Subject = model.Subject;
            emailDTO.Message = model.Message;
            emailDTO.To = model.To;
            emailDTO.CC = model.CC;
            emailDTO.BCC = model.BCC;
            emailDTO.From = model.From;
            emailDTO.IsBodyHtml = model.IsBodyHtml;
            emailDTO.MainBody = strfinaltemplate;



            int val = EmailManager.SendEmail(emailDTO);
            contactUsData.ContactInsert(model); //for table datastore

            model.StatusEmail = val;
            return model;

        }




        //Please Submit Your Invoice to appropriate Accounts Payable For Payment
        public EmailFormatDTO InvoiceAppropriate(EmailFormatDTO model)
        {
            //model.OrderNo = "SLS23882 / CUS03525";
            //model.Link = "https://www.plansonintl.com/portal/request/view?id=4066";
            //model.Name = "Daniel Thomas";
            //model.CreatedBy = "sphinizy@mercycorps.org";
            //model.ApprovedBy = "accountspayable@mercycorps.org";
            //model.Courier = "Fedex";


            StringBuilder sb = new StringBuilder();

            string ord = "<p> Order #   " + model.ReferenceNo + " </p>";
            sb.Append(ord);


            string Nam = "<p> Dear  " + model.Name + " </p>";
            sb.Append(Nam);

            string start = "<p>I hope this mail finds you well.</p>";
            sb.Append(start);

            string strtemplate = "<p>According to <b> " + model.Courier + " </b> , your order has been fully delivered. Kindly proceed with the next steps in completing this order.  </p> <br/><br/>";
            sb.Append(strtemplate);


            string questions = "<p> Please contact us if you have any questions. </p>";
            sb.Append(questions);


            string thanks = "<p>Thank you. </p>";
            sb.Append(thanks);


            string strfinaltemplate = sb.ToString();
            model.Message = strfinaltemplate;


            emailDTO.Subject = model.Subject;
            emailDTO.Message = model.Message;
            emailDTO.To = model.To;
            emailDTO.CC = model.CC;
            emailDTO.BCC = model.BCC;
            emailDTO.From = model.From;
            emailDTO.IsBodyHtml = model.IsBodyHtml;
            emailDTO.MainBody = strfinaltemplate;



            int val = EmailManager.SendEmail(emailDTO);
            contactUsData.ContactInsert(model); //for table datastore

            model.StatusEmail = val;
            return model;
        }

        //Freight Quote Required (Req. CUS03550) - Planson Customer Portal
        public EmailFormatDTO QuoteRequired(EmailFormatDTO model)
        {
            //model.RequestNo = "CUS03550";
            //model.Link = "https://www.plansonintl.com/portal/admin/freight?id=4073";

            StringBuilder sb = new StringBuilder();


            string strtemplate = "<p>Planson Customer Portal Request <a href='" + model.Link + "' target ='_blank'> <b> " + model.RequestNo + " </b> </a> requires a freight quote. </p> <br/><br/>";
            sb.Append(strtemplate);


            string questions = "<p><b> IMPORTANT:</b> Be sure to include insurance charges in your quoted freight rate. </p>";
            sb.Append(questions);


            string thanks = "<p>Thank you. </p>";
            sb.Append(thanks);

            model.Subject = "Freight Quoted for Request No. " + model.RequestNo  + " and in details it will give out details of Request";
            string strfinaltemplate = sb.ToString();
            model.Message = strfinaltemplate;

            emailDTO.Subject = model.Subject;
            emailDTO.Message = model.Message;
            emailDTO.To = model.To;
            emailDTO.CC = model.CC;
            emailDTO.BCC = model.BCC;
            emailDTO.From = model.From;
            emailDTO.IsBodyHtml = model.IsBodyHtml;
            emailDTO.MainBody = strfinaltemplate;



            int val = EmailManager.SendEmail(emailDTO);
            contactUsData.ContactInsert(model); //for table datastore

            ///contactus datarepo call here
            model.StatusEmail = val;
            return model;
        }

        public EmailFormatDTO ContactInfo(EmailFormatDTO model)
        {
             

            emailDTO.Subject = model.Subject;
            emailDTO.Message = model.Message;
            emailDTO.To = model.To;
            emailDTO.CC = model.CC;
            emailDTO.BCC = model.BCC;
            emailDTO.From = model.From;
            emailDTO.IsBodyHtml = model.IsBodyHtml;
            emailDTO.MainBody = model.Message; 



            int val = EmailManager.SendEmail(emailDTO);
            contactUsData.ContactInsert(model); //for table datastore

            ///contactus datarepo call here
            model.StatusEmail = val;
            return model;
        }

        //Freight Quote Required (Req. CUS03550) - Planson Customer Portal
        public EmailFormatDTO ReviewingOrder(EmailFormatDTO model)
        {
            //model.RequestNo = "CUS03550";
            //model.Link = "https://www.plansonintl.com/portal/admin/freight?id=4073";

            StringBuilder sb = new StringBuilder();


            string strtemplate = "<p>Planson Customer Portal Request <a href='" + model.Link + "' target ='_blank'> <b> " + model.RequestNo + " </b> </a> requires a Review Order. </p> <br/><br/>";
            sb.Append(strtemplate);


            string questions = "<p><b> IMPORTANT:</b> Be sure to include insurance charges in your quoted freight rate. </p>";
            sb.Append(questions);


            string thanks = "<p>Thank you. </p>";
            sb.Append(thanks);

            model.Subject = "Review Order for Request No. " + model.RequestNo + " and in details it will give out details of Request";
            string strfinaltemplate = sb.ToString();
            model.Message = strfinaltemplate;

            emailDTO.Subject = model.Subject;
            emailDTO.Message = model.Message;
            emailDTO.To = model.To;
            emailDTO.CC = model.CC;
            emailDTO.BCC = model.BCC;
            emailDTO.From = model.From;
            emailDTO.IsBodyHtml = model.IsBodyHtml;
            emailDTO.MainBody = strfinaltemplate;



            int val = EmailManager.SendEmail(emailDTO);
            contactUsData.ContactInsert(model); //for table datastore

            ///contactus datarepo call here
            model.StatusEmail = val;
            return model;
        }


        //Freight Quote Required (Req. CUS03550) - Planson Customer Portal
        public EmailFormatDTO ShipOrderOrder(EmailFormatDTO model)
        {
            //model.RequestNo = "CUS03550";
            //model.Link = "https://www.plansonintl.com/portal/admin/freight?id=4073";

            StringBuilder sb = new StringBuilder();


            string strtemplate = "<p>Planson Customer Portal Request <a href='" + model.Link + "' target ='_blank'> <b> " + model.RequestNo + " </b> </a> requires a Review Order. </p> <br/><br/>";
            sb.Append(strtemplate);


            string questions = "<p><b> IMPORTANT:</b> Be sure to include insurance charges in your quoted freight rate. </p>";
            sb.Append(questions);


            string thanks = "<p>Thank you. </p>";
            sb.Append(thanks);

            model.Subject = "Ship Order for Request No. " + model.RequestNo + " and in details it will give out details of Request";
            string strfinaltemplate = sb.ToString();
            model.Message = strfinaltemplate;

            emailDTO.Subject = model.Subject;
            emailDTO.Message = model.Message;
            emailDTO.To = model.To;
            emailDTO.CC = model.CC;
            emailDTO.BCC = model.BCC;
            emailDTO.From = model.From;
            emailDTO.IsBodyHtml = model.IsBodyHtml;
            emailDTO.MainBody = strfinaltemplate;



            int val = EmailManager.SendEmail(emailDTO);
            contactUsData.ContactInsert(model); //for table datastore

            ///contactus datarepo call here
            model.StatusEmail = val;
            return model;
        }
    }
}