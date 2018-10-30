using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Repository.Data;
using MyProject.Models;
namespace MyProject.Controllers
{
    public class AddManufactureController : Controller
    {
        // GET: AddManufacture
        ManufacturerData manufacturerData = new ManufacturerData();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetManufactureData()
        {

            var AllStatus = manufacturerData.GetManufacturer();
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult SaveManufactureData(ManufacturerDetail  manufacturerDetail)
        {

            var AllStatus = manufacturerData.AddManufacture(manufacturerDetail);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}