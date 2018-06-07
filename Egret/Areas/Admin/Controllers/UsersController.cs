﻿using System;
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
        private UserManager<User> userManager;
        
        public UsersController(UserManager<User> usrMgr)
        {
            userManager = usrMgr;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(userManager.Users.ToList());
        }

        [HttpGet]
        public ViewResult Edit()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
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