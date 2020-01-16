using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Egret.Models;
using Egret.DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace Egret.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryApiController : BaseController
    {
        public class Item
        {
            public string Description { get; set; }
            public string CustomerReservedFor { get; set; }
            public string Unit { get; set; }
        }

        public InventoryApiController(EgretContext context)
            : base(context) { }

        [HttpGet]
        [Authorize]
        public IEnumerable<InventoryItem> Get()
        {
            return Context.InventoryItems.ToList();
        }

        [HttpGet("{id}")]
        [Authorize]
        public Item Get(string id)
        {
            Item item = new Item();
            InventoryItem inventoryTarget = Context.InventoryItems.Where(x => x.Code == id).SingleOrDefault();
            if (inventoryTarget != null)
            {
                item.Description = inventoryTarget.Description;
                item.CustomerReservedFor = inventoryTarget.CustomerReservedFor;
                item.Unit = inventoryTarget.Unit;
            }
            
            return item;
        }

    }
}