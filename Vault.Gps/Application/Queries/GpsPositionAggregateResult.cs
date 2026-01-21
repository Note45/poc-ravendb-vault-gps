namespace vault_gps.Application.Queries;

public class GpsPositionAggregateResult
{
    public string AggregateId { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string Latitude { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
    public DateTime UpdateTime { get; set; }
    public int TotalEvents { get; set; }
}