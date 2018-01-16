using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Egret.Controllers;
using Egret.DataAccess;
using Egret.Models;

namespace Egret
{
    public class Startup
    {
        IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json").Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql();
            services.AddDbContext<EgretContext>(options =>
                options.UseNpgsql(Configuration["ConnectionStrings:DefaultConnection"]));

            //services.AddIdentity<User, Role>()
            //    .AddEntityFrameworkStores<EgretContext>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            loggerFactory.AddDebug();
            //app.UseIdentity();
            app.UseMvc(routes =>
            {
                /*routes.MapRoute(
                    name: "inventory",
                    template: "Home/Inventory/",
                    defaults: new { Controller = "Inventory", action = "Index" });*/

                routes.MapRoute(
                    name: "default", 
                    template: "{controller}/{action}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
