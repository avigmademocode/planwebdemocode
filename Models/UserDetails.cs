using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{
    [Table("users", Schema = "")]
    public class UserDetails
    {

        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "id")]
        public Int32 id { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "username")]
        public String Username { get; set; }

        [Required]
        [Display(Name = "email")]
        public String email { get; set; }

        [StringLength(500)]
        [Display(Name = "password")]
        public String password { get; set; }
    }
    public partial class Intialpage
    {
        public string textPart { get; set; }
        public string imagePath { get; set; }
    }
}