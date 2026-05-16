namespace ProfileService.Application.Profiles.Output;

public sealed record ProfileOutput(string UserId, string Email, string FirstName, string LastName, string PhoneNumber, string Description, string ImageUrl);

