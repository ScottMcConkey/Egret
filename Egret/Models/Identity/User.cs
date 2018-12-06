using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Egret.Models
{
    public class User : IdentityUser
    {
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }

        public ICollection<UserAccessGroup> UserAccessGroups { get; set; }
    }
}
