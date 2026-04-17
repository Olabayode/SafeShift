using Microsoft.AspNetCore.Mvc;
using SafeShift.BLL.Services.Interfaces;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InspectionReadDto>>> GetAllInspections([FromQuery] DateTime? date)
    {
        var inspections = await _inspectionService.GetAllAsync(date);

        return Ok(inspections);
    }

    [HttpGet("{inspectionId:int}")]
    public async Task<ActionResult<InspectionReadDto>> GetInspectionById(int inspectionId)
    {
        var inspection = await _inspectionService.GetByIdAsync(inspectionId);
        if (inspection is null)
        {
            return NotFound();
        }

        return Ok(inspection);
    }

    [HttpPost]
    public async Task<ActionResult<InspectionReadDto>> CreateInspection(InspectionCreateDto inspectionCreateDto)
    {
        try
        {
            var createdInspection = await _inspectionService.CreateAsync(inspectionCreateDto);

            return CreatedAtAction(
                nameof(GetInspectionById),
                new { inspectionId = createdInspection.InspectionId },
                createdInspection);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }

    [HttpPut("{inspectionId:int}")]
    public async Task<ActionResult<InspectionReadDto>> UpdateInspection(int inspectionId, InspectionUpdateDto inspectionUpdateDto)
    {
        try
        {
            var updatedInspection = await _inspectionService.UpdateAsync(inspectionId, inspectionUpdateDto);
            if (updatedInspection is null)
            {
                return NotFound();
            }

            return Ok(updatedInspection);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }

    [HttpDelete("{inspectionId:int}")]
    public async Task<IActionResult> DeleteInspection(int inspectionId)
    {
        var deleted = await _inspectionService.DeleteAsync(inspectionId);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
