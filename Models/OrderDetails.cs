using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace MyProject.Models
{
    [Table("OrderDetails", Schema = "")]
    public class OrderDetails
    {

        
        [Display(Name = "OrderId")]
        public Int64 OrderId { get; set; }

        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Serial")]
        public Int32 Serial { get; set; }

        
        [Display(Name = "ProductId")]
        public Int64 ProductId { get; set; }

        [Display(Name = "ODID")]
        public Int64 ODID { get; set; }


        [Display(Name = "Qty")]
        public Int32 Qty { get; set; }

         
        [Display(Name = "Rate")]
        public Decimal Rate { get; set; }

        [Display(Name = "Amount")]
        public Decimal Amount { get; set; }

        [Display(Name = "ModifiedBy")]
        public Int64 ModifiedBy { get; set; }

        public String ProductName { get; set; }

        public Decimal TotalOrderAmount { get; set; }
    }
}