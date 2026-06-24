namespace TeamProcess.API.Models;

public class Department : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
