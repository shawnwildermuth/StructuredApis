using StructuredApis.Data;
using StructuredApis.Data.Entities;

public class ClientApi : IApi
{
  private readonly ILogger<ClientApi> _logger;

  public ClientApi(ILogger<ClientApi> logger)
  {
    _logger = logger;
  }

  public void Register(WebApplication app)
  {
    app.MapGet("/api/clients", GetAll);
    app.MapGet("/api/clients/{id}", Get);
    app.MapPost("/api/clients", Post);
    app.MapPut("/api/clients/{id}", Put);
    app.MapDelete("/api/clients/{id}", Delete);

  }

  public async Task<IResult> Get(int id, IJurisRepository repository)
  {
    var result = await repository.GetClientAsync(id);
    if (result is null) return Results.NotFound();
    return Results.Ok(result);
  }

  public async Task<IResult> GetAll(IJurisRepository repository)
  {
    return Results.Ok(await repository.GetClientsAsync());
  }

  public async Task<IResult> Post(Client client, IJurisRepository repository)
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
  }

  public async Task<IResult> Put(int id, Client old, IJurisRepository repository)
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
      _logger.LogError($"Error Occurred while updating Client: {ex}");
      return Results.BadRequest($"Error Occurred while updating Client: {ex}");
    }

    return Results.BadRequest("Failed to Update Client");
  }

  public async Task<IResult> Delete(int id, IJurisRepository repository)
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
  }
}