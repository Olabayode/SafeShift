using Microsoft.EntityFrameworkCore;
using SafeShift.DAL.Data;
using SafeShift.DAL.Repositories.Interfaces;
using SafeShift.Models;

namespace SafeShift.DAL.Repositories;

public class ShiftRepository : IShiftRepository
{
    private readonly SafeShiftDbContext _context;

    public ShiftRepository(SafeShiftDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Shift>> GetAllAsync(int? userId = null, DateTime? date = null)
    {
        var query = _context.Shifts
            .AsNoTracking()
            .Include(shift => shift.User)
            .AsQueryable();

        if (userId.HasValue)
        {
            query = query.Where(shift => shift.UserId == userId.Value);
        }

        if (date.HasValue)
        {
            var targetDate = date.Value.Date;
            query = query.Where(shift =>
                shift.StartTime.Date == targetDate ||
                shift.EndTime.Date == targetDate);
        }

        return await query.ToListAsync();
    }

    public async Task<Shift?> GetByIdAsync(int shiftId)
    {
        return await _context.Shifts
            .AsNoTracking()
            .Include(shift => shift.User)
            .FirstOrDefaultAsync(shift => shift.ShiftId == shiftId);
    }

    public async Task<Shift> AddAsync(Shift shift)
    {
        await _context.Shifts.AddAsync(shift);
        await _context.SaveChangesAsync();

        return shift;
    }

    public async Task<Shift?> UpdateAsync(Shift shift)
    {
        var existingShift = await _context.Shifts.FindAsync(shift.ShiftId);
        if (existingShift is null)
        {
            return null;
        }

        existingShift.StartTime = shift.StartTime;
        existingShift.EndTime = shift.EndTime;
        existingShift.UserId = shift.UserId;

        await _context.SaveChangesAsync();

        return existingShift;
    }

    public async Task<bool> DeleteAsync(int shiftId)
    {
        var shift = await _context.Shifts.FindAsync(shiftId);
        if (shift is null)
        {
            return false;
        }

        _context.Shifts.Remove(shift);
        await _context.SaveChangesAsync();

        return true;
    }
}
