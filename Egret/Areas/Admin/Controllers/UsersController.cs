using System;
using System.Collections.Generic;
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
        private UserManager<User> _userManager;
        private IUserValidator<User> _userValidator;
        private IPasswordValidator<User> _passwordValidator;
        private IPasswordHasher<User> _passwordHasher;
        private static ILogger _logger;

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
        public async Task<IActionResult> Edit(string id, string email, string password, bool isactive)
        {
            User user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.Email = email;
                user.IsActive = isactive;
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
                    IsActive = true
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
        public async Task<IActionResult> EditAccessGroups(string id, UserAccessGroupsModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = Context.Users.Where(x => x.Id == id)
                .Include(x => x.UserAccessGroups)
                    .ThenInclude(y => y.AccessGroup)
                        .ThenInclude(a => a.AccessGroupRoles)
                    //        .ThenInclude(gr => gr.Role)
                .SingleOrDefault();

            model.UserName = user.UserName;

            if (ModelState.IsValid)
            {
                // Remove all existing rels
                foreach (UserAccessGroup userGroup in Context.UserAccessGroups.Where(x => x.UserId == id))
                {
                    Context.UserAccessGroups.Remove(userGroup);
                }

                Context.SaveChanges();

                IdentityResult result;

                // Set new rels
                foreach (AccessGroup group in model.AccessGroups.Where(x => x.RelationshipPresent == true))
                {
                    var localAccessGroup = Context.AccessGroups.Where(x => x.Id == group.Id)
                        .Include(x => x.AccessGroupRoles)
                            .ThenInclude(y => y.Role)
                        .SingleOrDefault();

                    var roleNames = localAccessGroup.AccessGroupRoles.Select(x => x.Role.NormalizedName).ToList();

                    User user1 = await _userManager.FindByIdAsync(user.Id);
                    if (user1 != null)
                    {
                        foreach (string name in roleNames)
                        {
                            result = await _userManager.AddToRoleAsync(user1, name);
                        } 
                    }

                    var newUserGroup = new UserAccessGroup() { AccessGroup = localAccessGroup, User = user };
                    Context.UserAccessGroups.Add(newUserGroup);

                }

                Context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
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
            var roles = Context.Roles.FromSql(
                          "select agr.roleid" +
                          "  from user_accessgroups uag" +
                          "  join accessgroup_roles agr" +
                          "    on agr.accessgroupid = uag.accessgroupid" +
                          " where uag.userid = @userid", user.Id)
                      .Select(x => x.Id).ToList();

            IdentityResult result = await _userManager.AddToRolesAsync(user, roles);
            return result;
        }
    }
}