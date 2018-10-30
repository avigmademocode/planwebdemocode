using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class ProductCustomerView
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public Int64 ProductId { get; set; }
        [StringLength(100)]
        [Display(Name = "Model")]
        public string Model { get; set; }
       
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [Display(Name = "Category Id")]
        public Int64 prodcatId { get; set; }
        [StringLength(100)]
        [Display(Name = "Category")]
        public string ProdCatDesc { get; set; }
    }

    public class ProductList
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public Int64 ProductId { get; set; }
        [StringLength(100)]
        [Display(Name = "Model")]
        public string Model { get; set; }
    }
}