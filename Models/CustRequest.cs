using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Collections.Generic;

namespace MyProject.Models
{
    //public class CustomerInfo
    //{
    //    public Int64 CustId { get; set; }
    //    public String CustName { get; set; }
    //    public String ProdCatId { get; set; }
    //    public String Price { get; set; }
    //    public String IsActive { get; set; }
    //    public String ExpDate { get; set; }
    //    public String PCRId { get; set; }

    //}
    public class CustomerInfo
    {
        public Int64 CustId { get; set; }
        public string CustName { get; set; }
        public object ProdCatId { get; set; }
        public object Price { get; set; }
        public object IsActive { get; set; }
        public object ExpDate { get; set; }
        public object PCRId { get; set; }
        public string strCustId { get; set; }
        public bool UseItemGroups { get; set; }
    }
    public class ApproverDet
    {

        public string ApproverNameDisplay { get; set; }
        public int ApproverSerial { get; set; }
       
    }

    public class orderID
    {
        public String strorderID { get; set; }
    }

    public class AuthorityLevel
    {

        public int level { get; set; }
        public bool Desg { get; set; }
        public bool RefNoAuto { get; set; }
    }
    public class CustomerShipTo
    {
        public Int64 BranchID { get; set; }
        public String DisplayName { get; set; }

    }

    public class CustomerDeliveryTerms
    {
        public Int64 IncoTermID { get; set; }
        public String IncoTermInfo { get; set; }

    }

    public class CustomerShipDetials
    {
        public String ShipAddress1 { get; set; }
        public String ShipAddress2 { get; set; }
        public String ShipAddress3 { get; set; }
        public Nullable<Int64> ShipCity { get; set; }
        public String ShipCityName { get; set; }
        public Nullable<Int64> ShipCountry { get; set; }
        public String ShipCountryName { get; set; }
        public String ShipState { get; set; }
        public String ShipPin { get; set; }
        public String ShipEmail { get; set; }
        public String ShipPhoneNo { get; set; }
        public String ContactPerson { get; set; }
        public String FullAddress { get; set; }

        public String BillAddress1 { get; set; }
        public String BillAddress2 { get; set; }
        public String BillAddress3 { get; set; }
        public Nullable<Int64> BillCity { get; set; }
        public String BillCityName { get; set; }
        public Nullable<Int64> BillCountry { get; set; }
        public String BillCountryName { get; set; }
        public String BillState { get; set; }
        public String BillPin { get; set; }
    }
    public class CustCity
    {
        public Int64 CityID { get; set; }
        public String CityName { get; set; }
    }
    public class Custtype
    {
      public String type { get; set; }
    }

    public class CustCountry
    {
        public Int64 CountryId { get; set; }
        public String CountryName { get; set; }
    }

    public class CustRequest
    {
        public String OrderID { get; set; }
        public String selectedCustomer { get; set; }
        public String Refernce { get; set; }
        public String Department { get; set; }
        public String Office { get; set; }
        public String BranchID { get; set; }
        public String DeliveryTo { get; set; }



        public String SAdd1 { get; set; }
        public String SAdd2 { get; set; }
        public String SAdd3 { get; set; }
        public String SCity { get; set; }
        public String SCityName { get; set; }
        public String SState { get; set; }
        public String SZip { get; set; }
        public String SCountry { get; set; }
        public String SCountryName { get; set; }
        public String Sname { get; set; }
        public String Sphone { get; set; }
        public String Semail { get; set; }


        public String BAdd1 { get; set; }
        public String BAdd2 { get; set; }
        public String BAdd3 { get; set; }
        public String BCity { get; set; }
        public String BCityName { get; set; }
        public String BState { get; set; }
        public String BZip { get; set; }
        public String BCountry { get; set; }
        public String BCountryName { get; set; }
        public String ApproverDetails { get; set; }
        public String Type { get; set; }




    }

    public class ApproverDetail
    {
        public String AprId { get; set; }
        public String AprName { get; set; }
        public String AprTitle { get; set; }
        public String AprEmail { get; set; }
    }

    public class ProductReqCategory
    {
        public Int64 CatId { get; set; }
        public String CatName { get; set; }
    }

