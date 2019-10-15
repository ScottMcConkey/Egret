using Egret.Controllers;
using Egret.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Egret.Tests
{
    [TestFixture]
    public class InventoryControllerTests
    {
        ILogger<ItemsController> _fakeLogger;
        DbContextOptionsBuilder<EgretContext> _options;
        EgretContext _context;

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
        }

        [TestCase]
        public void Edit_Get_NoIdProvided_ThrowsNotFound()
        {
            //+ Arrange
            ItemsController controller = new ItemsController(_context, _fakeLogger);

            //+ Act
            var actionResult = controller.Edit(null);

            //+ Assert
            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }
    }

}
