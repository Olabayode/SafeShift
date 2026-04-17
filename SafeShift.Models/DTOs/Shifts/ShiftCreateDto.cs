using System.ComponentModel.DataAnnotations;
using SafeShift.Models.DTOs.Validation;

namespace SafeShift.Models.DTOs.Shifts;

public class ShiftCreateDto : IValidatableObject
{
    [NotDefaultDateTime(ErrorMessage = "StartTime is required.")]
    public DateTime StartTime { get; set; }

    [NotDefaultDateTime(ErrorMessage = "EndTime is required.")]
    public DateTime EndTime { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "UserId is required.")]
    public int UserId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartTime != default && EndTime != default && EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime must be later than StartTime.",
                new[] { nameof(EndTime) });
        }
    }
}
