using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{
    [Table("roles", Schema="")]
    public class roles
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Id")]
        public Int32 id { get; set; }

        [Required]
        [StringLength(32)]
        [Display(Name = "Name")]
        public String name { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Description")]
        public String description { get; set; }


    }
}
 
