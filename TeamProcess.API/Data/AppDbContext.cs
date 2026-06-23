using Microsoft.EntityFrameworkCore;
using TeamProcess.API.Models;

namespace TeamProcess.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
}
