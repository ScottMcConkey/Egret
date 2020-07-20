using Egret.DataAccess;
using Egret.Models;
using Egret.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace Egret
{
    public class Startup
    {
        public IConfigurationRoot Configuration;

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                //.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"appsetings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EgretDbContext>(options =>
                options.UseNpgsql(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
                loggingBuilder.AddNLog();
            });
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //services.AddTransient<ServiceFactory>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IConsumptionEventService, ConsumptionEventService>();
            services.AddTransient<ISelectListFactoryService, SelectListFactoryService>();
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
                    opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    opts.Lockout.MaxFailedAccessAttempts = 10;
                }
            )
                .AddEntityFrameworkStores<EgretDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
            });

            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Add("~/Widgets/Views/{0}" + RazorViewEngine.ViewExtension);
                o.AreaViewLocationFormats.Add("~/Widgets/Views/{0}" + RazorViewEngine.ViewExtension);
            });
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "out",
                    pattern: "outbound/{controller=Home}/{action=Index}");
            });
        }
    }
}
