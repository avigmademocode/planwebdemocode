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
    public class grantsController : Controller
    {

      //  DataTable dtgrants = new DataTable();
      //  DataTable dtcustomer = new DataTable();

      //  // GET: /grants/
      //  public ActionResult Index(string sortOrder,  
      //                            String SearchField,
      //                            String SearchCondition,
      //                            String SearchText,
      //                            String Export,
      //                            int? PageSize,
      //                            int? page, 
      //                            string command)
      //  {

      //      if (command == "Show All") {
      //          SearchField = null;
      //          SearchCondition = null;
      //          SearchText = null;
      //          Session["SearchField"] = null;
      //          Session["SearchCondition"] = null;
      //          Session["SearchText"] = null; } 
      //      else if (command == "Add New Record") { return RedirectToAction("Create"); } 
      //      else if (command == "Export") { Session["Export"] = Export; } 
      //      else if (command == "Search" | command == "Page Size") {
      //          if (!string.IsNullOrEmpty(SearchText)) {
      //              Session["SearchField"] = SearchField;
      //              Session["SearchCondition"] = SearchCondition;
      //              Session["SearchText"] = SearchText; }
      //          } 
      //      if (command == "Page Size") { Session["PageSize"] = PageSize; }

      //      ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Id" : Convert.ToString(Session["SearchField"])));
      //      ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
      //      ViewData["SearchText"] = Session["SearchText"];
      //      ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
      //      ViewData["PageSizes"] = Library.GetPageSizes();

      //      ViewData["CurrentSort"] = sortOrder;
      //      ViewData["IdSortParm"] = sortOrder == "Id_asc" ? "Id_desc" : "Id_asc";
      //      ViewData["CustomerIdSortParm"] = sortOrder == "CustomerId_asc" ? "CustomerId_desc" : "CustomerId_asc";
      //      ViewData["PrNoSortParm"] = sortOrder == "PrNo_asc" ? "PrNo_desc" : "PrNo_asc";
      //      ViewData["T1SortParm"] = sortOrder == "T1_asc" ? "T1_desc" : "T1_asc";
      //      ViewData["AcctCodeSortParm"] = sortOrder == "AcctCode_asc" ? "AcctCode_desc" : "AcctCode_asc";
      //      ViewData["T3SortParm"] = sortOrder == "T3_asc" ? "T3_desc" : "T3_asc";
      //      ViewData["T5SortParm"] = sortOrder == "T5_asc" ? "T5_desc" : "T5_asc";
      //      ViewData["T2SortParm"] = sortOrder == "T2_asc" ? "T2_desc" : "T2_asc";

      //      dtgrants = grantsData.SelectAll();
      //      dtcustomer = grants_customerData.SelectAll();

      //      try
      //      {
      //          if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
      //          {
      //              dtgrants = grantsData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
      //          }
      //      }
      //      catch { }

      //      var Query = from rowgrants in dtgrants.AsEnumerable()
      //                  join rowcustomer in dtcustomer.AsEnumerable() on rowgrants.Field<Int32>("CustomerId") equals rowcustomer.Field<Int32>("CustomerId")
      //                  select new grants() {
      //                      Id = rowgrants.Field<Int32>("Id")
      //                     ,
      //                      customer = new customer() 
      //                      {
      //                             CustomerId = rowcustomer.Field<Int32>("CustomerId")
      //                      }
      //                     ,PrNo = rowgrants.Field<String>("PrNo")
      //                     ,T1 = rowgrants.Field<String>("T1")
      //                     ,AcctCode = rowgrants.Field<String>("AcctCode")
      //                     ,T3 = rowgrants.Field<String>("T3")
      //                     ,T5 = rowgrants.Field<String>("T5")
      //                     ,T2 = rowgrants.Field<String>("T2")
      //                  };

      //      switch (sortOrder)
      //      {
      //          case "Id_desc":
      //              Query = Query.OrderByDescending(s => s.Id);
      //              break;
      //          case "Id_asc":
      //              Query = Query.OrderBy(s => s.Id);
      //              break;
      //          case "CustomerId_desc":
      //              Query = Query.OrderByDescending(s => s.customer.CustomerId);
      //              break;
      //          case "CustomerId_asc":
      //              Query = Query.OrderBy(s => s.customer.CustomerId);
      //              break;
      //          case "PrNo_desc":
      //              Query = Query.OrderByDescending(s => s.PrNo);
      //              break;
      //          case "PrNo_asc":
      //              Query = Query.OrderBy(s => s.PrNo);
      //              break;
      //          case "T1_desc":
      //              Query = Query.OrderByDescending(s => s.T1);
      //              break;
      //          case "T1_asc":
      //              Query = Query.OrderBy(s => s.T1);
      //              break;
      //          case "AcctCode_desc":
      //              Query = Query.OrderByDescending(s => s.AcctCode);
      //              break;
      //          case "AcctCode_asc":
      //              Query = Query.OrderBy(s => s.AcctCode);
      //              break;
      //          case "T3_desc":
      //              Query = Query.OrderByDescending(s => s.T3);
      //              break;
      //          case "T3_asc":
      //              Query = Query.OrderBy(s => s.T3);
      //              break;
      //          case "T5_desc":
      //              Query = Query.OrderByDescending(s => s.T5);
      //              break;
      //          case "T5_asc":
      //              Query = Query.OrderBy(s => s.T5);
      //              break;
      //          case "T2_desc":
      //              Query = Query.OrderByDescending(s => s.T2);
      //              break;
      //          case "T2_asc":
      //              Query = Query.OrderBy(s => s.T2);
      //              break;
      //          default:  // Name ascending 
      //              Query = Query.OrderBy(s => s.Id);
      //              break;
      //      }

      //      if (command == "Export") {
      //          GridView gv = new GridView();
      //          DataTable dt = new DataTable();
      //          dt.Columns.Add("Id", typeof(string));
      //          dt.Columns.Add("Customer Id", typeof(string));
      //          dt.Columns.Add("Pr No", typeof(string));
      //          dt.Columns.Add("T1", typeof(string));
      //          dt.Columns.Add("Acct Code", typeof(string));
      //          dt.Columns.Add("T3", typeof(string));
      //          dt.Columns.Add("T5", typeof(string));
      //          dt.Columns.Add("T2", typeof(string));
      //          foreach (var item in Query)
      //          {
      //              dt.Rows.Add(
      //                  item.Id
      //                 ,item.customer.CustomerId
      //                 ,item.PrNo
      //                 ,item.T1
      //                 ,item.AcctCode
      //                 ,item.T3
      //                 ,item.T5
      //                 ,item.T2
      //              );
      //          }
      //          gv.DataSource = dt;
      //          gv.DataBind();
      //          ExportData(Export, gv, dt);
      //      }

      //      int pageNumber = (page ?? 1);
      //      int? pageSZ = (Convert.ToInt32(Session["PageSize"]) == 0 ? 5 : Convert.ToInt32(Session["PageSize"]));
      //      return View(Query.ToPagedList(pageNumber, (pageSZ ?? 5)));
      //  }

      //  // GET: /grants/Details/<id>
      //  public ActionResult Details(
      //                                Int32? Id
      //                             )
      //  {
      //      if (
      //              Id == null
      //         )
      //      {
      //          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      //      }

      //      dtcustomer = grants_customerData.SelectAll();

      //      grants grants = new grants();
      //      grants.Id = System.Convert.ToInt32(Id);
      //      grants = grantsData.Select_Record(grants);
      //      grants.customer = new customer()
      //      {
      //          CustomerId = (Int32)grants.CustomerId
      //      };

      //      if (grants == null)
      //      {
      //          return HttpNotFound();
      //      }
      //      return View(grants);
      //  }

      //  // GET: /grants/Create
      //  public ActionResult Create()
      //  {
      //  // ComboBox
      //      ViewData["CustomerId"] = new SelectList(grants_customerData.List(), "CustomerId", "CustomerId");

      //      return View();
      //  }

      //  // POST: /grants/Create
      //  // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      //  // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      //  [HttpPost]
      //  [ValidateAntiForgeryToken]
      //  public ActionResult Create([Bind(Include=
				  //         "Id"
				  // + "," + "CustomerId"
				  // + "," + "PrNo"
				  // + "," + "T1"
				  // + "," + "AcctCode"
				  // + "," + "T3"
				  // + "," + "T5"
				  // + "," + "T2"
				  //)] grants grants)
      //  {
      //      if (ModelState.IsValid)
      //      {
      //          bool bSucess = false;
      //          bSucess = grantsData.Add(grants);
      //          if (bSucess == true)
      //          {
      //              return RedirectToAction("Index");
      //          }
      //          else
      //          {
      //              ModelState.AddModelError("", "Can Not Insert");
      //          }
      //      }
      //  // ComboBox
      //      ViewData["CustomerId"] = new SelectList(grants_customerData.List(), "CustomerId", "CustomerId", grants.CustomerId);

      //      return View(grants);
      //  }

      //  // GET: /grants/Edit/<id>
      //  public ActionResult Edit(
      //                             Int32? Id
      //                          )
      //  {
      //      if (
      //              Id == null
      //         )
      //      {
      //          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      //      }

      //      grants grants = new grants();
      //      grants.Id = System.Convert.ToInt32(Id);
      //      grants = grantsData.Select_Record(grants);

      //      if (grants == null)
      //      {
      //          return HttpNotFound();
      //      }
      //  // ComboBox
      //      ViewData["CustomerId"] = new SelectList(grants_customerData.List(), "CustomerId", "CustomerId", grants.CustomerId);

      //      return View(grants);
      //  }

      //  // POST: /grants/Edit/<id>
      //  // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      //  // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      //  [HttpPost, ActionName("Edit")]
      //  [ValidateAntiForgeryToken]
      //  public ActionResult Edit(grants grants)
      //  {

      //      grants ogrants = new grants();
      //      ogrants.Id = System.Convert.ToInt32(grants.Id);
      //      ogrants = grantsData.Select_Record(grants);

      //      if (ModelState.IsValid)
      //      {
      //          bool bSucess = false;
      //          bSucess = grantsData.Update(ogrants, grants);
      //          if (bSucess == true)
      //          {
      //              return RedirectToAction("Index");
      //          }
      //          else
      //          {
      //              ModelState.AddModelError("", "Can Not Update");
      //          }
      //      }
      //  // ComboBox
      //      ViewData["CustomerId"] = new SelectList(grants_customerData.List(), "CustomerId", "CustomerId", grants.CustomerId);

      //      return View(grants);
      //  }

      //  // GET: /grants/Delete/<id>
      //  public ActionResult Delete(
      //                               Int32? Id
      //                            )
      //  {
      //      if (
      //              Id == null
      //         )
      //      {
      //          return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      //      }

      //      dtcustomer = grants_customerData.SelectAll();

      //      grants grants = new grants();
      //      grants.Id = System.Convert.ToInt32(Id);
      //      grants = grantsData.Select_Record(grants);
      //      grants.customer = new customer()
      //      {
      //          CustomerId = (Int32)grants.CustomerId
      //      };

      //      if (grants == null)
      //      {
      //          return HttpNotFound();
      //      }
      //      return View(grants);
      //  }

      //  // POST: /grants/Delete/<id>
      //  [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
      //  public ActionResult DeleteConfirmed(
      //                                      Int32? Id
      //                                      )
      //  {

      //      grants grants = new grants();
      //      grants.Id = System.Convert.ToInt32(Id);
      //      grants = grantsData.Select_Record(grants);

      //      bool bSucess = false;
      //      bSucess = grantsData.Delete(grants);
      //      if (bSucess == true)
      //      {
      //          return RedirectToAction("Index");
      //      }
      //      else
      //      {
      //          ModelState.AddModelError("", "Can Not Delete");
      //      }
      //      return null;
      //  }

      //  protected override void Dispose(bool disposing)
      //  {
      //      base.Dispose(disposing);
      //  }

      //  private static List<SelectListItem> GetFields(String select)
      //  {
      //      List<SelectListItem> list = new List<SelectListItem>();
      //      SelectListItem Item1 = new SelectListItem { Text = "Id", Value = "Id" };
      //      SelectListItem Item2 = new SelectListItem { Text = "Customer Id", Value = "Customer Id" };
      //      SelectListItem Item3 = new SelectListItem { Text = "Pr No", Value = "Pr No" };
      //      SelectListItem Item4 = new SelectListItem { Text = "T1", Value = "T1" };
      //      SelectListItem Item5 = new SelectListItem { Text = "Acct Code", Value = "Acct Code" };
      //      SelectListItem Item6 = new SelectListItem { Text = "T3", Value = "T3" };
      //      SelectListItem Item7 = new SelectListItem { Text = "T5", Value = "T5" };
      //      SelectListItem Item8 = new SelectListItem { Text = "T2", Value = "T2" };

      //           if (select == "Id") { Item1.Selected = true; }
      //      else if (select == "Customer Id") { Item2.Selected = true; }
      //      else if (select == "Pr No") { Item3.Selected = true; }
      //      else if (select == "T1") { Item4.Selected = true; }
      //      else if (select == "Acct Code") { Item5.Selected = true; }
      //      else if (select == "T3") { Item6.Selected = true; }
      //      else if (select == "T5") { Item7.Selected = true; }
      //      else if (select == "T2") { Item8.Selected = true; }

      //      list.Add(Item1);
      //      list.Add(Item2);
      //      list.Add(Item3);
      //      list.Add(Item4);
      //      list.Add(Item5);
      //      list.Add(Item6);
      //      list.Add(Item7);
      //      list.Add(Item8);

      //      return list.ToList();
      //  }

      //  private void ExportData(String Export, GridView gv, DataTable dt)
      //  {
      //      if (Export == "Pdf")
      //      {
      //          PDFform pdfForm = new PDFform(dt, "Dbo.Grants", "Many");
      //          Document document = pdfForm.CreateDocument();
      //          PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
      //          renderer.Document = document;
      //          renderer.RenderDocument();

      //          MemoryStream stream = new MemoryStream();
      //          renderer.PdfDocument.Save(stream, false);

      //          Response.Clear();
      //          Response.AddHeader("content-disposition", "attachment;filename=" + "Report.pdf");
      //          Response.ContentType = "application/Pdf.pdf";
      //          Response.BinaryWrite(stream.ToArray());
      //          Response.Flush();
      //          Response.End();
      //      }
      //      else
      //      {
      //          Response.ClearContent();
      //          Response.Buffer = true;
      //          if (Export == "Excel")
      //          {
      //              Response.AddHeader("content-disposition", "attachment;filename=" + "Report.xls");
      //              Response.ContentType = "application/Excel.xls";
      //          }
      //          else if (Export == "Word")
      //          {
      //              Response.AddHeader("content-disposition", "attachment;filename=" + "Report.doc");
      //              Response.ContentType = "application/Word.doc";
      //          }
      //          Response.Charset = "";
      //          StringWriter sw = new StringWriter();
      //          HtmlTextWriter htw = new HtmlTextWriter(sw);
      //          gv.RenderControl(htw);
      //          Response.Output.Write(sw.ToString());
      //          Response.Flush();
      //          Response.End();
      //      }
      //  }

    }
}
 
