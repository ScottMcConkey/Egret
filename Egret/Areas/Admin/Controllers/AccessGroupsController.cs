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
using System.Linq;
using System.Threading.Tasks;

namespace Egret.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin_Access")]
    public class AccessGroupsController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly EgretDbContext _context;

        public AccessGroupsController(EgretDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AccessGroup> groups = await _context.AccessGroups.OrderBy(x => x.Name).ToListAsync();
            return View(groups);
        }

        [HttpPost]
        public IActionResult Index(List<AccessGroup> groups, string action, int? id)
        {
            if (ModelState.IsValid)
            {
                for (int i = 0; i < groups.Count(); i++)
                {
                    _context.AccessGroups.Update(groups[i]);
                }
                _context.SaveChanges();
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
                await _context.AccessGroups.AddAsync(group);
                await _context.SaveChangesAsync();
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

            var accessGroup = _context.AccessGroups.Where(x => x.AccessGroupId == id).SingleOrDefault();
            var roles = _context.Roles.AsNoTracking().OrderBy(x => x.Name).ToList();
            var relatedRoles = _context.AccessGroupRoles.AsNoTracking().Where(x => x.AccessGroupId == id).Select(x => x.RoleId).ToList();
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
            // Make life 100x easier by setting default to notracking
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var accessGroupQuery = _context.AccessGroups.Where(x => x.AccessGroupId == id)
                .Include(i => i.UserAccessGroups)
                    .ThenInclude(y => y.User);
            var accessGroup = accessGroupQuery.FirstOrDefault();
            var accessGroupId = accessGroup.AccessGroupId;
            var relatedUsers = accessGroupQuery.SelectMany(x => x.UserAccessGroups).Select(y => y.User).ToList();
            model.AccessGroup = accessGroup;
            var allRoleIds = _context.Roles.Select(y => y.Id).ToList();
            var currentRoleIds = _context.AccessGroupRoles.Where(x => x.AccessGroupId == id).Select(y => y.RoleId).ToList();
            var futureRoleIds = model.Roles.Where(x => x.RelationshipPresent == true).Select(y => y.Id).ToList();
            

            if (ModelState.IsValid)
            {
                // Rebuild Access Group Roles
                foreach (var roleId in allRoleIds)
                {
                    if (futureRoleIds.Contains(roleId) && !currentRoleIds.Contains(roleId))
                    {
                        var roleToAdd = _context.AccessGroupRoles.Add(new AccessGroupRole { RoleId = roleId, AccessGroupId = id.Value });
                    }
                    else if (!futureRoleIds.Contains(roleId) && currentRoleIds.Contains(roleId))
                    {
                        var roleToRemove = _context.AccessGroupRoles.AsTracking().Where(x => x.AccessGroupId == id && x.RoleId == roleId).SingleOrDefault();
                        _context.AccessGroupRoles.Remove(roleToRemove);
                    }
                }

                _context.SaveChanges();

                // Rebuild User Roles
                foreach (User user in relatedUsers)
                {

                    var userIdParam = new NpgsqlParameter("userid", user.Id); //???
                    var accessGroupIdParam = new NpgsqlParameter("accessGroupId", accessGroupId);

                    var currentUserRoles = _context.Roles.FromSqlRaw(
                        "select r.* from roles r " +
                        "join user_roles ur " +
                        "on ur.role_id = r.id " +
                        $"where ur.user_id = @userid", userIdParam).ToList();

                    var roleIdsOutside = _context.Roles.FromSqlRaw(
                        "select r.* " +
                        "from user_access_groups uag " +
                        "join access_groups ag " +
                        "on ag.access_group_id = uag.access_group_id " +
                        "join access_group_roles agr " +
                        "on agr.access_group_id = ag.access_group_id " +
                        "join roles r " +
                        "on r.id = agr.role_id " +
                        "where uag.user_id = @userid " +
                        "and ag.access_group_id != @accessGroupId ", new[] { userIdParam, accessGroupIdParam }).Select(x => x.Id).ToList();

                    var futureUserRoleIds = model.Roles.Where(x => x.RelationshipPresent).Select(x => x.Id).ToList();
                    var currentUserRoleIds = currentUserRoles.Select(x => x.Id).ToList();
                    var accessGroupRoles = currentRoleIds;

                    foreach (var roleId in allRoleIds)
                    {
                        var theRole = _context.Roles.Where(x => x.Id == roleId).FirstOrDefault();

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

                    _context.SaveChanges();
                }

                _context.SaveChanges();

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

            var group = _context.AccessGroups.Where(x => x.AccessGroupId == id).SingleOrDefault();

            if (ModelState.IsValid)
            {
                _context.AccessGroups.Remove(group);
                _context.SaveChanges();
                TempData["SuccessMessage"] = $"Access Group '{group.Name}' Deleted";
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index));
        }

        [NonAction]
        private void RemoveAccessGroupRoles(int accessGroupId)
        {
            var accessGroupRoles = _context.AccessGroupRoles.Where(x => x.AccessGroupId == accessGroupId).ToList();
            foreach (AccessGroupRole groupRole in accessGroupRoles)
            {
                _context.AccessGroupRoles.Remove(groupRole);
            }
            _context.SaveChanges();
        }

        [NonAction]
        private void CreateAccessGroupRoles(int accessGroupId, IEnumerable<Role> roles)
        {
            var roleIds = roles.Select(x => x.Id);

            foreach (string id in roleIds)
            {
                var newGroupRole = new AccessGroupRole() { RoleId = id, AccessGroupId = accessGroupId };
                _context.AccessGroupRoles.Add(newGroupRole);
            }

            _context.SaveChanges();
        }

        [NonAction]
        private void RemoveRolesFromUser(User user)
        {
            var userRoles = _context.UserRoles.Where(x => x.UserId == user.Id).ToList();
            _context.UserRoles.RemoveRange(userRoles);
            _context.SaveChanges();
        }

        [NonAction]
        private void AddRolesToUser(User user, List<Role> rolesToAdd)
        {
            var roles = _context.Roles.AsNoTracking().Where(x => rolesToAdd.Select(y => y.Id).Contains(x.Id)).Select(x => x.Name).ToList();

            if (roles.Count() > 0)
            {
                foreach (string role in roles)
                {
                    var test = _userManager.AddToRoleAsync(user, role);
                }
            }

            _context.SaveChanges();
        }
    }
}