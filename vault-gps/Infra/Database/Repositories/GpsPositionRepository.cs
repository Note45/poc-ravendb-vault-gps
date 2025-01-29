using Raven.Client.Documents;
using vault_gps.Infra.Database.Contracts;

namespace vault_gps.Infra.Database.Repositories;

public class GpsPositionRepository: IGpsPositionRepository
{
    private static Lazy<IDocumentStore> _store;
    public static IDocumentStore _documentStore;

    public GpsPositionRepository(IDocumentStoreHolder _documentHolder)
    {
        _store = new Lazy<IDocumentStore>(_documentHolder.CreateStore);
        _documentStore = _store.Value;
    }
}