namespace SafeShift.Models.DTOs.Common;

public class ErrorResponseDto
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public IEnumerable<string>? Errors { get; set; }
}
