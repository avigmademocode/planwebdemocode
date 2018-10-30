using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class OrderShipmentDetailsDTO
    {
        public Int64 ShipmentDetailId { get; set; }
        public Int64 ShipmentId { get; set; }
        public Int64 ODID { get; set; }
        public string PartNo { get; set; }
        public int ActualQty { get; set; }
        public int? ShippedQty { get; set; }
        public int? BalanceQty { get; set; }
        public int Quantity { get; set; }
        public int ToShip { get; set; }
        public int BalQty { get; set; }
        public bool isActive { get; set; }
        public Int64 UserId { get; set; }
        public int Type { get; set; }
    }
}