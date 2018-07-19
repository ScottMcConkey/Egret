using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Egret.Models;

namespace Egret.ViewModels
{
    public class CreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }

    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }

    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }

        public string RoleId { get; set; }

        public string[] IdsToAdd { get; set; }

        public string[] IdsToDelete { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
