using Egret.Controllers;
using Egret.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Configuration;
using System.Threading.Tasks;

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
        public async Task Edit_Get_NoIdProvided_Throws404()
        {
            //+ Arrange
            ItemsController controller = new ItemsController(_context, _fakeLogger);

            //+ Act
            var actionResult = await controller.Edit(String.Empty);

            //+ Assert
            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }
    }

}
