namespace ProfileService.Api.Responses;

public sealed record UpdateProfileResponse(string UserId, string FirstName, string LastName, string PhoneNumber, string Description, string ImageUrl);

