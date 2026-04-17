using System.ComponentModel.DataAnnotations;

namespace SafeShift.Models.DTOs.Incidents;

public class IncidentUpdateDto
{
    [Required]
    public string Description { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    [Required]
    [RegularExpression("(?i)^(Low|Medium|High|Critical)$", ErrorMessage = "Severity must be one of: Low, Medium, High, Critical.")]
    public string Severity { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "UserId is required.")]
    public int UserId { get; set; }
}
