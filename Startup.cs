using FerryData.IS4.Data;
using FerryData.IS4.Infrastructure;
using IS4.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using System;

namespace FerryData.IS4
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Logger
            
            Log.Logger = new LoggerConfiguration()
                                    .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                                    .Enrich.FromLogContext()
                                    .WriteTo.File("BarsikLogs2.txt")
                                    .CreateLogger();

            Log.Logger.Information("Point 1");

            

            services.AddSingleton(typeof(LogsManager), (x) => new LogsManager());
            Log.Logger.Information("Point 2");
            services.AddTransient(typeof(Serilog.ILogger), (x) => new LoggerConfiguration()
                                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                                    .Enrich.FromLogContext()
                                    .WriteTo.File(new LogsManager().FullFileName)
                                    .CreateBootstrapLogger());

            #endregion



           services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<IsDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.UserInteraction.LoginUrl = "/Identification/Login";
                options.UserInteraction.LogoutUrl = "/Identification/Logout";
            })
                .AddAspNetIdentity<IdentityUser>()
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiScopes(IdentityServerConfiguration.GetScopes())
                .AddDeveloperSigningCredential();

            services.AddCors();

            services.AddControllersWithViews();

            #region DB

            services.AddDbContext<IsDbContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            });

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
