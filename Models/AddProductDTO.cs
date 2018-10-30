using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class AddProductDTO
    {
        public String Model { get; set; }
        public String PartNo { get; set; }
        public String Spec { get; set; }
        public String Manufacturer { get; set; }
        public String ProductType { get; set; }
       public String custlist { get; set; }

    }
    public class Product
    {
        public String ProductID { get; set; }
    }
    public class Productsearch
    {
        public String CustID { get; set; }
        public String CatID { get; set; }
        public String IsActive { get; set; }
    }

    public class ProductDetail
    {
        public String PartNo { get; set; }
        public String Model { get; set; }
        public Int64 ManufacturerId { get; set; }
        public String ManufacturerDesc { get; set; }
        public String ProductType { get; set; }
        public Int64 ProductTypeId { get; set; }
    }

    public class ProductCustDetail
    {
        public Int64 PCRId { get; set; }
        public String CustName { get; set; }
        public Int64 CustId { get; set; }
        public Decimal Price { get; set; }
        public String ProdCatDesc { get; set; }
        public Int64 ProdCatId { get; set; }
        public String ExpDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class ProductimageDetail
    {

        public Int64 PIID { get; set; }
        public String Spec { get; set; }
        public String ImagePath { get; set; }
        public String ImageID { get; set; }

    }

    public class ManufacturerDet
    {
        public Int64 ManufacturerId { get; set; }
        public String ManufacturerDesc { get; set; }
    }

    public class ProductTypeDet
    {
        public Int64 ProductTypeId { get; set; }
        public String ProductType { get; set; }


    }
    public class ProductMasterDTO
    {

        public Int64 ProductId { get; set; }
        public String PartNo { get; set; }
        public Int64 UserId { get; set; }
        public String Model { get; set; }
        public Int64 ManufacturerId { get; set; }
        public Int64 ProductTypeId { get; set; }
        public Boolean IsActive { get; set; }
        public int Type { get; set; }
        public String  Spec { get; set; }
        public String ImageType { get; set; }
        public String ImageLength { get; set; }
        public String ImageID { get; set; }
        public String ImagePath { get; set; }
        public String strProductId { get; set; }

    }

    public class ProductCustomrRate
    {
        public Int64 PCRId { get; set; }
        public Int64 CustId { get; set; }
        public Int64 ProductId { get; set; }
        public Int64 UserId { get; set; }
        public Int64 Serial { get; set; }
        public Decimal Price { get; set; }
        public Int64 ProdCatId { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveUpto { get; set; }
        public int Type { get; set; }
    }

    public class ProductCustomrTierRate
    {
        public Int64 PCTRId { get; set; }
        public Int64 CustId { get; set; }
        public Int64 ProductId { get; set; }
        public Int64 Serial { get; set; }
        public int Qty { get; set; }
        public Int64 UserId { get; set; }
        public Decimal Price { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public String EffectiveUpto { get; set; }
        public int Type { get; set; }

        public string CustName { get; set; }
        public string StrEffectiveFrom { get; set; }

    }

    public class DataInform
    {
        public int TotalReccount { get; set; }
        public int TotaProcesscount { get; set; }
        public int TotalDupcount { get; set; }

    }    

    public class ProductCustomrTierRateUI
    {
        public Int64 PCTRId { get; set; }
        public Int64 CustId { get; set; }
        public string StrProductId { get; set; }
        public Int64 ProductId { get; set; }
        public Int64 Serial { get; set; }
        public int Qty { get; set; }
        public Int64 UserId { get; set; }
        public Decimal Price { get; set; }
        public Boolean IsActive { get; set; }
        public String EffectiveFrom { get; set; }
        public String EffectiveUpto { get; set; }
        public int Type { get; set; }
        public string TierDetails { get; set; }
    }
}