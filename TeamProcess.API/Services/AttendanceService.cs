using Microsoft.EntityFrameworkCore;
using TeamProcess.API.Data;
using TeamProcess.API.DTOs;
using TeamProcess.API.Models;

namespace TeamProcess.API.Services;

public class AttendanceService
{
    private readonly AppDbContext _context;
    private readonly EntityValidator _entityValidator;

    public AttendanceService(AppDbContext context, EntityValidator entityValidator)
    {
        _context = context;
        _entityValidator = entityValidator;
    }

    public async Task<AttendanceResponseDto> CreateAsync(AttendanceRequestDto dto)
    {
        if (!await _entityValidator.ExistAsync<Employee>(dto.EmployeeId)) return null!;
        var attendance = new Attendance
        {
            EmployeeId = dto.EmployeeId,
            Date = dto.Date,
            Status = dto.Status
        };
        _context.Attendances.Add(attendance);
        await _context.SaveChangesAsync();
        var createdAttendance = await _context.Attendances
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(a => a.Id == attendance.Id);
        return MapToDto(createdAttendance!);
    }

    public async Task DeleteAsync(int id)
    {
        var attendance = await _context.Attendances.FirstOrDefaultAsync(a => a.Id == id);
        if (attendance == null) return;
        _context.Attendances.Remove(attendance);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AttendanceResponseDto>> GetAllAsync()
    {
        var attendances = await _context.Attendances
            .Include(a => a.Employee)
            .ToListAsync();
        return attendances.Select(MapToDto).ToList();
    }

    public async Task<AttendanceResponseDto> GetByIdAsync(int id)
    {
        var attendance = await _context.Attendances
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (attendance == null) return null!;
        return MapToDto(attendance);
    }

    public async Task<AttendanceResponseDto> UpdateAsync(int id, AttendanceRequestDto dto)
    {
        var attendance = await _context.Attendances.FindAsync(id);
        if (attendance == null) return null!;
        if (!await _entityValidator.ExistAsync<Employee>(dto.EmployeeId)) return null!;
        attendance.EmployeeId = dto.EmployeeId;
        attendance.Date = dto.Date;
        attendance.Status = dto.Status;
        await _context.SaveChangesAsync();
        var updatedAttendance = await _context.Attendances
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(a => a.Id == id);
        return MapToDto(updatedAttendance!);
    }

    private AttendanceResponseDto MapToDto(Attendance attendance)
    {
        return new AttendanceResponseDto
        {
            Id = attendance.Id,
            EmployeeName = $"{attendance.Employee.Name} {attendance.Employee.LastName}",
            Date = attendance.Date,
            Status = attendance.Status,
            EmployeeId = attendance.EmployeeId
        };
    }
}
