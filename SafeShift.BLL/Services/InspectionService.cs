using AutoMapper;
using SafeShift.BLL.Services.Interfaces;
using SafeShift.DAL.Repositories.Interfaces;
using SafeShift.Models;
using SafeShift.Models.DTOs.Inspections;

namespace SafeShift.BLL.Services;

public class InspectionService : IInspectionService
{
    private readonly IInspectionRepository _inspectionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public InspectionService(
        IInspectionRepository inspectionRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _inspectionRepository = inspectionRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InspectionReadDto>> GetAllAsync()
    {
        var inspections = await _inspectionRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<InspectionReadDto>>(inspections);
    }

    public async Task<InspectionReadDto?> GetByIdAsync(int inspectionId)
    {
        var inspection = await _inspectionRepository.GetByIdAsync(inspectionId);

        return inspection is null ? null : _mapper.Map<InspectionReadDto>(inspection);
    }

    public async Task<InspectionReadDto> CreateAsync(InspectionCreateDto inspectionCreateDto)
    {
        await EnsureUserExistsAsync(inspectionCreateDto.UserId);

        var inspection = _mapper.Map<Inspection>(inspectionCreateDto);
        var createdInspection = await _inspectionRepository.AddAsync(inspection);

        return _mapper.Map<InspectionReadDto>(createdInspection);
    }

    public async Task<InspectionReadDto?> UpdateAsync(int inspectionId, InspectionUpdateDto inspectionUpdateDto)
    {
        await EnsureUserExistsAsync(inspectionUpdateDto.UserId);

        var inspection = _mapper.Map<Inspection>(inspectionUpdateDto);
        inspection.InspectionId = inspectionId;
        var updatedInspection = await _inspectionRepository.UpdateAsync(inspection);

        return updatedInspection is null ? null : _mapper.Map<InspectionReadDto>(updatedInspection);
    }

    public Task<bool> DeleteAsync(int inspectionId)
    {
        return _inspectionRepository.DeleteAsync(inspectionId);
    }

    private async Task EnsureUserExistsAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            throw new KeyNotFoundException("User not found.");
        }
    }
}
