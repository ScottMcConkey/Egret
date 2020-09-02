using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Egret.Services;

namespace Egret.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin_Access")]
    public class HomeController : Controller
    {
        private readonly SystemService _systemService;

        public HomeController(SystemService systemService)
        {
            _systemService = systemService;
        }

        public IActionResult Index()
        {
            ViewBag.EgretVersion = _systemService.GetEgretMigrationVersion();

            return View();
        }
    }
}