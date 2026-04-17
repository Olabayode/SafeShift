namespace SafeShift.Models;

public class Inspection
{
    public int InspectionId { get; set; }
    public DateTime Date { get; set; }
    public string Notes { get; set; } = string.Empty;
    public int UserId { get; set; }

    public User? User { get; set; }
}
