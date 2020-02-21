using Egret.DataAccess;
using Egret.Models;
using Egret.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public IActionResult EditPermissions(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessGroup = Context.AccessGroups.Where(x => x.Id == id).SingleOrDefault();
            var roles = Context.Roles.AsNoTracking().OrderBy(x => x.Name).ToList();
            var relatedRoles = Context.AccessGroupRoles.AsNoTracking().Where(x => x.AccessGroupId == id).Select(x => x.RoleId).ToList();
            roles.Where(x => relatedRoles.Contains(x.Id)).ToList().ForEach(y => y.RelationshipPresent = true);

            var presentation = new AccessGroupPermissionsModel();
            presentation.AccessGroup = accessGroup;
            presentation.Roles = roles;

            return View(presentation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPermissions(int? id, AccessGroupPermissionsModel model)
        {
            // Make life 100x easier by setting default to notracking
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var accessGroupQuery = Context.AccessGroups.Where(x => x.Id == id)
                .Include(i => i.UserAccessGroups)
                    .ThenInclude(y => y.User);
            var accessGroup = accessGroupQuery.FirstOrDefault();
            var accessGroupId = accessGroup.Id;
            var relatedUsers = accessGroupQuery.SelectMany(x => x.UserAccessGroups).Select(y => y.User).ToList();
            model.AccessGroup = accessGroup;
            var allRoleIds = Context.Roles.Select(y => y.Id).ToList();
            var currentRoleIds = Context.AccessGroupRoles.Where(x => x.AccessGroupId == id).Select(y => y.RoleId).ToList();
            var futureRoleIds = model.Roles.Where(x => x.RelationshipPresent == true).Select(y => y.Id).ToList();
            

            if (ModelState.IsValid)
            {
                // Rebuild Access Group Roles
                foreach (var roleId in allRoleIds)
                {
                    if (futureRoleIds.Contains(roleId) && !currentRoleIds.Contains(roleId))
                    {
                        var roleToAdd = Context.AccessGroupRoles.Add(new AccessGroupRole { RoleId = roleId, AccessGroupId = id.Value });
                    }
                    else if (!futureRoleIds.Contains(roleId) && currentRoleIds.Contains(roleId))
                    {
                        var roleToRemove = Context.AccessGroupRoles.AsTracking().Where(x => x.AccessGroupId == id && x.RoleId == roleId).SingleOrDefault();
                        Context.AccessGroupRoles.Remove(roleToRemove);
                    }
                }

                Context.SaveChanges();

                // Rebuild User Roles
                foreach (User user in relatedUsers)
                {

                    var userIdParam = new NpgsqlParameter("userid", user.Id); //???
                    var accessGroupIdParam = new NpgsqlParameter("accessGroupId", accessGroupId);

                    var currentUserRoles = Context.Roles.FromSqlRaw(
                        "select r.* from roles r " +
                        "join user_roles ur " +
                        "on ur.roleid = r.id " +
                        $"where ur.userid = @userid", userIdParam).ToList();

                    var roleIdsOutside = Context.Roles.FromSqlRaw(
                        "select r.* " +
                        "from user_accessgroups uag " +
                        "join accessgroups ag " +
                        "on ag.id = uag.accessgroupid " +
                        "join accessgroup_roles agr " +
                        "on agr.accessgroupid = ag.id " +
                        "join roles r " +
                        "on r.id = agr.roleid " +
                        "where uag.userid = @userid " +
                        "and ag.id != @accessGroupId ", new[] { userIdParam, accessGroupIdParam }).Select(x => x.Id).ToList();

                    var futureUserRoleIds = model.Roles.Where(x => x.RelationshipPresent).Select(x => x.Id).ToList();
                    var currentUserRoleIds = currentUserRoles.Select(x => x.Id).ToList();
                    var accessGroupRoles = currentRoleIds;

                    foreach (var roleId in allRoleIds)
                    {
                        var theRole = Context.Roles.Where(x => x.Id == roleId).FirstOrDefault();

                        if (futureUserRoleIds.Contains(roleId) && !currentUserRoleIds.Contains(roleId))
                        {
                            await _userManager.AddToRoleAsync(user, theRole.Name);
                        }
                        else if (!futureUserRoleIds.Contains(roleId) && currentUserRoleIds.Contains(roleId))
                        {
                            // Remove the Role from the User only if that user
                            // doesn't have access to that Role through another Access Group
                            if (!roleIdsOutside.Contains(roleId))
                            {
                                await _userManager.RemoveFromRoleAsync(user, theRole.Name);
                            }
                        }
                    }

                    Context.SaveChanges();
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
        private void AddRolesToUser(User user, List<Role> rolesToAdd)
        {
            var roles = Context.Roles.AsNoTracking().Where(x => rolesToAdd.Select(y => y.Id).Contains(x.Id)).Select(x => x.Name).ToList();

            if (roles.Count() > 0)
            {
                foreach (string role in roles)
                {
                    var test = _userManager.AddToRoleAsync(user, role);
                }
            }

            Context.SaveChanges();
        }
    }
}