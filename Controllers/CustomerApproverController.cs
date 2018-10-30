using MyProject.Models;
using MyProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class CustomerApproverController : Controller
    {
        //
        // GET: /CustomerApprover/
        ApproverSettingData approverSettingData = new ApproverSettingData();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetCustomerApproverData(Int64 CustID)
        {
            var AllStatus = approverSettingData.GetApproverData(CustID);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult SaveCustomerApproverData(ApproverSetting approverSetting)
        {

            var AllStatus = approverSettingData.AddUpdateApproverData(approverSetting);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}