using Microsoft.AspNetCore.Mvc;
using SafeShift.BLL.Services.Interfaces;
using SafeShift.Models.DTOs.Common;
using SafeShift.Models.DTOs.Shifts;

namespace SafeShift.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShiftsController : ControllerBase
{
    private readonly IShiftService _shiftService;

    public ShiftsController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    /// <summary>
    /// Returns shifts with optional user and date filters.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ShiftReadDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ShiftReadDto>>> GetAllShifts(
        [FromQuery] int? userId,
        [FromQuery] DateTime? date)
    {
        var shifts = await _shiftService.GetAllAsync(userId, date);

        return Ok(shifts);
    }

    /// <summary>
    /// Returns a single shift by identifier.
    /// </summary>
    [HttpGet("{shiftId:int}")]
    [ProducesResponseType(typeof(ShiftReadDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<ShiftReadDto>> GetShiftById(int shiftId)
    {
        var shift = await _shiftService.GetByIdAsync(shiftId);
        if (shift is null)
        {
            return NotFound(new ErrorResponseDto
            {
                Message = "Shift not found."
            });
        }

        return Ok(shift);
    }

    /// <summary>
    /// Creates a new shift record.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ShiftReadDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<ShiftReadDto>> CreateShift(ShiftCreateDto shiftCreateDto)
    {
        var createdShift = await _shiftService.CreateAsync(shiftCreateDto);

        return CreatedAtAction(
            nameof(GetShiftById),
            new { shiftId = createdShift.ShiftId },
            createdShift);
    }

    /// <summary>
    /// Updates an existing shift record.
    /// </summary>
    [HttpPut("{shiftId:int}")]
    [ProducesResponseType(typeof(ShiftReadDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<ShiftReadDto>> UpdateShift(int shiftId, ShiftUpdateDto shiftUpdateDto)
    {
        var updatedShift = await _shiftService.UpdateAsync(shiftId, shiftUpdateDto);
        if (updatedShift is null)
        {
            return NotFound(new ErrorResponseDto
            {
                Message = "Shift not found."
            });
        }

        return Ok(updatedShift);
    }

    /// <summary>
    /// Deletes a shift by identifier.
    /// </summary>
    [HttpDelete("{shiftId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteShift(int shiftId)
    {
        var deleted = await _shiftService.DeleteAsync(shiftId);
        if (!deleted)
        {
            return NotFound(new ErrorResponseDto
            {
                Message = "Shift not found."
            });
        }

        return NoContent();
    }
}
