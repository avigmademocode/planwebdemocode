using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class GrantCodeMster
    {
        public Int64 GrantId { get; set; }
        public String GrantTitle { get; set; }
        public Int64 CustId { get; set; }
        public Boolean isRequired { get; set; }
        public int fldlength { get; set; }
        public int Serial { get; set; }
        public Int64 DependOn { get; set; }

        public bool IsActive { get; set; }
        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public int Status { get; set; }

    }

    public class GrantBudgeMster
    {
        public String GrantBudgeMsterlist { get; set; }
        public int value { get; set; }
        public Int64 CustId { get; set; }

    }

    public class GrantBudgetCodeMster
    {
        public Int64 GrantBudgetId { get; set; }
        public String GrantBudgetTitle { get; set; }
        public Int64 CustId { get; set; }
        public int Serial { get; set; }
        public Int64 DependOn { get; set; }
        public Boolean isRequired { get; set; }
        public int fldlength { get; set; }
        public bool IsActive { get; set; }
        public bool Ischange { get; set; }

        public bool IsDelete { get; set; }
        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public int Status { get; set; }

    }
}