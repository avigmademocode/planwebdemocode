using MyProject.Data;
using MyProject.Repository.Data;
using MyProject.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Repository.Security;
namespace MyProject.Controllers
{
    public class NewRequestController : Controller
    {
        //
        // GET: /NewRequest/

        CustRequestData objCustRequest = new CustRequestData();
        CustOrderData objCustOrderData = new CustOrderData();
        SecurityHelper securityHelper = new SecurityHelper();
        OrderData orderData = new OrderData();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCustomer(int type)
        {

            var custData = objCustRequest.GetCustMaster(type);

            return new JsonResult { Data = custData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; 
        }

        public JsonResult GetDeleveryAndTerms(int Custid)
        {

            var custData = objCustRequest.GetShipTo(Custid);

            return new JsonResult { Data = custData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; 
        }


        public JsonResult GetCustDetails(int Custid, int BranchID)
        //  public JsonResult GetCustDetails(int Custid)
        {

            var custData = objCustRequest.GetCustDetials(Custid, BranchID);
            return new JsonResult { Data = custData, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; 
        }

        public JsonResult SaveNewRequest(CustRequest Model)
        {
          
            if (string.IsNullOrEmpty(Model.OrderID))
            {
                var  OrderData = objCustRequest.SaveOrderRequest(Model);
                return new JsonResult { Data = OrderData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                if (Model.Type.Equals("1"))
                {
                    var OrderData = objCustRequest.SaveOrderRequest(Model);
                    return new JsonResult { Data = OrderData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
                else
                {
                    var OrderData = objCustOrderData.SaveOrderRequest(Model);
                    return new JsonResult { Data = OrderData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
             
            }
           
         
        }

        public JsonResult GetCustomersData(string OrderID,string Type)
        {
            if (Type == "1")
            {
                 

                var OrderData = objCustOrderData.GetCustOrderList(Convert.ToInt64(OrderID), Convert.ToInt32(Type));
                return new JsonResult { Data = OrderData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
               

                var OrderData = objCustOrderData.GetCustOrderList(Convert.ToInt64(securityHelper.Decrypt(OrderID, false)), Convert.ToInt32(Type));
                return new JsonResult { Data = OrderData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
     
        }

    }
}