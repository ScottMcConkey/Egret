using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Egret.Models
{
    public class UserRole
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}
