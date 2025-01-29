using vault_gps.Infra.Database;
using vault_gps.Infra.Database.Contracts;
using vault_gps.Infra.Database.Options;

namespace vault_gps.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection("Database"));

        return services;
    }
    
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddSingleton<IDocumentStoreHolder, DocumentStoreHolder>();

        return services;
    }
}