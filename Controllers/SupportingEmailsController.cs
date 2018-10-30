using MyProject.Models;
using MyProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class SupportingEmailsController : Controller
    {
        //
        // GET: /SupportingEmails/
        SupportingEmailData supportingEmailData = new SupportingEmailData();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetSupportingEmailsData(Int64 CustID)
        {
            var AllStatus = supportingEmailData.GetSupportingEmailData(CustID);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult SaveSupportingEmailsData(SupportingEmailDTO supportingEmailDTO)
        {

            var AllStatus = supportingEmailData.AddUpdateSupportingEmailData(supportingEmailDTO);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}