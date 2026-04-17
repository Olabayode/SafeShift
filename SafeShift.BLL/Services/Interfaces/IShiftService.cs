using SafeShift.Models.DTOs.Shifts;

namespace SafeShift.BLL.Services.Interfaces;

public interface IShiftService
{
    Task<IEnumerable<ShiftReadDto>> GetAllAsync(int? userId = null, DateTime? date = null);
    Task<ShiftReadDto?> GetByIdAsync(int shiftId);
    Task<ShiftReadDto> CreateAsync(ShiftCreateDto shiftCreateDto);
    Task<ShiftReadDto?> UpdateAsync(int shiftId, ShiftUpdateDto shiftUpdateDto);
    Task<bool> DeleteAsync(int shiftId);
}
