using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SmartHome.Data;
using SmartHome.Models;

namespace SmartHome.Endpoints;

public static class HomeEndpoints
{
    public static void MapSmartHomeRoutes(this IEndpointRouteBuilder app)
    {
        // Define a group for all Location-related actions
        var locations = app.MapGroup("/api/v1/locations")
                           .WithTags("Locations");

        // Map the specific endpoints to handler methods
        locations.MapGet("/", GetAllLocations);
    }

    private static async Task<Ok<List<HomeLocation>>> GetAllLocations(SmartHomeContext db)
    {
        var data = await db.Locations.AsNoTracking().ToListAsync();
        return TypedResults.Ok(data);
    }
}