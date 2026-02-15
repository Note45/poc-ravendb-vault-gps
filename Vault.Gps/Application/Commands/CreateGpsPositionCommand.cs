using System.ComponentModel;
using MediatR;
using vault_gps.Application.DTOs;
using vault_gps.Contracts.Enums;

namespace vault_gps.Application.Commands;

public class CreateGpsPositionCommand : IRequest<GpsPositionResponse>
{
    public string AggregateId { get; set; } = string.Empty;
    [DefaultValue(nameof(EventTypeEnum.GpsPositionItemCreated))]
    public string EventType { get; set; } = nameof(EventTypeEnum.GpsPositionItemCreated);
    public string Latitude { get; set; } = string.Empty;
    public string Longitude { get; set; } = string.Empty;
    public string UpdateTime { get; set; } = DateTime.UtcNow.ToString();
    public string Description { get; set; } = string.Empty;
}