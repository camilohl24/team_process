using Microsoft.AspNetCore.Mvc;
using TeamProcess.API.DTOs;
using TeamProcess.API.Services;

namespace TeamProcess.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController : ControllerBase
{
    private readonly AttendanceService _service;
    public AttendanceController(AttendanceService service)
    {
        _service = service;
    }

    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var attendance = await _service.GetByIdAsync(id);
        if (attendance == null) return NotFound();
        return Ok(attendance);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AttendanceRequestDto dto)
    {
        var attendance = await _service.CreateAsync(dto);
        if (attendance == null) return BadRequest("Employee not found");
        return CreatedAtAction(nameof(GetById), new { id = attendance.Id }, attendance);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AttendanceRequestDto dto)
    {
        var attendance = await _service.UpdateAsync(id, dto);
        if (attendance == null) return NotFound();
        return Ok(attendance);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
