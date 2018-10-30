using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Collections.Generic;

namespace MyProject.Models
{
    /* [Table("CustMaster", Schema="")]*/
    public class CustMaster
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Cust Id")]
        public Int64 CustId { get; set; }

        [StringLength(200)]
        [Display(Name = "Customer")]
        public String CustName { get; set; }

        [StringLength(100)]
        [Display(Name = "Acronym")]
        public String Acronym { get; set; }

        [Display(Name = "No of Branches")]
        public Int32? NoofBranches { get; set; }

        [Display(Name = "Levels of Authority")]
        public Int32? LevelofAuthority { get; set; }

        [StringLength(20)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [StringLength(20)]
        [Display(Name = "Ticker")]
        public String Ticker { get; set; }

        [Display(Name = "Demo")]
        public Boolean InDemo { get; set; }

        [Display(Name = "Tiered Pricing")]
        public Boolean TieredPricing { get; set; }

        [Display(Name = "Active")]
        public Boolean isActive { get; set; }




    }

    
}
