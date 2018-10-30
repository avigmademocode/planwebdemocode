using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{
    [Table("grants", Schema="")]
    public class grants
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Id")]
        public Int32 Id { get; set; }

        [Required]
        [Display(Name = "Customer Id")]
        public Int32 CustomerId { get; set; }

        [StringLength(50)]
        [Display(Name = "Pr No")]
        public String PrNo { get; set; }

        [StringLength(150)]
        [Display(Name = "T1")]
        public String T1 { get; set; }

        [StringLength(150)]
        [Display(Name = "Acct Code")]
        public String AcctCode { get; set; }

        [StringLength(150)]
        [Display(Name = "T3")]
        public String T3 { get; set; }

        [StringLength(150)]
        [Display(Name = "T5")]
        public String T5 { get; set; }

        [StringLength(150)]
        [Display(Name = "T2")]
        public String T2 { get; set; }

        // ComboBox
        public virtual Customers customer { get; set; }

    }
}
 
