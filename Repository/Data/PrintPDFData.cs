using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Repository.Data
{
    public class PrintPDFData
    {
        OrderApprovalData OrderApprovalData = new OrderApprovalData();
        public List<dynamic> GetCustOrderData(string strOrderID)
        {
            Int64 OrderID = Convert.ToInt64(strOrderID);
            List<dynamic> objDynamic = new List<dynamic>();
            objDynamic.Add(OrderApprovalData.GetOrderDetails(strOrderID));


            return objDynamic;
        }
    }
}