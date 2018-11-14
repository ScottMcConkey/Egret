using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Egret.Controllers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Egret.DataAccess;
using Egret.Models;
using System.Threading.Tasks;
using Npgsql.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Egret.Tests
{
    [TestFixture]
    public class InventoryControllerTests
    {
        [TestCase]
        public async Task Edit_Get_NoIdProvided_Throws404()
        {
            //+ Arrange
            ILogger<ItemsController> fakeLogger = Substitute.For<ILogger<ItemsController>>(); // new ILogger();
            var optionsBuilder = new DbContextOptionsBuilder<EgretContext>();
            optionsBuilder.UseNpgsql("Server = localhost; Port = 5432; Database = Egret; User Id = postgres; Password = postgres; Integrated Security = true");
            EgretContext fakeContext = new EgretContext(optionsBuilder.Options); // Substitute.For<EgretContext>();
            ItemsController controller = new ItemsController(fakeContext, fakeLogger);

            //+ Act
            var actionResult = await controller.Edit(String.Empty);

            //+ Assert
            Assert.IsInstanceOf<NotFoundResult>(actionResult);
        }
    }

}
