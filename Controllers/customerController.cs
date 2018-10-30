using MyProject.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public class customerController : Controller
    {
       

        // GET: /CustMaster/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Details()
        {
            List<SelectListItem> customers = new List<SelectListItem>();
            DataTable dtCustomer = new DataTable();
            dtCustomer = customersData.CustomerSelect_All();
            List<SelectListItem> countries = new List<SelectListItem>();
            DataTable dtCountry = new DataTable();
            dtCountry = customersData.CountrySelect_All();
            foreach (DataRow dr in dtCountry.Rows)
            {
                countries.Add(new SelectListItem
                {
                    Value =dr["CountryId"].ToString(),
                    Text = dr["CountryName"].ToString()
                });
            }
            foreach (DataRow dr in dtCustomer.Rows)
            {
                customers.Add(new SelectListItem
                {
                    Value = dr["CustId"].ToString(),
                    Text = dr["code"].ToString()
                });

            }
            var response = new { customers = customers, countries = countries };
            return Json(response, JsonRequestBehavior.AllowGet);
            //return Json(customers);// user
        }
    }
}