using Application.Contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Repository;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SocialSyncDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("SocialSyncDbConnection")));


        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IFollowsRepository, FollowsRepository>();

        services.AddScoped<IPostRepository, PostRepository>();

        services.AddScoped<ICommentRepository, CommentRepository>();

        services.AddScoped<ILikesRepository, LikesRepository>();
        services.AddScoped<IUnlikesRepository, UnlikesRepository>();

        return services;
    } 
}