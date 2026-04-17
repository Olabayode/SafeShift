using SafeShift.Models.DTOs.Inspections;

namespace SafeShift.BLL.Services.Interfaces;

public interface IInspectionService
{
    Task<IEnumerable<InspectionReadDto>> GetAllAsync();
    Task<InspectionReadDto?> GetByIdAsync(int inspectionId);
    Task<InspectionReadDto> CreateAsync(InspectionCreateDto inspectionCreateDto);
    Task<InspectionReadDto?> UpdateAsync(int inspectionId, InspectionUpdateDto inspectionUpdateDto);
    Task<bool> DeleteAsync(int inspectionId);
}
