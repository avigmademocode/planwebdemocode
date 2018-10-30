using MyProject.Models;
using MyProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Repository.Security;
namespace MyProject.Controllers
{
    public class AddNewGrantCodeController : Controller
    {
        GrantCodeMasterData objGrant = new GrantCodeMasterData();
        SecurityHelper securityHelper = new SecurityHelper();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddMultipleGrantCode()
        {
            return View();
        }
        public ActionResult AddMultipleGrantCodeEdit()
        {
            return View();
        }

        public JsonResult GetNewGrantCodeDtls(string OrderID)
        {
            var catData = objGrant.GetGrantCodeMastr(Convert.ToInt64(securityHelper.Decrypt(OrderID, false)));

            return new JsonResult { Data = catData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }

        public JsonResult AddNewGrantCode(GrantCodeMasterDTO model)
        {
            var catData = objGrant.AddGrantCodeOrders(model);

            return new JsonResult { Data = catData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }

        public JsonResult GrantCodeOrderDetails(string OrderID)
        {
            var catData = objGrant.GrantCodeOrderDetails(OrderID);
            return new JsonResult { Data = catData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }

        public JsonResult SaveGrantData(GrantCodeMasterDTO model)
        {
            var catData = objGrant.SaveGrantCodeOrders(model);
            return new JsonResult { Data = catData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }

        public JsonResult ViewGrantCodeOrderDetails(string OrderID)
        {
            var catData = objGrant.GetGrantOrderProductDetails(OrderID);
            return new JsonResult { Data = catData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }


        public JsonResult ViewRequestGrantCodeOrderDetails(string OrderID)
        {
            var catData = objGrant.GetViewGrantOrderProductDetails(OrderID);
            return new JsonResult { Data = catData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }
    }
}