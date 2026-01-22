using vault_gps.Application.Queries;
using vault_gps.Contracts.Models;

namespace vault_gps.Infra.Database.Contracts;

public interface IGpsPositionRepository
{
    public Task<GpsPositionItem> SaveGpsPositionItem(GpsPositionItem item); 
    public Task<IEnumerable<GpsPositionItem>> GetAllGpsPositionItems(int page, int size); 
    public Task<IEnumerable<GpsPositionAggregateResult>> GetAllGpsPositionAggregateResults(GetGpsAggregatesQuery query);
    public Task<GpsPositionAggregateResult> GetAggregateById(GetGpsAggregateByIdQuery query);
}