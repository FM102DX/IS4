using FerryData.IS4.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace FerryData.IS4
{
    public class DbInitializer
    {
        public static void Initialize(IServiceScope scope)
        {
            var services = scope.ServiceProvider;
            var ctx = services.GetRequiredService<IsDbContext>();

            var userMgr = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();

            ctx.Database.EnsureCreated();

            var adminRole = new IdentityRole("Admin");
            var userRole = new IdentityRole("User");

            if (!ctx.Roles.Any())
            {
                roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();
                roleMgr.CreateAsync(userRole).GetAwaiter().GetResult();
            }

            if (!ctx.Users.Any(x => x.UserName == "admin"))
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };

                userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();

                userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
            }
        }
    }
}
