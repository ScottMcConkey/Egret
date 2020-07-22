using Egret.Areas.Inventory.Controllers;
using Egret.DataAccess;
using Egret.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Egret.Tests.Controllers
{
    [Trait("Name", "ControllerTests")]
    public class InventoryControllerTests
    {
        private readonly ILogger<ItemsController> _fakeLogger;
        private readonly DbContextOptionsBuilder<EgretDbContext> _options;
        private readonly IItemService _itemService;
        private readonly ISelectListFactoryService _selectListService;

        public InventoryControllerTests()
        {
            _fakeLogger = Substitute.For<ILogger<ItemsController>>();
            _options = new DbContextOptionsBuilder<EgretDbContext>();
            _options.UseNpgsql("dummy");
            _itemService = Substitute.For<IItemService>();
            _selectListService = Substitute.For<ISelectListFactoryService>();
        }

        [Theory]
        [InlineData("0")]
        [InlineData(null)]
        [Trait("name", "edit_get_no_id_provided_throws_not_found")]
        public void Edit_get_no_id_provided_throws_not_found(string value)
        {
            //+ Arrange
            ItemsController controller = new ItemsController(_fakeLogger, _itemService,  _selectListService);

            //+ Act
            var actionResult = controller.Edit(value);

            //+ Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }

}
