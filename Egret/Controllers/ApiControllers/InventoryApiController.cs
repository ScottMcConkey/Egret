using Egret.DataAccess;
using Egret.Models.Common;
using Egret.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Egret.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryApiController : Controller
    {
        private IItemService _itemService { get; set; }

        public InventoryApiController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Item_Read")]
        public BasicInventoryLot Get(string id)
        {
            var item = new BasicInventoryLot();
            var inventoryTarget = _itemService.GetBasicLot(id);
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