using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Egret_Dev.EF;

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

        public IActionResult SearchInventory()
        {
            List<InventoryItem> inventItems = new List<InventoryItem>();
            using (var context = new EgretContext())
            {
                foreach (InventoryItem i in context.InventoryItems)
                {
                    inventItems.Add(i);
                }
            }
            return View(inventItems);
        }


        public ViewResult NewInventory()
        {
            // Requires New Inventory View
            return View();
        }
    }
}
