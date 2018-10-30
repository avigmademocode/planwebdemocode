using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class OrderShipmentComman
    {

        public string Title { get; set; }
        public DateTime ShipmentDate { get; set; }
        public Int64 OrderId { get; set; }
        public Int64 DocumentGroupId { get; set; }

        public Int64 ShipmentInfoId { get; set; }
        public int CarrierIdx { get; set; }
        public string Waybill { get; set; }
        public string DeliveryDate { get; set; }
        public Int64 ShipmentId { get; set; }

        public string strOrderID { get; set; }
        public bool isActive { get; set; }
        public Int64 UserId { get; set; }
        public int Type { get; set; }

        public Int64 ShipmentDetailId { get; set; }
        public Int64 ODID { get; set; }
        public int Quantity { get; set; }
        public int? ToShip { get; set; }
        public int? BalanceQty { get; set; }

        public string Ordershipmentinfo { get; set; }
        public string ShipmentDetails { get; set; }

        public bool SplitOrder { get; set; }
        public bool SendEmail { get; set; }
        public string SendEmailID { get; set; }
    }
}