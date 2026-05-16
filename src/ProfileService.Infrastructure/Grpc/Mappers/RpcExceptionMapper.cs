using Grpc.Core;
using ProfileService.Contracts.Shared.Results;

namespace ProfileService.Infrastructure.Grpc.Mappers;

public static class RpcExceptionMapper
{
    public static Result<T> MapRpcException<T>(RpcException ex)
    {
        return ex.StatusCode switch
        {
            StatusCode.NotFound => Result<T>.Failure(
                ErrorTypes.NotFound,
                ex.Status.Detail),

            StatusCode.InvalidArgument => Result<T>.Failure(
                ErrorTypes.BadRequest,
                ex.Status.Detail),

            StatusCode.PermissionDenied => Result<T>.Failure(
                ErrorTypes.Forbidden,
                ex.Status.Detail),

            _ => Result<T>.Failure(
                ErrorTypes.ExternalServiceError,
                "Failed to retrieve profile from IdentityService.",
                $"gRPC status: {ex.StatusCode}. Details: {ex.Status.Detail}")
        };
    }
}
