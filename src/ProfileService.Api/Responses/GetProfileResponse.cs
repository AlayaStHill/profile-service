namespace ProfileService.Api.Responses;

public sealed record GetProfileResponse
(
    string UserId,
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Description,
    string ImageUrl
);

