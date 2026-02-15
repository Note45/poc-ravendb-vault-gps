using MediatR;
using vault_gps.Application.Commands;
using vault_gps.Application.DTOs;
using vault_gps.Contracts.Models;
using vault_gps.Infra.Database.Contracts;

namespace vault_gps.Application.CommandHandlers;

public class CreateGpsPositionCommandHandler : IRequestHandler<CreateGpsPositionCommand, GpsPositionResponse>
{
    private readonly IGpsPositionRepository _repository;

    public CreateGpsPositionCommandHandler(IGpsPositionRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(IGpsPositionRepository));
    }

    public async Task<GpsPositionResponse> Handle(CreateGpsPositionCommand command, CancellationToken cancellationToken)
    {
        var gpsPositionItem = new GpsPositionItem
        {
            AggregateId = command.AggregateId,
            EventType = command.EventType,
            Latitude = command.Latitude,
            Longitude = command.Longitude,
            UpdateTime = command.UpdateTime,
            Description = command.Description,
        };

        var result = await _repository.SaveGpsPositionItem(gpsPositionItem);

        return new GpsPositionResponse
        {
            Id = result.Id ?? string.Empty,
            AggregateId = result.AggregateId,
            EventType = result.EventType,
            Latitude = result.Latitude,
            Longitude = result.Longitude,
            UpdateTime = result.UpdateTime,
            Description = result.Description,
        };
    }
}

