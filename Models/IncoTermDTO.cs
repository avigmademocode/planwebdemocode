using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class IncoTermDTO
    {
        public Int64 IncoTermId { get; set; }
        public String IncoTermCode { get; set; }
        public String IncoTermDesc { get; set; }
        public int Type { get; set; }
        public String CustID { get; set; }
        public Int64 intCustID { get; set; }
        public bool IsActive { get; set; }
        public String IncoTermData { get; set; }
        public bool IsChange { get; set; }
        public bool IsDelete { get; set; }
        public int Status { get; set; }
    }


    public class CustIncoTermDTO
    {
        public Int64 IncoTermId { get; set; }
        public Int64 CustId { get; set; }
        public bool IsCat { get; set; }


    }
    public class CustIncoTerm
    {
        public Int64 IncoTermId { get; set; }
        public String IncoTermCode { get; set; }
        public String IncoTermDesc { get; set; }
    }

}