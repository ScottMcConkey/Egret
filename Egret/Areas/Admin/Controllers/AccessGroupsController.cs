using Egret.DataAccess;
using Egret.Models;
using Egret.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Egret.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin_Access")]
    public class AccessGroupsController : BaseController
    {
        public AccessGroupsController(EgretContext context, ILogger<ItemsController> logger)
            :base(context) { }

        [HttpGet]
        public IActionResult Index()
        {
            List<AccessGroup> groups = Context.AccessGroups.OrderBy(x => x.Name).ToList();
            return View(groups);
        }

        [HttpPost]
        public IActionResult Index(List<AccessGroup> groups, string action, int? id)
        {
            switch( action )
            {
                case "delete":
                    Delete(id);
                    break;
                case "save":
                    break;
            }

            if (ModelState.IsValid)
            {
                for (int i = 0; i < groups.Count(); i++)
                {
                    Context.Update(groups[i]);
                }
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Access Groups Saved";
                return RedirectToAction(nameof(Index));
            }
            return View(groups);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AccessGroup group)
        {
            if (ModelState.IsValid)
            {
                Context.AccessGroups.Add(group);
                Context.SaveChanges();
                TempData["SuccessMessage"] = "Access Group Added";
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = Context.AccessGroups.Where(x => x.Id == id).SingleOrDefault();

            if (ModelState.IsValid)
            {
                Context.AccessGroups.Remove(group);
                Context.SaveChanges();
                TempData["SuccessMessage"] = $"Access Group '{group.Name}' Deleted";
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int? id = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessGroup = Context.AccessGroups.Where(x => x.Id == id)
                .Include(group => group.AccessGroupRoles)
                .ThenInclude(agRoles => agRoles.Role)
                .SingleOrDefault();

            List<AccessGroupRole> groupRoles = Context.AccessGroupRoles.AsNoTracking().Where(x => x.AccessGroupId == (int)id).ToList();
            List<string> roleIds = groupRoles.Select(x => x.RoleId).ToList();
            List<Role> selectedRoles = Context.Roles.AsNoTracking().Where(y => roleIds.Contains(y.Id)).ToList();
            List<Role> allRoles = Context.Roles.AsNoTracking().OrderBy(x => x.Name).ToList();

            foreach (Role role in allRoles)
            {
                foreach (Role selectedRole in selectedRoles)
                {
                    if (selectedRole.Id == role.Id)
                    {
                        role.FlagForAddition = true;
                    }
                }
            }

            var presentation = new AccessGroupViewModel();
            presentation.AccessGroup = accessGroup;
            presentation.Roles = allRoles;

            return View(presentation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, AccessGroupViewModel model)
        {
            var accessGroup = Context.AccessGroups.Where(x => x.Id == id)
                .Include(x => x.AccessGroupRoles)
                    //.ThenInclude(x => x.Roles)
                .SingleOrDefault();

            model.AccessGroup = accessGroup;

            if (ModelState.IsValid)
            {
                // Remove all existing rels
                foreach (AccessGroupRole groupRole in Context.AccessGroupRoles.Where(x => x.AccessGroupId == (int)id /*model.AccessGroup.Id*/))
                {
                    Context.AccessGroupRoles.Remove(groupRole);
                }

                Context.SaveChanges();

                // Set new rels
                foreach (Role role in model.Roles.Where(x => x.FlagForAddition == true))
                {
                    var localRole = Context.Roles.Where(x => x.Id == role.Id).SingleOrDefault();

                    var newGroupRole = new AccessGroupRole() { Role = localRole, AccessGroup = model.AccessGroup };//, /*Role = role, AccessGroup = model.AccessGroup*/ };

                    Context.AccessGroupRoles.Add(newGroupRole);
                }

                Context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
            
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