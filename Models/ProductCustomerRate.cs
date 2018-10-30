using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{

    [Table("ProductCustomerRate", Schema = "")]
    public class ProductCustomerRate
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "CustId")]
        public Int64 CustId { get; set; }

        [Key]
        [Required]
        [Display(Name = "ProductId")]
        public Int64 ProductId { get; set; }

        [Key]
        [Required]
        [Display(Name = "Serial")]
        public Int64 Serial { get; set; }


        [StringLength(200)]
        [Display(Name = "Price")]
        public Int32 Price { get; set; }

        [StringLength(200)]
        [Display(Name = "ProdCatId")]
        public Int64 ProdCatId { get; set; }

        [StringLength(200)]
        [Display(Name = "IsActive")]
        public Boolean IsActive { get; set; }

        [StringLength(200)]
        [Display(Name = "EffectiveFrom")]
        public DateTime EffectiveFrom { get; set; }


        [StringLength(200)]
        [Display(Name = "EffectiveUpto")]
        public DateTime EffectiveUpto { get; set; }


        [StringLength(200)]
        [Display(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [StringLength(200)]
        [Display(Name = "CreatedBy")]
        public Int64 CreatedBy { get; set; }

        [StringLength(200)]
        [Display(Name = "DeletdOn")]
        public DateTime DeletdOn { get; set; }

        [StringLength(200)]
        [Display(Name = "DeletedBy")]
        public Int64 DeletedBy { get; set; }



    }
}