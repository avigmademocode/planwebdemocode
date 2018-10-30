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
using MyProject.Repository.Security;

namespace MyProject.Controllers
{
    public class OrderSummaryController : Controller
    {
        // GET: OrderSummary
        
        public ActionResult Index()
        {
            return View();
        }
        CustRequestData objCustRequest = new CustRequestData();
        OrderApprovalData objOrderData = new OrderApprovalData();
        CustOrderData custOrderData = new CustOrderData();
        SecurityHelper securityHelper = new SecurityHelper();
        public JsonResult GetOrderSummary(String orderID, int CustId, int UserId,int Type)
        {
            if (Type == 1)
            {
               
                var OrderSummary = objCustRequest.GetOrderDetails(CustId, Convert.ToInt64(orderID));
                return new JsonResult { Data = OrderSummary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                string strOrderID = securityHelper.Decrypt(orderID, false);
                var OrderSummary = objOrderData.GetOrderDetails(strOrderID);
                return new JsonResult { Data = OrderSummary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            
        }

        public JsonResult SaveOrderSummary(SaveOrder SaveOrder)
        {
            if (SaveOrder.type == 3)
            {
                StatusChange model = new StatusChange();
                model.OrderID = SaveOrder.strorderID;
                model.Type = "8";
                objOrderData.GetUpdateCustomerStatus(model);
                var AllStatus = custOrderData.GetCustOrderDetailList(Convert.ToInt64(securityHelper.Decrypt(SaveOrder.strorderID,false)));
                return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                var OrderSummary = objCustRequest.SaveConfirmOrder(SaveOrder);
                return new JsonResult { Data = OrderSummary, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
           
        }
    }
}