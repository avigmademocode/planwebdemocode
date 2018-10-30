using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class BudgetCodeWiseOrderDetailDTO
    {
        public String OrderID { get; set; }
        public String Plansoncomment { get; set; }
        public String Approvercomment { get; set; }
        public String Approveremail { get; set; }
        public String BudgetSplit { get; set; }
        public String BudgetFreight { get; set; }
        public String BudgetList { get; set; }
        public int Type { get; set; }
    }

    public class BudgetCodeOrderDetail
    {
        public Int64 BudgetOrderSerialId { get; set; }
        public Int64 BudgetId { get; set; }
        public Decimal Value { get; set; }
        public Boolean Serial { get; set; }
        public String BudgetTitle { get; set; }
        public Int64 BudgetOrderPkey { get; set; }
        public String strValue { get; set; }

    }
    public class BudgetCodeMstr
    {
        public Int64 BudgetId { get; set; }
        public String BudgetTitle { get; set; }
        public Int64 CustId { get; set; }
        public Boolean isRequired { get; set; }
        public int FldLength { get; set; }
        public int Serial { get; set; }
        public Int64 DependOn { get; set; }

    }

    public class GetBudgetCodeMstr
    {
        public Int64 BudgetId { get; set; }
        public String BudgetTitle { get; set; }

        public Boolean isRequired { get; set; }
        public int FldLength { get; set; }
        public int Serial { get; set; }
        public Int64 DependOn { get; set; }
        public String Data { get; set; }

    }

    public class BudgetCodeOrder
    {
        public Int64 BudgetOrderId { get; set; }
        public Int64 OrderId { get; set; }
        public Int64 CustId { get; set; }
        public int NoofBudgetCodes { get; set; }
        public Boolean BudgetCodeSplitOn { get; set; }
        public Boolean BudgetCodetoFreight { get; set; }

        public String Plansoncomment { get; set; }
        public String Approvercomment { get; set; }
        public String Approveremail { get; set; }
        public Int64 UserID { get; set; }

    }
    public class BudgtCodeOrdrItmDtls
    {
        public Int64 BudgetOrderSerialItemId { get; set; }
        public Int64 BudgetOrderSerialId { get; set; }
        public Int64 ProductId { get; set; }
        public Decimal Amount { get; set; }
        public Boolean IsFreightAmount { get; set; }
        public Boolean BudgetCodetoFreight { get; set; }


    }
    public class BudgetCodewiseOrdrTotl
    {
        public Int64 BudgetOrderSerialId { get; set; }
        public Int64 BudgetOrderId { get; set; }
        public int Serial { get; set; }
        public Decimal Total { get; set; }
        public List<BudgetCodedetail> Data { get; set; }
    }

    public class BudgetCodewiseOrderDetail
    {
        public Int64 BudgetOrderPkey { get; set; }
        public Int64 BudgetOrderSerialId { get; set; }
        public int BudgetId { get; set; }
        public string Value { get; set; }
        public int Serial { get; set; }
    }
    public class OrdrDetail
    {
        public Int64 ODID { get; set; }
        public int Serial { get; set; }
        public Int64 ProductId { get; set; }
        public String ProductName { get; set; }
        public int Qty { get; set; }
        public Decimal Rate { get; set; }
        public Decimal Amount { get; set; }
        public String Data { get; set; }

    }
    public class BudgetCodedetail
    {
        public Int64 BudgetId { get; set; }
        public String BudgetTitle { get; set; }
        public Int64 CustId { get; set; }
        public Boolean isRequired { get; set; }
        public int FldLength { get; set; }
        public int Serial { get; set; }
        public Int64 DependOn { get; set; }

        public Int64 BudgetOrderSerialItemId { get; set; }
        public Int64 ODID { get; set; }
        public int prodSerial { get; set; }
        public Int64 ProductId { get; set; }
        public String ProductName { get; set; }
        public int Qty { get; set; }
        public Decimal Rate { get; set; }
        public Decimal Amount { get; set; }
        public String Data { get; set; }
    }
    

}