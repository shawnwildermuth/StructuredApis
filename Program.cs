using Microsoft.EntityFrameworkCore;
using StructuredApis.Data;
using StructuredApis.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<JurisContext>();
builder.Services.AddScoped<IJurisRepository, JurisRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/api/clients", async (IJurisRepository repository) =>
{
  return Results.Ok(await repository.GetClientsAsync());
});

app.MapGet("/api/clients/{id}", async (int id, IJurisRepository repository) =>
{
  var result = await repository.GetClientAsync(id);
  if (result is null) return Results.NotFound();
  return Results.Ok(result);
});

app.MapPost("/api/clients", async (Client client, IJurisRepository repository) =>
{
  try
  {
    repository.Add(client);
    if (await repository.SaveAll())
    {
      return Results.Created($"/clients/{client.Id}", client);
    }
  }
  catch (Exception ex)
  {
    return Results.BadRequest($"Error Occurred while posting to Client: {ex}");
  }

  return Results.BadRequest("Failed to Post Client");
});

app.MapPut("/api/clients/{id}", async (int id, Client old, IJurisRepository repository) =>
{
  try
  {
    if (!(await repository.HasClientAsync(id))) return Results.NotFound();

    repository.Update(old); // Should do merge, but this is just replacing it
    if (await repository.SaveAll())
    {
      return Results.Ok(old);
    }
  }
  catch (Exception ex)
  {
    return Results.BadRequest($"Error Occurred while updating Client: {ex}");
  }

  return Results.BadRequest("Failed to Update Client");
});

app.MapDelete("/api/clients/{id}", async (int id, IJurisRepository repository) =>
{
  try
  {
    var client = await repository.GetClientAsync(id);
    if (client is null) return Results.NotFound();

    repository.Delete(client);
    if (await repository.SaveAll())
    {
      return Results.Ok();
    }

  }
  catch (Exception ex)
  {
    return Results.BadRequest($"Error Occurred while deleting to Client: {ex}");
  }

  return Results.BadRequest("Failed to Delete Client");
});


app.Run();
