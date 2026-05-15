using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace ProfileService.Api.OpenApi;
// Provides an overall description of the API.
public sealed class OpenApiDocumentTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Components ??= new OpenApiComponents();

        document.Info.Title = "Profile Service API";
        document.Info.Version = "v1";
        document.Info.Description = """
        ## Introduction

        The Profile Service API provides REST endpoints for profile management.        
        Requests are forwarded to the Identity Service through internal gRPC communication.

        ### Available operations

        ### Authentication & Authorization

        Requests require a valid JWT bearer token.

        Access to operations depends on:
        - the caller's assigned role
        - whether the requested `userId` matches the `userId` in the JWT token
        - admins can manage profiles for all users
        """;

        return Task.CompletedTask;
    }
}
