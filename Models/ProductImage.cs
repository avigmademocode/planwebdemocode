using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyProject.Models
{
    [Table("ProductImage", Schema = "")]
    public class ProductImage
    {

        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "ProductId")]
        public Int64 ProductId { get; set; }

        [StringLength(200)]
        [Display(Name = "Spec")]
        public String Spec { get; set; }

        [StringLength(200)]
        [Display(Name = "ProdImage")]
        public String ProdImage { get; set; }

        [StringLength(200)]
        [Display(Name = "ImageType")]
        public String ImageType { get; set; }

    }
}