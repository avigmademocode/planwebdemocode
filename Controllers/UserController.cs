using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
using System.Data;

using MyProject.Data;

namespace MyProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            List<UserMaster> userMasterList = new List<Models.UserMaster>();
            //DataTable dtusers = usersData.SelectAll().Select().ToList();
            userMasterList = usersData.SelectAllUsers();
            //userMasterList = dtusers.Select().ToList();

            return Json( new { Data= userMasterList},JsonRequestBehavior.AllowGet);
        }
    }
}