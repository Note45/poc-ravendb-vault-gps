using vault_gps.Application.Queries;
using vault_gps.Contracts.Models;

namespace vault_gps.Contracts.Services;

public interface IGpsPositionService
{
    public Task<GpsPositionItem> SaveGpsPosition(GpsPositionItem item); 
    public Task<IEnumerable<GpsPositionItem>> GetAllGpsPosition(int page, int size); 
    public Task<IEnumerable<GpsPositionAggregateResult>> GetAllGpsPositionAggregateResults(GetGpsAggregatesQuery query);
    public Task<GpsPositionAggregateResult> GetAggregateById(GetGpsAggregateByIdQuery query);
}