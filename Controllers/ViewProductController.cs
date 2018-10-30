using MyProject.Repository.Data;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
namespace MyProject.Controllers
{
    public class ViewProductController : Controller
    {
        AddProductData objData = new AddProductData();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetProductsData()
        {
            var AllProductsData = objData.GetProdData();
            return new JsonResult { Data = AllProductsData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetCatDataByCustID(int CustID)
        {
            var custData = objData.GetProdCategory(CustID);
            return new JsonResult { Data = custData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetProducts(Productsearch Productsearch)
        {
            var custData = objData.GetProducts(Productsearch);
            return new JsonResult { Data = custData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult AddMultiProducts()
        {
            return View();
        }

        #region Upload Documents Code

        [HttpPost]
        public JsonResult SaveFiles(string[] description, HttpPostedFileBase files,string Tier)
        {
            int count = 0;
            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;
            bool flag = false;
            int TierCount = 0;
            Int64 CustomerID = 0;
            List<dynamic> objDynamic = new List<dynamic>();
            string strpath = System.Configuration.ConfigurationManager.AppSettings["UploadImagePath"];
            if (description.Length != 0)
            {
                TierCount = int.Parse(description[0].Split(',')[0].ToString());
                CustomerID = Int64.Parse(description[0].Split(',')[1].ToString());
            }
            
            if (Request.Files != null)
            {
                var file = Request.Files[0];
                actualFileName = file.FileName;
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                int size = file.ContentLength;
                try
                {
                    file.SaveAs(Path.Combine(Server.MapPath(strpath), fileName));
                    MyProject.Controllers.ViewRequestController.UploadedFile f = new MyProject.Controllers.ViewRequestController.UploadedFile
                    {
                        FileName = actualFileName,
                        FilePath = fileName,
                    };

                    //Save Code Here.
                    string path = Path.Combine(Server.MapPath(strpath), fileName);
                    objDynamic =  objData.InsertExcelRecords(path, TierCount, CustomerID,1);
                    flag = true;
                }
                catch (Exception ex)
                {
                    Message = "File Upload Failed!";
                }
            }

            return new JsonResult { Data = objDynamic };
        }

        #endregion
    }
}