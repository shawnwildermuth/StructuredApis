namespace StructuredApis;
public class ClientApi
{
  public ClientApi()
  {
  }

  public void Register(WebApplication app)
  {
    app.MapGet("/api/clients", Get);
    app.MapGet("/api/clients/{id}", GetById);
    app.MapPost("/api/clients", Post);
    app.MapPut("/api/clients/{id}", Put);
    app.MapDelete("/api/clients/{id}", Delete);
  }

  async Task<IResult> Get(IJurisRepository repository)
  {
    return Results.Ok(await repository.GetClientsAsync());
  }

  async Task<IResult> GetById(int id, IJurisRepository repository)
  {
    var result = await repository.GetClientAsync(id);
    if (result is null) return Results.NotFound();
    return Results.Ok(result);
  }

  async Task<IResult> Post(Client client, IJurisRepository repository)
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

  async Task<IResult> Put(int id, Client old, IJurisRepository repository)
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
  }

  async Task<IResult> Delete(int id, IJurisRepository repository)
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