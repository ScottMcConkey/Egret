using Egret.Services;
using System;
using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace Egret.IntegrationTests.Tests.Services
{
    public class ItemServiceTests : IClassFixture<EgretDbContextFixture>
    {
        private ItemService _itemService { get; set; }

        public ItemServiceTests(EgretDbContextFixture fixture)
        {
            _itemService = new ItemService(fixture.Context);
        }

        [Fact]
        public void Get_system_currency_returns_a_value()
        {
            var sut = _itemService.GetSystemCurrency();

            Assert.Equal("NRP", sut, StringComparer.OrdinalIgnoreCase);
        }
    }
}
