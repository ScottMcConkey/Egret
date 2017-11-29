using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Egret.DataAccess;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Egret.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public ViewResult Index()
        {
            return View();
        }

        public IActionResult Items()
        {
            return View("Items"); // RedirectToAction("Items", "Home");  //View("~/Inventory");
        }

        public IActionResult Projects()
        {
            return View();
        }

        public ViewResult Events()
        {
            return View();
        }

        public ViewResult Customers()
        {
            return View();
        }

        public ViewResult Reports()
        {
            return View();
        }

        public ViewResult Admin()
        {
            return View();
        }
    }
}
