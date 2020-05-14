using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Egret.Models
{
    public class User : IdentityUser
    {
        public bool Active { get; set; }

        public ICollection<UserAccessGroup> UserAccessGroups { get; set; }
    }
}
