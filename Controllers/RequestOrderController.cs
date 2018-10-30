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
    public class RequestOrderController : Controller
    {
        // GET: Shipping
        OrderApproverData orderApproverData = new OrderApproverData();
        SecurityHelper securityHelper = new SecurityHelper();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ViewRequestOrderDetails(orderID OrderID)
        {
            var AllStatus = orderApproverData.GetOrderDetails(securityHelper.Decrypt(OrderID.strorderID, false));
            // var AllStatus = BudgetCodeWiseOrderDetailData.GetCustBudgetMastr(OrderID);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult ChangedStatus(StatusChange model)
        {
            var AllStatus = orderApproverData.GetUpdateCustomerStatus(model);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //email sent
        public JsonResult SendEmailData(EmailFormatDTO model)
        {

            var AllStatus = orderApproverData.SendEmail(model);
            return new JsonResult { Data = AllStatus, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}