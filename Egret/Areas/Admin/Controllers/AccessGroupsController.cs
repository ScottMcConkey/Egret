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
                Context.AccessGroups.Add(group);
                Context.SaveChanges();
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
                foreach (Role role in model.Roles.Where(x => x.RelationshipPresent == true))
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


    }
}