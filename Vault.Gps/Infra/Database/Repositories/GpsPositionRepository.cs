using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using vault_gps.Contracts.Models;
using vault_gps.Infra.Database.Contracts;

namespace vault_gps.Infra.Database.Repositories;

public class GpsPositionRepository: IGpsPositionRepository
{
    private static Lazy<IDocumentStore> _store;
    private static IDocumentStore _documentStore;
    private IAsyncDocumentSession _session;

    public GpsPositionRepository(IDocumentStoreHolder _documentHolder)
    {
        _store = new Lazy<IDocumentStore>(_documentHolder.CreateStore);
        _documentStore = _store.Value;
        _session = _documentStore.OpenAsyncSession();
    }

    public async Task<GpsPositionItem> saveGpsPositionItem(GpsPositionItem item)
    {
        await _session.StoreAsync(item);

        await _session.SaveChangesAsync();

        return item;
    }

    public async Task<IEnumerable<GpsPositionItem>> getAllGpsPositionItems(int page = 0, int size = 30)
    {
        var results = await _session.Query<GpsPositionItem>().Skip(page * size).Take(size).ToListAsync();

        return results; 
    }
}