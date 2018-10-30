using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class ApproverSettingDTO
    {
        public Int64 CustId { get; set; }
        public Int64 CustApproverId { get; set; }
        public String ApproverNameDisplay { get; set; }
        public int ApproverSerial { get; set; }
        public bool IsActive { get; set; }
        public int LevelofAuthority { get; set; }
        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public int Status { get; set; }
        public int Ischange { get; set; }
        public bool IsDelete { get; set; }
        public bool IsEdit { get; set; }

    }


    public class ApproverSetting
    {
        public String ApproverData { get; set; }

        public Int64 CustId { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
    }
}