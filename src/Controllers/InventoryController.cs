using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Egret_Dev.Controllers
{
    public class InventoryController : Controller
    {
        // GET: /<controller>/
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult EditInventory()
        {
            // Requires EditInventory View
            return View();
        }

        public ViewResult NewInventory()
        {
            // Requires New Inventory View
            return View();
        }
    }
}
