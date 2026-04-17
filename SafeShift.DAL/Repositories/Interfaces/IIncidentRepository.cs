using SafeShift.Models;

namespace SafeShift.DAL.Repositories.Interfaces;

public interface IIncidentRepository
{
    Task<IEnumerable<Incident>> GetAllAsync(string? severity = null, int? userId = null, DateTime? date = null);
    Task<Incident?> GetByIdAsync(int incidentId);
    Task<Incident> AddAsync(Incident incident);
    Task<Incident?> UpdateAsync(Incident incident);
    Task<bool> DeleteAsync(int incidentId);
}
