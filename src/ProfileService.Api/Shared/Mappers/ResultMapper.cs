using Microsoft.AspNetCore.Mvc;
using ProfileService.Contracts.Shared.Results;

namespace ProfileService.Api.Shared.Mappers;

public static class ResultMapper
{
    public static IResult MapToHttpFailResult(Result result)
        => MapError(result.Error!);

    public static IResult MapToHttpFailResult<T>(Result<T> result)
        => MapError(result.Error!);

    public static IResult MapError(ResultError error)
    {
        return error.Type switch
        {
            ErrorTypes.NotFound => Results.NotFound(error.Message),
            ErrorTypes.BadRequest => Results.BadRequest(error.Message),
            ErrorTypes.Conflict => Results.Conflict(error.Message),
            ErrorTypes.ExternalServiceError => Results.Problem(detail: error.Message, statusCode: StatusCodes.Status502BadGateway),
            _ => Results.Problem(detail: error.Message, statusCode: StatusCodes.Status500InternalServerError)
        };
    }

}
