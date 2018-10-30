using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{
    [Table("CustBranches", Schema = "")]

    public class CustBranches
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "BranchId")]
        public Int64 BranchId { get; set; }

        [Required]
        [Display(Name = "CustId")]
        public Int64 CustId { get; set; }

        [Display(Name = "IsHeadOffice")]
        public Boolean BIsHeadOffice { get; set; }

        [Display(Name = "PreSetAddress")]
        public Boolean PreSetAddress { get; set; }

        [Display(Name = "HideBillingAddress")]
        public Boolean HideBillingAddress { get; set; }

        [Display(Name = "BrAdd1")]
        public String BrAdd1 { get; set; }

        [Display(Name = "BrAdd2")]
        public String BrAdd2 { get; set; }

        [Display(Name = "BrAdd3")]
        public String BrAdd3 { get; set; }

        [Display(Name = "BrCity")]
        public String BrCity { get; set; }

        [Display(Name = "BrState")]
        public String BrState { get; set; }

        [Display(Name = "BrCountry")]
        public String BrCountry{ get; set; }

        [Display(Name = "BrPin")]
        public String BrPin { get; set; }

        [Display(Name = "BrEmailId")]
        public String BrEmailId { get; set; }

        [Display(Name = "BrContactNo")]
        public String BrContactNo { get; set; }

        [Display(Name = "BrContactPerson")]
        public String BrContactPerson { get; set; }

        [Display(Name = "BrFullAddress")]
        public String BrFullAddress { get; set; }

        [Display(Name = "BlAdd1")]
        public String BlAdd1 { get; set; }

        [Display(Name = "BlAdd2")]
        public String BlAdd2 { get; set; }

        [Display(Name = "BlAdd3")]
        public String BlAdd3 { get; set; }

        [Display(Name = "BlCity")]
        public String BlCity { get; set; }

        [Display(Name = "BlState")]
        public String BlState { get; set; }

        [Display(Name = "BlCountry")]
        public String BlCountry { get; set; }

        [Display(Name = "BlPin")]
        public String BlPin { get; set; }

        [Display(Name = "BlEmailId")]
        public String BlEmailId { get; set; }

        [Display(Name = "BlContactNo")]
        public String BlContactNo { get; set; }

        [Display(Name = "BlContactPerson")]
        public String BlContactPerson { get; set; }

        [Display(Name = "Needs_Delivery_Term")]
        public Boolean Needs_Delivery_Term { get; set; }

        [Display(Name = "Needs_Fee_Warning")]
        public Boolean Needs_Fee_Warning { get; set; }

        [Display(Name = "Fee_Warning")]
        public String Fee_Warning { get; set; }

        [Display(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "CreatedBy")]
        public Int64 CreatedBy { get; set; }

        [Display(Name = "ModifiedOn")]
        public DateTime ModifiedOn { get; set; }

        [Display(Name = "ModifiedBy")]
        public Int64 ModifiedBy { get; set; }

        [Display(Name = "ModifiedOn")]
        public DateTime DeletedOn { get; set; }

        [Display(Name = "DeletedBy")]
        public Int64 DeletedBy { get; set; }

        [Display(Name = "BrName")]
        public String BrName { get; set; }

        [Display(Name = "DisplayName")]
        public String DisplayName { get; set; }
    }
}