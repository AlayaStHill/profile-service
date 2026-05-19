namespace ProfileService.Infrastructure.Grpc.Options;

internal class IdentityGrpcOptions
{
    public const string SectionName = "GrpcSettings";

    public string IdentityServiceUrl { get; set; } = null!;
}
