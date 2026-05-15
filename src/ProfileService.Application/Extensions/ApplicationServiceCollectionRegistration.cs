using Microsoft.Extensions.DependencyInjection;
using ProfileService.Application.Profiles;
using ProfileService.Application.Profiles.Interfaces;

namespace ProfileService.Application.Extensions;

public static class ApplicationServiceCollectionRegistrationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<IProfileService, ProfileManager>();

        return services;
    }
}
