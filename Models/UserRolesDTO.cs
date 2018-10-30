using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class UserRolesDTO
    {
        public Int64 RoleId { get; set; }
        public String RoleName { get; set;}
        public String Description { get; set; }
        public Boolean IsActive { get; set; }

        public int Type { get; set; }
        public Int64 UserId { get; set; }
        public Boolean IsEdit { get; set; }
        public Boolean IsDelete { get; set; }
        public int Ischange { get; set; }
        public int val { get; set; }

    }

    public class UserRoles
    {

        public String RoleDesc { get; set; }
        public Boolean IsActive { get; set; }
        public int Type { get; set; }
    }
}