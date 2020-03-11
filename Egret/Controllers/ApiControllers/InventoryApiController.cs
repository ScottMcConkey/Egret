using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Egret.Models;
using Egret.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("{id}")]
        [Authorize]
        public Item Get(string id)
        {
            var item = new Item();
            InventoryItem inventoryTarget = Context.InventoryItems.Where(x => x.Code == id)
                .Include(x => x.UnitNavigation).SingleOrDefault();
            if (inventoryTarget != null)
            {
                item.Description = inventoryTarget.Description;
                item.CustomerReservedFor = inventoryTarget.CustomerReservedFor;
                item.Unit = inventoryTarget.UnitNavigation.Abbreviation;
            }
            
            return item;
        }

    }
}