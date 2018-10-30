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
    public class CustomerBranchesController : Controller
    {
        // GET: CustomerBranches
        CustBranchesData CustBranchesData = new CustBranchesData();
        SecurityHelper securityHelper = new SecurityHelper();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SaveNewRequest(CustomerBranches Model)
        {

            var OrderData = CustBranchesData.AddCustomerBranchesx(Model);
            return new JsonResult { Data = OrderData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult GetCustomerBranches(int CustID)
        {

            var CustData = CustBranchesData.GetCustDetials(CustID);
            return new JsonResult { Data = CustData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
    }
}