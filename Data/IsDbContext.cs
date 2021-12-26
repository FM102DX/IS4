using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FerryData.IS4.Data
{
    public class IsDbContext : IdentityDbContext
    {
        public IsDbContext(DbContextOptions<IsDbContext> options)
            : base(options)
        {

        }
    }
}
