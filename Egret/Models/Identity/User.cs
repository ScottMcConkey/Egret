using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Egret.Models
{
    public class User : IdentityUser
    {
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
    }
}
