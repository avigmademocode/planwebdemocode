using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class OrderItsetupDTO
    {
        public Int64 ITSetUpId { get; set; }
        public Int64 OrderId { get; set; }
        public string strOrderID { get; set; }
        public Int64 ProductId { get; set; }
        public int Type { get; set; }
        public int Serial { get; set; }
        public string UserName { get; set; }
        public Int64 UserTypeId { get; set; }
        public Int64 WorkLocationId { get; set; }
        public string DeliveryLocation { get; set; }
        public string Department { get; set; }
        public string Applications { get; set; }
        public string SpecialInstructions { get; set; }
        public int MigrateInfo { get; set; }
        public bool isActive { get; set; }

        public string UserType { get; set; }
        public string WorkLocation { get; set; }
        public string PartNo { get; set; }
    }
    public class OrderItsetupUI
    {
        public Int64 ITSetUpId { get; set; }
        public Int64 OrderId { get; set; }
        public string strOrderID { get; set; }
        public Int64 ProductId { get; set; }
        public int Type { get; set; }
        public int Serial { get; set; }
        public string UserName { get; set; }
        public Int64 UserTypeId { get; set; }
        public Int64 WorkLocationId { get; set; }
        public string DeliveryLocation { get; set; }
        public string Department { get; set; }
        public string Applications { get; set; }
        public string SpecialInstructions { get; set; }
        public int MigrateInfo { get; set; }
        public bool isActive { get; set; }
    }

    public class CustomerUserType
    {
        public Int64 UserTypeId { get; set; }
        public Int64 CustId { get; set; }
        public string UserType { get; set; }
    }

    public class CustomerWorkLocation
    {
        public Int64 WorkLocationId { get; set; }
        public string WorkLocation { get; set; }
        public Int64 Custid { get; set; }
       
    }
    public class ItShipmemntInfo
    {
        public string Waybill { get; set; }
        public string DeliveryDate { get; set; }
        public Int64 ShipmentId { get; set; }
        public string ShipmentDate { get; set; }
        public Int64 OrderId { get; set; }
        public string Carrier { get; set; }
    }


    public class OrderProductSoftwareSetupDTO
    {
      
        public string PartNo { get; set; }
        public Int64 ProductId { get; set; }
        public List<OrderItsetupDTO> DataIt { get; set; }
        public List<OrderSoftwareSetupDTO> DataSS { get; set; }
    }

    // Order Software Setup 
    public class OrderSoftwareSetupDTO
    {
        public Int64 SoftwareSetupId { get; set; }
        public Int64 OrderId { get; set; }
        public string strOrderID { get; set; }
        public Int64 ProductId { get; set; }
        public int Type { get; set; }
        public int Serial { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool isActive { get; set; }
        public string PartNo { get; set; }
    }
    public class OrderSoftwareSetupUI
    {
        public Int64 SoftwareSetupId { get; set; }
        public Int64 OrderId { get; set; }
        public string strOrderID { get; set; }
        public Int64 ProductId { get; set; }
        public int Type { get; set; }
        public int Serial { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool isActive { get; set; }
    }
}