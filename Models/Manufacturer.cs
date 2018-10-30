using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class ManufacturerList
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public Int64 ManufacturerId { get; set; }
        [StringLength(100)]
        [Display(Name = "Manufacturer")]
        public string ManufacturerDesc { get; set; }

    }
}