using ProfileService.Api.Requests;
using ProfileService.Api.Responses;
using ProfileService.Api.Shared.Mappers;
using ProfileService.Application.Profiles.Inputs;
using ProfileService.Application.Profiles.Interfaces;
using ProfileService.Application.Profiles.Output;
using ProfileService.Contracts.Shared.Results;

namespace ProfileService.Api.Endpoints;

public static class ProfileEndpoints
{
    public static void MapProfileEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("/api/profiles")
            // den som anropar endpointen måste vara inloggad med en giltig token
            .RequireAuthorization()
            .WithTags("Profiles")
            .WithDescription("REST endpoints for profile operations. " + "Requests are forwarded to IdentityService through gRPC.");

        group.MapGet("/get", GetProfileEndpoint)
            .WithName("GetProfile")
            .WithSummary("Retrieve profile")
            .WithDescription("Retrieves profile information for authorized users. Access depends on assigned roles and whether the requested user matches the authenticated user.")
            .Produces<GetProfileResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);


    }

    public static async Task<IResult> GetProfileEndpoint(GetProfileRequest request, IProfileService profileManager, CancellationToken ct = default)
    {
        GetProfileInput input = new(request.UserId);

        Result<ProfileOutput> result = await profileManager.GetProfileAsync(input, ct);

        if (result.IsFailure)
            return ResultMapper.MapToHttpFailResult(result);

        GetProfileResponse response = ResponseMapper.MapToGetProfileResponse(result.Value!);

        return Results.Ok(response);
    }
}

