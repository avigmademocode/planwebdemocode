using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{
    [Table("Status", Schema = "")]
    public class Status
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "StatusId")]
        public Int64 StatusId { get; set; }

        [StringLength(100)]
        [Display(Name = "StatusName")]
        public String StatusName { get; set; }

        [StringLength(100)]
        [Display(Name = "IsActive")]
        public Boolean IsActive { get; set; }

        [StringLength(100)]
        [Display(Name = "AltName")]
        public String AltName { get; set; }

        [StringLength(100)]
        [Display(Name = "UserAction")]
        public Int32 UserAction { get; set; }

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