using MediatR;
using Microsoft.AspNetCore.Mvc;
using vault_gps.Application.Commands;
using vault_gps.Application.Queries;

namespace vault_gps.Controllers;

[ApiController]
[Route("api/gps")]
public class GpsPositionController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostPosition([FromBody] CreateGpsPositionCommand command)
    {
        var result = await mediator.Send(command);
        
        return CreatedAtAction(nameof(GetAggregateById), new { aggregateId = result.AggregateId }, result);
    }  
    
    [HttpGet]
    public async Task<IActionResult> GetPositions(
        [FromQuery] int page = 0, 
        [FromQuery] int size = 30)
    {
        var query = new GetAllGpsPositionsQuery(page, size);
        var results = await mediator.Send(query);
        
        return Ok(results);
    }
    
    [HttpGet("aggregates/{aggregateId}")]
    public async Task<IActionResult> GetAggregateById(string aggregateId)
    {
        var query = new GetGpsAggregateByIdQuery(aggregateId);
        var result = await mediator.Send(query);

        return result is null ? NotFound() : Ok(result);
    }
    
    [HttpGet("aggregates")]
    public async Task<IActionResult> GetAggregates(
        [FromQuery] int page = 0,
        [FromQuery] int size = 30)
    {
        var query = new GetGpsAggregatesQuery(page, size);
        var results = await mediator.Send(query);
        
        return Ok(results);
    }
}