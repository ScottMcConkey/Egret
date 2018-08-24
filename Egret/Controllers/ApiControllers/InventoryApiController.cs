using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Egret.Models;
using Egret.DataAccess;

namespace Egret.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ManagedController
    {
        public class Item
        {
            public string Description { get; set; }
            public string CustomerReservedFor { get; set; }
            public string Unit { get; set; }
        }

        public InventoryController(EgretContext context)
            : base(context) { }

        [HttpGet]
        public IEnumerable<InventoryItem> Get()
        {
            return Context.InventoryItems.ToList();
        }

        [HttpGet("{id}")]
        public Item Get(string id)
        {
            Item item = new Item();
            InventoryItem inventoryTarget = Context.InventoryItems.Where(x => x.Code == id).SingleOrDefault();
            //Context.InventoryItems.Where(x => x.Code == id).SingleOrDefault();
            item.Description = inventoryTarget.Description;
            item.CustomerReservedFor = inventoryTarget.CustomerReservedFor;
            item.Unit = inventoryTarget.Unit;
            return item;
        }

    }
}