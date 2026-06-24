namespace TeamProcess.API.Models;

public class Employee : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Position { get; set; } = null!;
    public DateTime EntryDate { get; set; }
    public Department Department { get; set; } = null!;
    public int DepartmentId { get; set; }
}
