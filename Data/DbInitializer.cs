using FerryData.IS4.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace FerryData.IS4
{
    public class DbInitializer
    {
        public static void Initialize(IServiceScope scope, Serilog.ILogger logger)
        {

            logger.Information("DbInitializer is here");

            var services = scope.ServiceProvider;
            var ctx = services.GetRequiredService<IsDbContext>();

            var userMgr = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();

            ctx.Database.EnsureCreated();

            logger.Information("Db is created");

            var adminRole = new IdentityRole("Admin");
            var userRole = new IdentityRole("User");

            if (!ctx.Roles.Any())
            {
                roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                roleMgr.CreateAsync(userRole).GetAwaiter().GetResult();
            }

            logger.Information("Roles:");
            foreach (var role in ctx.Roles)
            {
                logger.Information($"Role: {role.Id} {role.Name} ");
            }



            if (!ctx.Users.Any(x => x.UserName == "admin"))
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };
                try
                {
                    logger.Information("Point1");

                   var rez= userMgr.CreateAsync(adminUser, "admin").GetAwaiter().GetResult();

                    logger.Information($"Point2 rez.Succeeded={rez.Succeeded} rez.ToString()={rez.ToString()} ");

                    userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();

                    logger.Information("Point3");
                }
                catch (Exception ex)
                {
                    logger.Error($"An error occured on creating user: {ex.Message}");
                }

            }

            logger.Information("Users:");
            foreach (var user in ctx.Users)
            {
                logger.Information($"User: {user.Id} {user.UserName} {user.Email} ");
            }
        }
    }
}
