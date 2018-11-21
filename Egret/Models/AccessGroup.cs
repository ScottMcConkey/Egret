using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Models
{
    public class AccessGroup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<AccessGroupRole> AccessGroupRoles { get; set; }
    }
}
