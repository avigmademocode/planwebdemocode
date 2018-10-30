using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class ManufacturerDTO
    {
        
        public String ManufacturerDesc { get; set; }
        public Boolean IsActive { get; set; }
        public int Type { get; set; }
    }
    public class Manufacturer
    {
        public Int64 ManufacturerId { get; set; }
        public String ManufacturerDesc { get; set; }
        public Boolean IsActive { get; set; }
        public Int64 UserId { get; set; }
        public int Type { get; set; }
        public Boolean IsEdit { get; set; }
        public Boolean IsDelete { get; set; }
        public int Ischange { get; set; }
        public int val { get; set; }

    }

    public class ManufacturerDetail
    {
        public String Manufacturerdet { get; set; }
    }

}