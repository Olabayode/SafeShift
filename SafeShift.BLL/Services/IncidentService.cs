using AutoMapper;
using SafeShift.BLL.Services.Interfaces;
using SafeShift.DAL.Repositories.Interfaces;
using SafeShift.Models;
using SafeShift.Models.DTOs.Incidents;

namespace SafeShift.BLL.Services;

public class IncidentService : IIncidentService
{
    private readonly IIncidentRepository _incidentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public IncidentService(
        IIncidentRepository incidentRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _incidentRepository = incidentRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<IncidentReadDto>> GetAllAsync(string? severity = null, int? userId = null, DateTime? date = null)
    {
        var incidents = await _incidentRepository.GetAllAsync(severity, userId, date);

        return _mapper.Map<IEnumerable<IncidentReadDto>>(incidents);
    }

    public async Task<IncidentReadDto?> GetByIdAsync(int incidentId)
    {
        var incident = await _incidentRepository.GetByIdAsync(incidentId);

        return incident is null ? null : _mapper.Map<IncidentReadDto>(incident);
    }

    public async Task<IncidentReadDto> CreateAsync(IncidentCreateDto incidentCreateDto)
    {
        await EnsureUserExistsAsync(incidentCreateDto.UserId);

        var incident = _mapper.Map<Incident>(incidentCreateDto);
        var createdIncident = await _incidentRepository.AddAsync(incident);

        return _mapper.Map<IncidentReadDto>(createdIncident);
    }

    public async Task<IncidentReadDto?> UpdateAsync(int incidentId, IncidentUpdateDto incidentUpdateDto)
    {
        await EnsureUserExistsAsync(incidentUpdateDto.UserId);

        var incident = _mapper.Map<Incident>(incidentUpdateDto);
        incident.IncidentId = incidentId;
        var updatedIncident = await _incidentRepository.UpdateAsync(incident);

        return updatedIncident is null ? null : _mapper.Map<IncidentReadDto>(updatedIncident);
    }

    public Task<bool> DeleteAsync(int incidentId)
    {
        return _incidentRepository.DeleteAsync(incidentId);
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
