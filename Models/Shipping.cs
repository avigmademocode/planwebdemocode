using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{
    [Table("Shipping", Schema = "")]
    public class Shipping
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "ShippingId")]
        public Int64 ShippingId { get; set; }

        [StringLength(100)]
        [Display(Name = "ShippingDesc")]
        public String ShippingDesc { get; set; }

        [StringLength(100)]
        [Display(Name = "IsActive")]
        public Boolean IsActive { get; set; }

        [StringLength(100)]
        [Display(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [StringLength(100)]
        [Display(Name = "CreatedBy")]
        public Int64 CreatedBy { get; set; }

        [StringLength(100)]
        [Display(Name = "ModifiedOn")]
        public DateTime ModifiedOn { get; set; }

        [StringLength(100)]
        [Display(Name = "ModifiedBy")]
        public Int64 ModifiedBy { get; set; }

        [StringLength(100)]
        [Display(Name = "DeletedOn")]
        public DateTime DeletedOn { get; set; }

        [StringLength(100)]
        [Display(Name = "DeletedBy")]
        public Int64 DeletedBy { get; set; }
    }
}