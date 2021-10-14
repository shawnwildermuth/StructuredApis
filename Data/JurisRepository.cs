using Microsoft.EntityFrameworkCore;
using StructuredApis.Data.Entities;

namespace StructuredApis.Data;

public class JurisRepository : IJurisRepository
{
  private readonly JurisContext _context;

  public JurisRepository(JurisContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Client>> GetClientsAsync(int page = 1, int pageSize = 25)
  {
    return await _context.Clients
      .Include(c => c.Address)
      .OrderBy(c => c.Name)
      .Skip((page - 1) * pageSize)
      .Take(pageSize)
      .ToListAsync();
  }

  public async Task<Client?> GetClientAsync(int id)
  {
    return await _context.Clients
      .Include(c => c.Address)
      .Where(c => c.Id == id)
      .FirstOrDefaultAsync();
  }

  public async Task<bool> HasClientAsync(int id) => await _context.Clients.CountAsync(c => c.Id == id) > 0;

  public async Task<bool> SaveAll() => (await _context.SaveChangesAsync()) > 0;
  public void Add<T>(T entity) where T : class => _context.Add(entity);
  public void Delete<T>(T entity) where T : class => _context.Remove(entity);
  public void Update(Client existingClient) => _context.Update(existingClient);


}
