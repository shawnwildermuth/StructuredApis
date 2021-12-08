var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<JurisContext>();
builder.Services.AddScoped<IJurisRepository, JurisRepository>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// TODO: APIs
app.MapGet("/api/foo", () => "Hello World");

app.Run();
