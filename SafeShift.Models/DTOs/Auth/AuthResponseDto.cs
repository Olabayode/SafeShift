namespace SafeShift.Models.DTOs.Auth;

/// <summary>
/// Standard response returned by register and login endpoints.
/// </summary>
public class AuthResponseDto
{
    /// <summary>
    /// Indicates whether the request succeeded.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Human-readable outcome message.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// User identifier when auth succeeds.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// User email when auth succeeds.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User role when auth succeeds.
    /// </summary>
    public string Role { get; set; } = string.Empty;
}
