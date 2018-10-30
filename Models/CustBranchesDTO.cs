using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class CustBranchesDTO
    {
    }
    public class CustBrnches
    {
        public Int64 BranchId { get; set; }
        public Int64 CustId { get; set; }
        public String BrName { get; set; }
        public String DisplayName { get; set; }
        public Boolean BIsHeadOffice { get; set; }
        public Boolean PreSetAddress { get; set; }
        public Boolean HideBillingAddress { get; set; }
        public String BrAdd1 { get; set; }
        public String BrAdd2 { get; set; }
        public String BrAdd3 { get; set; }
        public Int64 BrCity { get; set; }
        public String BrCityName { get; set; }
        public String BrState { get; set; }
        public Int64 BrCountry { get; set; }
        public String BrCountryName { get; set; }
        public String BrPin { get; set; }
        public String BrEmailId { get; set; }
        public String BrContactNo { get; set; }
        public String BrContactPerson { get; set; }
        public String BrFullAddress { get; set; }
        public String BlAdd1 { get; set; }
        public String BlAdd2 { get; set; }
        public String BlAdd3 { get; set; }
        public Int64 BlCity { get; set; }
        public String BICityName { get; set; }
        public String BlState { get; set; }
        public Int64 BlCountry { get; set; }
        public String BillCountryName { get; set; }
        public String BlPin { get; set; }
        public String BlEmailId { get; set; }
        public String BlContactNo { get; set; }
        public String BlContactPerson { get; set; }
        public Boolean Needs_Delivery_Term { get; set; }
        public Boolean Needs_Fee_Warning { get; set; }
        public String Fee_Warning { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 UserID { get; set; }
        public int Type { get; set; }
    }


    public class CustomerBranches
    {
        public Int64 BranchId { get; set; }
        public Int64 CustomerId { get; set; }
        public string BranchName { get; set; }
        public string DisplayName { get; set; }
        public bool HeadOffice { get; set; }
        public bool PreSetAddress { get; set; }
        public bool HideBillingAddress { get; set; }
        public string BrAdd1 { get; set; }
        public string BrAdd2 { get; set; }
        public string BrAdd3 { get; set; }
        public string BrCity { get; set; }
        public string BrCityName { get; set; }
        public string BrState { get; set; }
        public string BrCountry { get; set; }
        public string BrCountryName { get; set; }
        public string BrFullAddress { get; set; }
        public string Brpin { get; set; }
        public string BrConName { get; set; }
        public string BrContact { get; set; }
        public string BrEmail { get; set; }
        public string BIAdd1 { get; set; }
        public string BIAdd2 { get; set; }
        public string BIAdd3 { get; set; }
        public string BICity { get; set; }
        public string BICityName { get; set; }
        public string BlState { get; set; }
        public string BlCountry { get; set; }
        public string BillCountryName { get; set; }
        public string BIpin { get; set; }
        public string BIConName { get; set; }
        public string BIContact { get; set; }
        public string BIEmail { get; set; }
        public bool NeedDelivery { get; set; }
        public bool NeedWarning { get; set; }
        public string FeeWarning { get; set; }
        public int Type { get; set; }
        public Nullable <Int64> intBrCity { get; set; }
        public Nullable<Int64> intBrCountry { get; set; }
        public Nullable<Int64> intBlCity { get; set; }
        public Nullable<Int64> intBlCountry { get; set; }
    }
}