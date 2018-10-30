using MyProject.Data;
using MyProject.Repository.Data;
using MyProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MyProject.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        CustRequestData objCustRequest = new CustRequestData();
        public JsonResult GetCategoryData(int Custid)
        {
            var catData = objCustRequest.GetCategory(Custid);
            return new JsonResult { Data = catData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }
    }
}