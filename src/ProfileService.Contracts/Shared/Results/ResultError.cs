namespace ProfileService.Contracts.Shared.Results;

public sealed record ResultError(ErrorTypes Type, string Message, string? Details = null);
