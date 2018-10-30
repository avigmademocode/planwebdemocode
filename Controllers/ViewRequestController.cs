using MyProject.Models;
using MyProject.Repository.Data;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MyProject.Repository.Security;
using MyProject.Repository.Library;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Net.Http.Headers;

namespace MyProject.Controllers
{
    public class ViewRequestController : Controller
    {
        OrderApprovalData objOrderData = new OrderApprovalData();
        SecurityHelper securityHelper = new SecurityHelper();
        AddProductData addProductData = new AddProductData(); 
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ViewRequestOrderDetails(string OrderID)
        {
            var AllStatus = objOrderData.GetOrderDetails(securityHelper.Decrypt(OrderID,false));
            // var AllStatus = BudgetCodeWiseOrderDetailData.GetCustBudgetMastr(OrderID);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult ChangedStatus(StatusChange model)
        {
            var AllStatus = objOrderData.GetUpdateCustomerStatus(model);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        #region Upload Documents Code

        [HttpPost]

         public JsonResult SaveFiles(string[] description,string[] OrderID, string[] DocumentGroupID, HttpPostedFileBase files)
        //public JsonResult SaveFiles(string[] descriptions, HttpFileCollectionBase files)
        {
            #region comment
            /*
            string Message, fileName, actualFileName;
            Message = fileName = actualFileName = string.Empty;
            bool flag = false;
            if (Request.Files != null)
            {
                var file = Request.Files[0];
                actualFileName = file.FileName;
                fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                int size = file.ContentLength;
                try
                {
                    file.SaveAs(Path.Combine(Server.MapPath("~/image/docs"), fileName));
                    UploadedFile f = new UploadedFile
                    {
                        FileName = actualFileName,
                        FilePath = fileName,
                    };

                    //Save Code Here.
                }
                catch (Exception ex)
                {
                    Message = "File Upload Failed!";
                }
            }

    */
            #endregion
            string Message, fileName, actualFileName, strfilepath,StrFileType;
            Message = fileName = actualFileName = string.Empty;
            string strpath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
            bool flag = false;
            Int64 documentgroupid = 0;
            if (Request.Files != null)
            {
                if (DocumentGroupID.Length >0)
                {
                    if (!string.IsNullOrEmpty(DocumentGroupID[0]))
                    {
                        documentgroupid = Convert.ToInt64(DocumentGroupID[0]);
                        if (documentgroupid == 0)
                        {
                            documentgroupid = objOrderData.AddOrderFileDocumentmaster(securityHelper.Decrypt(OrderID[0], false), Request.Files.Count.ToString());
                        }
                    }
                    else
                    {
                        documentgroupid = objOrderData.AddOrderFileDocumentmaster(securityHelper.Decrypt(OrderID[0], false), Request.Files.Count.ToString());
                    }
                }
                else
                {
                    documentgroupid = objOrderData.AddOrderFileDocumentmaster(securityHelper.Decrypt(OrderID[0], false), Request.Files.Count.ToString());
                }
               
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    actualFileName = file.FileName;
                    fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string  size = file.ContentLength.ToString();
                    //  strfilepath = strpath + "\\" + fileName;
                    strfilepath = strpath + "//" + fileName;
                    StrFileType = file.ContentType;
                    OrderFilesData OrderFilesData = new OrderFilesData();
                    try
                    {
                       // file.SaveAs(Path.Combine(strpath, fileName));
                        //  file.SaveAs(Path.Combine(Server.MapPath("~/image/docs"), fileName));
                        file.SaveAs(Path.Combine(Server.MapPath(strpath), fileName));
                        UploadedFile f = new UploadedFile
                        {
                            FileName = actualFileName,
                            FilePath = fileName,
                        };
                        //addProductData.InsertExcelRecords(strfilepath, TierCount);


                        //Save Code Here.
                        OrderFilesData.DocumentGroupId = documentgroupid;
                        OrderFilesData.FileLocation = strfilepath;
                        OrderFilesData.FileName = actualFileName;
                        OrderFilesData.FileType = StrFileType;
                        OrderFilesData.FileSize = size;
                        OrderFilesData.Description = description[i];
                        OrderFilesData.FileId = objOrderData.AddOrderFiles(OrderFilesData);

                    }
                    catch (Exception ex)
                    {
                        Message = "File Upload Failed!";
                    }
                }
            }
            var UploadData = objOrderData.GetOrderFileDetails(Convert.ToInt64(securityHelper.Decrypt(OrderID[0], false)));
            UploadData.Add(documentgroupid);
            return new JsonResult { Data = UploadData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            
        }

        #endregion

        #region Upload Documents Code

        [HttpPost]

        public JsonResult GetRequestedDocs(string OrderID)
        {
            return new JsonResult { Data = objOrderData.GetOrderFileDetails(Convert.ToInt64(securityHelper.Decrypt(OrderID, false))) };
        }

        #endregion

        //email sent
        public JsonResult SendEmailData(EmailFormatDTO model)
        {

            var AllStatus = objOrderData.SendEmail(model);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public class UploadedFile
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
        }
        [HttpPost]
        public HttpResponseMessage PrintExcel(StatusChange model)
        {
            ExcelPackage excel = new ExcelPackage();
            ExcelManager excelManager = new ExcelManager();
            ExcelDTO excelDTO = new ExcelDTO();
            HttpResponseMessage result = null;
           
            excel = excelManager.ExportDataToExcel(excelDTO);
            byte[] bytes = excel.GetAsByteArray();
            result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(bytes)
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
             
            return result;
        }

    }
}

