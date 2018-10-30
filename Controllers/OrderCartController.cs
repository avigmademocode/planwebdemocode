 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Data;
using MyProject.Repository.Data;
using MyProject.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
namespace MyProject.Controllers
{
    public class OrderCartController : Controller
    {
        CustRequestData objCustRequest = new CustRequestData();
        CustOrderData custOrderData = new CustOrderData();
        // GET: OrderCart
        public ActionResult Index()
        {
            return View();
        }
       /*
        //IList<CustOrderDetails> productlist { get; set; }
        [HttpPost]
        public JsonResult SaveOrderDetails(string OrderID, string CustomerID, string SubTotal, CustOrderDetails model)
        {

            var OrderData = objCustRequest.SaveOrderDetails(OrderID,CustomerID,SubTotal,model);

            return new JsonResult { Data = OrderData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }

    */
        [HttpPost]
        public async Task<JsonResult> SaveOrderDetails(string OrderID, string CustomerID, string SubTotal, CustOrderListDetails model)
        {
            if (model.Type == 1)
            {
                var OrderData = objCustRequest.SaveOrderDetails(OrderID, CustomerID, SubTotal, model);
                return new JsonResult { Data = OrderData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                var OrderData = custOrderData.SaveOrderDetails(OrderID, CustomerID, SubTotal, model);
                return new JsonResult { Data = OrderData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
           
        }
    }
}