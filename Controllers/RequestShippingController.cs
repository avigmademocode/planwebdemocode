using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
using MyProject.Repository.Data;
using MyProject.Repository.Security;


namespace MyProject.Controllers
{
    public class RequestShippingController : Controller
    {
        GetCarrier getCarrier = new GetCarrier();
        OrderShipmentInfoCRUD orderShipmentInfoCRUD = new OrderShipmentInfoCRUD();
        OrderShipmentCRUD orderShipmentCRUD = new OrderShipmentCRUD();
        SecurityHelper securityHelper = new SecurityHelper();
        ShipData shipData = new ShipData();
        // GET: RequestShipping
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCarrier()
        {
            var Carrier = getCarrier.GetCarrierDetails();
            return new JsonResult { Data = Carrier, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult Ordershipmentinfo(OrderShipmentComman model)
        {
           
            var Response = orderShipmentCRUD.AddOrderShipment(model);
            return new JsonResult { Data = Response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // get order shipment 
        public JsonResult GetShipmentDataDtls(string OrderID) 
        {
            var Shipment = shipData.GetOrderShipment(securityHelper.Decrypt(OrderID, false));
            return new JsonResult { Data = Shipment, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // get all shipment
        public JsonResult GetShipmentAll(Int64 ShipmentId)
        {
            var Shipment = shipData.GetOrderShipmentDetails(ShipmentId);
            return new JsonResult { Data = Shipment, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        // get order shipment second time data
        public JsonResult GetNewShipmentDataDtls(string OrderID)
        {
            var Shipment = shipData.GetNewOrderShipment(securityHelper.Decrypt(OrderID, false));
            return new JsonResult { Data = Shipment, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}