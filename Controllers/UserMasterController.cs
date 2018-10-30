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

namespace MyProject.Controllers
{
    public class UserMasterController : Controller
    {

        DataTable dtUserMaster = new DataTable();

        // GET: /UserMaster/
        public ActionResult Index(string sortOrder,  
                                  String SearchField,
                                  String SearchCondition,
                                  String SearchText,
                                  String Export,
                                  int? PageSize,
                                  int? page, 
                                  string command)
        {

            if (command == "Clear") {
                SearchField = null;
                SearchCondition = null;
                SearchText = null;
                Session["SearchField"] = null;
                Session["SearchCondition"] = null;
                Session["SearchText"] = null; } 
           /* else if (command == "Add New Record") { return RedirectToAction("Create"); } */
            else if (command == "Export") { Session["Export"] = Export; } 
            else if (command == "Search" | command == "Page Size") {
                if (!string.IsNullOrEmpty(SearchText)) {
                    Session["SearchField"] = SearchField;
                    Session["SearchCondition"] = SearchCondition;
                    Session["SearchText"] = SearchText; }
                } 
            if (command == "Page Size") { Session["PageSize"] = PageSize; }

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "User Id" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["UserIdSortParm"] = sortOrder == "UserId_asc" ? "UserId_desc" : "UserId_asc";
            ViewData["LoginIdSortParm"] = sortOrder == "LoginId_asc" ? "LoginId_desc" : "LoginId_asc";
            ViewData["PwdSortParm"] = sortOrder == "Pwd_asc" ? "Pwd_desc" : "Pwd_asc";
            ViewData["UserNameSortParm"] = sortOrder == "UserName_asc" ? "UserName_desc" : "UserName_asc";
            ViewData["IsPlansonUserSortParm"] = sortOrder == "IsPlansonUser_asc" ? "IsPlansonUser_desc" : "IsPlansonUser_asc";
            ViewData["FirstNameSortParm"] = sortOrder == "FirstName_asc" ? "FirstName_desc" : "FirstName_asc";
            ViewData["LastNameSortParm"] = sortOrder == "LastName_asc" ? "LastName_desc" : "LastName_asc";
            ViewData["LockedSortParm"] = sortOrder == "Locked_asc" ? "Locked_desc" : "Locked_asc";
            ViewData["IsActiveSortParm"] = sortOrder == "IsActive_asc" ? "IsActive_desc" : "IsActive_asc";

            dtUserMaster = UserMasterData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtUserMaster = UserMasterData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowUserMaster in dtUserMaster.AsEnumerable()
                        select new UserMaster() {
                            UserId = rowUserMaster.Field<Int64>("UserId")
                           ,LoginId = rowUserMaster.Field<String>("LoginId")
                           ,Pwd = rowUserMaster.Field<String>("Pwd")
                           ,UserName = rowUserMaster.Field<String>("UserName")
                           ,IsPlansonUser = rowUserMaster.Field<Boolean>("IsPlansonUser")
                           ,FirstName = rowUserMaster.Field<String>("FirstName")
                           ,LastName = rowUserMaster.Field<String>("LastName")
                           ,Locked = rowUserMaster.Field<Boolean>("Locked")
                           ,IsActive = rowUserMaster.Field<Boolean>("IsActive")
                        };

            switch (sortOrder)
            {
                case "UserId_desc":
                    Query = Query.OrderByDescending(s => s.UserId);
                    break;
                case "UserId_asc":
                    Query = Query.OrderBy(s => s.UserId);
                    break;
                case "LoginId_desc":
                    Query = Query.OrderByDescending(s => s.LoginId);
                    break;
                case "LoginId_asc":
                    Query = Query.OrderBy(s => s.LoginId);
                    break;
                case "Pwd_desc":
                    Query = Query.OrderByDescending(s => s.Pwd);
                    break;
                case "Pwd_asc":
                    Query = Query.OrderBy(s => s.Pwd);
                    break;
                case "UserName_desc":
                    Query = Query.OrderByDescending(s => s.UserName);
                    break;
                case "UserName_asc":
                    Query = Query.OrderBy(s => s.UserName);
                    break;
                case "IsPlansonUser_desc":
                    Query = Query.OrderByDescending(s => s.IsPlansonUser);
                    break;
                case "IsPlansonUser_asc":
                    Query = Query.OrderBy(s => s.IsPlansonUser);
                    break;
                case "FirstName_desc":
                    Query = Query.OrderByDescending(s => s.FirstName);
                    break;
                case "FirstName_asc":
                    Query = Query.OrderBy(s => s.FirstName);
                    break;
                case "LastName_desc":
                    Query = Query.OrderByDescending(s => s.LastName);
                    break;
                case "LastName_asc":
                    Query = Query.OrderBy(s => s.LastName);
                    break;
                case "Locked_desc":
                    Query = Query.OrderByDescending(s => s.Locked);
                    break;
                case "Locked_asc":
                    Query = Query.OrderBy(s => s.Locked);
                    break;
                case "IsActive_desc":
                    Query = Query.OrderByDescending(s => s.IsActive);
                    break;
                case "IsActive_asc":
                    Query = Query.OrderBy(s => s.IsActive);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.UserId);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("User Id", typeof(string));
                dt.Columns.Add("Login Id", typeof(string));
                dt.Columns.Add("Pwd", typeof(string));
                dt.Columns.Add("User Name", typeof(string));
                dt.Columns.Add("Is Planson User", typeof(string));
                dt.Columns.Add("First Name", typeof(string));
                dt.Columns.Add("Last Name", typeof(string));
                dt.Columns.Add("Locked", typeof(string));
                dt.Columns.Add("Is Active", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.UserId
                       ,item.LoginId
                       ,item.Pwd
                       ,item.UserName
                       ,item.IsPlansonUser
                       ,item.FirstName
                       ,item.LastName
                       ,item.Locked
                       ,item.IsActive
                    );
                }
                gv.DataSource = dt;
                gv.DataBind();
                ExportData(Export, gv, dt);
            }

