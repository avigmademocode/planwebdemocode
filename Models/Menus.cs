using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    [Table("Menu", Schema = "")]
    public class Menu
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Menu Id")]
        public Int64 MenuId { get; set; }

        [StringLength(100)]
        [Display(Name = "Menu Name")]
        public String MenuName { get; set; }

        [StringLength(200)]
        [Display(Name = "Url")]
        public String Url { get; set; }

        [StringLength(200)]
        [Display(Name = "Description")]
        public String Description { get; set; }

        [Required]
        [Display(Name = "Role Id")]
        public Int64 RoleId { get; set; }

        [Display(Name = "Is Active")]
        public Boolean? IsActive { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

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