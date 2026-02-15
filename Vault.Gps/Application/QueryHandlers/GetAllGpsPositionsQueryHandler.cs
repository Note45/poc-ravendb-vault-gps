using MediatR;
using vault_gps.Application.DTOs;
using vault_gps.Application.Queries;
using vault_gps.Infra.Database.Contracts;

namespace vault_gps.Application.QueryHandlers;

public class GetAllGpsPositionsQueryHandler : IRequestHandler<GetAllGpsPositionsQuery, PaginatedResponse<GpsPositionResponse>>
{
    private readonly IGpsPositionRepository _repository;

    public GetAllGpsPositionsQueryHandler(IGpsPositionRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(IGpsPositionRepository));
    }

    public async Task<PaginatedResponse<GpsPositionResponse>> Handle(GetAllGpsPositionsQuery query, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllGpsPositionItems(query.Page, query.Size);

        var responses = items.Select(item => new GpsPositionResponse
        {
            Id = item.Id ?? string.Empty,
            AggregateId = item.AggregateId,
            EventType = item.EventType,
            Latitude = item.Latitude,
            Longitude = item.Longitude,
            UpdateTime = item.UpdateTime,
            Description = item.Description,
        }).ToList();

        return new PaginatedResponse<GpsPositionResponse>
        {
            Page = query.Page,
            Size = query.Size,
            TotalCount = responses.Count,
            Items = responses
        };
    }
}

