using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class ContactUsDTO
    {
        public Int64 ContactId { get; set; }
        public Int64 CustId { get; set; }
        public Int64 UserId { get; set; }
        public Int64 OrderId { get; set; }
        public String AddressTo { get; set; }
        public String Subject { get; set; }
        public String Contents { get; set; }
        public int Type { get; set; }
       


    }
}