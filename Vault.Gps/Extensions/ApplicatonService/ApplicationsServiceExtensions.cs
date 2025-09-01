using vault_gps.Application.Services;
using vault_gps.Contracts.Services;
using vault_gps.Infra.Database.Contracts;
using vault_gps.Infra.Database.Repositories;

namespace vault_gps.Extensions;

public static class ApplicationsServiceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IGpsPositionRepository, GpsPositionRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IGpsPositionService, GpsPositionService>();

        return services;
    }
}