using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Repository.Data;
using MyProject.Models;
namespace MyProject.Controllers
{
  
    public class AddRoleController : Controller
    {
        UserRolesDTO objUserRole = new UserRolesDTO();
        UserRolesData objuserRolesData = new UserRolesData();
        // GET: AddRole
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetRoleData()
        {
            var Data = objuserRolesData.GetUseRolerData();
            return new JsonResult { Data = Data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult SaveRoleData(UserRoles model)
        {
            var Data = objuserRolesData.SaveUserRoleData(model);
            return new JsonResult { Data = Data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}