using ProfileService.Application.Profiles.Inputs;
using ProfileService.Contracts.Protos;

namespace ProfileService.Infrastructure.Grpc.Mappers;

public static class ProfileMapper
{
    public static UpdateProfileRequest MapToProfileUpdateRequest(UpdateProfileInput input)
    {
        return new UpdateProfileRequest
        {
            UserId = input.UserId,
            FirstName = input.FirstName,
            LastName = input.LastName,
            PhoneNumber = input.PhoneNumber,
            Description = input.Description,
            ImageUrl = input.ImageUrl
        };
    }
}
