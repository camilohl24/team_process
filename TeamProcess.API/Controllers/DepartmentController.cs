using Microsoft.AspNetCore.Mvc;
using TeamProcess.API.DTOs;
using TeamProcess.API.Services;

namespace TeamProcess.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly DepartmentService _service;
    public DepartmentController(DepartmentService service)
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
        var department = await _service.GetByIdAsync(id);
        if (department == null)
            return NotFound();
        return Ok(department);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DepartmentRequestDto dto)
    {
        var department = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, DepartmentRequestDto dto)
    {
        var department = await _service.UpdateAsync(id, dto);
        if (department == null)
            return NotFound();
        return Ok(department);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}

