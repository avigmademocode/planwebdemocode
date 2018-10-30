using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Repository.Security;
using MyProject.Repository.Data;
using MyProject.Models;


namespace MyProject.Controllers
{
    public class PrintPDFController : Controller
    {
        SecurityHelper securityHelper = new SecurityHelper();

        OrderFreightData OrderFreightData = new OrderFreightData();
        PrintPDFData PrintPDFData = new PrintPDFData();
        // GET: PrintPDF
        public ActionResult Index()
        {
            return View();
        }

        // GET: PrintPDF/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PrintPDF/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrintPDF/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PrintPDF/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PrintPDF/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PrintPDF/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrintPDF/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetPrintPDFData(string OrderID)
        {

            var AllStatus = PrintPDFData.GetCustOrderData(securityHelper.Decrypt(OrderID, false));
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult AddFreightQuote(FreightDetails FreightDetails)
        {

            var AllStatus = "";
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult AddPrintPDFApprovalDetail(FreightDetails FreightDetails)
        {

            var AllStatus = "";
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult AddPrintPDFDenyDetails(FreightDetails FreightDetails)
        {

            var AllStatus = "";
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult AddPrintPDFResubmitDetails(FreightDetails FreightDetails)
        {

            var AllStatus = "";
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult AddPrintPDFBackDetails(FreightDetails FreightDetails)
        {

            var AllStatus = "";
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
