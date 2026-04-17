using System.ComponentModel.DataAnnotations;

namespace SafeShift.Models.DTOs.Auth;

/// <summary>
/// Request body used to register a new user.
/// </summary>
public class RegisterUserDto
{
    /// <summary>
    /// Full name of the user being registered.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Unique email used for registration and login.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Plain-text password supplied during registration.
    /// </summary>
    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Application role assigned to the user.
    /// </summary>
    public string Role { get; set; } = string.Empty;
}
