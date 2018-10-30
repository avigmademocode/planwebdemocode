using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class LanguagesDTO
    {
        public Int64 LanguageId { get; set; }
        public String LanguageName { get; set; }
        public bool IsActive { get; set; }
        public int Type { get; set; }
        public Int64 UserID { get; set; }
        public int Status { get; set; }

        public Boolean IsEdit { get; set; }
        public Boolean IsDelete { get; set; }
        public bool IsChange { get; set; }
        public Int64 CustId { get; set; }
        public String Customerdet { get; set; }
        public String Languagesdet { get; set; }
    }


    public class CustLanguages
    {
        public Int64 LanguageId { get; set; }
        public Int64 CustId { get; set; }
        public int Type { get; set; }
        public bool IsActive { get; set; }
        public bool IsCat { get; set; }
    }

     



    public class LanguagesDetail
    {
        public String Languagesdet { get; set; }
    }
}