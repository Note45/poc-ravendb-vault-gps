namespace vault_gps.Application.DTOs;

public class GpsAggregateResponse
{
    public string AggregateId { get; set; } = string.Empty;
    public string Latitude { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
    public string UpdateTime { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int EventCount { get; set; }
}

