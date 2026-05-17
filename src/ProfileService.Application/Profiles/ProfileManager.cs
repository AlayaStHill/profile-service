using ProfileService.Application.Profiles.Inputs;
using ProfileService.Application.Profiles.Interfaces;
using ProfileService.Application.Profiles.Output;
using ProfileService.Contracts.Protos;
using ProfileService.Contracts.Shared.Results;


namespace ProfileService.Application.Profiles;

public sealed class ProfileManager(IIdentityServiceProfileClient identityProfileClient) : IProfileService
{
    public async Task<Result<ProfileOutput>> GetProfileAsync(GetProfileInput input, CancellationToken ct)
    {
        if (input is null)
            return Result<ProfileOutput>.Failure(ErrorTypes.BadRequest, "Input cannot be null");

        if (string.IsNullOrWhiteSpace(input.UserId))
            return Result<ProfileOutput>.Failure(ErrorTypes.BadRequest, "User ID cannot be null or empty");

        Result<ProfileReply> result = await identityProfileClient.GetProfileAsync(input.UserId, ct);

        return MapProfileResult(result);
    }

    public async Task<Result<ProfileOutput>> UpdateProfileAsync(UpdateProfileInput input, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(input.UserId))
            return Result<ProfileOutput>.Failure(ErrorTypes.BadRequest, "User ID cannot be null or empty");

        if (string.IsNullOrWhiteSpace(input.FirstName))
            return Result<ProfileOutput>.Failure(ErrorTypes.BadRequest, "First name cannot be null or empty");

        if (string.IsNullOrWhiteSpace(input.LastName))
            return Result<ProfileOutput>.Failure(ErrorTypes.BadRequest, "Last name cannot be null or empty");

        Result<ProfileReply> result = await identityProfileClient.UpdateProfileAsync(input, ct);

        return MapProfileResult(result);
    }

    public async Task<Result> DeleteProfileAsync(DeleteProfileInput input, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(input.UserId))
            return Result.Failure(ErrorTypes.BadRequest, "User ID cannot be null or empty");

        Result result = await identityProfileClient.DeleteProfileAsync(input.UserId, ct);

        return result;
    }
         


    private static ProfileOutput ToOutput(ProfileReply profile)
    {
        return new ProfileOutput
        (
            profile.UserId,
            profile.Email,
            profile.FirstName,
            profile.LastName,
            profile.PhoneNumber,
            profile.Description,
            profile.ImageUrl
        );
    }

    private static Result<ProfileOutput> MapProfileResult(Result<ProfileReply> result)
    {
        if (result.IsFailure)
        {
            ResultError error = result.Error!;
            return Result<ProfileOutput>.Failure(error.Type, error.Message, error.Details);
        }

        ProfileReply profile = result.Value!;
        ProfileOutput output = ToOutput(profile);

        return Result<ProfileOutput>.Success(output);
    }
}
