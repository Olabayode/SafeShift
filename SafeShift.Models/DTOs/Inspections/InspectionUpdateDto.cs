using System.ComponentModel.DataAnnotations;
using SafeShift.Models.DTOs.Validation;

namespace SafeShift.Models.DTOs.Inspections;

public class InspectionUpdateDto
{
    [NotDefaultDateTime(ErrorMessage = "Date is required.")]
    public DateTime Date { get; set; }

    [Required]
    public string Notes { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "UserId is required.")]
    public int UserId { get; set; }
}
