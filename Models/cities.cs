using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{
    [Table("cities", Schema="")]
    public class cities
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Id")]
        public Int32 id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public Boolean IsActive { get; set; }

        [Display(Name = "Country Id")]
        public Int32? CountryId { get; set; }

    }
}
 
