namespace vault_gps.Contracts.Models;

public class GpsPositionItem
{    
    public string AggregateId { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string Latitude { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
    public string UpdateTime { get; set; } = DateTime.UtcNow.ToString();
    public string Description { get; set; } = string.Empty;
}