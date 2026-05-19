using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.Application.Profiles.Interfaces;
using ProfileService.Contracts.Protos;
using ProfileService.Infrastructure.Grpc.Clients;
using ProfileService.Infrastructure.Grpc.Options;

namespace ProfileService.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        // för att kunna läsa ut bearer-token från inkommande HTTP-requests
        services.AddHttpContextAccessor();

        services.Configure<IdentityGrpcOptions>(configuration.GetSection(IdentityGrpcOptions.SectionName));
        IdentityGrpcOptions grpcOptions = configuration.GetSection(IdentityGrpcOptions.SectionName).Get<IdentityGrpcOptions>()
            ?? throw new InvalidOperationException("Grpc settings missing");

        services.AddGrpcClient<ProfileGrpcService.ProfileGrpcServiceClient>(options =>
        {
            options.Address = new Uri(grpcOptions.IdentityServiceUrl);
        });

        services.AddScoped<IIdentityServiceProfileClient, IdentityServiceProfileClient>();
        return services;
    }
}
