using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace SocialSync.Core.Application;

public static class ApplicationServiceRegistration {
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services) {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
