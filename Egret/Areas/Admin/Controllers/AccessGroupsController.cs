using Egret.DataAccess;
using Egret.Models;
using Egret.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin_Access")]
    public class AccessGroupsController : BaseController
    {
        private readonly UserManager<User> _userManager;

        public AccessGroupsController(EgretContext context, ILogger<ItemsController> logger, UserManager<User> usrMgr)
            :base(context)
        {
            _userManager = usrMgr;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AccessGroup> groups = await Context.AccessGroups.OrderBy(x => x.Name).ToListAsync();
            return View(groups);
        }

        [HttpPost]
        public IActionResult Index(List<AccessGroup> groups, string action, int? id)
        {
            //switch( action )
            //{
            //    case "delete":
            //        Delete(id);
            //        break;
            //    case "save":
            //        break;
            //}

            if (ModelState.IsValid)
            {
                for (int i = 0; i < groups.Count(); i++)
                {
                    Context.AccessGroups.Update(groups[i]);
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
        public async Task<IActionResult> Create(AccessGroup group)
        {
            if (ModelState.IsValid)
            {
                await Context.AccessGroups.AddAsync(group);
                await Context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Access Group Added";
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        [HttpGet]
        public async Task<IActionResult> EditPermissions(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessGroup = await Context.AccessGroups.Where(x => x.Id == id).SingleOrDefaultAsync();
            var roles = Context.Roles.AsNoTracking().OrderBy(x => x.Name).ToList();
            var relatedRoles = Context.AccessGroupRoles.AsNoTracking().Where(x => x.AccessGroupId == id).Select(x => x.RoleId).ToList();
            roles.Where(x => relatedRoles.Contains(x.Id)).ToList().ForEach(y => y.RelationshipPresent = true);

            var presentation = new AccessGroupPermissionsModel
            {
                AccessGroup = accessGroup,
                Roles = roles
            };

            return View(presentation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPermissions(int? id, AccessGroupPermissionsModel model)
        {
            var accessGroupQuery = Context.AccessGroups.AsNoTracking().Where(x => x.Id == id)
                .Include(i => i.UserAccessGroups)
                    .ThenInclude(y => y.User)
                    .AsNoTracking();
            var accessGroup = accessGroupQuery.FirstOrDefault();
            var users = accessGroupQuery.AsNoTracking().SelectMany(x => x.UserAccessGroups).AsNoTracking().Select(y => y.User).AsNoTracking().ToList();
            model.AccessGroup = accessGroup;
            var rolesToAdd = model.Roles.Where(x => x.RelationshipPresent == true).ToList();

            if (ModelState.IsValid)
            {
                RemoveAccessGroupRoles(accessGroup.Id);
                CreateAccessGroupRoles(accessGroup.Id, rolesToAdd);

                foreach (User user in users)
                {
                    RemoveRolesFromUser(user);
                    AddRolesToUser(user, rolesToAdd);
                }

                Context.SaveChanges();

                TempData["SuccessMessage"] = "Access Group Permissions Updated";
                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
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

        [NonAction]
        private void RemoveAccessGroupRoles(int accessGroupId)
        {
            var accessGroupRoles = Context.AccessGroupRoles.Where(x => x.AccessGroupId == accessGroupId).ToList();
            foreach (AccessGroupRole groupRole in accessGroupRoles)
            {
                Context.AccessGroupRoles.Remove(groupRole);
            }
            Context.SaveChanges();
        }

        [NonAction]
        private void CreateAccessGroupRoles(int accessGroupId, IEnumerable<Role> roles)
        {
            var roleIds = roles.Select(x => x.Id);

            foreach (string id in roleIds)
            {
                var newGroupRole = new AccessGroupRole() { RoleId = id, AccessGroupId = accessGroupId };
                Context.AccessGroupRoles.Add(newGroupRole);
            }

            Context.SaveChanges();
        }

        [NonAction]
        private void RemoveRolesFromUser(User user)
        {
            var userRoles = Context.UserRoles.Where(x => x.UserId == user.Id).ToList();
            Context.UserRoles.RemoveRange(userRoles);
            Context.SaveChanges();
        }

        [NonAction]
        private void AddRolesToUser(User user, List<Role> roles)
        {
            var roles2 = Context.Roles.AsNoTracking().Where(x => roles.Select(y => y.Id).Contains(x.Id)).Select(x => x.Name).ToList();

            if (roles2.Count() > 0)
            {
                foreach (string role in roles2)
                {
                    var test = _userManager.AddToRoleAsync(user, role);
                }
            }

            Context.SaveChanges();
        }

        private Exception Exception(string v)
        {
            throw new Exception(v);
        }
    }
}