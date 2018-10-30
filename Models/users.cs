using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{
    [Table("users", Schema="")]
    public class users
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Id")]
        public Int32 id { get; set; }

        [Required]
        [StringLength(254)]
        [Display(Name = "Email")]
        public String email { get; set; }

        [Required]
        [StringLength(254)]
        [Display(Name = "Username")]
        public String username { get; set; }

       
        [Display(Name = "IsPlansonUser")]
        public bool IsPlansonUser { get; set; }
        

        [Required]
        [StringLength(64)]
        [Display(Name = "Password")]
        public String password { get; set; }

        [Required]
        [Display(Name = "Logins")]
        public Int32 logins { get; set; }

        [Display(Name = "Last Login")]
        public Int32? last_login { get; set; }

        [Display(Name = "Customer Id")]
        public Int32? CustomerId { get; set; }

        [Display(Name = "Field Office Id")]
        public Int32? FieldOfficeId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        
        [StringLength(100)]
        [Display(Name = "City")]
        public String City { get; set; }

        
        [StringLength(100)]
        [Display(Name = "Country")]
        public String Country { get; set; }

        [Required]
        [Display(Name = "Country Id")]
        public Int32 CountryId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Key")]
        public String Key { get; set; }

        // ComboBox
        public virtual cities cities { get; set; }
        public virtual Countries countries { get; set; }

        //public virtual Customers customers { get; set; }

    }
    
}
 
