using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class StatusDTO
    { }
    public class StatusInfo
    {
        public Int64 StatusId { get; set; }
        public String StatusName { get; set; }
        public String AltName { get; set; }
        public Byte UserAction { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public String CustID { get; set; }
        public Int64 intCustID { get; set; }
        public bool IsActive { get; set; }
        public String StatusData { get; set; }
        public bool IsChange { get; set; }
        public bool IsDelete { get; set; }
        public bool ShowStatus { get; set; }
        public Int64 UserID { get; set; }
    }

    public class GetCustomerStatus
    {
        public Int64 CustId { get; set; }
        public Int64 StatusId { get; set; }
        public int Type { get; set; }
        public int StatusOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsCat { get; set; }
        public bool IsDelete { get; set; }
        public bool ShowStatus { get; set; }
    }
    
    public class GetCustomerStatusData
    {
        public String CustStatusData { get; set; }
        public Int64 CustId { get; set; }
        public int Type { get; set; }
        public String StatusName { get; set; }
        public String AltName { get; set; }
        public Byte UserAction { get; set; }
        public int Status { get; set; }
        public bool ShowStatus { get; set; }
    }

    public class CustomerStatus
    {
        public Int64 CustStatusId { get; set; }
        public Int64 CustId { get; set; }
        public Int64 StatusId { get; set; }
        public int StatusOrder { get; set; }
        public Int64 UserId { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDelete { get; set; }
        public String StatusName { get; set; }
        public int StatementType { get; set; }
        public int Status { get; set; }
    }
}