using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace MyProject.Models
{
    [Table("OrderApprover", Schema = "")]
    public class OrderApprover
    {

      
        [Display(Name = "OrderId")]
        public Int64 OrderId { get; set; }

        [Display(Name = "OAId")]
        public Int64 OAId { get; set; }

        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Serial")]
        public Int32 Serial { get; set; }

        [StringLength(200)]
        [Display(Name = "ApproverName")]
        public String ApproverName { get; set; }

        [StringLength(200)]
        [Display(Name = "ApproverTitle")]
        public String ApproverTitle { get; set; }

        [StringLength(200)]
        [Display(Name = "ApproverEmail")]
        public String ApproverEmail { get; set; }

   
        [Display(Name = "UserId")]
        public Int64 UserId { get; set; }

        
        [Display(Name = "Comments")]
        public String Comments { get; set; }

        
        [Display(Name = "isLoggedin")]
        public Boolean isLoggedin { get; set; }


        
        [Display(Name = "isApproved")]
        public Boolean isApproved { get; set; }


        
        [Display(Name = "LoggedDateTime")]
        public DateTime LoggedDateTime { get; set; }
    }
}