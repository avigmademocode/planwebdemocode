using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class ProductMaster
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public Int64 ProductId { get; set; }
        [StringLength(100)]
        [Display(Name = "Model")]
        public string Model { get; set; }
        [StringLength(50)]
        [Display(Name = "Part No")]
        public string Partno { get; set; }
        [Display(Name = "Manufacturer")]
        public Int64? manufacturerId { get; set; }

        [Display(Name = "Product Type")]
        public Int64? ProductTypeId { get; set; }
        [DefaultValue(typeof(Boolean), "true")]
        [Display(Name = "Active")]
        public Boolean IsActive { get; set; }

        public virtual ProductTypeList ProdTypelist { get; set; }
        public virtual ManufacturerList Manufactlist { get; set; }
    }

    public class ProductMasterDetail
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Product Id")]
        public Int64 ProductId { get; set; }
        [StringLength(100)]
        [Display(Name = "Model")]
        public string Model { get; set; }
        [StringLength(50)]
        [Display(Name = "Part No")]
        public string Partno { get; set; }
        [Display(Name = "Manufacturer")]
        public string manufacturerdesc { get; set; }

        [Display(Name = "Product Type")]
        public string ProductType { get; set; }
        [DefaultValue(typeof(Boolean), "true")]
        [Display(Name = "Active")]
        public Boolean IsActive { get; set; }

  
    }

     
}