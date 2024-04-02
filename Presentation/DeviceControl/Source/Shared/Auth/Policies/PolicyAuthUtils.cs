using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DeviceControl.Source.Shared.Auth.Policies;

public static class PolicyAuthUtils
{
    public static void RegisterAuthorization(AuthorizationOptions options)
    {
        options.FallbackPolicy = options.DefaultPolicy;
        options.AddPolicy(PolicyEnum.Admin, builder =>
            builder.RequireRole(ClaimTypes.Role, RoleEnum.Admin)
        );

        options.AddPolicy(PolicyEnum.SupportSenior, builder =>
            builder.RequireAssertion(x =>
                x.User.HasRole(RoleEnum.Admin, RoleEnum.SupportSenior)
            )
        );

        options.AddPolicy(PolicyEnum.Support, builder =>
            builder.RequireAssertion(x =>
                x.User.HasRole(
                    RoleEnum.Support, RoleEnum.Admin, RoleEnum.SupportSenior
                )
            )
        );
    }

    private static bool HasRole(this ClaimsPrincipal user, params string[] roles)
    {
        return roles.Any(role => user.HasClaim(ClaimTypes.Role, role));
    }
}