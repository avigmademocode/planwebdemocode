using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class ProductCategoryDTO
    {
       

        public String ProdCatDesc { get; set; }
            public Boolean IsActive { get; set; }
       
            public int Type { get; set; }
        
    }

    public class ProductCatgry
    {

        public Int64 ProdCatId { get; set; }
        public Int64 CustID { get; set; }
        public String ProdCatDesc { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 UserId { get; set; }
        public int Type { get; set; }
        public Boolean IsEdit { get; set; }
        public Boolean IsDelete { get; set; }
        public int Ischange { get; set; }
        public int count { get; set; }
        public Boolean IsCat { get; set; }

    }
    public class ProductCategoryDetail
    {
        public String Productcategorydet { get; set; }
        public String CustID { get; set; }
        public string Type { get; set; }
        public String CatID { get; set; }

    }

    public class ProdcatCust
    {
        public int CustId { get; set; }
        public string CustName { get; set; }
        public bool IsCat { get; set; }

    }



}