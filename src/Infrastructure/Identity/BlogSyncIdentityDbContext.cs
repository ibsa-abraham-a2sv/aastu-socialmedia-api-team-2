using Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity
{
    public class BlogSyncIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public BlogSyncIdentityDbContext(DbContextOptions<BlogSyncIdentityDbContext> options)
            : base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //
        //     // modelBuilder.ApplyConfiguration(new RoleConfiguration());
        //     // modelBuilder.ApplyConfiguration(new UserConfiguration());
        //     // modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        // }
    }
}
