using StructuredApis;

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

var app = builder.Build();

new ClientApi().Register(app);



app.Run();

void RegisterServices(IServiceCollection services)
{
  // Add services to the container.
  builder.Services.AddDbContext<JurisContext>();
  builder.Services.AddScoped<IJurisRepository, JurisRepository>();
}


