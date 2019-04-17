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
        public async Task<IActionResult> EditPermissions(int? id, AccessGroupPermissionsModel model)
        {
            var accessGroup = Context.AccessGroups.Where(x => x.Id == id).SingleOrDefault();

            model.AccessGroup = accessGroup;

            if (ModelState.IsValid)
            {
                // Remove rels from Access Group to Roles
                foreach (AccessGroupRole groupRole in Context.AccessGroupRoles.Where(x => x.AccessGroupId == id))
                {
                    Context.AccessGroupRoles.Remove(groupRole);
                }
                Context.SaveChanges();

                // Create new rels from Access Group to Roles
                foreach (Role role in model.Roles.Where(x => x.RelationshipPresent == true))
                {
                    var localRole = Context.Roles.Where(x => x.Id == role.Id).SingleOrDefault();
                    var newGroupRole = new AccessGroupRole() { Role = localRole, AccessGroup = model.AccessGroup };
                    Context.AccessGroupRoles.Add(newGroupRole);
                }
                Context.SaveChanges();


                // Loop through every user assigned to this access group
                var users = Context.UserAccessGroups.AsNoTracking().Where(x => x.AccessGroupId == id).Select(y => y.User).ToList();
                var roles = Context.Roles.AsNoTracking().Select(y => y.Name);

                foreach (User user in users)
                {
                    // Remove all Roles from each of those users
                    IdentityResult result = await _userManager.RemoveFromRolesAsync(user, roles);

                    var rolesToAdd = Context.Roles.AsNoTracking().FromSql(
                        "select r.name" +
                        "  from user_accessgroups uag" +
                        "  join accessgroup_roles agr" +
                        "    on agr.accessgroupid = uag.accessgroupid" +
                        "  join roles r" +
                        "    on r.id = agr.roleid" +
                        " where uag.userid = {0}", user.Id)
                        .Select(x => x.Name).ToList();

                    // Rebuild all Roles based on new specifications
                    IdentityResult result2 = await _userManager.AddToRolesAsync(user, rolesToAdd);
                }
                
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


    }
}