using Microsoft.Extensions.DependencyInjection;

namespace Shared.Auth;

public static class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("MustBeProductOwner",
                a => a.RequireAuthenticatedUser().RequireClaim("Role", "ProductOwner"));
            options.AddPolicy("MustBeScrumMaster",
                a => a.RequireAuthenticatedUser().RequireClaim("Role", "ScrumMaster"));
            options.AddPolicy("MustBeDeveloper",
                a => a.RequireAuthenticatedUser().RequireClaim("Role", "Developer"));
            //todo Might be redundant, if not used can be deleted.

        });
    }
}