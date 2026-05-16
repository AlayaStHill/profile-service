using ProfileService.Contracts.Protos;
using ProfileService.Contracts.Shared.Results;

namespace ProfileService.Application.Profiles.Interfaces;

public interface IIdentityServiceProfileClient
{
    Task<Result<ProfileReply>> GetProfileAsync(string userId, CancellationToken ct);
}
