using Microsoft.EntityFrameworkCore;
using SmartHome.Models;

namespace SmartHome.Data;

public class SmartHomeContext(DbContextOptions<SmartHomeContext> options) : DbContext(options)
{
    public DbSet<HomeLocation> Locations => Set<HomeLocation>();
    public DbSet<SmartDevice> Devices => Set<SmartDevice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Enforce a unique serial number for devices
        modelBuilder.Entity<SmartDevice>()
            .HasIndex(d => d.SerialNumber).IsUnique();
    }
}