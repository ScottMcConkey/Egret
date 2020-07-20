using Egret.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Egret.IntegrationTests.Tests.Services
{
    public class SystemServiceTests : IClassFixture<EgretDbContextFixture>
    {
        private SystemService _systemService { get; set; }

        public SystemServiceTests(EgretDbContextFixture fixture)
        {
            _systemService = new SystemService(fixture.Context, fixture.Configuration);
        }

        [Fact]
        public void Get_egret_version_returns_a_value()
        {
            var sut = _systemService.GetEgretVersion();

            Assert.StartsWith("v", sut, StringComparison.OrdinalIgnoreCase);
        }
    }
}
