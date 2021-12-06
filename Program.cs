var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<JurisContext>();
builder.Services.AddScoped<IJurisRepository, JurisRepository>();

var app = builder.Build();

// TODO: APIs

app.Run();
