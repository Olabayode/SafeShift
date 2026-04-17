namespace SafeShift.Models.DTOs.Inspections;

public class InspectionReadDto
{
    public int InspectionId { get; set; }
    public DateTime Date { get; set; }
    public string Notes { get; set; } = string.Empty;
    public int UserId { get; set; }
}
