using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{
    [Table("IncoTerm", Schema = "")]

    public class IncoTerm
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "IncoTermId")]
        public Int64 IncoTermId { get; set; }

        [Display(Name = "IncoTermCode")]
        public String IncoTermCode { get; set; }

        [Display(Name = "IncoTermDesc")]
        public String IncoTermDesc { get; set; }

        [Display(Name = "IsActive")]
        public Boolean IsActive { get; set; }

        [Display(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "CreatedBy")]
        public Int64 CreatedBy { get; set; }

        [Display(Name = "ModifiedOn")]
        public DateTime ModifiedOn { get; set; }

        [Display(Name = "ModifiedBy")]
        public Int64 ModifiedBy { get; set; }

        [Display(Name = "DeletedOn")]
        public DateTime DeletedOn { get; set; }

        [Display(Name = "DeletedBy")]
        public Int64 DeletedBy { get; set; }


    }
}