using Domain.Post;
using Domain.Comment;
using Domain.Follows;
using Domain.Likes;
using Domain.Unlikes;
using Domain.Notification;

namespace Persistence;


using Microsoft.EntityFrameworkCore;
using Domain.Hashtag;

public class SocialSyncDbContext : AuditableDbContext
{
    public SocialSyncDbContext(DbContextOptions<SocialSyncDbContext> option) : base(option){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SocialSyncDbContext).Assembly);
    }
    public DbSet<Follows> Follows { get; set; } = null!;

     public DbSet<Post> Posts { get; set; } = null!;


    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Likes> Likes { get; set; } = null!;
    public DbSet<Unlikes> Unlikes { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;
    public DbSet<Hashtag> Hashtags { get; set; } = null!;

}