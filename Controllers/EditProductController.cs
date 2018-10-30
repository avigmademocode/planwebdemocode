using MyProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using MyProject.Repository.Security;
namespace MyProject.Controllers
{
    public class EditProductController : Controller
    {
        AddProductData objData = new AddProductData();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        public JsonResult GetProductsDataList()
        {
            var AllProductsData = objData.GetCustProdData();
            return new JsonResult { Data = AllProductsData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        public JsonResult GetDataByProductID(Product Product)
        {
          
          var ProdData = objData.GetProdDataByProductID(Product.ProductID);
           
            return new JsonResult { Data = ProdData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #region Upload Documents Code

        [HttpPost]
        public JsonResult SaveFiles(string[] description, HttpPostedFileBase files, AddProductDTO addProductDTO)
        {
            #region comment
            string Message, fileName, actualFileName;
            string Manufacturer = string.Empty , Model = string.Empty, PartNo = string.Empty, ProductType = string.Empty, Spec = string.Empty,ProductID = string.Empty;
            ProductMasterDTO productMasterDTO = new ProductMasterDTO();
            Message = fileName = actualFileName = string.Empty;
            bool flag = false;
            string strpath = System.Configuration.ConfigurationManager.AppSettings["UploadImagePath"];
            int size = 0;
            if (Request.Files != null)
            {
                var list = new List<KeyValuePair<string, string>>();

                foreach (string key in System.Web.HttpContext.Current.Request.Form.AllKeys)
                {
                    string value = System.Web.HttpContext.Current.Request.Form[key];
                    
                    list.Add(new KeyValuePair<string, string>(key, value));

                }
                if (Request.Files.Count != 0)
                {
                    var file = Request.Files[0];
                    actualFileName = file.FileName;
                    fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    size = file.ContentLength;
                    file.SaveAs(Path.Combine(Server.MapPath(strpath), fileName));
                    MyProject.Controllers.ViewRequestController.UploadedFile f = new MyProject.Controllers.ViewRequestController.UploadedFile
                    {
                        FileName = actualFileName,
                        FilePath = fileName,
                    };
                }
              
                var custData = new List<CustomerInfo>(); 
                try
                {
                   

                    if (list.Count > 0 && list != null)
                    {
                        Manufacturer = list.Find(x => x.Key == "Manufacturer").Value.ToString();
                        Model = list.Find(x => x.Key == "Model").Value.ToString();
                        PartNo = list.Find(x => x.Key == "PartNo").Value.ToString();
                        ProductType = list.Find(x => x.Key == "ProductType").Value.ToString();
                        Spec = list.Find(x => x.Key == "Spec").Value.ToString();
                        ProductID = list.Find(x => x.Key == "ProductID").Value.ToString();
                        custData = JsonConvert.DeserializeObject<List<CustomerInfo>>(list.Find(x => x.Key == "custlist").Value.ToString());
                       // var custData1 = list.Find(x => x.Key == "custlist").Value.ToString();
                    }
                    flag = true;

                    if (!string.IsNullOrEmpty(Manufacturer) && Manufacturer != "undefined")
                    {
                        productMasterDTO.ManufacturerId = Convert.ToInt64(Manufacturer);

                    }
                    if (!string.IsNullOrEmpty(ProductType) && ProductType != "undefined")
                    {
                        productMasterDTO.ProductTypeId = Convert.ToInt64(ProductType);
                    }
                    productMasterDTO.IsActive = true;
                    productMasterDTO.Model = Model;
                    productMasterDTO.PartNo = PartNo;
                    productMasterDTO.Spec = Spec;
                    productMasterDTO.ImageID = actualFileName;
                    productMasterDTO.ImagePath = fileName;
                    productMasterDTO.ImageType = fileName;// Change name
                    productMasterDTO.ImageLength = size.ToString();
                    productMasterDTO.strProductId = ProductID;
                   var Data = objData.AddUpdateProducts(custData,productMasterDTO);
                    return new JsonResult { Data = Data };

                }
                catch (Exception ex)
                {
                    Message = "File Upload Failed!";
                    return new JsonResult { Data = null };
                }
            }

            #endregion

            return new JsonResult { Data = null };
        }

        #endregion

        public JsonResult AddTierData(ProductCustomrTierRateUI Model)
        {

            var ProdData = objData.SaveTierData(Model);

            return new JsonResult { Data = ProdData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}