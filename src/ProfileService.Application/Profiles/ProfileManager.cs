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

        if (result.IsFailure)
        {
            ResultError error = result.Error!;
            return Result<ProfileOutput>.Failure(error.Type, error.Message, error.Details);
        }

        ProfileReply profile = result.Value!;
        ProfileOutput output = ToOutput(profile); 


        return Result<ProfileOutput>.Success(output);
    }

    public ProfileOutput ToOutput(ProfileReply profile)
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
}
// Update ansvarar för full uppdatering, skicka med alla värden så att de inte överskrivs i identityservice