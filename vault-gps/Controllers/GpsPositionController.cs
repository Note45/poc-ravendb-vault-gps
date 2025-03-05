using Microsoft.AspNetCore.Mvc;
using vault_gps.Contracts.Models;
using vault_gps.Contracts.Services;
using vault_gps.Domain;

namespace vault_gps.Controllers;

[ApiController]
[Route("api/gps")]
public class GpsPositionController: ControllerBase
{
    private IGpsPositionService _service;

    public GpsPositionController(IGpsPositionService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostPosition([FromBody] CreateGpsPositionCommand command)
    {
        var position = await _service.saveGpsPosition((GpsPositionItem)command);
        
        return Ok(position);
    }    
}