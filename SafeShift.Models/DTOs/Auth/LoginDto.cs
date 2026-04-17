using System.ComponentModel.DataAnnotations;

namespace SafeShift.Models.DTOs.Auth;

/// <summary>
/// Request body used to log in an existing user.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Registered email address.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Plain-text password to validate.
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;
}
