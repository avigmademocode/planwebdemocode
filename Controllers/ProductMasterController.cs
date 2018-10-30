using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
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
    public class ProductMasterController : Controller
    {
        // GET: ProductMaster
       
        static long sessionCustId=0;
       
        public ActionResult Index()
        {

            sessionCustId = Convert.ToInt64(Session["CustId"].ToString());
          
            if (sessionCustId == 2)
            {
                
                return RedirectToAction("Index1", "ProductMaster");
            }
            else  /* When Session Custid other than 2 */
            {
                
                return RedirectToAction("Index2", "ProductMaster", new {Custid=sessionCustId, CategoryId=0 });
            }
            
        }

        public ActionResult Index1(string sortOrder,
                                  String SearchField,
                                  String SearchCondition,
                                  String SearchText,
                                  String Export,
                                  int? PageSize,
                                  int? page,
                                  string command)
        {
            DataTable dtProdMasterAll = new DataTable();

            if (command == "Clear")
            {
                SearchField = null;
                SearchCondition = null;
                SearchText = null;
                Session["SearchField"] = null;
                Session["SearchCondition"] = null;
                Session["SearchText"] = null;
            }
            else if (command == "Add New Record") { return RedirectToAction("Create"); }
            else if (command == "Export") { Session["Export"] = Export; }
            else if (command == "Search" | command == "Page Size")
            {
                if (!string.IsNullOrEmpty(SearchText))
                {
                    Session["SearchField"] = SearchField;
                    Session["SearchCondition"] = SearchCondition;
                    Session["SearchText"] = SearchText;
                }
            }
            if (command == "Page Size") { Session["PageSize"] = PageSize; }

            ViewData["SearchFields"] = GetFields1((Session["SearchField"] == null ? "Product Id" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ModelSortParm"] = sortOrder == "Model_asc" ? "Model_desc" : "Model_asc";
            ViewData["lstCustomers"] = getCustomers(0);
            ViewData["lstCategory"] = getBlankCategory(0);

            dtProdMasterAll = ProductMasterData.SelectAll();
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtProdMasterAll = ProductMasterData.SearchfrIndex1(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowProdMaster in dtProdMasterAll.AsEnumerable()
            select new ProductList()
            {
                
                ProductId = rowProdMaster.Field<Int64>("ProductId")
                ,
                Model = rowProdMaster.Field<String>("Model")
            };
            switch (sortOrder)
            {
                case "Model_asc":
                    Query = Query.OrderBy(s => s.Model);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.Model);
                    break;
            }

            if (command == "Export")
            {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Product Id", typeof(Int64));
                dt.Columns.Add("Model", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.ProductId
                       , item.Model
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

        public ActionResult Index2(Int64 Custid, Int64 CategoryId, 
                                   string sortOrder,
                                  String SearchField,
                                  String SearchCondition,
                                  String SearchText,
                                  String Export,
                                  int? PageSize,
                                  int? page,
                                  string command)
        {
            /*sessionCustId = Convert.ToInt64(Session["CustId"].ToString());*/
            DataTable dtProdMasterAll = new DataTable();
            Int64 pCustId =  Custid==0  ? sessionCustId : Custid;
            Int64 pCategoryid =  CategoryId;
            if (command == "Clear")
            {
                SearchField = null;
                SearchCondition = null;
                SearchText = null;
                Session["SearchField"] = null;
                Session["SearchCondition"] = null;
                Session["SearchText"] = null;
            }
            else if (command == "Add New Record") { return RedirectToAction("Create"); }
            else if (command == "Export") { Session["Export"] = Export; }
            else if (command == "Search" | command == "Page Size")
            {
                if (!string.IsNullOrEmpty(SearchText))
                {
                    Session["SearchField"] = SearchField;
                    Session["SearchCondition"] = SearchCondition;
                    Session["SearchText"] = SearchText;
                }
            }
            if (command == "Page Size") { Session["PageSize"] = PageSize; }

            ViewData["SearchFields"] = GetFields2((Session["SearchField"] == null ? "Cust Id" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ModelSortParm"] = sortOrder == "Model_asc" ? "Model_desc" : "Model_asc";
            ViewData["PriceSortParm"] = sortOrder == "Price_asc" ? "Price_desc" : "Price_asc";
            ViewData["ProdCatIdSortParm"] = sortOrder == "ProdCatId_asc" ? "ProdCatId_desc" : "ProdCatId_asc";
            ViewData["ProdCatDescSortParm"] = sortOrder == "ProdCatDesc_asc" ? "ProdCatDesc_desc" : "ProdCatDesc_asc";

            ViewData["lstCustomers"] = getCustomers(pCustId);
            ViewData["lstCategory"] = SelectCategoryfrCustomer(pCustId,pCategoryid );
            ViewData["SelCustId"] = pCustId;
            ViewData["SelProdCatId"] = pCategoryid;
         
            dtProdMasterAll = ProductMasterData.SelectAllForCustomer(pCustId, pCategoryid);

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtProdMasterAll = ProductMasterData.SearchfrIndex2(Convert.ToString(pCustId), Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowProdMaster in dtProdMasterAll.AsEnumerable()
                        select new ProductCustomerView()
                        {

                            ProductId = rowProdMaster.Field<Int64>("ProductId")
                            ,
                            Model = rowProdMaster.Field<String>("Model")
                            ,
                            Price = rowProdMaster.Field<decimal>("price")
                            ,
                            prodcatId = rowProdMaster.Field<Int64>("prodcatId")
                            ,
                            ProdCatDesc= rowProdMaster.Field<String>("ProdCatDesc")


                        };
            switch (sortOrder)
            {
  
                case "Model_desc":
                    Query = Query.OrderByDescending(s => s.Model);
                    break;
                case "Model_asc":
                    Query = Query.OrderBy(s => s.Model);
                    break;
                case "Price_desc":
                    Query = Query.OrderByDescending(s => s.Price);
                    break;
                case "Price_asc":
                    Query = Query.OrderBy(s => s.Price);
                    break;
                case "ProdCatId_desc":
                    Query = Query.OrderByDescending(s => s.prodcatId);
                    break;
                case "ProdCatId_asc":
                    Query = Query.OrderBy(s => s.prodcatId);
                    break;
                case "ProdCatDesc_desc":
                    Query = Query.OrderByDescending(s => s.ProdCatDesc);
                    break;
                case "ProdCatDesc_asc":
                    Query = Query.OrderBy(s => s.ProdCatDesc);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.ProductId);
                    break;
            }
            if (command == "Export")
            {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Product Id", typeof(Int64));
                dt.Columns.Add("Model", typeof(string));
                dt.Columns.Add("Price", typeof(decimal));
                dt.Columns.Add("Category ID", typeof(Int64));
                dt.Columns.Add("Category", typeof(string));

                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.ProductId
                       , item.Model
                       , item.Price
                       , item.prodcatId
                       , item.ProdCatDesc
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

        // GET: ProductMaster/Details/5
        public ActionResult Details(Int64? ProductId)
        {
            if (ProductId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ProductMasterDetail ProductMasterDetail = new ProductMasterDetail();
            Int64 pProductId = System.Convert.ToInt64(ProductId);
            ProductMasterDetail = ProductMasterData.Select_Record_Detail(pProductId);

            if (ProductMasterDetail == null)
            {
                return HttpNotFound();
            }
            return View(ProductMasterDetail);

            
        }

        // GET: ProductMaster/Create
        public ActionResult Create()
        {
            ViewData["lstManufacturer"] = getManufacturer(0);
            ViewData["lstProductType"] = getProductTypeList(0);
            return View();
        }

        // POST: ProductMaster/Create
        [HttpPost]
        public ActionResult Create(ProductMaster prdmaster)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool bSucess = false;
                    prdmaster.ProductId = 0;
                    bSucess = ProductMasterData.Add(prdmaster);
                    if (bSucess == true)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Can Not Insert");
                        return View();
                    }

                }
                catch
                {
                    return View();
                }
            }
            else
            { return View(); }
            
        }

        // GET: ProductMaster/Edit/5
        public ActionResult Edit(Int64 Productid)
        {
            ViewData["lstManufacturer"] = getManufacturer(Productid);
            ViewData["lstProductType"] = getProductTypeList(Productid);
            ProductMaster ProductMaster = new ProductMaster();
            ProductMaster = ProductMasterData.Select_Record(Productid);
            if (ProductMaster == null)
            {
                return HttpNotFound();
            }
            return View(ProductMaster);
            
        }

        // POST: ProductMaster/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Productid, ProductMaster prdmaster)
        {
            ProductMaster oProdMaster = new ProductMaster();
            Int64 pProductId = System.Convert.ToInt64(prdmaster.ProductId);
            oProdMaster = ProductMasterData.Select_Record(pProductId);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ProductMasterData.Update(oProdMaster, prdmaster);
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

            return View(prdmaster);
        }

        // GET: ProductMaster/Delete/5
        public ActionResult Delete(Int64? Productid)
        {


            if (Productid == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ProductMasterDetail ProductMasterDetail = new ProductMasterDetail();
            Int64 pProductId = System.Convert.ToInt64(Productid);
            ProductMasterDetail = ProductMasterData.Select_Record_Detail(pProductId);

            if (ProductMasterDetail == null)
            {
                return HttpNotFound();
            }
            return View(ProductMasterDetail);

           
        }

        // POST: ProductMaster/Delete/5
        [HttpPost]
        public ActionResult Delete(Int64 productId, FormCollection collection)
        {
            ProductMaster ProductMaster = new ProductMaster();
            Int64 pCustId = System.Convert.ToInt64(productId);
            ProductMaster = ProductMasterData.Select_Record(pCustId);

            bool bSucess = false;
            bSucess = ProductMasterData.Delete(ProductMaster);
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

        private static List<SelectListItem> GetFields1(String select)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem Item1 = new SelectListItem { Text = "Product Id", Value = "Product Id" };
            SelectListItem Item2 = new SelectListItem { Text = "Model", Value = "Model" };

            if (select == "Product Id") { Item1.Selected = true; }
            else if (select == "Model") { Item2.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            return list.ToList();
        }
        private static List<SelectListItem> GetFields2(String select)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem Item1 = new SelectListItem { Text = "Product Id", Value = "Product Id" };
            SelectListItem Item2 = new SelectListItem { Text = "Model", Value = "Model" };
            SelectListItem Item3 = new SelectListItem { Text = "Price", Value = "Price" };
            SelectListItem Item4 = new SelectListItem { Text = "Category Id", Value = "Category Id" };
            SelectListItem Item5 = new SelectListItem { Text = "Category", Value = "Category" };
            if (select == "Product Id") { Item1.Selected = true; }
            else if (select == "Model") { Item2.Selected = true; }
            else if (select == "Price") { Item3.Selected = true; }
            else if (select == "Category Id") { Item4.Selected = true; }
            else if (select == "Category") { Item5.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);

            return list.ToList();
        }
        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Product Master", "Many");
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

        public static List<SelectListItem> getCustomers(Int64 pCustId)
        {
            List<SelectListItem> lstcustomers = new List<SelectListItem>();
            DataTable dtcustomer = new DataTable();
            dtcustomer= ProductMasterData.SelectCustomer();
            lstcustomers.Add(new SelectListItem
            {
                Value="0",
                Text="---Select---"
            });

            foreach (DataRow dr in dtcustomer.Rows)
            {
                if (pCustId.ToString() == dr["CustId"].ToString())
                {
                    lstcustomers.Add(new SelectListItem
                    {
                        Value = dr["CustId"].ToString(),
                        Text = dr["Custname"].ToString(),
                        Selected = true
                    });
                }
                else
                { 
                    lstcustomers.Add(new SelectListItem
                    {
                        Value = dr["CustId"].ToString(),
                        Text = dr["Custname"].ToString()
                    
                    });
                }
            }
            return lstcustomers.ToList();
        }


        public static List<SelectListItem> getBlankCategory(Int64 pCustId)
        {
            List<SelectListItem> lstcategory = new List<SelectListItem>();
            DataTable dtcustomer = new DataTable();
            dtcustomer = ProductMasterData.SelectCustomer();
            lstcategory.Add(new SelectListItem
            {
                Value = "0",
                Text = "---Select---"
            });

          /*  foreach (DataRow dr in dtcustomer.Rows)
            {
                lstcustomers.Add(new SelectListItem
                {
                    Value = dr["CustId"].ToString(),
                    Text = dr["Custname"].ToString()
                });
            }*/
            return lstcategory.ToList();
        }

        public static List<SelectListItem> SelectCategoryfrCustomer(Int64 pCustId, Int64 pCategoryId)
        {
            List<SelectListItem> lstcategory = new List<SelectListItem>();
            DataTable dtcategory = new DataTable();
            dtcategory = ProductMasterData.SelectCategoryForCustomer(pCustId);
            lstcategory.Add(new SelectListItem
            {
                Value = "0",
                Text = "---Select---"
            });
            foreach (DataRow dr in dtcategory.Rows)
            {
                if (pCategoryId.ToString() == dr["ProdCatId"].ToString())
                {

                    lstcategory.Add(new SelectListItem
                    {
                        Value = dr["ProdCatId"].ToString(),
                        Text = dr["ProdCatDesc"].ToString(),
                        Selected = true
                    });
                }
                else {
                    lstcategory.Add(new SelectListItem
                    {
                        Value = dr["ProdCatId"].ToString(),
                        Text = dr["ProdCatDesc"].ToString()
                       
                    });
                }
            }
            return lstcategory.ToList();

        }

        public static List<SelectListItem> getManufacturer(Int64 manufid)
        {
            List<SelectListItem> lstmanufacturer = new List<SelectListItem>();
            DataTable dtcustomer = new DataTable();
            dtcustomer = ProductMasterData.ManufactureList();
  
            foreach (DataRow dr in dtcustomer.Rows)
            {
                if (manufid.ToString() == dr["ManufacturerId"].ToString())
                {
                    lstmanufacturer.Add(new SelectListItem
                    {
                        Value = dr["ManufacturerId"].ToString(),
                        Text = dr["manufacturerdesc"].ToString(),
                        Selected = true
                    });

                }
                else
                {
                    lstmanufacturer.Add(new SelectListItem
                    {
                        Value = dr["ManufacturerId"].ToString(),
                        Text = dr["manufacturerdesc"].ToString()
                        
                    });


                }
            }
            return lstmanufacturer.ToList();
        }

        public static List<SelectListItem> getProductTypeList(Int64 ProductTypeId)
        {
            List<SelectListItem> lstproducttype = new List<SelectListItem>();
            DataTable dtcustomer = new DataTable();
            dtcustomer = ProductMasterData.ProductTypeList();
            lstproducttype.Add(new SelectListItem
            {
                Value = "0",
                Text = "---Select---"
            });
            foreach (DataRow dr in dtcustomer.Rows)
            {
                if (ProductTypeId.ToString() == dr["ProductTypeId"].ToString())
                {
                    lstproducttype.Add(new SelectListItem
                    {
                        Value = dr["ProductTypeId"].ToString(),
                        Text = dr["ProductType"].ToString(),
                        Selected = true
                    });

                }
                else
                {
                    lstproducttype.Add(new SelectListItem
                    {
                        Value = dr["ProductTypeId"].ToString(),
                        Text = dr["ProductType"].ToString()

                    });


                }
            }
            return lstproducttype.ToList();
        }

    }
}
