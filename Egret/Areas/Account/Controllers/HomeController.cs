using Egret.Models;
using Egret.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Egret.Areas.Account.Controllers
{
    [Area("Account")]
    public class HomeController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private static ILogger _logger;

        public HomeController(UserManager<User> userMgr,
            SignInManager<User> signinMgr, ILogger<HomeController> logger)
        {
            _userManager = userMgr;
            _signInManager = signinMgr;
            _logger = logger;
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
            User user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            //User person = User.Identity;
            IdentityResult passwordChangeResult = await _userManager.ChangePasswordAsync(user, details.CurrentPassword, details.NewPassword);


            if (passwordChangeResult.Succeeded)
            {
                TempData["SuccessMessage"] = "User Password Updated";
                return RedirectToAction("Index", "Home", new { area = "" } );
            }
            return View(details);

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(details.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(
                        user, details.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                _logger.LogWarning($"Failed login attempt from {Request.HttpContext.Connection.RemoteIpAddress}");
                ModelState.AddModelError(nameof(LoginModel.Email), "Invalid email or password");
            }
            return View(details);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" } );
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}