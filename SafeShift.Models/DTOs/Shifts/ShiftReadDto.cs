namespace SafeShift.Models.DTOs.Shifts;

public class ShiftReadDto
{
    public int ShiftId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int UserId { get; set; }
}
