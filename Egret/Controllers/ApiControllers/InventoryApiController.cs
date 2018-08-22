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
        public InventoryController(EgretContext context)
            : base(context) { }

        [HttpGet]
        public IEnumerable<InventoryItem> Get()
        {
            return Context.InventoryItems.ToList();
        }

        [HttpGet("{id}")]
        public InventoryItem Get(string id) => Context.InventoryItems.Where(x => x.Code == id).SingleOrDefault();

    }
}