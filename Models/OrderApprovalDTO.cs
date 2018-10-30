using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class OrderApprovalDTO
    {
        public String OrderId { get; set; }
        public String ReferenceNo { get; set; }
        public String Department { get; set; }
        public String CreatedOn { get; set; }
        public String StatusName { get; set; }
        public String CountryName { get; set; }
        public String CityName { get; set; }
        public Decimal TotalOrderAmount { get; set; }
    }

    public class CustList
    {
        public Int64 CustId { get; set; }

    }

    public class StatusList
    {
        public Int64 StatusId { get; set; }

    }

    public class CountryList
    {
        public Int64 CountryId { get; set; }

    }

    public class SearchRequestData
    {
        public String CustomerID { get; set; }
        public String CountryId { get; set; }
        public String CancelOrder { get; set; }
        public String Status { get; set; }
        public String StatusID { get; set; }
    }

    public class StatusDetail
    {
        public Int64 StatusId { get; set; }
        public String StatusName { get; set; }
        public String StatusChk { get; set; }

    }

    public class CustomerStatusDetail
    {
        public Int64 StatusId { get; set; }
        public String StatusName { get; set; }
        public int StatusOrder { get; set; }
        public Boolean StatusCheck { get; set; }

    }

    public class SatusChange
    {
        public Int64 OrderId { get; set; }
        public Int64 StatusId { get; set; }
    }

 
    public class OrderFilesData
    {
        public Int64 FileId { get; set; }
        public Int64 DocumentGroupId { get; set; }
        public String FileName { get; set; }
        public String FileSize { get; set; }
        public String FileType { get; set; }
        public String FileLocation { get; set; }
        public String Description { get; set; }
    }

    public class OrderFilesDocumentData
    {
        public Int64 DocumentGroupId { get; set; }
        public Int64 OrderId { get; set; }
        public Int64 CustId { get; set; }
        public Int64 StatusId { get; set; }
        public int NoofDocuments { get; set; }

    }
    public class FileData
    {
        public Int64 FileId { get; set; }
        public Int64 DocumentGroupId { get; set; }
        public String FileName { get; set; }
        public String FileSize { get; set; }
        public String FileType { get; set; }
        public String FileLocation { get; set; }
        public Int64 OrderId { get; set; }
        public Int64 StatusId { get; set; }
        public int NoofDocuments { get; set; }

    }
    public class StatusChange
    {
        public String OrderID { get; set; }
        public String Type { get; set; }
        public String ChangedStatus { get; set; }
        public String Reason { get; set; }
        public String LeadTime { get; set; }
        public String IncoID { get; set; }
        public String FullStatus { get; set; }
        public String SalesOrderNo { get; set; }
        public String ApproverID { get; set; }
        public Boolean SendEmail { get; set; }
    }

    public class ButtonList
    {
        public int ButtonId { get; set; }
        public Boolean IsPlanson { get; set; }
        public Boolean IsUser { get; set; }

    }

    public class OrderFileData
    {
        public Int64 FileId { get; set; }
        public String FileName { get; set; }
        public String FileLocation { get; set; }
        public String Description { get; set; }
        public Int64 DocumentGroupId { get; set; }
    }
    public class ContactTypes
    {
        public Int64 ContactTypeId { get; set; }
        public String ContactType { get; set; }
        public Boolean OrderIdRequired { get; set; }

    }
    public class OrdFreightDetails
    {
        public Int64 OrderFreightId { get; set; }
        public String FreightTitle { get; set; }
        public String Value { get; set; }
        public Boolean isTaxApplicable { get; set; }
        public Boolean isFreightAmount { get; set; }
        public Boolean isLeadTime { get; set; }

    }

}