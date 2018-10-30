using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class BudgetCodeMasterDTO
    {
        public Int64 BudgetId { get; set; }
        public String BudgetTitle { get; set; }
        public Int64 CustId { get; set; }
        public Boolean isRequired { get; set; }
        public int FldLength { get; set; }
        public int Serial { get; set; }
        public Int64 DependOn { get; set; }
        public bool IsActive { get; set; }
        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public int Status { get; set; }

    }
}