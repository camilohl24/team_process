namespace TeamProcess.API.Models;

public class Attendance : IEntity
{
    public int Id { get; set; }
    public Employee Employee { get; set; } = null!;
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public AttendanceStatus Status { get; set; }


}
