using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egret.Models;
using Egret.DataAccess;

namespace Egret.ViewModels
{
    public class AccessGroupViewModel
    {
        public AccessGroup AccessGroup { get; set; }

        public List<Role> Roles { get; set; }
    }
}
