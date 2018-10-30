using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{

    [Table("CustMaster", Schema = "")]
    public class Customers
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Cust Id")]
        public Int64 CustId { get; set; }

        [StringLength(200)]
        [Display(Name = "Cust Name")]
        public String CustName { get; set; }

        [StringLength(100)]
        [Display(Name = "Acronym")]
        public String Acronym { get; set; }

        [Display(Name = "Noof Branches")]
        public Int32? NoofBranches { get; set; }

        [Display(Name = "Levelof Authority")]
        public Int32? LevelofAuthority { get; set; }

        [StringLength(20)]
        [Display(Name = "Code")]
        public String Code { get; set; }

        [StringLength(20)]
        [Display(Name = "Ticker")]
        public String Ticker { get; set; }

        [Display(Name = "In Demo")]
        public Boolean? InDemo { get; set; }

        [Display(Name = "Tiered Pricing")]
        public Boolean? TieredPricing { get; set; }

        [StringLength(100)]
        [Display(Name = "H O Add1")]
        public String HOAdd1 { get; set; }

        [StringLength(100)]
        [Display(Name = "H O Add2")]
        public String HOAdd2 { get; set; }

        [StringLength(100)]
        [Display(Name = "H O Add3")]
        public String HOAdd3 { get; set; }

        [StringLength(100)]
        [Display(Name = "H O C I T Y")]
        public String HOCITY { get; set; }

        [StringLength(100)]
        [Display(Name = "H O State")]
        public String HOState { get; set; }

        [StringLength(100)]
        [Display(Name = "H O Country")]
        public String HOCountry { get; set; }

        [StringLength(50)]
        [Display(Name = "H O Pin")]
        public String HOPin { get; set; }

        [StringLength(1000)]
        [Display(Name = "H O Full Address")]
        public String HOFullAddress { get; set; }

        [StringLength(200)]
        [Display(Name = "H O Email Id")]
        public String HOEmailId { get; set; }

        [StringLength(100)]
        [Display(Name = "H O Contact Person")]
        public String HOContactPerson { get; set; }

        [Display(Name = "Is Active")]
        public Boolean? isActive { get; set; }

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
