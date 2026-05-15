namespace ProfileService.Application.Shared.Results;

public enum ErrorTypes
{
    BadRequest,
    NotFound,
    Conflict,
    Unexpected,
    ExternalServiceError,
    InternalServerError
}
