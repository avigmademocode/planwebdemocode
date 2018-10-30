using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Repository.Data;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        LanguagesData  languagesData = new LanguagesData();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetLanguagesData(int langid)
        {

            var AllStatus = languagesData.GetLanguagesData(langid);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult SaveLanguagesData(LanguagesDTO languagesDetail)
        {

            var AllStatus = languagesData.AddLanguagesData(languagesDetail);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}