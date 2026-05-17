namespace ProfileService.Application.Profiles.Inputs;

public sealed record UpdateProfileInput(string UserId, string FirstName, string LastName, string PhoneNumber, string Description, string ImageUrl);
