using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models1
{
    [Table("customer", Schema="")]
    public class customer
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Customer Id")]
        public Int32 CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public String Name { get; set; }

        [StringLength(10)]
        [Display(Name = "Acronym")]
        public String Acronym { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public Boolean IsActive { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Code")]
        public String code { get; set; }

        [StringLength(100)]
        [Display(Name = "Ticker")]
        public String ticker { get; set; }

        [Required]
        [Display(Name = "In Demo")]
        public Byte inDemo { get; set; }

        [Required]
        [Display(Name = "Tiered Pricing")]
        public Byte tiered_pricing { get; set; }

    }
}
 
