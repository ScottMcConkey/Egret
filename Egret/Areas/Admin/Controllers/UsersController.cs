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
        private UserManager<User> userManager;
        private IUserValidator<User> userValidator;
        private IPasswordValidator<User> passwordValidator;
        private IPasswordHasher<User> passwordHasher;
        private static ILogger _logger;

        public UsersController(EgretContext context,
            UserManager<User> usrMgr,
            IUserValidator<User> userValid,
            IPasswordValidator<User> passValid,
            IPasswordHasher<User> passwordHash,
            ILogger<UsersController> logger)
                : base(context)
        {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passValid;
            passwordHasher = passwordHash;
            _logger = logger;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(userManager.Users.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await userManager.FindByIdAsync(id);
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
            User user = await userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.Email = email;
                user.IsActive = isactive;
                IdentityResult validEmail = await userValidator.ValidateAsync(userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager,
                        user, password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = passwordHasher.HashPassword(user,
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
                    IdentityResult result = await userManager.UpdateAsync(user);
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

        [HttpGet]
        public IActionResult EditAccessGroups(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<UserAccessGroup> userAccessGroups = Context.UserAccessGroups.AsNoTracking().Where(x => x.UserId == id).ToList();
            List<int> accessGroupdIds = userAccessGroups.Select(x => x.AccessGroupId).ToList();
            List<AccessGroup> selectedAccessGroups = Context.AccessGroups.AsNoTracking().Where(y => accessGroupdIds.Contains(y.Id)).ToList();
            List<AccessGroup> allAccessGroups = Context.AccessGroups.AsNoTracking().ToList();

            foreach (AccessGroup ag in allAccessGroups)
            {
                foreach (AccessGroup selecedtAg in selectedAccessGroups)
                {
                    if (selecedtAg.Id == ag.Id)
                    {
                        ag.FlagForAddition = true;
                    }
                }
            }

            var presentation = new UserGroupViewModel();

            var user = Context.Users.Where(x => x.Id == id).Where(x => x.Id == id).SingleOrDefault();

            presentation.UserName = user.UserName;
            presentation.AccessGroups = allAccessGroups;

            //var selectedAccessGroups = Context.A
            // Must create UserAccessGroups table!!!

            return View(presentation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccessGroups(string id, UserGroupViewModel model)
        {
            User user = Context.Users.Where(x => x.Id == id)
                .Include(x => x.UserAccessGroups)
                    //.ThenInclude(y => y.AccessGroup)
                    //    .ThenInclude(a => a.AccessGroupRoles)
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
                foreach (AccessGroup group in model.AccessGroups.Where(x => x.FlagForAddition == true))
                {
                    var localAccessGroup = Context.AccessGroups.Where(x => x.Id == group.Id)
                        .Include(x => x.AccessGroupRoles)
                            .ThenInclude(y => y.Role)
                        .SingleOrDefault();

                    var roleNames = localAccessGroup.AccessGroupRoles.Select(x => x.Role.NormalizedName).ToList();

                    User user1 = await userManager.FindByIdAsync(user.Id);
                    if (user1 != null)
                    {
                        foreach (string name in roleNames)
                        {
                            result = await userManager.AddToRoleAsync(user1, name);
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
            User user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
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
            return View("Index", userManager.Users);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public void UpdateUserRoles ()
        {
            // UserId
            // Remove previous Roles
            // match matrix to new roles
            // set roles
        }
    }
}