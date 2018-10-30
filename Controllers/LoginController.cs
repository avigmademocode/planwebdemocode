using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using PagedList;
using PagedList.Mvc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MyProject.Models;
using MyProject.Data;
using System.Configuration;

namespace MyProject.Controllers
{
    public class LoginController : Controller
    {
        DataTable dtusers = new DataTable();
        DataTable dtmenu = new DataTable();

        // GET: Login
        public ActionResult Index()
        {
            Intialpage obj = loginData.Select_Text();
            ViewBag.textPart = obj.textPart;
            return View();
            
        }

        [HttpPost]
        public JsonResult ValidateUser(string username, string password)
        {
            if (dtusers != null || dtusers.Rows.Count <= 0)
            {
                dtusers = usersData.SelectAll();
                try
                {
                    DataRow data = (from DataRow c in dtusers.Rows
                                    where c.Field<string>("username") == username && c.Field<string>("Pwd") == password
                                    select c).SingleOrDefault();
                    if (data != null && data.ItemArray.Count() > 0)
                    {

                        Session["IsPlansonUser"] = data["IsPlansonUser"].ToString();
                        Session["FirstName"] = data["FirstName"].ToString();
                        Session["RoleName"] = data["RoleName"].ToString();
                        Session["CustId"] = data["CustId"].ToString();
                        Session["UserId"] = data["UserId"].ToString();
                        Session["RoleId"] = data["RoleId"].ToString();
                        dtmenu = MenuData.MenuSelectForRole(Convert.ToInt64(data["RoleId"]));
                        Session["listMenu"] = dtmenu;
                        return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ViewBag.message = "Invalid username/password.";
                        return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception ex) { return Json(new { Success = false }, JsonRequestBehavior.AllowGet); }
            }
            else
            {
                //FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
            //var data =from c dtusers.id where c.username==userid && c.password==password select c;
        }

        [HttpPost]
        public JsonResult SendForgotPasword(string emailid)
        {
            string strSubject, strBody;
            strSubject = "Password Recovery";
            dtusers = usersData.SelectAll();

            DataRow data = (from DataRow c in dtusers.Rows
                            where c.Field<string>("LoginId") == emailid
                            select c).SingleOrDefault();
            if (data != null && data.ItemArray.Count() > 0)
            {
                if (data["LoginId"] != null && data["Pwd"] != null)
                {
                    strBody = "<font face=\"Arial\">Your password has been reset.</font><br />&nbsp;<br /><font face=\"Arial\">Username: <strong>" + data["LoginId"].ToString() + "<br /></strong>Password: <strong>" + data["Pwd"].ToString() + "</strong><br /><br />You can login with your new information here: [[Site.SiteName]]&nbsp;[[Site.TimeStamp]]</font>";
                    try
                    {
                        if (Common.SendMail(emailid, strSubject, strBody))
                            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                        else
                        {
                            ViewBag.message = "Invalid email";
                            return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch (Exception ex) { return Json(new { Success = false }, JsonRequestBehavior.AllowGet); }
                }
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);//some issue
            }
            return Json(new { Success = false }, JsonRequestBehavior.AllowGet);//not an user
        }

        [HttpPost]
        public JsonResult CreateUser(string firstName, string lastName, string city, string country, string email, string autokey, string password)
        {
            decimal keyValidhr = Convert.ToDecimal(ConfigurationManager.AppSettings["keyValidHours"]);
            users obj = new users();
            obj.FirstName = firstName;
            obj.LastName = lastName;
            obj.City    = city;
            obj.Country = country;
            obj.email = email;
            obj.Key = autokey;
            obj.password = password;
            
           
            bool res = new bool();
            res=usersData.UserNewInsert(obj, keyValidhr);
            if(res)
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);// user
            else
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GenerateKey(string customerCode, string emailKey)
        {
            string strkey = Common.RandomString();
            if(loginData.CustomerKeyInsert(customerCode, emailKey, strkey))
            {
                if (Common.SendMail(emailKey, "Key for registration.", "<font face=\"Arial\">Your key has been generated.</font><br />&nbsp;<br /><font face=\"Arial\">Key: <strong>" + strkey + "<br /></strong><br /><br /> "))
                {
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);// user
                }
                else
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
               
            else
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
           
        }

        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            //FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Login/index");
        }
    }
}