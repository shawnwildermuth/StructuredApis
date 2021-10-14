using Microsoft.EntityFrameworkCore;
using StructuredApis.Data.Entities;

namespace StructuredApis.Data;

public class JurisContext : DbContext
{
  private readonly IConfiguration _config;

  public JurisContext(IConfiguration config)
  {
    _config = config;
  }

  public DbSet<Client> Clients => Set<Client>();

  protected override void OnConfiguring(DbContextOptionsBuilder bldr)
  {
    base.OnConfiguring(bldr);

    bldr.UseSqlServer(_config["ConnectionString:JurisDb"]);
  }
}
