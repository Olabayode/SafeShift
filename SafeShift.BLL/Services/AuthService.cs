using System.Security.Cryptography;
using System.Text;
using SafeShift.BLL.Services.Interfaces;
using SafeShift.DAL.Repositories.Interfaces;
using SafeShift.Models;
using SafeShift.Models.DTOs.Auth;

namespace SafeShift.BLL.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterUserDto registerUserDto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(registerUserDto.Email);
        if (existingUser is not null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "A user with this email already exists."
            };
        }

        var user = new User
        {
            Name = registerUserDto.Name,
            Email = registerUserDto.Email,
            PasswordHash = HashPassword(registerUserDto.Password),
            Role = string.IsNullOrWhiteSpace(registerUserDto.Role) ? "User" : registerUserDto.Role
        };

        var createdUser = await _userRepository.AddAsync(user);

        return CreateAuthResponse(createdUser, "Registration successful.");
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginDto.Email);
        if (user is null)
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        var passwordHash = HashPassword(loginDto.Password);
        if (!string.Equals(user.PasswordHash, passwordHash, StringComparison.Ordinal))
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }

        return CreateAuthResponse(user, "Login successful.");
    }

    private static AuthResponseDto CreateAuthResponse(User user, string message)
    {
        return new AuthResponseDto
        {
            Success = true,
            Message = message,
            UserId = user.UserId,
            Email = user.Email,
            Role = user.Role
        };
    }

    private static string HashPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = SHA256.HashData(bytes);

        return Convert.ToBase64String(hash);
    }
}
