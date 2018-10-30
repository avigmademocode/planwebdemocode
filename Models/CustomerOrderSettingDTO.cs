using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class CustomerOrderSettingDTO
    {
    }

    public class CustomerOrderSetting
    {
        public Int64 COSId { get; set; }
        public Int64 CustId { get; set; }
        public Boolean ReferenceNoAuto { get; set; }
        public Boolean UseItemGroups { get; set; }
        public Boolean UseItemGroupSeparatedFreight { get; set; }
        public Boolean RequestNewProducts { get; set; }
        public Boolean Desgination { get; set; }
        public int Addresses { get; set; }
        public int Approver { get; set; }
        public int LevelofAuthority { get; set; }
        public int Type { get; set; }
    }


    public class CustomerSettings
    {
        public Int64 CustomerId { get; set; }
        public bool Reference { get; set; }
        public int Addresses { get; set; }
        public int Approver { get; set; }
        public bool UserItem { get; set; }
        public bool UserItemSp { get; set; }
        public bool RequestNew { get; set; }
        public bool Desgination { get; set; }
        public int No_Approver { get; set; }
    }
}