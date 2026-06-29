using Microsoft.AspNetCore.Mvc;
using TeamProcess.API.DTOs;
using TeamProcess.API.Services;
namespace TeamProcess.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _service;

    public EmployeeController(EmployeeService service)

    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EmployeeRequestDto dto)
    {
        var employee = await _service.CreateAsync(dto);
        if (employee == null) return BadRequest("Department not found");
        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var employee = await _service.GetByIdAsync(id);
        if (employee == null)
            return NotFound();
        return Ok(employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EmployeeRequestDto dto)
    {
        var employee = await _service.UpdateAsync(id, dto);
        if (employee == null)
            return NotFound();
        return Ok(employee);
    }
}