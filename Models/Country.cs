using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{
    [Table("Country", Schema = "")]
    public class Country
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "CountryId")]
        public Int64 CountryId { get; set; }

        [StringLength(100)]
        [Display(Name = "CountryName")]
        public String CountryName { get; set; }

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