using Microsoft.AspNetCore.Mvc;
using SafeShift.BLL.Services.Interfaces;
using SafeShift.Models.DTOs.Common;
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

    /// <summary>
    /// Returns incidents with optional severity, user, and date filters.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<IncidentReadDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<IncidentReadDto>>> GetAllIncidents(
        [FromQuery] string? severity,
        [FromQuery] int? userId,
        [FromQuery] DateTime? date)
    {
        var incidents = await _incidentService.GetAllAsync(severity, userId, date);

        return Ok(incidents);
    }

    /// <summary>
    /// Creates a new incident record.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(IncidentReadDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<IncidentReadDto>> CreateIncident(IncidentCreateDto incidentCreateDto)
    {
        var createdIncident = await _incidentService.CreateAsync(incidentCreateDto);

        return CreatedAtAction(nameof(GetIncidentById), new { incidentId = createdIncident.IncidentId }, createdIncident);
    }

    /// <summary>
    /// Returns a single incident by identifier.
    /// </summary>
    [HttpGet("{incidentId:int}")]
    [ProducesResponseType(typeof(IncidentReadDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<IncidentReadDto>> GetIncidentById(int incidentId)
    {
        var incident = await _incidentService.GetByIdAsync(incidentId);
        if (incident is null)
        {
            return NotFound(new ErrorResponseDto
            {
                Message = "Incident not found."
            });
        }

        return Ok(incident);
    }

    /// <summary>
    /// Updates an existing incident record.
    /// </summary>
    [HttpPut("{incidentId:int}")]
    [ProducesResponseType(typeof(IncidentReadDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<IncidentReadDto>> UpdateIncident(int incidentId, IncidentUpdateDto incidentUpdateDto)
    {
        var updatedIncident = await _incidentService.UpdateAsync(incidentId, incidentUpdateDto);
        if (updatedIncident is null)
        {
            return NotFound(new ErrorResponseDto
            {
                Message = "Incident not found."
            });
        }

        return Ok(updatedIncident);
    }

    /// <summary>
    /// Deletes an incident by identifier.
    /// </summary>
    [HttpDelete("{incidentId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteIncident(int incidentId)
    {
        var deleted = await _incidentService.DeleteAsync(incidentId);
        if (!deleted)
        {
            return NotFound(new ErrorResponseDto
            {
                Message = "Incident not found."
            });
        }

        return NoContent();
    }
}
