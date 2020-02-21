using Egret.Controllers;
using Egret.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using Egret.Services;

namespace Egret.Tests
{
    [TestFixture]
    public class InventoryControllerTests
    {
        ILogger<ItemsController> _fakeLogger;
        DbContextOptionsBuilder<EgretContext> _options;
        EgretContext _context;
        IItemService _itemService;
        ISelectListFactoryService _selectListService;

        public InventoryControllerTests()
        {
            _fakeLogger = Substitute.For<ILogger<ItemsController>>();
            _options = new DbContextOptionsBuilder<EgretContext>();
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string connection = config["ConnectionString"];
            _options.UseNpgsql(connection);
            _context = new EgretContext(_options.Options);
            _itemService = Substitute.For<IItemService>();
            _selectListService = Substitute.For<ISelectListFactoryService>();
        }

        [TestCase]
        public void Edit_Get_NoIdProvided_ThrowsNotFound()
        {
            //+ Arrange
            ItemsController controller = new ItemsController(_itemService, _fakeLogger, _selectListService);

            //+ Act
            var actionResult = controller.Edit(null);

            //+ Assert
            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }
    }

}
