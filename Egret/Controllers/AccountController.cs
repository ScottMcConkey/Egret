using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel;
using Microsoft.Extensions;
using Egret.Models;
using Egret.ViewModels;

namespace Egret.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public AccountController(UserManager<User> userMgr,
            SignInManager<User> signinMgr)
        {
            userManager = userMgr;
            signInManager = signinMgr;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel details)
        {
            //User person = ();
            //IdentityResult passwordChangeResult = await userManager.ChangePasswordAsync(User.Identity.GetUserId(), details.CurrentPassword, details.NewPassword);
           

            //if (passwordChangeResult.Succeeded)
            //{
            //    ViewData["SuccessMessage"] = "User Password Updated";
            //    return RedirectToAction("Index", "Home");
            //}
            return View(details);

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(
                        user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
            }
            return View(details);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}