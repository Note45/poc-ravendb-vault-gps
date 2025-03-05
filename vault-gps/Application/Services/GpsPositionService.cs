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
    
    public async Task<GpsPositionItem> saveGpsPosition(GpsPositionItem item)
    {
        var result = await this._repository.saveGpsPositionItem(item);

        return result;
    }
}