using SmartHome.Models;

namespace SmartHome.Data;

public static class DbInitializer
{
    public static void Seed(SmartHomeContext db)
    {
        // EnsureCreated creates the database if it doesn't exist 
        // (Essential for In-Memory providers)
        db.Database.EnsureCreated();

        // Check if we already have data to avoid duplicates
        if (db.Locations.Any()) return;

        var defaultLocation = new HomeLocation
        {
            Nickname = "Dave's Home",
            TimeZone = "GMT",
            Devices = [
                new SmartDevice 
                { 
                    SerialNumber = "SN-1001", 
                    ModelName = "Thermos-X", 
                    Type = DeviceType.Thermostat,
                    IsOnline = true,
                    FirmwareVersion = "1.0.0",
                },
                new SmartDevice 
                { 
                    SerialNumber = "SN-2002", 
                    ModelName = "ViewMaster-3000", 
                    Type = DeviceType.Camera,
                    IsOnline = false,
                    FirmwareVersion = "2.1.5",
                }
            ]
        };

        db.Locations.Add(defaultLocation);
        db.SaveChanges();
    }
}