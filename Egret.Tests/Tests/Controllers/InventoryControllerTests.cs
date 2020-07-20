﻿using Egret.Areas.Inventory.Controllers;
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
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string connection = config["ConnectionString"];
            _options.UseNpgsql(connection);
            _itemService = Substitute.For<IItemService>();
            _selectListService = Substitute.For<ISelectListFactoryService>();
        }

        [Theory]
        [InlineData("0")]
        [InlineData(null)]
        [Trait("name", "edit_get_no_id_provided_throws_not_found")]
        public void edit_get_no_id_provided_throws_not_found(string value)
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
