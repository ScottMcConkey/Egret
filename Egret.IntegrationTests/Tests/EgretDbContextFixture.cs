using Egret.DataAccess;
using Egret.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Egret.IntegrationTests.Tests
{
    public class EgretDbContextFixture : IDisposable
    {
        public EgretDbContext Context { get; set; }

        public IConfiguration Configuration { get; set; }
        

        public EgretDbContextFixture()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var options = new DbContextOptionsBuilder<EgretDbContext>()
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .Options;

            //var options = new DbContextOptions<EgretDbContext>();
            Context = new EgretDbContext(options);
            Configuration = configuration;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
