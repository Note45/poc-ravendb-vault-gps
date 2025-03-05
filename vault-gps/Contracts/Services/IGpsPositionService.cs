using vault_gps.Contracts.Models;

namespace vault_gps.Contracts.Services;

public interface IGpsPositionService
{
    public Task<GpsPositionItem> saveGpsPosition(GpsPositionItem item); 
}