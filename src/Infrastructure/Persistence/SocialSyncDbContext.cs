using Domain.Comment;
using Domain.Follows;
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
    public DbSet<Comment> Comments { get; set; }
}