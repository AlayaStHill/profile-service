using ProfileService.Api.Requests;
using ProfileService.Api.Responses;
using ProfileService.Application.Profiles.Inputs;
using ProfileService.Application.Profiles.Output;

namespace ProfileService.Api.Shared.Mappers;

public static class ProfileMapper
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

    public static UpdateProfileInput MapToUpdateProfileInput(UpdateProfileRequest request)
    {
        return new UpdateProfileInput 
        (
           request.UserId,
           request.FirstName,
           request.LastName,
           request.PhoneNumber,
           request.Description,
           request.ImageUrl
       );
    }

    public static UpdateProfileResponse MapToUpdateProfileResponse(ProfileOutput output)
    {
        return new UpdateProfileResponse(
            output.UserId,
            output.FirstName,
            output.LastName,
            output.PhoneNumber,
            output.Description,
            output.ImageUrl
        );
    }
}
