namespace StructuredApis.Data.Entities;

public record Address(
  int id, 
  string Address1, 
  string Address2, 
  string Address3, 
  string cityTown, 
  string stateProvince, 
  string postalCode, 
  string country);
