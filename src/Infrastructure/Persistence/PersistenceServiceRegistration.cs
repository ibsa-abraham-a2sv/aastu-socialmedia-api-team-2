using Application.Contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Repository;
using Persistence.Service;

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
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IUserConnectionMapRepository, UserConnectionMapRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IUserConnectionMapRepository, UserConnectionMapRepository>();
        services.AddSignalRCore();

        services.AddScoped<ILikesRepository, LikesRepository>();
        services.AddScoped<IUnlikesRepository, UnlikesRepository>();
        return services;
    } 
}