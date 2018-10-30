using MyProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Data;
using MyProject.Repository.Data;
using MyProject.Repository.Security;
using MyProject.Models;
namespace MyProject.Controllers
{
    public class AddFreightQuoteController : Controller
    {
        
        OrderFreightData OrderFreightData = new OrderFreightData();
        SecurityHelper securityHelper = new SecurityHelper();
        public ActionResult Index()
        {
            return View();
        }

        //public JsonResult SaveAndNotify(int OrderID, string LeadTime, string Freight, string Tax)
        //{
        //    int msg = 1;

        //    return new JsonResult { Data = msg, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        public JsonResult ViewFreightQuote(string OrderID)
        {
         
            var AllStatus = OrderFreightData.GetCustDataFreight(securityHelper.Decrypt(OrderID,false));
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult AddFreightQuote(FreightDetails FreightDetails)
        {

            var AllStatus = OrderFreightData.AddCustDataFreight(FreightDetails);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }

}