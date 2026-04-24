namespace SmartHome.Models;

public class HomeLocation
{
    public int Id { get; init; }
    
    public required string Nickname { get; set; }
    
    public required string TimeZone { get; set; }
    
    // Relationship: One-to-Many
    public List<SmartDevice> Devices { get; init; } = [];
}