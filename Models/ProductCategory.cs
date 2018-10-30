using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace MyProject.Models
{
    [Table("ProductCategory", Schema = "")]
    public class ProductCategory
    {

        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "ProdCatId")]
        public Int64 ProdCatId { get; set; }

        [StringLength(200)]
        [Display(Name = "ProdCatDesc")]
        public String ProdCatDesc { get; set; }

        [StringLength(200)]
        [Display(Name = "IsActive")]
        public Boolean IsActive { get; set; }


        [StringLength(200)]
        [Display(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [StringLength(200)]
        [Display(Name = "CreatedBy")]
        public Int64 CreatedBy { get; set; }

        [StringLength(200)]
        [Display(Name = "ModifiedOn")]
        public DateTime ModifiedOn { get; set; }

        [StringLength(200)]
        [Display(Name = "ModifiedBy")]
        public Int64 ModifiedBy { get; set; }

        [StringLength(200)]
        [Display(Name = "DeletedOn")]
        public DateTime DeletedOn { get; set; }

        [StringLength(200)]
        [Display(Name = "DeletedBy")]
        public Int64 DeletedBy { get; set; }

    }
}