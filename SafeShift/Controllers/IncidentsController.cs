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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IncidentReadDto>>> GetAllIncidents(
        [FromQuery] string? severity,
        [FromQuery] int? userId,
        [FromQuery] DateTime? date)
    {
        var incidents = await _incidentService.GetAllAsync(severity, userId, date);

        return Ok(incidents);
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

    [HttpPut("{incidentId:int}")]
    public async Task<ActionResult<IncidentReadDto>> UpdateIncident(int incidentId, IncidentUpdateDto incidentUpdateDto)
    {
        try
        {
            var updatedIncident = await _incidentService.UpdateAsync(incidentId, incidentUpdateDto);
            if (updatedIncident is null)
            {
                return NotFound();
            }

            return Ok(updatedIncident);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }

    [HttpDelete("{incidentId:int}")]
    public async Task<IActionResult> DeleteIncident(int incidentId)
    {
        var deleted = await _incidentService.DeleteAsync(incidentId);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
