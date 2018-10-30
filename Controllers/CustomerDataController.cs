using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Repository.Data;
using MyProject.Models;
namespace MyProject.Controllers
{
    public class CustomerDataController : Controller
    {
        //
        // GET: /CustomData/
        public ActionResult ChangeIncoterms()
        {
            return View();
        }

        public ActionResult AddEditStatus()
        {
            return View();
        }

        public ActionResult AddEditCustomerStatus()
        {
            return View();
        }

        public ActionResult EditGrantsItemGroups()
        {
            return View();
        }

        IncoTermData objIncoTermData = new IncoTermData();
        public JsonResult GetCustIncoTermData(int IncoTermId)
        {
            var AllCustIncoTermData = objIncoTermData.GetCustIncoTermDetail(IncoTermId);
            return new JsonResult { Data = AllCustIncoTermData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult SaveIncoTerm(IncoTermDTO incoTermDTO)
        {
            var AllCustIncoTermData = objIncoTermData.AddCustIncoTerm(incoTermDTO);
            return new JsonResult { Data = AllCustIncoTermData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        StatusData objStatusData = new StatusData();
        CustomerStatusData customerStatusData = new CustomerStatusData();
        public JsonResult  GetStatusData(int StatusId)
        {
            var StatusData = objStatusData.GetStatusData(StatusId);
            return new JsonResult { Data = StatusData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

     
        }

        public JsonResult SaveStatus(StatusInfo statusInfo)
        {
            var AllCustIncoTermData = objStatusData.AddUpdateStatus(statusInfo);
            return new JsonResult { Data = AllCustIncoTermData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetCustStatusData(Int64 CustID)
        {
            var AllCustIncoTermData = customerStatusData.GetCustomerStatusData(CustID);
            return new JsonResult { Data = AllCustIncoTermData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult SaveStatusData(GetCustomerStatusData getCustomerStatusData)
        {
            var AllCustIncoTermData = customerStatusData.AddUpdateCustStatusData(getCustomerStatusData);
            return new JsonResult { Data = AllCustIncoTermData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}