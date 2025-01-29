using vault_gps.Infra.Database.Options;

namespace vault_gps.Extensions;

public static class ApplicationsServiceExtensions
{
    public static IServiceCollection AddDatabaseConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection("Database"));

        return services;
    }
}