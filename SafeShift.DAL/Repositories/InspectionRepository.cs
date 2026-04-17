using Microsoft.EntityFrameworkCore;
using SafeShift.DAL.Data;
using SafeShift.DAL.Repositories.Interfaces;
using SafeShift.Models;

namespace SafeShift.DAL.Repositories;

public class InspectionRepository : IInspectionRepository
{
    private readonly SafeShiftDbContext _context;

    public InspectionRepository(SafeShiftDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Inspection>> GetAllAsync()
    {
        return await _context.Inspections
            .AsNoTracking()
            .Include(inspection => inspection.User)
            .ToListAsync();
    }

    public async Task<Inspection?> GetByIdAsync(int inspectionId)
    {
        return await _context.Inspections
            .AsNoTracking()
            .Include(inspection => inspection.User)
            .FirstOrDefaultAsync(inspection => inspection.InspectionId == inspectionId);
    }

    public async Task<Inspection> AddAsync(Inspection inspection)
    {
        await _context.Inspections.AddAsync(inspection);
        await _context.SaveChangesAsync();

        return inspection;
    }

    public async Task<Inspection?> UpdateAsync(Inspection inspection)
    {
        var existingInspection = await _context.Inspections.FindAsync(inspection.InspectionId);
        if (existingInspection is null)
        {
            return null;
        }

        existingInspection.Date = inspection.Date;
        existingInspection.Notes = inspection.Notes;
        existingInspection.UserId = inspection.UserId;

        await _context.SaveChangesAsync();

        return existingInspection;
    }

    public async Task<bool> DeleteAsync(int inspectionId)
    {
        var inspection = await _context.Inspections.FindAsync(inspectionId);
        if (inspection is null)
        {
            return false;
        }

        _context.Inspections.Remove(inspection);
        await _context.SaveChangesAsync();

        return true;
    }
}
