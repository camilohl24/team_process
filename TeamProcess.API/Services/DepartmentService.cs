using Microsoft.EntityFrameworkCore;
using TeamProcess.API.Data;
using TeamProcess.API.DTOs;
using TeamProcess.API.Models;

namespace TeamProcess.API.Services;

public class DepartmentService
{
    private readonly AppDbContext _context;

    public DepartmentService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<DepartmentResponseDto>> GetAllAsync()
    {
        var departments = await _context.Departments.ToListAsync();
        return departments.Select(MapToDto).ToList();

    }
    public async Task<DepartmentResponseDto> GetByIdAsync(int id)
    {
        var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
        if (department == null) return null!;
        return MapToDto(department);

    }
    public async Task<DepartmentResponseDto> CreateAsync(DepartmentRequestDto dto)
    {
        var department = new Department
        {
            Name = dto.Name
        };
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return MapToDto(department);
    }

    public async Task<DepartmentResponseDto> UpdateAsync(int id, DepartmentRequestDto dto)
    {
        var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
        if (department == null)
        {
            return null!;
        }
        department.Name = dto.Name;
        await _context.SaveChangesAsync();
        return MapToDto(department);

    }

    public async Task DeleteAsync(int id)
    {
        var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
        if (department == null)
        {
            return;
        }
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();

    }

    private DepartmentResponseDto MapToDto(Department department)
    {
        return new DepartmentResponseDto
        {
            Id = department.Id,
            Name = department.Name
        };

    }

}
