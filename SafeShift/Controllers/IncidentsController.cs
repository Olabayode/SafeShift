using Microsoft.AspNetCore.Mvc;
using SafeShift.BLL.Services.Interfaces;
using SafeShift.Models.DTOs.Incidents;

namespace SafeShift.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentsController : ControllerBase
{
    private readonly IIncidentService _incidentService;

    public IncidentsController(IIncidentService incidentService)
    {
        _incidentService = incidentService;
    }

    [HttpPost]
    public async Task<ActionResult<IncidentReadDto>> CreateIncident(IncidentCreateDto incidentCreateDto)
    {
        try
        {
            var createdIncident = await _incidentService.CreateAsync(incidentCreateDto);

            return CreatedAtAction(nameof(GetIncidentById), new { incidentId = createdIncident.IncidentId }, createdIncident);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }

    [HttpGet("{incidentId:int}")]
    public async Task<ActionResult<IncidentReadDto>> GetIncidentById(int incidentId)
    {
        var incident = await _incidentService.GetByIdAsync(incidentId);
        if (incident is null)
        {
            return NotFound();
        }

        return Ok(incident);
    }
}
