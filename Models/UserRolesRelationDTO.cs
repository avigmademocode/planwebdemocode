using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class UserRolesRelationDTO
    {
        public Int64 URRId { get; set; }
        public Int64 CurUserId { get; set; }
        public Int64 UserId { get; set; }
        public Int64 RoleId { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsCat { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
       

    }



    public class UserRolesRelation
    {
         
        public Int64 UserId { get; set; }
        public String UserRoledet { get; set; }
        public int Type { get; set; }
    }
}