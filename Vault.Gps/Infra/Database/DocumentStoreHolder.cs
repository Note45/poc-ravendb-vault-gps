using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using vault_gps.Infra.Database.Contracts;
using vault_gps.Infra.Database.Options;

namespace vault_gps.Infra.Database;

public class DocumentStoreHolder : IDocumentStoreHolder
{
    private readonly IDocumentStore _store;

    public DocumentStoreHolder(IOptions<DatabaseOptions> options)
    {
        var opts = options.Value;

        _store = new DocumentStore
        {
            Urls = new[] { opts.Url },
            Database = opts.Base,
            Conventions =
            {
                MaxNumberOfRequestsPerSession = 10,
                UseOptimisticConcurrency = true
            }
        }.Initialize();
    }

    public IDocumentStore GetStore() => _store;
}
