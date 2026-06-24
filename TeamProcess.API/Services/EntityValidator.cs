using Microsoft.EntityFrameworkCore;
using TeamProcess.API.Data;
using TeamProcess.API.Models;

namespace TeamProcess.API.Services;

public class EntityValidator
{
    private readonly AppDbContext _context;

    public EntityValidator(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistAsync<T>(int id) where T : class, IEntity =>
        await _context.Set<T>().AnyAsync(e => e.Id == id);


}
