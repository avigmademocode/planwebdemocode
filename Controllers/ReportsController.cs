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
    public class ReportsController : Controller
    {
        ReportsData reportsData = new ReportsData();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCustomerStatus()
        {
            CustomerStatusData customerStatusData = new CustomerStatusData();
            var response = customerStatusData.GetAllCustomerActiveStatusData();
            return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetReports(ReportUI model)
        {
            var response = reportsData.GetReportData(model);
            return new JsonResult { Data = response, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


    }
}