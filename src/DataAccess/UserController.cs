using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Egret.Models;

namespace Egret.DataAccess
{
    public class UserController : Controller
    {
        //private UserManager<User> userManager;
        //
        //public UserController(UserManager<User> usrMgr)
        //{
        //    userManager = usrMgr;
        //}
        //
        public IActionResult Index()
        {
            return View(new Dictionary<string, object> { ["Placeholder"] = "Placeholder" });
        }
    }
}