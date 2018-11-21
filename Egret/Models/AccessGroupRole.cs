using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Models
{
    public class AccessGroupRole
    {
        public int AccessGroupId { get; set; }

        public string RoleId { get; set; }

        public Role Role { get; set; }

        public AccessGroup AccessGroup { get; set; }

    }
}
