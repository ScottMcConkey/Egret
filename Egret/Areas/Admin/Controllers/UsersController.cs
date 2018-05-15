using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Egret.Models;
using Egret.ViewModels;

namespace Egret.DataAccess
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private UserManager<AppUser> userManager;
        
        public UsersController(UserManager<AppUser> usrMgr)
        {
            userManager = usrMgr;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(userManager.Users.ToList());
        }

        [HttpGet]
        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email
                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);

        }
    }
}