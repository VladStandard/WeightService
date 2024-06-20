using System.Security.Claims;

namespace DeviceControl.Source.Shared.Auth.Policies;

public static class PolicyAuthUtils
{
    public static void RegisterAuthorization(AuthorizationOptions options)
    {

        options.AddPolicy(PolicyEnum.Admin, builder =>
            builder.RequireRole(ClaimTypes.Role, RoleEnum.Admin)
        );

        options.AddPolicy(PolicyEnum.SeniorSupport, builder =>
            builder.RequireAssertion(x =>
                x.User.HasRole(RoleEnum.Admin, RoleEnum.SeniorSupport)
            )
        );

        options.AddPolicy(PolicyEnum.Support, builder =>
            builder.RequireAssertion(x =>
                x.User.HasRole(
                    RoleEnum.Support, RoleEnum.Admin, RoleEnum.SeniorSupport
                )
            )
        );

        options.AddPolicy(PolicyEnum.Developer, builder =>
            builder.RequireAssertion(x =>
                x.User.HasRole(
                    RoleEnum.Developer, RoleEnum.Admin
                )
            )
        );
    }

    private static bool HasRole(this ClaimsPrincipal user, params string[] roles) =>
        roles.Any(role => user.HasClaim(ClaimTypes.Role, role));
}