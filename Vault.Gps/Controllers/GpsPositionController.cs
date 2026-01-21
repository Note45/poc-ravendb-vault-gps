using Microsoft.AspNetCore.Mvc;
using vault_gps.Application.Commands;
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
        var position = await service.saveGpsPosition((GpsPositionItem)command);
        
        return Ok(position);
    }  
    
    [HttpGet]
    public async Task<IActionResult> GetPositions([FromQuery] int page = 0, [FromQuery] int size = 30)
    {
        var results = await service.getAllGpsPosition(page, size);
        
        return Ok(results);
    }    
}