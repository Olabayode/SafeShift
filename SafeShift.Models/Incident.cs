namespace SafeShift.Models;

public class Incident
{
    public int IncidentId { get; set; }
    public string Description { get; set; } 
    public DateTime Date { get; set; }
    public string Severity { get; set; } 
    public int UserId { get; set; }

    public User? User { get; set; }
}
