using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Repository.Data;
using MyProject.Models;

namespace MyProject.Controllers
{
    public class GrandMasterController : Controller
    {
        // GET: GrandMaster
        GrantCodeMstrData grantCodeMstrData = new GrantCodeMstrData();
      
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetGrantBudgetMaster(GrantBudgeMster grantBudgeMster)
        {
            if (grantBudgeMster.value == 1)
            {
                var AllStatus = grantCodeMstrData.GetGrantcodeMasterData(grantBudgeMster.CustId);
                return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                var AllStatus = grantCodeMstrData.GetBudgetCodeData(grantBudgeMster.CustId);
                return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
           
        }

        public JsonResult SaveGrantBudgetMaster(GrantBudgeMster grantBudgeMster)
        {

            var AllStatus = grantCodeMstrData.AddGrantBudget(grantBudgeMster);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}