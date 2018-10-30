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
    public class usersController : Controller
    {

        DataTable dtusers = new DataTable();
        DataTable dtcities = new DataTable();
        DataTable dtcountries = new DataTable();

        // GET: /users/
        public ActionResult Index(string sortOrder,  
                                  String SearchField,
                                  String SearchCondition,
                                  String SearchText,
                                  String Export,
                                  int? PageSize,
                                  int? page, 
                                  string command)
        {

            if (command == "Show All") {
                SearchField = null;
                SearchCondition = null;
                SearchText = null;
                Session["SearchField"] = null;
                Session["SearchCondition"] = null;
                Session["SearchText"] = null; } 
            else if (command == "Add New Record") { return RedirectToAction("Create"); } 
            else if (command == "Export") { Session["Export"] = Export; } 
            else if (command == "Search" | command == "Page Size") {
                if (!string.IsNullOrEmpty(SearchText)) {
                    Session["SearchField"] = SearchField;
                    Session["SearchCondition"] = SearchCondition;
                    Session["SearchText"] = SearchText; }
                } 
            if (command == "Page Size") { Session["PageSize"] = PageSize; }

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Id" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["idSortParm"] = sortOrder == "id_asc" ? "id_desc" : "id_asc";
            ViewData["emailSortParm"] = sortOrder == "email_asc" ? "email_desc" : "email_asc";
            ViewData["usernameSortParm"] = sortOrder == "username_asc" ? "username_desc" : "username_asc";
            ViewData["passwordSortParm"] = sortOrder == "password_asc" ? "password_desc" : "password_asc";
            ViewData["loginsSortParm"] = sortOrder == "logins_asc" ? "logins_desc" : "logins_asc";
            ViewData["last_loginSortParm"] = sortOrder == "last_login_asc" ? "last_login_desc" : "last_login_asc";
            ViewData["CustomerIdSortParm"] = sortOrder == "CustomerId_asc" ? "CustomerId_desc" : "CustomerId_asc";
            ViewData["FieldOfficeIdSortParm"] = sortOrder == "FieldOfficeId_asc" ? "FieldOfficeId_desc" : "FieldOfficeId_asc";
            ViewData["FirstNameSortParm"] = sortOrder == "FirstName_asc" ? "FirstName_desc" : "FirstName_asc";
            ViewData["LastNameSortParm"] = sortOrder == "LastName_asc" ? "LastName_desc" : "LastName_asc";
            ViewData["CitySortParm"] = sortOrder == "City_asc" ? "City_desc" : "City_asc";
            ViewData["CountryIdSortParm"] = sortOrder == "CountryId_asc" ? "CountryId_desc" : "CountryId_asc";

            dtusers = usersData.SelectAll();
            dtcities = users_citiesData.SelectAll();
            dtcountries = users_countriesData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtusers = usersData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowusers in dtusers.AsEnumerable()
                        join rowcities in dtcities.AsEnumerable() on rowusers.Field<String>("City") equals rowcities.Field<String>("Name")
                        join rowcountries in dtcountries.AsEnumerable() on rowusers.Field<Int32>("CountryId") equals rowcountries.Field<Int32>("id")
                        select new users() {
                            id = rowusers.Field<Int32>("id")
                           ,email = rowusers.Field<String>("email")
                           ,username = rowusers.Field<String>("username")
                           ,password = rowusers.Field<String>("password")
                           ,logins = rowusers.Field<Int32>("logins")
                           ,last_login = rowusers.Field<Int32?>("last_login")
                           ,CustomerId = rowusers.Field<Int32?>("CustomerId")
                           ,FieldOfficeId = rowusers.Field<Int32?>("FieldOfficeId")
                           ,FirstName = rowusers.Field<String>("FirstName")
                           ,LastName = rowusers.Field<String>("LastName")
                           ,
                            cities = new cities() 
                            {
                                   Name = rowcities.Field<String>("Name")
                                  ,CountryId = rowcities.Field<Int32>("CountryId")
                            }
                           ,
                            countries = new countries() 
                            {
                                   id = rowcountries.Field<Int32>("id")
                                  ,Name = rowcountries.Field<String>("Name")
                            }
                        };

            switch (sortOrder)
            {
                case "id_desc":
                    Query = Query.OrderByDescending(s => s.id);
                    break;
                case "id_asc":
                    Query = Query.OrderBy(s => s.id);
                    break;
                case "email_desc":
                    Query = Query.OrderByDescending(s => s.email);
                    break;
                case "email_asc":
                    Query = Query.OrderBy(s => s.email);
                    break;
                case "username_desc":
                    Query = Query.OrderByDescending(s => s.username);
                    break;
                case "username_asc":
                    Query = Query.OrderBy(s => s.username);
                    break;
                case "password_desc":
                    Query = Query.OrderByDescending(s => s.password);
                    break;
                case "password_asc":
                    Query = Query.OrderBy(s => s.password);
                    break;
                case "logins_desc":
                    Query = Query.OrderByDescending(s => s.logins);
                    break;
                case "logins_asc":
                    Query = Query.OrderBy(s => s.logins);
                    break;
                case "last_login_desc":
                    Query = Query.OrderByDescending(s => s.last_login);
                    break;
                case "last_login_asc":
                    Query = Query.OrderBy(s => s.last_login);
                    break;
                case "CustomerId_desc":
                    Query = Query.OrderByDescending(s => s.CustomerId);
                    break;
                case "CustomerId_asc":
                    Query = Query.OrderBy(s => s.CustomerId);
                    break;
                case "FieldOfficeId_desc":
                    Query = Query.OrderByDescending(s => s.FieldOfficeId);
                    break;
                case "FieldOfficeId_asc":
                    Query = Query.OrderBy(s => s.FieldOfficeId);
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
                case "City_desc":
                    Query = Query.OrderByDescending(s => s.cities.CountryId);
                    break;
                case "City_asc":
                    Query = Query.OrderBy(s => s.cities.CountryId);
                    break;
                case "CountryId_desc":
                    Query = Query.OrderByDescending(s => s.countries.Name);
                    break;
                case "CountryId_asc":
                    Query = Query.OrderBy(s => s.countries.Name);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.id);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("Username", typeof(string));
                dt.Columns.Add("Password", typeof(string));
                dt.Columns.Add("Logins", typeof(string));
                dt.Columns.Add("Last Login", typeof(string));
                dt.Columns.Add("Customer Id", typeof(string));
                dt.Columns.Add("Field Office Id", typeof(string));
                dt.Columns.Add("First Name", typeof(string));
                dt.Columns.Add("Last Name", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("Country Id", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.id
                       ,item.email
                       ,item.username
                       ,item.password
                       ,item.logins
                       ,item.last_login
                       ,item.CustomerId
                       ,item.FieldOfficeId
                       ,item.FirstName
                       ,item.LastName
                       ,item.cities.CountryId
                       ,item.countries.Name
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

        // GET: /users/Details/<id>
        public ActionResult Details(
                                      Int32? id
                                   )
        {
            if (
                    id == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtcities = users_citiesData.SelectAll();
            dtcountries = users_countriesData.SelectAll();

            users users = new users();
            users.id = System.Convert.ToInt32(id);
            users = usersData.Select_Record(users);
            users.cities = new cities()
            {
                Name = (String)users.username
               ,CountryId = (from DataRow rowcities in dtcities.Rows
                      where users.City == rowcities["id"].ToString()
                      select (Int32)rowcities["CountryId"]).FirstOrDefault()
            };
            users.countries = new countries()
            {
                id = (Int32)users.id
               ,Name = (from DataRow rowcountries in dtcountries.Rows
                      where users.CountryId == (int)rowcountries["id"]
                      select (String)rowcountries["Name"]).FirstOrDefault()
            };

            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: /users/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["Name"] = new SelectList(users_citiesData.List(), "Name", "CountryId");
            ViewData["id"] = new SelectList(users_countriesData.List(), "id", "Name");

            return View();
        }

        // POST: /users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "id"
				   + "," + "email"
				   + "," + "username"
				   + "," + "password"
				   + "," + "logins"
				   + "," + "last_login"
				   + "," + "CustomerId"
				   + "," + "FieldOfficeId"
				   + "," + "FirstName"
				   + "," + "LastName"
				   + "," + "City"
				   + "," + "CountryId"
				  )] users users)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = usersData.Add(users);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }
        // ComboBox
            ViewData["Name"] = new SelectList(users_citiesData.List(), "Name", "CountryId", users.City);
            ViewData["id"] = new SelectList(users_countriesData.List(), "id", "Name", users.CountryId);

            return View(users);
        }

        // GET: /users/Edit/<id>
        public ActionResult Edit(
                                   Int32? id
                                )
        {
            if (
                    id == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            users users = new users();
            users.id = System.Convert.ToInt32(id);
            users = usersData.Select_Record(users);

            if (users == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["Name"] = new SelectList(users_citiesData.List(), "Name", "CountryId", users.City);
            ViewData["id"] = new SelectList(users_countriesData.List(), "id", "Name", users.CountryId);

            return View(users);
        }

        // POST: /users/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(users users)
        {

            users ousers = new users();
            ousers.id = System.Convert.ToInt32(users.id);
            ousers = usersData.Select_Record(users);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = usersData.Update(ousers, users);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }
        // ComboBox
            ViewData["Name"] = new SelectList(users_citiesData.List(), "Name", "CountryId", users.City);
            ViewData["id"] = new SelectList(users_countriesData.List(), "id", "Name", users.CountryId);

            return View(users);
        }

        // GET: /users/Delete/<id>
        public ActionResult Delete(
                                     Int32? id
                                  )
        {
            if (
                    id == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtcities = users_citiesData.SelectAll();
            dtcountries = users_countriesData.SelectAll();

            users users = new users();
            users.id = System.Convert.ToInt32(id);
            users = usersData.Select_Record(users);
            users.cities = new cities()
            {
                Name = (String)users.username
               ,CountryId = (from DataRow rowcities in dtcities.Rows
                      where users.City == (string)rowcities["id"]
                      select (Int32)rowcities["CountryId"]).FirstOrDefault()
            };
            users.countries = new countries()
            {
                id = (Int32)users.id
               ,Name = (from DataRow rowcountries in dtcountries.Rows
                      where users.CountryId == (int)rowcountries["id"]
                      select (String)rowcountries["Name"]).FirstOrDefault()
            };

            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: /users/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? id
                                            )
        {

            users users = new users();
            users.id = System.Convert.ToInt32(id);
            users = usersData.Select_Record(users);

            bool bSucess = false;
            bSucess = usersData.Delete(users);
            if (bSucess == true)
            {
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
            SelectListItem Item1 = new SelectListItem { Text = "Id", Value = "Id" };
            SelectListItem Item2 = new SelectListItem { Text = "Email", Value = "Email" };
            SelectListItem Item3 = new SelectListItem { Text = "Username", Value = "Username" };
            SelectListItem Item4 = new SelectListItem { Text = "Password", Value = "Password" };
            SelectListItem Item5 = new SelectListItem { Text = "Logins", Value = "Logins" };
            SelectListItem Item6 = new SelectListItem { Text = "Last Login", Value = "Last Login" };
            SelectListItem Item7 = new SelectListItem { Text = "Customer Id", Value = "Customer Id" };
            SelectListItem Item8 = new SelectListItem { Text = "Field Office Id", Value = "Field Office Id" };
            SelectListItem Item9 = new SelectListItem { Text = "First Name", Value = "First Name" };
            SelectListItem Item10 = new SelectListItem { Text = "Last Name", Value = "Last Name" };
            SelectListItem Item11 = new SelectListItem { Text = "City", Value = "City" };
            SelectListItem Item12 = new SelectListItem { Text = "Country Id", Value = "Country Id" };

                 if (select == "Id") { Item1.Selected = true; }
            else if (select == "Email") { Item2.Selected = true; }
            else if (select == "Username") { Item3.Selected = true; }
            else if (select == "Password") { Item4.Selected = true; }
            else if (select == "Logins") { Item5.Selected = true; }
            else if (select == "Last Login") { Item6.Selected = true; }
            else if (select == "Customer Id") { Item7.Selected = true; }
            else if (select == "Field Office Id") { Item8.Selected = true; }
            else if (select == "First Name") { Item9.Selected = true; }
            else if (select == "Last Name") { Item10.Selected = true; }
            else if (select == "City") { Item11.Selected = true; }
            else if (select == "Country Id") { Item12.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);
            list.Add(Item7);
            list.Add(Item8);
            list.Add(Item9);
            list.Add(Item10);
            list.Add(Item11);
            list.Add(Item12);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo.Users", "Many");
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
 
