using Egret.DataAccess;
using Egret.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace Egret
{
    public class Startup
    {
        public IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsetings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql();
            services.AddDbContext<EgretContext>(options =>
                options.UseNpgsql(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();

            services.AddIdentity<User, Role>(opts => 
                {
                    opts.Password.RequiredLength = 6;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                    opts.User.RequireUniqueEmail = true;
                    //opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    opts.Lockout.MaxFailedAccessAttempts = 10;
                }
            )
                .AddEntityFrameworkStores<EgretContext>()
                //.AddDefaultUI()
                ;

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.AccessDeniedPath = "/Account/Home/AccessDenied";
                options.LoginPath = "/Account/Home/Login";
                options.LogoutPath = "/Account/Home/Logout";
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                //options.SlidingExpiration = true;
            });

            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Add("~/Views/Widgets/{0}" + RazorViewEngine.ViewExtension);
                o.AreaViewLocationFormats.Add("~/Views/Widgets/{0}" + RazorViewEngine.ViewExtension);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCors(options =>
               options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            loggerFactory.AddNLog();
            loggerFactory.AddDebug();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "areas",
                template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "out",
                    template: "outbound/{controller=Home}/{action=Index}");
            });
        }
    }
}
