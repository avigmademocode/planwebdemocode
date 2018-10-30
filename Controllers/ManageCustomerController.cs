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
    public class ManageCustomerController : Controller
    {
        CustomerMasterData objcustomerMasterData = new CustomerMasterData();
        // GET: ManageCustomer
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCustomerData(int CustID)
        {

            var CustData = objcustomerMasterData.GetCustMasterData(CustID);
            return new JsonResult { Data = CustData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult SaveCustomerData(CustomerMaster customerMaster)
        {

            var CustData = objcustomerMasterData.AddCustMasterData(customerMaster);
            return new JsonResult { Data = CustData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
    }
}