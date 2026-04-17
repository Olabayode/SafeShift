using Microsoft.AspNetCore.Mvc;
using SafeShift.BLL.Services.Interfaces;
using SafeShift.Models.DTOs.Auth;

namespace SafeShift.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterUserDto registerUserDto)
    {
        var response = await _authService.RegisterAsync(registerUserDto);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
    {
        var response = await _authService.LoginAsync(loginDto);
        if (response is null || !response.Success)
        {
            return Unauthorized(response ?? new AuthResponseDto
            {
                Success = false,
                Message = "Invalid email or password."
            });
        }

        return Ok(response);
    }
}
