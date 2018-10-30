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
    public class ItsetupController : Controller
    {
        AddOrderItsetup addOrderItsetup = new AddOrderItsetup();
        GetOrderITSetup getOrderITSetup = new GetOrderITSetup();
        AddOrderSoftwareSetup addOrderSoftwareSetup = new AddOrderSoftwareSetup();
        SecurityHelper securityHelper = new SecurityHelper();
        // GET: Itsetup
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetCustomerUserType()
        {
            var UserType = addOrderItsetup.GetCustomerUserTypeDetails();
            return new JsonResult { Data = UserType, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // get order shipment 
        public JsonResult GetITShipmentData(string OrderID)
        {
            var Shipment = addOrderItsetup.GetITsetupShipment(securityHelper.Decrypt(OrderID, false));
            return new JsonResult { Data = Shipment, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // post ITsetup
        public JsonResult PostITShipmentData(OrderItsetupUI model)
        {
            var response = addOrderItsetup.AddOrderITSetUp(model);
            return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // get order ITSetUp data
        public JsonResult GetITSetUpData(string OrderID)
        {
            var ITSetUpData = getOrderITSetup.GetITSetup(securityHelper.Decrypt(OrderID, false));
            return new JsonResult { Data = ITSetUpData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // post Softwaresetup
        public JsonResult PostSoftwareSetUpData(OrderSoftwareSetupUI model)
        {
            var response = addOrderSoftwareSetup.AddOrderSoftwareSetUp(model);
            return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // get order ITSetUp data
        public JsonResult GetSoftSetUpData(string OrderID)
        {
            var ITSetUpData = addOrderSoftwareSetup.GetSoftSetup(securityHelper.Decrypt(OrderID, false));
            return new JsonResult { Data = ITSetUpData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}