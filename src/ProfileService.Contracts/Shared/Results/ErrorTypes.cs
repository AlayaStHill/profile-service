namespace ProfileService.Contracts.Shared.Results;

public enum ErrorTypes
{
    BadRequest,
    NotFound,
    Conflict,
    Unauthorized,
    Forbidden,
    Timeout,
    ExternalServiceError,
    InternalServerError
}
