using Microsoft.EntityFrameworkCore;
using StructuredApis.Data;
using StructuredApis.Data.Entities;

var bldr = WebApplication.CreateBuilder(args);

// Register Your Services
RegisterServices(bldr.Services);

var app = bldr.Build();

// Configure the Middleware

app.UseHttpsRedirection();

var apis = app.Services.GetServices<IApi>();
foreach (var api in apis)
{
  if (api is null) throw new InvalidProgramException("Apis not found");

  api.Register(app);
}

app.Run();


void RegisterServices(IServiceCollection svcs)
{
  // Add services to the container.
  svcs.AddDbContext<JurisContext>();
  svcs.AddScoped<IJurisRepository, JurisRepository>();
  svcs.AddTransient<IApi, ClientApi>();
}