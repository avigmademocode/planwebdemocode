using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MyProject.Models
{
    public class OrderFreightDTO
    {
    }

    public class CustFreightTitle
    {
        public Int64 FreightTitleId { get; set; }
        public String FreightTitle { get; set; }
        public bool isRequired { get; set; }
        public int fldlength { get; set; }
        public int Serial { get; set; }
        public String fldType { get; set; }
        public bool isFreightAmount { get; set; }
        public bool isLeadTime { get; set; }
        public bool isTaxApplicable { get; set; }
        public string Data { get; set; }

    }

        public class FreightDetails
        {
        public String FreightData { get; set; }
        public string TotalAmount { get; set; }
        public string strOrderID { get; set; }

    }

    public class FreightTotal
    {
        public Decimal TaxAmount { get; set; }
        public Decimal TotalAmount { get; set; }
        public Decimal Freight { get; set; }

    }
}