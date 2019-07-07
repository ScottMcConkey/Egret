using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Egret.Models;
using Egret.ViewModels;

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