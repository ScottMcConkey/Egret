using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Egret.Models;
using Egret.ViewModels;
using Egret.Controllers;

namespace Egret.DataAccess
{
    [Area("Admin")]
    [Authorize(Roles = "Admin_Access")]
    public class UsersController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserValidator<User> _userValidator;
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger _logger;

        public UsersController(EgretContext context,
            UserManager<User> usrMgr,
            IUserValidator<User> userValid,
            IPasswordValidator<User> passValid,
            IPasswordHasher<User> passwordHash,
            ILogger<UsersController> logger)
                : base(context)
        {
            _userManager = usrMgr;
            _userValidator = userValid;
            _passwordValidator = passValid;
            _passwordHasher = passwordHash;
            _logger = logger;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, string email, string password, bool active)
        {
            User user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.Email = email;
                user.Active = active;
                IdentityResult validEmail = await _userValidator.ValidateAsync(_userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await _passwordValidator.ValidateAsync(_userManager,
                        user, password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user,
                            password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if ((validEmail.Succeeded && validPass == null)
                        || (validEmail.Succeeded
                        && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = model.Name,
                    Email = model.Email,
                    Active = true
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

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

        [HttpGet]
        public async Task<IActionResult> EditAccessGroups(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await Context.Users.Where(x => x.Id == id).SingleOrDefaultAsync();
            var accessGroups = Context.AccessGroups.AsNoTracking().ToList();
            var relatedAccessGroups = Context.UserAccessGroups.AsNoTracking().Where(x => x.UserId == id).Select(x => x.AccessGroupId).ToList();

            accessGroups.Where(x => relatedAccessGroups.Contains(x.Id)).ToList().ForEach(y => y.RelationshipPresent = true);

            var presentation = new UserAccessGroupsModel
            {
                UserName = user.UserName,
                AccessGroups = accessGroups
            };

            return View(presentation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccessGroups(string id, UserAccessGroupsModel presentation)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = Context.Users.Where(x => x.Id == id).SingleOrDefault();

            presentation.UserName = user.UserName;

            if (ModelState.IsValid)
            {
                // Remove all User Access Group rels
                foreach (UserAccessGroup userGroup in Context.UserAccessGroups.Where(x => x.UserId == id))
                {
                    Context.UserAccessGroups.Remove(userGroup);
                }

                // Set all User Access Group rels
                foreach (AccessGroup group in presentation.AccessGroups.Where(x => x.RelationshipPresent == true))
                {
                    var localAccessGroup = Context.AccessGroups.AsNoTracking().Where(x => x.Id == group.Id).SingleOrDefault();

                    var newUserGroup = new UserAccessGroup() { AccessGroupId = localAccessGroup.Id, UserId = user.Id };
                    Context.UserAccessGroups.Add(newUserGroup);
                }
                Context.SaveChanges();

                // Remove all existing rels
                IdentityResult removeRoles = await RemoveUserRoles(user);

                // Set new rels
                IdentityResult addRoles = await AddUserRoles(user);

                TempData["SuccessMessage"] = $"Access Groups Updated for User {user.UserName}";
                return RedirectToAction(nameof(Index));
            }

            return View(presentation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "User Deleted";
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View("Index", _userManager.Users);
        }

        [NonAction]
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [NonAction]
        private async Task<IdentityResult> RemoveUserRoles(User user)
        {
            var rolesToRemoveUserFrom = await _userManager.GetRolesAsync(user);
            IdentityResult result = await _userManager.RemoveFromRolesAsync(user, rolesToRemoveUserFrom.ToArray());
            return result;
        }

        [NonAction]
        private async Task<IdentityResult> AddUserRoles(User user)
        {
            var roles = Context.Roles.FromSqlRaw(
                          "select r.name" +
                          "  from user_accessgroups uag" +
                          "  join accessgroup_roles agr" +
                          "    on agr.accessgroupid = uag.accessgroupid" +
                          "  join roles r" +
                          "    on r.id = agr.roleid" +
                          " where uag.userid = {0}", user.Id)
                          .Select(x => x.Name).ToList();

            IdentityResult result = await _userManager.AddToRolesAsync(user, roles);
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> UserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var roleNames = await _userManager.GetRolesAsync(user);

            List<Role> roles = new List<Role>();

            foreach (var name in roleNames)
            {
                roles.Add(Context.Roles.AsNoTracking().Where(x => x.Name == name).SingleOrDefault());
            }

            roles = roles.OrderBy(x => x.Name).ToList();

            return View("UserRoles", roles);
        }
    }
}