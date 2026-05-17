using ProfileService.Application.Profiles.Inputs;
using ProfileService.Application.Profiles.Output;
using ProfileService.Contracts.Shared.Results;

namespace ProfileService.Application.Profiles.Interfaces;

public interface IProfileService
{
    Task<Result> DeleteProfileAsync(DeleteProfileInput input, CancellationToken ct);
    Task<Result<ProfileOutput>> GetProfileAsync(GetProfileInput input, CancellationToken ct);
    Task<Result<ProfileOutput>> UpdateProfileAsync(UpdateProfileInput input, CancellationToken ct);
}
