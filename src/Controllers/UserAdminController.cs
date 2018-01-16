using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Egret.Models;

namespace Egret.Controllers
{
    public class UserAdminController : Controller
    {
        private UserManager<User> userManager;

        public UserAdminController(UserManager<User> usrMgr)
        {
            userManager = usrMgr;
        }

        public ViewResult Index() => View(userManager.Users);
    }
}