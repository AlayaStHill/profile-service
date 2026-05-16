using ProfileService.Api.Responses;
using ProfileService.Application.Profiles.Output;

namespace ProfileService.Api.Shared.Mappers;

public static class ResponseMapper
{
    public static GetProfileResponse MapToGetProfileResponse(ProfileOutput output)
    {
        return new GetProfileResponse(
            output.UserId,
            output.Email,
            output.FirstName,
            output.LastName,
            output.PhoneNumber,
            output.Description,
            output.ImageUrl
        );
    }
}
