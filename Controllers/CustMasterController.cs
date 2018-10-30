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
    public class CustMasterController : Controller
    {

        DataTable dtCustMaster = new DataTable();

        // GET: /CustMaster/
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
            else if (command == "Add New Record") { return RedirectToAction("Create"); } 
            else if (command == "Export") { Session["Export"] = Export; } 
            else if (command == "Search" | command == "Page Size") {
                if (!string.IsNullOrEmpty(SearchText)) {
                    Session["SearchField"] = SearchField;
                    Session["SearchCondition"] = SearchCondition;
                    Session["SearchText"] = SearchText; }
                } 
            if (command == "Page Size") { Session["PageSize"] = PageSize; }

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Cust Id" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CustIdSortParm"] = sortOrder == "CustId_asc" ? "CustId_desc" : "CustId_asc";
            ViewData["CustNameSortParm"] = sortOrder == "CustName_asc" ? "CustName_desc" : "CustName_asc";
            ViewData["AcronymSortParm"] = sortOrder == "Acronym_asc" ? "Acronym_desc" : "Acronym_asc";
            ViewData["NoofBranchesSortParm"] = sortOrder == "NoofBranches_asc" ? "NoofBranches_desc" : "NoofBranches_asc";
            ViewData["LevelofAuthoritySortParm"] = sortOrder == "LevelofAuthority_asc" ? "LevelofAuthority_desc" : "LevelofAuthority_asc";
            ViewData["CodeSortParm"] = sortOrder == "Code_asc" ? "Code_desc" : "Code_asc";
            ViewData["TickerSortParm"] = sortOrder == "Ticker_asc" ? "Ticker_desc" : "Ticker_asc";
            ViewData["InDemoSortParm"] = sortOrder == "InDemo_asc" ? "InDemo_desc" : "InDemo_asc";
            ViewData["TieredPricingSortParm"] = sortOrder == "TieredPricing_asc" ? "TieredPricing_desc" : "TieredPricing_asc";
            ViewData["isActiveSortParm"] = sortOrder == "isActive_asc" ? "isActive_desc" : "isActive_asc";

            dtCustMaster = CustMasterData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtCustMaster = CustMasterData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowCustMaster in dtCustMaster.AsEnumerable()
                        select new CustMaster() {
                            CustId = rowCustMaster.Field<Int64>("CustId")
                           ,CustName = rowCustMaster.Field<String>("CustName")
                           ,Acronym = rowCustMaster.Field<String>("Acronym")
                           ,NoofBranches = rowCustMaster.Field<Int32?>("NoofBranches")
                           ,LevelofAuthority = rowCustMaster.Field<Int32?>("LevelofAuthority")
                            ,Code = rowCustMaster.Field<String>("Code")
                           ,Ticker = rowCustMaster.Field<String>("Ticker")
                           ,InDemo = rowCustMaster.Field<Boolean>("InDemo")
                           ,TieredPricing = rowCustMaster.Field<Boolean>("TieredPricing")
                           ,isActive = rowCustMaster.Field<Boolean>("isActive")
                        };

            switch (sortOrder)
            {
                case "CustId_desc":
                    Query = Query.OrderByDescending(s => s.CustId);
                    break;
                case "CustId_asc":
                    Query = Query.OrderBy(s => s.CustId);
                    break;
                case "CustName_desc":
                    Query = Query.OrderByDescending(s => s.CustName);
                    break;
                case "CustName_asc":
                    Query = Query.OrderBy(s => s.CustName);
                    break;
                case "Acronym_desc":
                    Query = Query.OrderByDescending(s => s.Acronym);
                    break;
                case "Acronym_asc":
                    Query = Query.OrderBy(s => s.Acronym);
                    break;
                case "NoofBranches_desc":
                    Query = Query.OrderByDescending(s => s.NoofBranches);
                    break;
                case "NoofBranches_asc":
                    Query = Query.OrderBy(s => s.NoofBranches);
                    break;
                case "LevelofAuthority_desc":
                    Query = Query.OrderByDescending(s => s.LevelofAuthority);
                    break;
                case "LevelofAuthority_asc":
                    Query = Query.OrderBy(s => s.LevelofAuthority);
                    break;
                case "Code_desc":
                    Query = Query.OrderByDescending(s => s.Code);
                    break;
                case "Code_asc":
                    Query = Query.OrderBy(s => s.Code);
                    break;
                case "Ticker_desc":
                    Query = Query.OrderByDescending(s => s.Ticker);
                    break;
                case "Ticker_asc":
                    Query = Query.OrderBy(s => s.Ticker);
                    break;
                case "InDemo_desc":
                    Query = Query.OrderByDescending(s => s.InDemo);
                    break;
                case "InDemo_asc":
                    Query = Query.OrderBy(s => s.InDemo);
                    break;
                case "TieredPricing_desc":
                    Query = Query.OrderByDescending(s => s.TieredPricing);
                    break;
                case "TieredPricing_asc":
                    Query = Query.OrderBy(s => s.TieredPricing);
                    break;
                case "isActive_desc":
                    Query = Query.OrderByDescending(s => s.isActive);
                    break;
                case "isActive_asc":
                    Query = Query.OrderBy(s => s.isActive);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.CustId);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Cust Id", typeof(string));
                dt.Columns.Add("Cust Name", typeof(string));
                dt.Columns.Add("Acronym", typeof(string));
                dt.Columns.Add("Noof Branches", typeof(string));
                dt.Columns.Add("Levelof Authority", typeof(string));
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("Ticker", typeof(string));
                dt.Columns.Add("In Demo", typeof(string));
                dt.Columns.Add("Tiered Pricing", typeof(string));
                dt.Columns.Add("Is Active", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.CustId
                       ,item.CustName
                       ,item.Acronym
                       ,item.NoofBranches
                       ,item.LevelofAuthority
                       ,item.Code
                       ,item.Ticker
                       ,item.InDemo
                       ,item.TieredPricing
                       ,item.isActive
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

        // GET: /CustMaster/Details/<id>
        public ActionResult Details (Int64? CustId)
        {
            if (
                    CustId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            CustMaster CustMaster = new CustMaster();
            Int64 pCustId = System.Convert.ToInt64(CustId);
            CustMaster = CustMasterData.Select_Record(pCustId);

            if (CustMaster == null)
            {
                return HttpNotFound();
            }
            return View(CustMaster);
        }

        // GET: /CustMaster/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /CustMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /* 
         [Bind(Include=
				           "CustId"
				   + "," + "CustName"
				   + "," + "Acronym"
				   + "," + "NoofBranches"
				   + "," + "LevelofAuthority"
                   + "," + "Code"
				   + "," + "Ticker"
				   + "," + "InDemo"
				   + "," + "TieredPricing"
				   + "," + "isActive"
				  )]
             */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( CustMaster CustMaster)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                CustMaster.CustId = 0;
                bSucess = CustMasterData.Add(CustMaster);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(CustMaster);
        }

        // GET: /CustMaster/Edit/<id>
        public ActionResult Edit(
                                   Int64? CustId
                                )
        {
            if (
                    CustId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CustMaster CustMaster = new CustMaster();
            // CustMaster.CustId = System.Convert.ToInt64(CustId);
            Int64 pCustid = System.Convert.ToInt64(CustId); 
            
            CustMaster = CustMasterData.Select_Record(pCustid);

            if (CustMaster == null)
            {
                return HttpNotFound();
            }

            return View(CustMaster);
        }

        // POST: /CustMaster/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustMaster CustMaster)
        {

            CustMaster oCustMaster = new CustMaster();
            Int64 pCustId = System.Convert.ToInt64(CustMaster.CustId);
            oCustMaster = CustMasterData.Select_Record(pCustId);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = CustMasterData.Update(oCustMaster, CustMaster);
                if (bSucess == true)
                {
                    ViewData["resultUpdate"] = "1";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["resultUpdate"] = "0";
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(CustMaster);
        }

        // GET: /CustMaster/Delete/<id>
        public ActionResult Delete(
                                     Int64? CustId
                                  )
        {
            if (
                    CustId == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            CustMaster CustMaster = new CustMaster();
            Int64 pCustId = System.Convert.ToInt64(CustId);
            CustMaster = CustMasterData.Select_Record(pCustId);

            if (CustMaster == null)
            {
                return HttpNotFound();
            }
            return View(CustMaster);
        }

        // POST: /CustMaster/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int64? CustId
                                            )
        {

            CustMaster CustMaster = new CustMaster();
            Int64 pCustId = System.Convert.ToInt64(CustId);
            CustMaster = CustMasterData.Select_Record(pCustId);

            bool bSucess = false;
            bSucess = CustMasterData.Delete(CustMaster);
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
            SelectListItem Item1 = new SelectListItem { Text = "Cust Id", Value = "Cust Id" };
            SelectListItem Item2 = new SelectListItem { Text = "Cust Name", Value = "Cust Name" };
            SelectListItem Item3 = new SelectListItem { Text = "Acronym", Value = "Acronym" };
            SelectListItem Item4 = new SelectListItem { Text = "Noof Branches", Value = "Noof Branches" };
            SelectListItem Item5 = new SelectListItem { Text = "Levelof Authority", Value = "Levelof Authority" };
            SelectListItem Item6 = new SelectListItem { Text = "Code", Value = "Code" };
            SelectListItem Item7 = new SelectListItem { Text = "Ticker", Value = "Ticker" };
            SelectListItem Item8 = new SelectListItem { Text = "In Demo", Value = "In Demo" };
            SelectListItem Item9 = new SelectListItem { Text = "Tiered Pricing", Value = "Tiered Pricing" };
            SelectListItem Item10 = new SelectListItem { Text = "Is Active", Value = "Is Active" };

                 if (select == "Cust Id") { Item1.Selected = true; }
            else if (select == "Cust Name") { Item2.Selected = true; }
            else if (select == "Acronym") { Item3.Selected = true; }
            else if (select == "Noof Branches") { Item4.Selected = true; }
            else if (select == "Levelof Authority") { Item5.Selected = true; }
            else if (select == "Code") { Item6.Selected = true; }
            else if (select == "Ticker") { Item7.Selected = true; }
            else if (select == "In Demo") { Item8.Selected = true; }
            else if (select == "Tiered Pricing") { Item9.Selected = true; }
            else if (select == "Is Active") { Item10.Selected = true; }

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
            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Cust Master", "Many");
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


        [HttpPost]
        public JsonResult DetailsFrRegister()
        {
            List<SelectListItem> customers = new List<SelectListItem>();
            DataTable dtCustomer = new DataTable();
            dtCustomer = CustMasterData.CustomerSelect_All();
            List<SelectListItem> countries = new List<SelectListItem>();
            DataTable dtCountry = new DataTable();
            dtCountry = CustMasterData.CountrySelect_All();
            foreach (DataRow dr in dtCountry.Rows)
            {
                countries.Add(new SelectListItem
                {
                    Value = dr["CountryId"].ToString(),
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

 
