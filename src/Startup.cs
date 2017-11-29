using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Configuration;
using Egret.Controllers;
using Egret.DataAccess;

namespace Egret
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //var test = ConfigurationManager.ConnectionStrings["Egret"].ConnectionString;
            services.AddMvc();
            services.AddEntityFrameworkNpgsql();
            services.AddDbContext<EgretContext>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            loggerFactory.AddDebug();
            //app.UseMvcWithDefaultRoute();
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
