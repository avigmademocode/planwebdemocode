using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class SupportingEmailDTO
    {
        public String EmailData { get; set; }
        public String Customerdet { get; set; }
        public Int64 CustId { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public Int64 SuppEmailId { get; set; }
        public String Email { get; set; }
        public bool IsActive { get; set; }

    }

    public class SupportingEmail
    {
        public Int64 SuppEmailId { get; set; }
        public String email { get; set; }
        public Boolean IsActive { get; set; }

        public Int64 CustId { get; set; }
        public int Type { get; set; }

        public Int64 UserID { get; set; }
        public int Status { get; set; }
        public bool IsChange { get; set; }
        public bool IsDelete { get; set; }
        public bool IsEdit { get; set; }

    }



    public class CustEmail
    { 
        public Int64 SuppEmailId { get; set; }
        public Int64 CustId { get; set; }
        public int Type { get; set; }
        public bool IsActive { get; set; }
        public bool IsCat { get; set; }
    }


    public class OrderSupportingEmail
    {
        public Int64 SuppEmailId { get; set; }
        public String email { get; set; }
        public Boolean IsActive { get; set; }
    }
    }