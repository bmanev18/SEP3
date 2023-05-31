using Microsoft.Extensions.DependencyInjection;
namespace Shared.Auth;

public static class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeProductOwner",
                a => a.RequireAuthenticatedUser().RequireClaim("Role", "product owner"));
            options.AddPolicy("MustBeScrumMaster",
                a => a.RequireAuthenticatedUser().RequireClaim("Role", "scrum master"));
            options.AddPolicy("MustBeDeveloper",
                a => a.RequireAuthenticatedUser().RequireClaim("Role", "developer"));
        });
    }
}