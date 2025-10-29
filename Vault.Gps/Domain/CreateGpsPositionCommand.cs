using vault_gps.Contracts.Enums;
using vault_gps.Contracts.Models;

namespace vault_gps.Domain;

public class CreateGpsPositionCommand
{
    public string AggregateId { get; set; } = string.Empty;
    public EventTypeEnum EventType { get; set; } = EventTypeEnum.GpsPositionItemCreated;
    public string Latitude { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
    public string UpdateTime { get; set; } = DateTime.UtcNow.ToString();
    public string Description { get; set; } = string.Empty;

    public static explicit operator GpsPositionItem(CreateGpsPositionCommand command)
    {
        return new GpsPositionItem()
        {
            AggregateId = command.AggregateId,
            EventType = command.EventType.ToString(),
            Latitude = command.Latitude,
            Longitude = command.Longitude,
            UpdateTime = command.UpdateTime,
            Description = command.Description,
        };
    }
}