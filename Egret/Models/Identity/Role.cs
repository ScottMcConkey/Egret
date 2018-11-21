﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Egret.Models
{
    public class Role : IdentityRole
    {
        public ICollection<AccessGroupRole> AccessGroupRoles { get; set; }
    }
}
