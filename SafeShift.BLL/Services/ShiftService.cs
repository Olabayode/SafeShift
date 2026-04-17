using AutoMapper;
using SafeShift.BLL.Services.Interfaces;
using SafeShift.DAL.Repositories.Interfaces;
using SafeShift.Models;
using SafeShift.Models.DTOs.Shifts;

namespace SafeShift.BLL.Services;

public class ShiftService : IShiftService
{
    private readonly IShiftRepository _shiftRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ShiftService(
        IShiftRepository shiftRepository,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _shiftRepository = shiftRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ShiftReadDto>> GetAllAsync()
    {
        var shifts = await _shiftRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<ShiftReadDto>>(shifts);
    }

    public async Task<ShiftReadDto?> GetByIdAsync(int shiftId)
    {
        var shift = await _shiftRepository.GetByIdAsync(shiftId);

        return shift is null ? null : _mapper.Map<ShiftReadDto>(shift);
    }

    public async Task<ShiftReadDto> CreateAsync(ShiftCreateDto shiftCreateDto)
    {
        await EnsureUserExistsAsync(shiftCreateDto.UserId);

        var shift = _mapper.Map<Shift>(shiftCreateDto);
        var createdShift = await _shiftRepository.AddAsync(shift);

        return _mapper.Map<ShiftReadDto>(createdShift);
    }

    public async Task<ShiftReadDto?> UpdateAsync(int shiftId, ShiftUpdateDto shiftUpdateDto)
    {
        await EnsureUserExistsAsync(shiftUpdateDto.UserId);

        var shift = _mapper.Map<Shift>(shiftUpdateDto);
        shift.ShiftId = shiftId;
        var updatedShift = await _shiftRepository.UpdateAsync(shift);

        return updatedShift is null ? null : _mapper.Map<ShiftReadDto>(updatedShift);
    }

    public Task<bool> DeleteAsync(int shiftId)
    {
        return _shiftRepository.DeleteAsync(shiftId);
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
