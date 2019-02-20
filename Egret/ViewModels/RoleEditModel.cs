using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egret.Models;

namespace Egret.ViewModels
{
    public class RoleEditModel
    {
        public Role Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }
}
