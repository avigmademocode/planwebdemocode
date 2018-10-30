using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{
    [Table("Order", Schema = "")]
    public class Order
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "OrderId")]
        public Int64 OrderId { get; set; }

        [Required]
        [Display(Name = "ReferenceNo")]
        public String ReferenceNo { get; set; }

        [Required]

        [Display(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        [Required]

        [Display(Name = "Department")]
        public String Department { get; set; }


        [Display(Name = "CustId")]
        public Int64 CustId { get; set; }


        [Display(Name = "UserId")]
        public Int64 UserId { get; set; }


        [Display(Name = "BranchId")]
        public Int64 BranchId { get; set; }


        [Display(Name = "IncoTermId")]
        public Int64 IncoTermId { get; set; }


        [Display(Name = "StatusId")]
        public Int64 StatusId { get; set; }


        [Display(Name = "ShippingAddress")]
        public String ShippingAddress { get; set; }


        [Display(Name = "CountryId")]
        public Int64 CountryId { get; set; }



        [Display(Name = "CountryName")]
        public String CountryName { get; set; }


        [Display(Name = "CityId")]
        public Int64 CityId { get; set; }

        [Display(Name = "CityName")]
        public String CityName { get; set; }

        [Display(Name = "BillingAddress")]
        public String BillingAddress { get; set; }



        [Display(Name = "CustomerComments")]
        public String CustomerComments { get; set; }


        [Display(Name = "Feight")]
        public Int32 Feight { get; set; }


        [Display(Name = "IsSalesOrder")]
        public Boolean IsSalesOrder { get; set; }


        [StringLength(200)]
        [Display(Name = "SalesOrderNo")]
        public String SalesOrderNo { get; set; }

        [StringLength(200)]
        [Display(Name = "ContactName")]
        public String ContactName {get; set;}


        [StringLength(200)]
        [Display(Name = "ContactNum")]
        public String ContactNum { get; set; }


        [StringLength(200)]
        [Display(Name = "ContactEmail")]
        public String ContactEmail { get; set; }

        [Display(Name = "isClosed")]
        public Boolean isClosed { get; set; }


 
        [Display(Name = "ModifiedBy")]
        public Int64 ModifiedBy { get; set; }


 
        [Display(Name = "ModfiedOn")]
        public DateTime ModfiedOn { get; set; }

 
        [Display(Name = "isDeleted")]
        public Boolean isDeleted { get; set; }

        [StringLength(200)]
        [Display(Name = "DeletedReason")]
        public String DeletedReason { get; set; }


 
        [Display(Name = "DeletedOn")]
        public DateTime DeletedOn { get; set; }

 
        [Display(Name = "DeletedBy")]
        public Int64 DeletedBy { get; set; }

    }
}