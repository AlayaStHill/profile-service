using Grpc.Core;
using Microsoft.AspNetCore.Http;
using ProfileService.Application.Profiles.Interfaces;
using ProfileService.Contracts.Protos;
using ProfileService.Contracts.Shared.Results;
using ProfileService.Infrastructure.Grpc.Mappers;

namespace ProfileService.Infrastructure.Grpc.Clients;

public sealed class IdentityServiceProfileClient(ProfileGrpcService.ProfileGrpcServiceClient client, IHttpContextAccessor httpContextAccessor) : IIdentityServiceProfileClient
{
    public async Task<Result<ProfileReply>> GetProfileAsync(string userId, CancellationToken ct = default)
    {
        try
        {
            GetProfileRequest request = new GetProfileRequest
            {
                UserId = userId
            };

            Metadata metadata = new();
            // hämtar den inkommande bearer-token från frontend-requesten till 
            string? authHeader = httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrWhiteSpace(authHeader))
                metadata.Add("Authorization", authHeader);


            // Code generator genererar två klientmetoder för samma RPC både en synkron och en asynkron metod. Här anropas den asynkrona versionen.
            ProfileReply response = await client.GetProfileAsync(request, headers: metadata, cancellationToken:ct);

            return Result<ProfileReply>.Success(response);
        }
        catch (RpcException ex)
        {
            return RpcExceptionMapper.MapRpcException<ProfileReply>(ex);
        }
        catch (Exception ex)
        {
            return Result<ProfileReply>.Failure(ErrorTypes.InternalServerError, "Unexpected error while retrieving profile:", ex.Message);
        }
    }





}

