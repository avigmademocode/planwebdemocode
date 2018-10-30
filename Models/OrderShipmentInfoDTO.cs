using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class OrderShipmentInfoDTO
    {
        public Int64 ShipmentInfoId { get; set; }
        public Int64 CarrierId { get; set; }
        public Int64 CarrierIdx { get; set; }
        public string Waybill { get; set; }
        public string DeliveryDate { get; set; }
        public Int64 ShipmentId { get; set; }
        public DateTime? DeliveryDateDB { get; set; }

        public bool isActive { get; set; }
        public Int64 UserId { get; set; }
        public int Type { get; set; }
    }
   
}