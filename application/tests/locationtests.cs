using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using SmartHome.Models;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace SmartHome.Tests;

public class LocationTests : IClassFixture<SmartHomeApiFactory>
{
    private readonly SmartHomeApiFactory _factory;

    public LocationTests(SmartHomeApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetLocations_ReturnsSuccessAndSeedData()
    {
        // Arrange: Create a client to talk to the in-memory server
        // Note: We have seeded the database with 1 record
        var client = _factory.CreateClient();

        // Act: Call the endpoint
        var response = await client.GetAsync("/api/v1/locations");

        // Assert: Verify the results
        response.EnsureSuccessStatusCode();
        var locations = await response.Content.ReadFromJsonAsync<List<HomeLocation>>();
        
        locations.Should().NotBeNull();
        locations.Should().HaveCount(1); 
    }

    [Fact]
    public async Task CreateLocation_AddsNewLocationToDatabase()
    {
        // Arrange
        var client = _factory.CreateClient();
        var newLocation = new { Nickname = "Test Flat", TimeZone = "UTC" };

        // Act
        var response = await client.PostAsJsonAsync("/api/v1/locations", newLocation);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        
        var createdLocation = await response.Content.ReadFromJsonAsync<HomeLocation>();
        createdLocation!.Nickname.Should().Be("Test Flat");
        createdLocation!.TimeZone.Should().Be("UTC");
        createdLocation.Id.Should().BeGreaterThan(0);
    }
}