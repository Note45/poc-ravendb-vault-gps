using vault_gps.Application.Queries;
using vault_gps.Contracts.Models;
using vault_gps.Contracts.Services;
using vault_gps.Infra.Database.Contracts;

namespace vault_gps.Application.Services;

public class GpsPositionService: IGpsPositionService
{
    private readonly IGpsPositionRepository _repository;

    public GpsPositionService(IGpsPositionRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(IGpsPositionRepository));
    }
    
    public async Task<GpsPositionItem> SaveGpsPosition(GpsPositionItem item)
    {
        var result = await this._repository.SaveGpsPositionItem(item);

        return result;
    }

    public Task<IEnumerable<GpsPositionItem>> GetAllGpsPosition(int page = 0, int size = 30)
    {
        var results = this._repository.GetAllGpsPositionItems(page, size);

        return results;
    }
    
    public Task<IEnumerable<GpsPositionAggregateResult>> GetAllGpsPositionAggregateResults(GetGpsAggregatesQuery query)
    {
        var results = this._repository.GetAllGpsPositionAggregateResults(query);

        return results;
    }

    public Task<GpsPositionAggregateResult> GetAggregateById(GetGpsAggregateByIdQuery query)
    {
        var results = this._repository.GetAggregateById(query);

        return results;
    }
}