using Microsoft.AspNetCore.Mvc;
using SafeShift.BLL.Services.Interfaces;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShiftReadDto>>> GetAllShifts(
        [FromQuery] int? userId,
        [FromQuery] DateTime? date)
    {
        var shifts = await _shiftService.GetAllAsync(userId, date);

        return Ok(shifts);
    }

    [HttpGet("{shiftId:int}")]
    public async Task<ActionResult<ShiftReadDto>> GetShiftById(int shiftId)
    {
        var shift = await _shiftService.GetByIdAsync(shiftId);
        if (shift is null)
        {
            return NotFound();
        }

        return Ok(shift);
    }

    [HttpPost]
    public async Task<ActionResult<ShiftReadDto>> CreateShift(ShiftCreateDto shiftCreateDto)
    {
        try
        {
            var createdShift = await _shiftService.CreateAsync(shiftCreateDto);

            return CreatedAtAction(
                nameof(GetShiftById),
                new { shiftId = createdShift.ShiftId },
                createdShift);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }

    [HttpPut("{shiftId:int}")]
    public async Task<ActionResult<ShiftReadDto>> UpdateShift(int shiftId, ShiftUpdateDto shiftUpdateDto)
    {
        try
        {
            var updatedShift = await _shiftService.UpdateAsync(shiftId, shiftUpdateDto);
            if (updatedShift is null)
            {
                return NotFound();
            }

            return Ok(updatedShift);
        }
        catch (KeyNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }

    [HttpDelete("{shiftId:int}")]
    public async Task<IActionResult> DeleteShift(int shiftId)
    {
        var deleted = await _shiftService.DeleteAsync(shiftId);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
