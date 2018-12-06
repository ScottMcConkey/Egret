using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egret.Models;

namespace Egret.ViewModels
{
    public class UserGroupViewModel
    {
		public string UserName { get; set; }

        public List<AccessGroup> AccessGroups { get; set; } = new List<AccessGroup>();
    }
}
