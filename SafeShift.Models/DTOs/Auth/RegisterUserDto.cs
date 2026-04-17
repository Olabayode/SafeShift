using System.ComponentModel.DataAnnotations;

namespace SafeShift.Models.DTOs.Auth;

public class RegisterUserDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;
}
