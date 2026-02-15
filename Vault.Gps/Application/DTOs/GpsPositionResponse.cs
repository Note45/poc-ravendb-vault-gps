namespace vault_gps.Application.DTOs;

public class GpsPositionResponse
{
    public string Id { get; set; } = string.Empty;
    public string AggregateId { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string Latitude { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
    public string UpdateTime { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

