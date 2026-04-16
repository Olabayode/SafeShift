namespace SafeShift.Models;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; } 
    public string PasswordHash { get; set; } 
    public string Role { get; set; } 

    public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
    public ICollection<Inspection> Inspections { get; set; } = new List<Inspection>();
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}
