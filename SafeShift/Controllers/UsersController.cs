using Microsoft.AspNetCore.Mvc;
using SafeShift.BLL.Services.Interfaces;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllAsync();

        return Ok(users);
    }

    [HttpGet("{userId:int}")]
    public async Task<ActionResult<UserReadDto>> GetUserById(int userId)
    {
        var user = await _userService.GetByIdAsync(userId);
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpDelete("{userId:int}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var deleted = await _userService.DeleteAsync(userId);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
