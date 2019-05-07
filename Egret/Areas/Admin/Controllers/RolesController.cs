using Egret.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Egret.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin_Access")]
    public class RolesController : Controller
    {
        private RoleManager<Role> _roleManager;

        public RolesController(RoleManager<Role> roleMgr)
        {
            _roleManager = roleMgr;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(_roleManager.Roles.OrderBy(x => x.Name).ToList());
        }

    }
}