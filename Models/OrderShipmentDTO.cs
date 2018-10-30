using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class OrderShipmentDTO
    {
        public Int64 ShipmentId { get; set; }
        public string Title { get; set; }
        public string ShipmentDate { get; set; }
        public DateTime ShipmentDateDB { get; set; }
        public Int64 OrderId { get; set; }
        public Int64 DocumentGroupId { get; set; }

        public bool isActive { get; set; }
        public Int64 UserId { get; set; }
        public int Type { get; set; }
    }
    
}