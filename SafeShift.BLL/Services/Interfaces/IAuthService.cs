using SafeShift.Models.DTOs.Auth;

namespace SafeShift.BLL.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterUserDto registerUserDto);
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
}
