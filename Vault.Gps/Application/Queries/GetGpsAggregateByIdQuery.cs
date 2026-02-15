using MediatR;
using vault_gps.Application.DTOs;

namespace vault_gps.Application.Queries;

public record GetGpsAggregateByIdQuery(string AggregateId) : IRequest<GpsAggregateResponse?>;
