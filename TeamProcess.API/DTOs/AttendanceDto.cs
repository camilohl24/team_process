using TeamProcess.API.Models;

namespace TeamProcess.API.DTOs;

public class AttendanceResponseDto
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = null!;
    public DateTime Date { get; set; }
    public AttendanceStatus Status { get; set; }
    public int EmployeeId { get; set; }

}

public class AttendanceRequestDto
{
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public AttendanceStatus Status { get; set; }
}
