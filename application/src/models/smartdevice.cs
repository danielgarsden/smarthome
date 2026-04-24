namespace SmartHome.Models;

public class SmartDevice
{
    public int Id { get; init; }
    
    // 'init' ensures the Serial Number cannot be changed after registration
    public required string SerialNumber { get; init; }
    
    public required string ModelName { get; set; }
    
    public DeviceType Type { get; set; }
    
    public bool IsOnline { get; set; }
    
    // JSON Metadata for flexible device settings
    public Dictionary<string, string> Metadata { get; set; } = [];

    // Foreign Key and Navigation Property
    public int HomeLocationId { get; set; }
    public HomeLocation? HomeLocation { get; set; }
}