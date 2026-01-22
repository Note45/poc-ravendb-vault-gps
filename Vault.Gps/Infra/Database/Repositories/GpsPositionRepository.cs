using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using vault_gps.Application.Queries;
using vault_gps.Contracts.Models;
using vault_gps.Infra.Database.Contracts;
using vault_gps.Infra.Database.Indexes;

namespace vault_gps.Infra.Database.Repositories;

public class GpsPositionRepository: IGpsPositionRepository
{
    private readonly IAsyncDocumentSession _session;

    public GpsPositionRepository(IDocumentStoreHolder documentHolder)
    {
        var store = new Lazy<IDocumentStore>(documentHolder.GetStore);
        var documentStore = store.Value;
        _session = documentStore.OpenAsyncSession();
    }

    public async Task<GpsPositionItem> SaveGpsPositionItem(GpsPositionItem item)
    {
        await _session.StoreAsync(item);

        await _session.SaveChangesAsync();

        return item;
    }

    public async Task<IEnumerable<GpsPositionItem>> GetAllGpsPositionItems(int page = 0, int size = 30)
    {
        var results = await _session.Query<GpsPositionItem>().Skip(page * size).Take(size).ToListAsync();

        return results; 
    }

    public async Task<IEnumerable<GpsPositionAggregateResult>> GetAllGpsPositionAggregateResults(GetGpsAggregatesQuery query)
    {
        return await _session
            .Query<GpsPositionAggregateResult, GpsPositionByAggregateId>()
            .Skip(query.Page * query.Size)
            .Take(query.Size)
            .ToListAsync();
    }

    public async Task<GpsPositionAggregateResult> GetAggregateById(GetGpsAggregateByIdQuery query)
    {
        return await _session
            .Query<GpsPositionAggregateResult, GpsPositionByAggregateId>()
            .Where(x => x.AggregateId == query.AggregateId)
            .FirstOrDefaultAsync();
    }
}