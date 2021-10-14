namespace StructuredApis.Data.Entities;

public class Client
{
  public int Id { get; set; } = 0;
  public string Name { get; set; } = "";
  public string? Phone { get; set; }
  public string? Contact { get; set; }
  public Address? Address { get; set; }
}