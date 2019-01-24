using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Egret.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin_Access")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}