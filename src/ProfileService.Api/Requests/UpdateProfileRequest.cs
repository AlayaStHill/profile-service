namespace ProfileService.Api.Requests;
// Tomma strängar används som standard för att representera "inget värde" på ett enhetligt sätt
// i hela flödet, i stället för att blanda null och tomma strängar. (i gRPC har string "" som standardvärde)
public sealed class UpdateProfileRequest
{
    public string UserId { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
}