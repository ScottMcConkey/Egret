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
using System.Threading.Tasks;

namespace Egret.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin_Access")]
    public class AccessGroupsController : BaseController
    {
        private UserManager<User> _userManager;

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
        public IActionResult EditPermissions(int? id)
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

            var presentation = new AccessGroupViewModel
            {
                AccessGroup = accessGroup,
                Roles = allRoles
            };

            return View(presentation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPermissions(int? id, AccessGroupViewModel model)
        {
            var accessGroup = Context.AccessGroups.Where(x => x.Id == id)
                .Include(x => x.AccessGroupRoles)
                .SingleOrDefault();

            model.AccessGroup = accessGroup;

            if (ModelState.IsValid)
            {
                // Remove rels Access Groups to Roles
                foreach (AccessGroupRole groupRole in Context.AccessGroupRoles.Where(x => x.AccessGroupId == (int)id))
                {
                    Context.AccessGroupRoles.Remove(groupRole);
                }
                Context.SaveChanges();

                // Create new rels Access Groups to Roles
                foreach (Role role in model.Roles.Where(x => x.FlagForAddition == true))
                {
                    var localRole = Context.Roles.Where(x => x.Id == role.Id).SingleOrDefault();
                    var newGroupRole = new AccessGroupRole() { Role = localRole, AccessGroup = model.AccessGroup };
                    Context.AccessGroupRoles.Add(newGroupRole);
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