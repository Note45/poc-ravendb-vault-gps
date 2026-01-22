using Microsoft.AspNetCore.Mvc;
using vault_gps.Application.Commands;
using vault_gps.Application.Queries;
using vault_gps.Contracts.Models;
using vault_gps.Contracts.Services;

namespace vault_gps.Controllers;

[ApiController]
[Route("api/gps")]
public class GpsPositionController(IGpsPositionService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostPosition([FromBody] CreateGpsPositionCommand command)
    {
        var position = await service.SaveGpsPosition((GpsPositionItem)command);
        
        return Ok(position);
    }  
    
    [HttpGet]
    public async Task<IActionResult> GetPositions(
        [FromQuery] int page = 0, 
        [FromQuery] int size = 30)
    {
        var results = await service.GetAllGpsPosition(page, size);
        
        return Ok(results);
    }
    
    [HttpGet("aggregates/{aggregateId}")]
    public async Task<IActionResult> GetAggregateById(string aggregateId)
    {
        var result = await service.GetAggregateById(new GetGpsAggregateByIdQuery(aggregateId));

        return result is null ? NotFound() : Ok(result);
    }
    
    [HttpGet("aggregates")]
    public async Task<IActionResult> GetAggregates(
        [FromQuery] int page = 0,
        [FromQuery] int size = 30)
    {
        var results = await service.GetAllGpsPositionAggregateResults(
                new GetGpsAggregatesQuery(page, size)
            );
        
        return Ok(results);
    }
}