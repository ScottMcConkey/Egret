using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Egret.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    [Authorize(Roles = "Item_Create")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
