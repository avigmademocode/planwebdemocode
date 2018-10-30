using MyProject.Data;
using MyProject.Repository.Data;
using MyProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MyProject.Controllers
{
    public class AllRequestsController : Controller
    {
        // GET: AllRequests
        OrderApprovalData objOrderData = new OrderApprovalData();
        public ActionResult Index()
        {
            return View();
        } 
        
       
        public JsonResult GetAllRequests(SearchRequestData model)
        {
            // var custData = "";
            var custData = objOrderData.SearchCustomerOrderData(model);
            return new JsonResult { Data = custData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetAllStatus()
        {

            var AllStatus = objOrderData.GetAllOrderStatus();
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}