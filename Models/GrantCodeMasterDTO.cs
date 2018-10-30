using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class GrantCodeMasterDTO
    {
        public String OrderID { get; set; }
        public String GrantCodeMaster { get; set; }


    }
    public class GrantCodeMaster
    {
        public Int64 GrantId { get; set; }
        public String GrantTitle { get; set; }
        public Int64 CustId { get; set; }
        public Boolean isRequired { get; set; }
        public int fldlength { get; set; }
        public int Serial { get; set; }
        public Int64 DependOn { get; set; }
        public string Data { get; set; }
    }
    public class GrantCodeOrderDetails
    {
        public Int64 GrantOrderSerialId { get; set; }
        public Int64 GrantId { get; set; }
        public String Value { get; set; }
        public int Serial { get; set; }
    }
    public class GrantCodeOrderItemDetails
    {
        public Int64 GrantOrderserialItemId { get; set; }
        public Int64 GrantOrderSerialId { get; set; }
        public Int64 ProductId { get; set; }
        public int Serial { get; set; }
        public int Qty { get; set; }
        public Decimal Rate { get; set; }
        public Decimal Amount { get; set; }
        public Boolean isDelayedTime { get; set; }
        public Boolean isTaxAmount { get; set; }
        public String TaxName { get; set; }
        public Decimal Total { get; set; }

    }


    public class GrantCodeOrders
    {
        public Int64 GrantOrderId { get; set; }
        public Int64 OrderId { get; set; }
        public Int64 CustId { get; set; }
        public int NoofGrantCodes { get; set; }
    }

    public class GrantCodeWiseOrderTotal
    {
        public Int64 GrantOrderSerialId { get; set; }
        public Int64 GrantOrderId { get; set; }
        public int Serial { get; set; }
        public Decimal Total { get; set; }


    }

    public class GrantOrdrDetail
    {
        public Int64 ODID { get; set; }
        public int Serial { get; set; }
        public Int64 ProductId { get; set; }
        public String ProductName { get; set; }
        public int Qty { get; set; }
        public Decimal Rate { get; set; }
        public Decimal Amount { get; set; }
  
    }

    public class GrantDetail
    {
        public Int64 GrantIdOrderID { get; set; }
        public Int64 GrantOrderSerialId { get; set; }
        public Int64 GrantId { get; set; }
        public String GrantTitle { get; set; }
        public String Value { get; set; }
        public String Data { get; set; }
       
        
    }
    public class GrantSerial
    {
        public Int64 GrantOrderSerial { get; set; }
        public int Serial { get; set; }
        public Decimal Total { get; set; }
        public List <GrantData> Data { get; set; }
        
    }

    public class OrderTotal
    {
        public Decimal TotalOrderAmount { get; set; }
    }
    

    public class GrantorderSerial
    {
        public int GrantOrderSerial { get; set; }
        public String Data { get; set; }
        public String SelRate { get; set; }
        public String SelQty { get; set; }
        public String SelAmount { get; set; }
        public String SelSubTotal { get; set; }
        public String SelODID { get; set; }
    }

    public class GrantData
    {
       
        public String Data { get; set; }
        public Decimal SelRate { get; set; }
        public int SelQty { get; set; }
        public Decimal SelAmount { get; set; }
        public Decimal SelSubTotal { get; set; }
        public Int64 SelODID { get; set; }
        public String ProductName { get; set; }
        public Int64 ProductID { get; set; }
        public Int64 GrantOrderserialItemId { get; set; }

        public Int64 ProductIDTemp { get; set; }
        public Decimal SelAmountTemp { get; set; }
        public int SelQtyTemp { get; set; }
        public Decimal SelRateTemp { get; set; }



        public List<GrantOrdrDetail> Items { get; set; }
        public GrantOrdrDetail SelectedItem { get; set; }

    }
    public class Item
    {
        public int ODID { get; set; }
        public int Serial { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public Decimal Rate { get; set; }
        public Decimal Amount { get; set; }
       
}

public class Datum
{
    public object Data { get; set; }
    public object SelRate { get; set; }
    public string SelQty { get; set; }
    public object SelAmount { get; set; }
    public object SelSubTotal { get; set; }
    public object SelODID { get; set; }
    public List<Item> Items { get; set; }
    public string SelectedItem { get; set; }
    
}

public class RootObject
{
    public int GrantOrderSerial { get; set; }
    public int Serial { get; set; }
    public List<Datum> Data { get; set; }
   
}

}
