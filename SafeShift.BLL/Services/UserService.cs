using AutoMapper;
using SafeShift.BLL.Services.Interfaces;
using SafeShift.DAL.Repositories.Interfaces;
using SafeShift.Models.DTOs.Users;

namespace SafeShift.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserReadDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserReadDto>>(users);
    }

    public async Task<UserReadDto?> GetByIdAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        return user is null ? null : _mapper.Map<UserReadDto>(user);
    }

    public Task<bool> DeleteAsync(int userId)
    {
        return _userRepository.DeleteAsync(userId);
    }
}
