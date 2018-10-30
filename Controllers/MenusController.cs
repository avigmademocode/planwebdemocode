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


namespace MyProject.Controllers
{
    public class MenusController : Controller
    {
        DataTable dtmenu = MenuData.MenuSelectAll();
        // GET: Menus
        public ActionResult Index()
        {
           DataTable dtmenu = MenuData.MenuSelectAll();
            ViewBag["Menu"] = dtmenu;
            return View();
        }

        //// GET: /Menu/Details/<id>
        //public ActionResult Details(int64 ? MenuId )
        //{
        //    if (
        //            MenuId == null
        //       )
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Menu Menu = db.Menu.Find(
        //                                         MenuId
        //                                    );
        //    if (Menu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Menu);
        //}

        //// GET: /Menu/Create
        //public ActionResult Create()
        //{
        //    // ComboBox
        //    ViewBag.RoleId = new SelectList(db.UserRoles, "RoleId", "RoleId");

        //    return View();
        //}

        //// POST: /Menu/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include=
        //                   "MenuId"
        //           + "," + "MenuName"
        //           + "," + "RoleId"
        //           + "," + "IsActive"
        //           + "," + "CreatedOn"
        //           + "," + "CreatedBy"
        //           + "," + "ModifiedOn"
        //           + "," + "ModifiedBy"
        //           + "," + "DeletedOn"
        //           + "," + "DeletedBy"
        //          )] Menu Menu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Menu.Add(Menu);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    // ComboBox
        //    ViewBag.RoleId = new SelectList(db.UserRoles, "RoleId", "RoleId", Menu.RoleId);

        //    return View(Menu);
        //}

        //// GET: /Menu/Edit/<id>
        //public ActionResult Edit(
        //                           Int64? MenuId
        //                        )
        //{
        //    if (
        //            MenuId == null
        //       )
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Menu Menu = db.Menu.Find(
        //                                         MenuId
        //                                    );
        //    if (Menu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    // ComboBox
        //    ViewBag.RoleId = new SelectList(db.UserRoles, "RoleId", "RoleId", Menu.RoleId);

        //    return View(Menu);
        //}

        //// POST: /Menu/Edit/<id>
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost, ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Menu Menu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(Menu).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    // ComboBox
        //    ViewBag.RoleId = new SelectList(db.UserRoles, "RoleId", "RoleId", Menu.RoleId);

        //    return View(Menu);
        //}

        //// GET: /Menu/Delete/<id>
        //public ActionResult Delete(
        //                             Int64? MenuId
        //                          )
        //{
        //    if (
        //            MenuId == null
        //       )
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Menu Menu = db.Menu.Find(
        //                                         MenuId
        //                                    );
        //    if (Menu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(Menu);
        //}

        //// POST: /Menu/Delete/<id>
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(
        //                                    Int64? MenuId
        //                                    )
        //{
        //    Menu Menu = db.Menu.Find(
        //                                         MenuId
        //                                    );
        //    db.Menu.Remove(Menu);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }
}