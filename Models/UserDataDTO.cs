using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class UserDataDTO
    {
        public Int64 UserId { get; set; }
        public String strUserId { get; set; }
        public String LoginId { get; set; }
        public String Pwd { get; set; }
        public String confirmPwd { get; set; }
        public String UserName { get; set; }
        public Int64 CustId { get; set; }
        public Int64 BranchId { get; set; }
        public Boolean IsPlansonUser { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public Int64 CityId { get; set; }
        public Int64 CountryId { get; set; }
        public String Logins { get; set; }
        public String Last_Login { get; set; }
        public Boolean Locked { get; set; }
        public Boolean IsActive { get; set; }
    
        public int Type { get; set; }
        public int Status { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }

    }
    public class UserDataDetails
    {
        public String strUserData { get; set; }
    }

    }