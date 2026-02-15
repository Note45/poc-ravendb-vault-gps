using MediatR;
using vault_gps.Application.DTOs;
using vault_gps.Application.Queries;
using vault_gps.Infra.Database.Contracts;

namespace vault_gps.Application.QueryHandlers;

public class GetGpsAggregateByIdQueryHandler : IRequestHandler<GetGpsAggregateByIdQuery, GpsAggregateResponse?>
{
    private readonly IGpsPositionRepository _repository;

    public GetGpsAggregateByIdQueryHandler(IGpsPositionRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(IGpsPositionRepository));
    }

    public async Task<GpsAggregateResponse?> Handle(GetGpsAggregateByIdQuery query, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAggregateById(query);

        if (result == null)
        {
            return null;
        }

        return new GpsAggregateResponse
        {
            AggregateId = result.AggregateId,
            Latitude = result.Latitude,
            Longitude = result.Longitude,
            UpdateTime = result.UpdateTime.ToString(),
            Description = result.EventType,
            EventCount = result.TotalEvents
        };
    }
}

