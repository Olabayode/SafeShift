using Microsoft.EntityFrameworkCore;
using SafeShift.DAL.Data;
using SafeShift.DAL.Repositories.Interfaces;
using SafeShift.Models;

namespace SafeShift.DAL.Repositories;

public class IncidentRepository : IIncidentRepository
{
    private readonly SafeShiftDbContext _context;

    public IncidentRepository(SafeShiftDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Incident>> GetAllAsync(string? severity = null, int? userId = null, DateTime? date = null)
    {
        var query = _context.Incidents
            .AsNoTracking()
            .Include(incident => incident.User)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(severity))
        {
            query = query.Where(incident => incident.Severity == severity);
        }

        if (userId.HasValue)
        {
            query = query.Where(incident => incident.UserId == userId.Value);
        }

        if (date.HasValue)
        {
            var targetDate = date.Value.Date;
            query = query.Where(incident => incident.Date.Date == targetDate);
        }

        return await query.ToListAsync();
    }

    public async Task<Incident?> GetByIdAsync(int incidentId)
    {
        return await _context.Incidents
            .AsNoTracking()
            .Include(incident => incident.User)
            .FirstOrDefaultAsync(incident => incident.IncidentId == incidentId);
    }

    public async Task<Incident> AddAsync(Incident incident)
    {
        await _context.Incidents.AddAsync(incident);
        await _context.SaveChangesAsync();

        return incident;
    }

    public async Task<Incident?> UpdateAsync(Incident incident)
    {
        var existingIncident = await _context.Incidents.FindAsync(incident.IncidentId);
        if (existingIncident is null)
        {
            return null;
        }

        existingIncident.Description = incident.Description;
        existingIncident.Date = incident.Date;
        existingIncident.Severity = incident.Severity;
        existingIncident.UserId = incident.UserId;

        await _context.SaveChangesAsync();

        return existingIncident;
    }

    public async Task<bool> DeleteAsync(int incidentId)
    {
        var incident = await _context.Incidents.FindAsync(incidentId);
        if (incident is null)
        {
            return false;
        }

        _context.Incidents.Remove(incident);
        await _context.SaveChangesAsync();

        return true;
    }
}
