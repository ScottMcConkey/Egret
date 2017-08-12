using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Egret_Dev.EF;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Egret_Dev.Controllers
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
            List<InventoryItem> inventItems = new List<InventoryItem>();
            using (var context = new EgretContext())
            {
                foreach (InventoryItem i in context.InventoryItems)
                {
                    inventItems.Add(i);
                }
            }
            //return View(inventItems);
            return RedirectToAction("Index", "Inventory");  //View("~/Inventory");
        }

        public ViewResult Projects()
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
