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

        group.MapPut("/update", UpdateProfileEndpoint)
            .WithName("UpdateProfile")
            .WithSummary("Update profile")
            .WithDescription("Updates profile information for authorized users. Access depends on assigned roles and whether the requested user matches the authenticated user.")
            .Produces<UpdateProfileResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapDelete("/delete", DeleteProfileEndpoint)
            .WithName("DeleteProfile")
            .WithSummary("Delete profile")
            .WithDescription("Deletes a profile. Access is restricted to administrators.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound);

    }

    public static async Task<IResult> GetProfileEndpoint([AsParameters] GetProfileRequest request, IProfileService profileManager, CancellationToken ct = default)
    {
        GetProfileInput input = new(request.UserId);

        Result<ProfileOutput> result = await profileManager.GetProfileAsync(input, ct);

        if (result.IsFailure)
            return ResultMapper.MapToHttpFailResult(result);

        GetProfileResponse response = ProfileMapper.MapToGetProfileResponse(result.Value!);

        return Results.Ok(response);
    }

    public static async Task<IResult> UpdateProfileEndpoint(UpdateProfileRequest request, IProfileService profileManager, CancellationToken ct = default)
    {
        UpdateProfileInput input = ProfileMapper.MapToUpdateProfileInput(request);

        Result<ProfileOutput> result = await profileManager.UpdateProfileAsync(input, ct);

        if (result.IsFailure)
            return ResultMapper.MapToHttpFailResult(result);

        UpdateProfileResponse response = ProfileMapper.MapToUpdateProfileResponse(result.Value!);

        return Results.Ok(response);
    }

    public static async Task<IResult> DeleteProfileEndpoint(string userId, IProfileService profileManager, CancellationToken ct = default)
    {
        DeleteProfileInput input = new(userId);

        Result result = await profileManager.DeleteProfileAsync(input, ct);

        return result.IsSuccess 
            ? Results.NoContent() 
            : ResultMapper.MapToHttpFailResult(result);
    }
}

