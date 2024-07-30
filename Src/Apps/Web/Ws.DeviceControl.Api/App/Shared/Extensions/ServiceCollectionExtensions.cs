using System.Security.Claims;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;

namespace Ws.DeviceControl.Api.App.Shared.Extensions;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKeycloakWebApiAuthentication(configuration);

        services
            .AddAuthorization(PolicyAuthUtils.RegisterAuthorization)
            .AddKeycloakAuthorization(options =>
            {
                options.RolesResource = configuration.GetSection("Keycloak").GetValue<string>("resource");
                options.EnableRolesMapping = RolesClaimTransformationSource.ResourceAccess;
                options.RoleClaimType = ClaimTypes.Role;
            })
            .AddAuthorizationServer(configuration);

        return services;
    }
}