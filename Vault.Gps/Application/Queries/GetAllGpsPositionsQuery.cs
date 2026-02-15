using MediatR;
using vault_gps.Application.DTOs;

namespace vault_gps.Application.Queries;

public record GetAllGpsPositionsQuery(int Page = 0, int Size = 30) : IRequest<PaginatedResponse<GpsPositionResponse>>;

