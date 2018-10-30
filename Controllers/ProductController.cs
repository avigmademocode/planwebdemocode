using MyProject.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Models;
using MyProject.Repository.Security;
namespace MyProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        CustRequestData objCustRequest = new CustRequestData();
        CustOrderData objcustOrderData = new CustOrderData();
        SecurityHelper securityHelper = new SecurityHelper();
        public JsonResult GetCategoryData(int Custid)
        {
            var catData = objCustRequest.GetCategory(Custid);
            return new JsonResult { Data = catData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; 
        }

        public JsonResult GetProductList(int Custid, int ProdCatId)
        {
            var ProdData = objCustRequest.GetProducts(Custid, ProdCatId,1,string.Empty);
            return new JsonResult { Data = ProdData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; 
        }

        public JsonResult GetOrderDetails(orderID orderID)
        {
            Int64 Order = Convert.ToInt64(securityHelper.Decrypt(orderID.strorderID, false));
            var ProdData = objcustOrderData.GetCustOrderDetailList(Order);
            return new JsonResult { Data = ProdData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}