namespace SafeShift.Models.DTOs.Incidents;

public class IncidentReadDto
{
    public int IncidentId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Severity { get; set; } = string.Empty;
    public int UserId { get; set; }
}
