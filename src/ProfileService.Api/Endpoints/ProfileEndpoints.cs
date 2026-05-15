namespace ProfileService.Api.Endpoints;

public static class ProfileEndpoints
{
    public static void MapProfileEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("/api/profiles")
            .WithTags("Profiles")
            .WithDescription("REST endpoints for profile operations. " +
        "Requests are delegated to internal services through gRPC.");


    }
}
