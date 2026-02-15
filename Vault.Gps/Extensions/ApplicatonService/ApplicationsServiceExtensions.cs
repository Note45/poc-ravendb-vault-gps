using vault_gps.Infra.Database.Contracts;
using vault_gps.Infra.Database.Repositories;

namespace vault_gps.Extensions.ApplicatonService;

public static class ApplicationsServiceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IGpsPositionRepository, GpsPositionRepository>();


        return services;
    }
}