using MyProject.Models;
using MyProject.Repository.Data;
using MyProject.Repository.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class CustomerSettingsController : Controller
    {
        // GET: CustomerSettings
        CustomerOrderSettingData objCustomerOrderSettingData = new CustomerOrderSettingData();
        SecurityHelper securityHelper = new SecurityHelper();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveNewRequest(CustomerSettings Model)
        {

            var CustData = objCustomerOrderSettingData.AddUpdateCustomerSetting(Model);
            return new JsonResult { Data = CustData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult GetCustomerSettings(int CustID)
        {

            var CustData = objCustomerOrderSettingData.GetCustSettingOrderDetails(CustID);
            return new JsonResult { Data = CustData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
    }
}