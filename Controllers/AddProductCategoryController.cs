using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Repository.Data;
using MyProject.Models;
using System.IO;

namespace MyProject.Controllers
{
    public class AddProductCategoryController : Controller
    {
        // GET: AddProductCategory
        ProductCategoryData productCategoryData = new ProductCategoryData();
        AddProductData objData = new AddProductData();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetProdCategryData(int Custid)
        {
            if (Custid == 0)
            {
                var AllStatus = productCategoryData.GetProdCatgry(Custid, 2);
                return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                var AllStatus = productCategoryData.GetProdCatgry(Custid, 1);
                return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }

        public JsonResult SaveProductCatData(ProductCategoryDetail productCategoryDetail)
        {

            var AllStatus = productCategoryData.AddProdCatgry(productCategoryDetail);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult UpdateCategories()
        {
            return View();
        }

        #region Upload Documents Code

        [HttpPost]
        public JsonResult UpdateCategories(string[] description)
        {
            int count = 0;
            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;
            bool flag = false;
            int TierCount = 0;
            Int64 CustomerID = 0;
            List<dynamic> objDynamic = new List<dynamic>();
            string strpath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
            if (description.Length != 0)
            {
               CustomerID = Int64.Parse(description[0]);
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
                    objDynamic = objData.InsertExcelRecords(path, TierCount, CustomerID, 2);
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