using Domain.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public abstract class AuditableDbContext : DbContext
{
    protected AuditableDbContext(DbContextOptions options) : base(options){}

    public virtual async Task<int> SaveChangesAsync(string username = "SYSTEM")
    {
        foreach (var entry in base.ChangeTracker.Entries<EntityBase>()
                     .Where(q => q.State is EntityState.Added or EntityState.Modified))
        {
            entry.Entity.UpdatedAt= DateTime.UtcNow;

            if (entry.State != EntityState.Added) continue;

            entry.Entity.CreatedAt = DateTime.UtcNow;
        }

        var result = await base.SaveChangesAsync();

        return result;
    }
}