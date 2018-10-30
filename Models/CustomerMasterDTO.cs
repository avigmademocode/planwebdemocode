using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class CustomerMasterDTO
    { }
    public class CustomerMaster
    {
        public Int64 CustId { get; set; }
        public String CustName { get; set; }
        public String Acronym { get; set; }
        public int NoofBranches { get; set; }
        public int LevelofAuthority { get; set; }
        public string Code { get; set; }
        public String Ticker { get; set; }
        public Boolean InDemo { get; set; }
        public Boolean TieredPricing { get; set; }
        public String HOAdd1 { get; set; }
        public String HOAdd2 { get; set; }
        public String HOAdd3 { get; set; }
        public String HOCITY { get; set; }
        public String HOState { get; set; }
        public String HOCountry { get; set; }
        public String HOPin { get; set; }
        public String HOFullAddress { get; set;}
        public String HOEmailId { get; set; }
        public String HOContactPerson { get; set; }
        public Boolean isActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public DateTime DeletedOn { get; set; }
        public Int64 DeletedBy { get; set; }

        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public int Status { get; set; }


    }
}