using Microsoft.AspNetCore.Mvc;
using SafeShift.BLL.Services.Interfaces;
using SafeShift.Models.DTOs.Common;
using SafeShift.Models.DTOs.Users;

namespace SafeShift.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Returns all users.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserReadDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllAsync();

        return Ok(users);
    }

    /// <summary>
    /// Returns a single user by identifier.
    /// </summary>
    [HttpGet("{userId:int}")]
    [ProducesResponseType(typeof(UserReadDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserReadDto>> GetUserById(int userId)
    {
        var user = await _userService.GetByIdAsync(userId);
        if (user is null)
        {
            return NotFound(new ErrorResponseDto
            {
                Message = "User not found."
            });
        }

        return Ok(user);
    }

    /// <summary>
    /// Deletes a user by identifier.
    /// </summary>
    [HttpDelete("{userId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var deleted = await _userService.DeleteAsync(userId);
        if (!deleted)
        {
            return NotFound(new ErrorResponseDto
            {
                Message = "User not found."
            });
        }

        return NoContent();
    }
}