            int pageNumber = (page ?? 1);
            int? pageSZ = (Convert.ToInt32(Session["PageSize"]) == 0 ? 5 : Convert.ToInt32(Session["PageSize"]));
            return View(Query.ToPagedList(pageNumber, (pageSZ ?? 5)));
        }

        // GET: /UserMaster/Details/<id>
        public ActionResult Details(
                                      Int64? UserId
                                   )
        {
            if (
                    UserId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            UserMaster UserMaster = new UserMaster();
            UserMaster.UserId = System.Convert.ToInt32(UserId);
            UserMaster = UserMasterData.Select_Record(UserMaster);

            if (UserMaster == null)
            {
                return HttpNotFound();
            }
            return View(UserMaster);
        }

        // GET: /UserMaster/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /UserMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "UserId"
				   + "," + "LoginId"
				   + "," + "Pwd"
				   + "," + "UserName"
				   + "," + "IsPlansonUser"
				   + "," + "FirstName"
				   + "," + "LastName"
				   + "," + "Locked"
				   + "," + "IsActive"
				  )] UserMaster UserMaster)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = UserMasterData.Add(UserMaster);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(UserMaster);
        }

        // GET: /UserMaster/Edit/<id>
        public ActionResult Edit(
                                   Int64? UserId
                                )
        {
            if (
                    UserId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserMaster UserMaster = new UserMaster();
            UserMaster.UserId = System.Convert.ToInt32(UserId);
            UserMaster = UserMasterData.Select_Record(UserMaster);

            if (UserMaster == null)
            {
                return HttpNotFound();
            }

            return View(UserMaster);
        }

        // POST: /UserMaster/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserMaster UserMaster)
        {

            UserMaster oUserMaster = new UserMaster();
            oUserMaster.UserId = System.Convert.ToInt32(UserMaster.UserId);
            oUserMaster = UserMasterData.Select_Record(UserMaster);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = UserMasterData.Update(oUserMaster, UserMaster);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(UserMaster);
        }

        // GET: /UserMaster/Delete/<id>
        public ActionResult Delete(
                                     Int64? UserId
                                  )
        {
            if (
                    UserId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            UserMaster UserMaster = new UserMaster();
            UserMaster.UserId = System.Convert.ToInt32(UserId);
            UserMaster = UserMasterData.Select_Record(UserMaster);

            if (UserMaster == null)
            {
                return HttpNotFound();
            }
            return View(UserMaster);
        }

        // POST: /UserMaster/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int64? UserId
                                            )
        {

            UserMaster UserMaster = new UserMaster();
            UserMaster.UserId = System.Convert.ToInt32(UserId);
            UserMaster = UserMasterData.Select_Record(UserMaster);

            bool bSucess = false;
            bSucess = UserMasterData.Delete(UserMaster);
            if (bSucess == true)
            {
                TempData["success"] = "Successfully deleted.";
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Can Not Delete");
            }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private static List<SelectListItem> GetFields(String select)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem Item1 = new SelectListItem { Text = "User Id", Value = "User Id" };
            SelectListItem Item2 = new SelectListItem { Text = "Login Id", Value = "Login Id" };
            SelectListItem Item3 = new SelectListItem { Text = "Pwd", Value = "Pwd" };
            SelectListItem Item4 = new SelectListItem { Text = "User Name", Value = "User Name" };
            SelectListItem Item5 = new SelectListItem { Text = "Is Planson User", Value = "Is Planson User" };
            SelectListItem Item6 = new SelectListItem { Text = "First Name", Value = "First Name" };
            SelectListItem Item7 = new SelectListItem { Text = "Last Name", Value = "Last Name" };
            SelectListItem Item8 = new SelectListItem { Text = "Locked", Value = "Locked" };
            SelectListItem Item9 = new SelectListItem { Text = "Is Active", Value = "Is Active" };

                 if (select == "User Id") { Item1.Selected = true; }
            else if (select == "Login Id") { Item2.Selected = true; }
            else if (select == "Pwd") { Item3.Selected = true; }
            else if (select == "User Name") { Item4.Selected = true; }
            else if (select == "Is Planson User") { Item5.Selected = true; }
            else if (select == "First Name") { Item6.Selected = true; }
            else if (select == "Last Name") { Item7.Selected = true; }
            else if (select == "Locked") { Item8.Selected = true; }
            else if (select == "Is Active") { Item9.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);
            list.Add(Item7);
            list.Add(Item8);
            list.Add(Item9);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "User Master", "Many");
                Document document = pdfForm.CreateDocument();
                PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
                renderer.Document = document;
                renderer.RenderDocument();

                MemoryStream stream = new MemoryStream();
                renderer.PdfDocument.Save(stream, false);

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + "Report.pdf");
                Response.ContentType = "application/Pdf.pdf";
                Response.BinaryWrite(stream.ToArray());
                Response.Flush();
                Response.End();
            }
            else
            {
                Response.ClearContent();
                Response.Buffer = true;
                if (Export == "Excel")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Report.xls");
                    Response.ContentType = "application/Excel.xls";
                }
                else if (Export == "Word")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Report.doc");
                    Response.ContentType = "application/Word.doc";
                }
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

    }
}
 
