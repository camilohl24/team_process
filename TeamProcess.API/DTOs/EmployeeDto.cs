namespace TeamProcess.API.DTOs;

public class EmployeeResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Position { get; set; } = null!;
    public DateTime EntryDate { get; set; }
    public string DepartmentName { get; set; } = null!;
    public int DepartmentId { get; set; }
}

public class EmployeeRequestDto
{
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Position { get; set; } = null!;
    public DateTime EntryDate { get; set; }
    public int DepartmentId { get; set; }
}
