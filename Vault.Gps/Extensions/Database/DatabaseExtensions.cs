using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using vault_gps.Infra.Database;
using vault_gps.Infra.Database.Contracts;
using vault_gps.Infra.Database.Indexes;
using vault_gps.Infra.Database.Options;

namespace vault_gps.Extensions.Database;

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

        services.AddSingleton<IDocumentStore>(sp =>
        {
            var holder = sp.GetRequiredService<IDocumentStoreHolder>();
            var store = holder.CreateStore();
            
            IndexCreation.CreateIndexes(
                typeof(GpsPositionByAggregateId).Assembly,
                store
            );

            return store;
        });
        
        services.AddScoped(sp =>
            sp.GetRequiredService<IDocumentStore>().OpenAsyncSession());
        
        return services;
    }
}