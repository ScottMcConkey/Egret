using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Egret.Models
{
    public class Role : IdentityRole
    {
        public Role() : base() { }
        public Role(string name) : base(name) { }

        public Role(string name, string description)
            : base(name)
        {
            base.Name = name;

            Description = description;
        }

        public string Description { get; set; }

        public ICollection<AccessGroupRole> AccessGroupRoles { get; set; }
    }
}