    public class ProductDetails
    {
        public Int64 ProdID { get; set; }
        public String strProdID { get; set; }
        public Int64 ProdCatID { get; set; }
        public String ProdName { get; set; }
        public String ProductName { get; set; }
        public String ProdPart { get; set; }
        public Decimal ProdPrice { get; set; }
        public String ImagePath { get; set; }
        public String ImageID { get; set; }
        public String Desc { get; set; }
        public int TierCount { get; set; }
        public List<ProductTierPrice> TierData { get; set; }
    }

    public class ProductTierPrice
    {
        public Int64 CustId { get; set; }
        public Int64 ProdID { get; set; }
        public Int64 Serial { get; set; }
        public int Qty { get; set; }
        public Decimal Price { get; set; }
    }
 

    public class CustOrderDetails
    {
        public String ODID { get; set; }
        public String ProdID { get; set; }
        public String ProdName { get; set; }
        public String ProdPrice { get; set; }
        public String TotalPrice { get; set; }
        public String Quantity { get; set; }
    }
    public class CustOrderListDetails
    {
        public string CustOrderDetails { get; set; }
        public int Type { get; set; }
        // public List <CustOrderDetails> listorder { get; set; }
    }
    

  

    public class CustOrderValue
    {
        public String CustomerID { get; set; }
        public String UserID { get; set; }
        public String OrderID { get; set; }
        public String TotalAmount { get; set; }
    }

    public class CustOrderDetail
    {
        public String ReferenceNo { get; set; }
        public String Department { get; set; }

        public String DsiplayName { get; set; }
        public String CustomerName { get; set; }
        public Decimal TotalOrderAmount { get; set; }
        public String StatusName { get; set; }
        public Int64 StatusID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String UserName { get; set; }
        public Int64 CustID { get; set; }


        public String CustName { get; set; }
        public String HOEmailId { get; set; }
        public String IncoTermDesc { get; set; }

        public String SEmailId { get; set; }
        public String SContactPerson { get; set; }
        public String SContactNo { get; set; }
                

        public String BEmailId { get; set; }
        public String BContactPerson { get; set; }
        public String BContactNo { get; set; }

        public Decimal Feight { get; set; }
        public Decimal TaxValue { get; set; }

    }


    public class CustomerOrderAddress
    {
        public String SAdd1 { get; set; }
        public String SAdd2 { get; set; }
        public String SAdd3 { get; set; }

        public String SCity { get; set; }
        public String SState { get; set; }
        public String SZip { get; set; }
        public String SCountry { get; set; }

        public String BAdd1 { get; set; }
        public String BAdd2 { get; set; }
        public String BAdd3 { get; set; }
  
    }
    
    public class ProductCount
    {
        public int ProdCount { get; set; }
    }
    public class ApproverDetails
    {
        public Int64 OAID { get; set; }
        public Int64 OrderId { get; set; }
        public String AprName { get; set; }
        public String AprTitle { get; set; }
        public String AprEmail { get; set; }
        public String Comments { get; set; }
    }

    public class CustProdDetails
    {
        
        public Int64 ODID { get; set; }
        public Int64 ProdID { get; set; }
        public String PartNo { get; set; }
        public Decimal ProdPrice { get; set; }
        public Decimal TotalPrice { get; set; }
        public int Quantity { get; set; }
        public Boolean ItFormRequired { get; set; }
        public Boolean SoftwareFormRequired { get; set; }
        public Boolean LanguageFormRequired { get; set; }
    }


    public class CustOrderLog
    {
        public String CurrentStatus { get; set; }
        public String NewStatus { get; set; }
        public String FullName { get; set; }
        public String ActionDate { get; set; }
        public String Remarks { get; set; }
    }

    public class SaveOrder
    {
        public String strorderID { get; set; }
        public String strCustId { get; set; }
        public int type { get; set; }

    }

    public class SaveTempOrder
    {
        public int RetVal { get; set; }
        public Int64 TempOrderID { get; set; }


    }

    public class PlansonComment
    {
        public String Comments { get; set; }


    }
}