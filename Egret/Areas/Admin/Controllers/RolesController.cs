using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Egret.Models;
using Egret.ViewModels;

namespace Egret.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private RoleManager<Role> _roleManager;
        private UserManager<User> _userManager;

        public RolesController(RoleManager<Role> roleMgr,
                               UserManager<User> userMrg)
        {
            _roleManager = roleMgr;
            _userManager = userMrg;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ViewResult Index(List<Role> roles)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        for (int i = 0; i < roles.Count(); i++)
        //        {
        //            Context.Update(roles[i]);
        //        }
        //        Context.SaveChanges();
        //        TempData["SuccessMessage"] = "Save Complete";
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(roles);

        //}

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new Role() { Name = role.Name });
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Role Created";
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = $"Role '{role.Name}' Deleted";
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }
            return View("Index", _roleManager.Roles);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            Role role = await _roleManager.FindByIdAsync(id);
            List<User> members = new List<User>();
            List<User> nonMembers = new List<User>();
            foreach (User user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name)
                    ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    User user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user,
                            model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    User user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user,
                            model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
            }

            if (ModelState.IsValid)
            {
                TempData["SuccessMessage"] = "Save Complete";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return await Edit(model.RoleId);
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}