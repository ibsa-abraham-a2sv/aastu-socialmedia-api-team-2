using Domain.Follows;
using Domain.Post;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class SocialSyncDbContext : AuditableDbContext
{
    public SocialSyncDbContext(DbContextOptions<SocialSyncDbContext> option) : base(option){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SocialSyncDbContext).Assembly);
    }

    public DbSet<Follows> Follows { get; set; } = null!;
     public DbSet<Post> Posts { get; set; } = null!;
}