using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.Data;
using SmartHome.Models;

namespace SmartHome.Tests;

public class SmartHomeApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // 1. Find the existing DbContext registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<SmartHomeContext>));

            if (descriptor != null) services.Remove(descriptor);

            // 2. Add a fresh In-Memory database just for this test session
            services.AddDbContext<SmartHomeContext>(options =>
            {
                options.UseInMemoryDatabase("TestDatabase");
            });

            // 3. Seed the data
            using var scope = services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SmartHomeContext>();
            
            db.Database.EnsureDeleted(); // Start clean
            db.Database.EnsureCreated();

            var seedLocation = new HomeLocation 
            { 
                Id = 1, 
                Nickname = "Test Lab", 
                TimeZone = "UTC",
                Devices = new List<SmartDevice> 
                {
                    new() { 
                        Id = 1, 
                        SerialNumber = "DEV-001", 
                        ModelName = "Sensor-X1", 
                        Type = DeviceType.Thermostat, 
                        IsOnline = true,
                        FirmwareVersion = "1.0.0"
                    },
                    new() { 
                        Id = 2, 
                        SerialNumber = "DEV-002", 
                        ModelName = "Cam-Alpha", 
                        Type = DeviceType.Camera, 
                        IsOnline = false,
                        FirmwareVersion = "2.1.3"
                    },
                    new() { 
                        Id = 3, 
                        SerialNumber = "DEV-003", 
                        ModelName = "Light-Bright", 
                        Type = DeviceType.Lighting, 
                        IsOnline = true,
                        FirmwareVersion = "3.0.5"
                    }
                }
            };

            db.Locations.Add(seedLocation);;
            db.SaveChanges();
        });
    }
}