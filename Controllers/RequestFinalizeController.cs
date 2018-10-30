using MyProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
using MyProject.Repository.Security;
namespace MyProject.Controllers
{
    public class RequestFinalizeController : Controller
    {
        OrderFreightData OrderFreightData = new OrderFreightData();
        BudgetCodeData budgetcodewisedata = new BudgetCodeData();
        SecurityHelper securityHelper = new SecurityHelper();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ViewOrderFinalizeDetails(string OrderID)
        {
            var AllStatus = OrderFreightData.GetCustDataFreight(securityHelper.Decrypt(OrderID,false));
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetCustBudgetMastr(string OrderID)
        {
            var AllStatus = budgetcodewisedata.GetCustBudgetMastr(Convert.ToInt64(securityHelper.Decrypt(OrderID, false)));
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult AddBudgetDetails(BudgetCodeWiseOrderDetailDTO BudgetCodeWise)
        {
            var AllStatus = budgetcodewisedata.AddCustBudgetMastr(BudgetCodeWise);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult BudgetCodeOrderDetails(string OrderID)
        {
            var catData = budgetcodewisedata.GetViewBudgetOrderProductDetails(OrderID);
            return new JsonResult { Data = catData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }

        public JsonResult GetBudgetOrderDetails(string OrderID)
        {
            var catData = budgetcodewisedata.GetViewBudgetProductDetails(OrderID);
            return new JsonResult { Data = catData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }
    }
}