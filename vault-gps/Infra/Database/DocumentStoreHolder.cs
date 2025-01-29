using Microsoft.Extensions.Options;
using Raven.Client.Documents;
using vault_gps.Infra.Database.Contracts;
using vault_gps.Infra.Database.Options;

namespace vault_gps.Infra.Database;

public class DocumentStoreHolder: IDocumentStoreHolder
{
    private DatabaseOptions _options;
    
    public DocumentStoreHolder(IOptions<DatabaseOptions>? options = null)
    {
        _options = options is not null ? options.Value : new DatabaseOptions();
    }

    public IDocumentStore CreateStore()
    {
        IDocumentStore store = new DocumentStore()
        {
            Urls = [_options.Url],

            Conventions =
            {
                MaxNumberOfRequestsPerSession = 10,
                UseOptimisticConcurrency = true
            },

            Database = _options.Base,
        }.Initialize();

        return store;
    }
}
