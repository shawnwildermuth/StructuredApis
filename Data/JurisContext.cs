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
  public DbSet<Address> Addresses => Set<Address>();

  protected override void OnConfiguring(DbContextOptionsBuilder bldr)
  {
    base.OnConfiguring(bldr);

    bldr.UseSqlServer(_config["ConnectionStrings:JurisDb"]);
  }

  protected override void OnModelCreating(ModelBuilder bldr)
  {
    base.OnModelCreating(bldr);

    bldr.Entity<Client>()
      .HasOne(c => c.Address);
  }
}
