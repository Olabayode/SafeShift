using SafeShift.Models.DTOs.Incidents;

namespace SafeShift.BLL.Services.Interfaces;

public interface IIncidentService
{
    Task<IEnumerable<IncidentReadDto>> GetAllAsync();
    Task<IncidentReadDto?> GetByIdAsync(int incidentId);
    Task<IncidentReadDto> CreateAsync(IncidentCreateDto incidentCreateDto);
    Task<IncidentReadDto?> UpdateAsync(int incidentId, IncidentUpdateDto incidentUpdateDto);
    Task<bool> DeleteAsync(int incidentId);
}
