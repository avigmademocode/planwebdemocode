using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class EmailDTO
    {
        public long CustId { get; set; }
        public long UserId { get; set; }
        public string MainBody { get; set; }
        public String From { get; set; }
        public String To { get; set; }
        public String Subject { get; set; }
        public String Message { get; set; }
        public String CC { get; set; }
        public String BCC { get; set; }
        public String Attachment { get; set; }
        public String Password { get; set; }
        public String Username { get; set; }
        public bool IsBodyHtml { get; set; }
    }
    public class EmailFormatDTO
    {
        public int Type { get; set; }
        public string Subject { get; set; }
        public string ReferenceNo { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedByEmail { get; set; }
        public string CreatedBy { get; set; }
        public string AddOffice { get; set; }
        public string strOrderID { get; set; }
        public string Link { get; set; }
        public string Message { get; set; }
        public String From { get; set; }
        public string To { get; set; }
        public string CC { get; set; }
        public String BCC { get; set; }
        public string Name { get; set; }
        public string Courier { get; set; }
        public string RequestNo { get; set; }
        public bool IsBodyHtml { get; set; }
        public Int64 Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int StatusEmail { get; set; }
		
		public long CustId { get; set; }
        public Int64 UserId { get; set; }
        public long OrderId { get; set; }
        public string MainBody { get; set; }
        public string EncOrderNo { get; set; }
        public string EncOrderApproverNo { get; set; }

    }
}