using SafeShift.Models;

namespace SafeShift.DAL.Repositories.Interfaces;

public interface IInspectionRepository
{
    Task<IEnumerable<Inspection>> GetAllAsync();
    Task<Inspection?> GetByIdAsync(int inspectionId);
    Task<Inspection> AddAsync(Inspection inspection);
    Task<Inspection?> UpdateAsync(Inspection inspection);
    Task<bool> DeleteAsync(int inspectionId);
}
