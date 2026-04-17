using SafeShift.Models;

namespace SafeShift.DAL.Repositories.Interfaces;

public interface IShiftRepository
{
    Task<IEnumerable<Shift>> GetAllAsync();
    Task<Shift?> GetByIdAsync(int shiftId);
    Task<Shift> AddAsync(Shift shift);
    Task<Shift?> UpdateAsync(Shift shift);
    Task<bool> DeleteAsync(int shiftId);
}
