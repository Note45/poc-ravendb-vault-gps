using vault_gps.Contracts.Models;

namespace vault_gps.Domain;

public class CreateGpsPositionCommand
{
    public string EventType { get; set; } = string.Empty;
    public string Latitude { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
    public string UpdateTime { get; set; } = DateTime.UtcNow.ToString();
    public string Type { get; set; } = string.Empty;

    public static explicit operator GpsPositionItem(CreateGpsPositionCommand command)
    {
        return new GpsPositionItem()
        {
            EventType = command.EventType,
            Latitude = command.Latitude,
            Longitude = command.Longitude,
            UpdateTime = command.UpdateTime,
            Type = command.Type,
        };
    }
}