using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;

namespace vault_gps.Infra.Database.Indexes;

public class RavenIndexHostedService : IHostedService
{
    private readonly IDocumentStore _store;

    public RavenIndexHostedService(IDocumentStore store)
    {
        _store = store;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        IndexCreation.CreateIndexes(
            typeof(GpsPositionByAggregateId).Assembly,
            _store
        );

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}