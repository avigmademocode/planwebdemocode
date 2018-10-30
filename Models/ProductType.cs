using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class ProductTypeList
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public Int64 ProductTypeId { get; set; }
        [StringLength(100)]
        [Display(Name = "Model")]
        public string ProductType { get; set; }
    }



}