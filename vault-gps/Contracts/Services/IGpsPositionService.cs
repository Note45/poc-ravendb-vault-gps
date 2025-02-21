using vault_gps.Contracts.Models;

namespace vault_gps.Contracts.Services;

public class IGpsPositionService
{
    public Task<GpsPositionItem> saveGpsPosition(GpsPositionItem item); 
}