using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egret.Models;

namespace Egret.Models
{
    public class UserAccessGroup
    {
        public string UserId { get; set; }

        public int AccessGroupId { get; set; }

        public User User { get; set; }

        public AccessGroup AccessGroup { get; set; }
    }
}
