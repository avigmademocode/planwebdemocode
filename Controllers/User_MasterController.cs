using MyProject.Data;
using MyProject.Models;
using MyProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Repository.Security;

namespace MyProject.Controllers
{
    public class User_MasterController : Controller
    {
        UserData objUserData = new UserData();
        UserMasterData UserMasterData = new UserMasterData();
        SecurityHelper SecurityHelper = new SecurityHelper();
        UserRolesRelationData userRolesRelationData = new UserRolesRelationData();
        // GET: User_Master
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserView()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }




        public JsonResult GetUserData(UserDataDTO Model)
        {
            if (string.IsNullOrEmpty(Model.strUserId) || (Model.strUserId == "0"))
            {
                var Data = objUserData.GetUserData(Model.UserId);
                return new JsonResult { Data = Data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                var Data = objUserData.GetUserData(Convert.ToInt64(SecurityHelper.Decrypt(Model.strUserId, false)));
                return new JsonResult { Data = Data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }

        public JsonResult AddUserData(UserDataDTO Model)
        {
            UserDataDetails userData = new UserDataDetails();
            var Data = objUserData.AddUserDetailsData(Model, 1, userData);
            return new JsonResult { Data = Data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult UpdateUserData(UserDataDetails userData)
        {
            UserDataDTO Model = new UserDataDTO();
            var Data = objUserData.AddUserDetailsData(Model, 2, userData);
            return new JsonResult { Data = Data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //user role assign
        public JsonResult SaveUserRoleData(UserRolesRelation Model)
        {
            UserRolesRelationData UserRolesRelationData = new UserRolesRelationData();
            var Data = UserRolesRelationData.AddUserRoleRelation(Model);
            return new JsonResult { Data = Data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}