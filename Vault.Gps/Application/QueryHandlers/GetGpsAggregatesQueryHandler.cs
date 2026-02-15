using MediatR;
using vault_gps.Application.DTOs;
using vault_gps.Application.Queries;
using vault_gps.Infra.Database.Contracts;

namespace vault_gps.Application.QueryHandlers;

public class GetGpsAggregatesQueryHandler : IRequestHandler<GetGpsAggregatesQuery, PaginatedResponse<GpsAggregateResponse>>
{
    private readonly IGpsPositionRepository _repository;

    public GetGpsAggregatesQueryHandler(IGpsPositionRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(IGpsPositionRepository));
    }

    public async Task<PaginatedResponse<GpsAggregateResponse>> Handle(GetGpsAggregatesQuery query, CancellationToken cancellationToken)
    {
        var results = await _repository.GetAllGpsPositionAggregateResults(query);

        var responses = results.Select(result => new GpsAggregateResponse
        {
            AggregateId = result.AggregateId,
            Latitude = result.Latitude,
            Longitude = result.Longitude,
            UpdateTime = result.UpdateTime.ToString(),
            Description = result.EventType,
            EventCount = result.TotalEvents
        }).ToList();

        return new PaginatedResponse<GpsAggregateResponse>
        {
            Page = query.Page,
            Size = query.Size,
            TotalCount = responses.Count,
            Items = responses
        };
    }
}

