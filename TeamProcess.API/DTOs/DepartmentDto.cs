namespace TeamProcess.API.DTOs;

public class DepartmentResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}

public class DepartmentRequestDto
{
    public string Name { get; set; } = null!;
}
