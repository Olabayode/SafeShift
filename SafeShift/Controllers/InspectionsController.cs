using Microsoft.AspNetCore.Mvc;
using SafeShift.BLL.Services.Interfaces;
using SafeShift.Models.DTOs.Common;
using SafeShift.Models.DTOs.Inspections;

namespace SafeShift.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InspectionsController : ControllerBase
{
    private readonly IInspectionService _inspectionService;

    public InspectionsController(IInspectionService inspectionService)
    {
        _inspectionService = inspectionService;
    }

    /// <summary>
    /// Returns inspections with an optional date filter.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InspectionReadDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<InspectionReadDto>>> GetAllInspections([FromQuery] DateTime? date)
    {
        var inspections = await _inspectionService.GetAllAsync(date);

        return Ok(inspections);
    }

    /// <summary>
    /// Returns a single inspection by identifier.
    /// </summary>
    [HttpGet("{inspectionId:int}")]
    [ProducesResponseType(typeof(InspectionReadDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<InspectionReadDto>> GetInspectionById(int inspectionId)
    {
        var inspection = await _inspectionService.GetByIdAsync(inspectionId);
        if (inspection is null)
        {
            return NotFound(new ErrorResponseDto
            {
                Message = "Inspection not found."
            });
        }

        return Ok(inspection);
    }

    /// <summary>
    /// Creates a new inspection record.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(InspectionReadDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<InspectionReadDto>> CreateInspection(InspectionCreateDto inspectionCreateDto)
    {
        var createdInspection = await _inspectionService.CreateAsync(inspectionCreateDto);

        return CreatedAtAction(
            nameof(GetInspectionById),
            new { inspectionId = createdInspection.InspectionId },
            createdInspection);
    }

    /// <summary>
    /// Updates an existing inspection record.
    /// </summary>
    [HttpPut("{inspectionId:int}")]
    [ProducesResponseType(typeof(InspectionReadDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<InspectionReadDto>> UpdateInspection(int inspectionId, InspectionUpdateDto inspectionUpdateDto)
    {
        var updatedInspection = await _inspectionService.UpdateAsync(inspectionId, inspectionUpdateDto);
        if (updatedInspection is null)
        {
            return NotFound(new ErrorResponseDto
            {
                Message = "Inspection not found."
            });
        }

        return Ok(updatedInspection);
    }

    /// <summary>
    /// Deletes an inspection by identifier.
    /// </summary>
    [HttpDelete("{inspectionId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteInspection(int inspectionId)
    {
        var deleted = await _inspectionService.DeleteAsync(inspectionId);
        if (!deleted)
        {
            return NotFound(new ErrorResponseDto
            {
                Message = "Inspection not found."
            });
        }

        return NoContent();
    }
}
