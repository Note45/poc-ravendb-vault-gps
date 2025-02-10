using vault_gps.Contracts.Models;

namespace vault_gps.Infra.Database.Contracts;

public interface IGpsPositionRepository
{
    public Task<GpsPositionItem> saveGpsPositionItem(GpsPositionItem item); 
}