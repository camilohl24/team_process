using Microsoft.EntityFrameworkCore;
using TeamProcess.API.Data;
using TeamProcess.API.DTOs;
using TeamProcess.API.Models;

namespace TeamProcess.API.Services;

public class EmployeeService
{
    private readonly AppDbContext _context;
    private readonly EntityValidator _entityValidator;

    public EmployeeService(AppDbContext context, EntityValidator entityValidator)
    {
        _context = context;
        _entityValidator = entityValidator;
    }

    public async Task<EmployeeResponseDto> CreateAsync(EmployeeRequestDto dto)
    {
        if (!await _entityValidator.ExistAsync<Department>(dto.DepartmentId)) return null!;
        var employee = new Employee
        {
            Name = dto.Name,
            LastName = dto.LastName,
            Position = dto.Position,
            EntryDate = dto.EntryDate,
            DepartmentId = dto.DepartmentId
        };
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        var createdEmployee = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == employee.Id);
        return MapToDto(createdEmployee!);
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (employee == null) return;
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

    }

    public async Task<IEnumerable<EmployeeResponseDto>> GetAllAsync()
    {
        var employees = await _context.Employees
            .Include(e => e.Department)
            .ToListAsync();
        return employees.Select(MapToDto).ToList();
    }

    public async Task<EmployeeResponseDto> GetByIdAsync(int id)
    {
        var employee = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (employee == null) return null!;
        return MapToDto(employee);
    }

    public async Task<EmployeeResponseDto> UpdateAsync(int id, EmployeeRequestDto dto)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return null!;
        if (!await _entityValidator.ExistAsync<Department>(dto.DepartmentId)) return null!;
        employee.Name = dto.Name;
        employee.LastName = dto.LastName;
        employee.Position = dto.Position;
        employee.EntryDate = dto.EntryDate;
        employee.DepartmentId = dto.DepartmentId;
        await _context.SaveChangesAsync();
        var updatedEmployee = await _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);
        return MapToDto(employee);
    }

    private EmployeeResponseDto MapToDto(Employee employee)
    {
        return new EmployeeResponseDto
        {
            Id = employee.Id,
            Name = employee.Name,
            LastName = employee.LastName,
            Position = employee.Position,
            EntryDate = employee.EntryDate,
            DepartmentName = employee.Department.Name
        };
    }
}