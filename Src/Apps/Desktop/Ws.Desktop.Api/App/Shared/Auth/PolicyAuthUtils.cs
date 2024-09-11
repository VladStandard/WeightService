using System.Security.Claims;

namespace Ws.Desktop.Api.App.Shared.Auth;

public static class PolicyAuthUtils
{
    public static void RegisterAuthorization(AuthorizationOptions options)
    {
        options.AddPolicy(PolicyEnum.Universal, builder =>
            builder.RequireAssertion(x =>
                x.User.HasRole(RoleEnum.Universal)
            )
        );

        options.AddPolicy(PolicyEnum.Pc, builder =>
            builder.RequireAssertion(x =>
                x.User.HasRole(RoleEnum.Pc, RoleEnum.Universal)
            )
        );

        options.AddPolicy(PolicyEnum.Tablet, builder =>
            builder.RequireAssertion(x =>
                x.User.HasRole(RoleEnum.Tablet, RoleEnum.Universal)
            )
        );
    }

    private static bool HasRole(this ClaimsPrincipal user, params string[] roles) =>
        roles.Any(role => user.HasClaim(ClaimTypes.Role, role));
}