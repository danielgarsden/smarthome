namespace SmartHome.Models;

public class SmartDevice
{
    public int Id { get; init; }
    
    // 'init' ensures the Serial Number cannot be changed after registration
    public required string SerialNumber { get; init; }
    
    public required string ModelName { get; set; }
    
    public DeviceType Type { get; set; }
    
    public bool IsOnline { get; set; }
    
    public required string FirmwareVersion { get; set; }

    public string? IpAddress { get; set; }
    
    public string? Room { get; set; }

    // Foreign Key and Navigation Property
    public int HomeLocationId { get; set; }
    public HomeLocation? HomeLocation { get; set; }
}