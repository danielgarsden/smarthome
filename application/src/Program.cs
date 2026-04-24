using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartHome.Data;
using SmartHome.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Set up the database context
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<SmartHomeContext>(options =>
        options.UseInMemoryDatabase("SmartHomeDB"));
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<SmartHomeContext>(options =>
        options.UseSqlServer(connectionString));
}

// Add services
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Add swagger UI for testing the API
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Smart Home API V1");
    });

    // Seed the in-memory database with initial data
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<SmartHomeContext>();
    DbInitializer.Seed(db);
}

app.UseHttpsRedirection();

// Map the endpoints
app.MapSmartHomeRoutes();

app.Run();

