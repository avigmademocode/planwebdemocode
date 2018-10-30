using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{
    
        [Table("UserMaster", Schema = "")]
    public class UserMaster
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "User Id")]
        public Int64 UserId { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Login Id")]
        public String LoginId { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Pwd")]
        public String Pwd { get; set; }

        [StringLength(200)]
        [Display(Name = "User Name")]
        public String UserName { get; set; }

        [Display(Name = "Cust Id")]
        public Int64? CustId { get; set; }

        [Display(Name = "Is Planson User")]
        [DefaultValue(typeof(Boolean), "false")]
        public Boolean IsPlansonUser { get; set; }

        [StringLength(100)]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [StringLength(100)]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Display(Name = "City Id")]
        public Int64? CityId { get; set; }

        [Display(Name = "Country Id")]
        public Int64? CountryId { get; set; }

        [StringLength(100)]
        [Display(Name = "Logins")]
        public String Logins { get; set; }

        [StringLength(100)]
        [Display(Name = "Last Login")]
        public String Last_Login { get; set; }

        [Display(Name = "Locked")]
        [DefaultValue(typeof(Boolean), "false")]
        public Boolean Locked { get; set; }

        [Display(Name = "Is Active")]
        [DefaultValue(typeof(Boolean), "false")]
        public Boolean IsActive { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created On")]
        public DateTime?  CreatedOn { get; set; }

        [Display(Name = "Created By")]
        public Int64? CreatedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modified On")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Modified By")]
        public Int64? ModifiedBy { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deleted On")]
        public DateTime? DeletedOn { get; set; }

        [Display(Name = "Deleted By")]
        public Int64? DeletedBy { get; set; }

    }


}
