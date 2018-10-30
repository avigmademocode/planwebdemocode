using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class ReportsDTO
    {
        /// get Country wise
        public string TotalOrderAmount { get; set; }
        public int Request { get; set; }
        public string CountryName { get; set; }
    }
    public class ReportUI
    {
        public string CustomerID { get; set; }
        public string Startmonth { get; set; }
        public string Endmonth { get; set; }
        public string Startyear { get; set; }
        public string Endyear { get; set; }
        public string StatusID { get; set; }
		
	    public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
		
    }
    public class ReportsDTOO
    {
        public Int64 OrderId { get; set; }
        public string ReferenceNo { get; set; }
        public string Department { get; set; }
        public string CountryName { get; set; }
        public string SalesOrderNo { get; set; }
        public string Requesters_Name { get; set; }
        public string Requesters_Email { get; set; }
        public string BillingCotactEmail { get; set; }
        public string StatusName { get; set; }
        public string CreatedOn { get; set; }
        public string LeadTime { get; set; }
        public string Est_Ship_Date { get; set; }
        public string ShipmentDate { get; set; }
        public string FOB { get; set; }
        public string TaxValue { get; set; }
        public string Feight { get; set; }
        public string TotalOrderAmount { get; set; }
        public string Approvedby { get; set; }
        public string AuthorizedOn { get; set; }
    }
}