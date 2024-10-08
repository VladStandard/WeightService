using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace KeycloakBlazor.Source.Utils.Auth;

public static class AuthenticationServiceExtensions
{
    public static IServiceCollection ConfigureKeycloakAuthorization(this IServiceCollection services, IConfigurationSection oidcConfiguration, string oidcScheme)
    {
        services.AddAuthentication(oidcScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
            {
                opt.Events.OnRedirectToAccessDenied  = context =>
                {
                    context.Response.Redirect(RouteUtils.Home);
                    return Task.CompletedTask;
                };
            })
            .AddOpenIdConnect(oidcScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.SignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.Authority = $"{oidcConfiguration.GetValue<string>("Authority")}/realms/{oidcConfiguration.GetValue<string>("Realm")}";
                options.ClientId = oidcConfiguration.GetValue<string>("ClientId");
                options.ClientSecret = oidcConfiguration.GetValue<string>("ClientSecret");
                options.RequireHttpsMetadata = oidcConfiguration.GetValue<bool>("RequireHttpsMetadata");
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.MapInboundClaims = false;

                options.Events = new()
                {
                    OnUserInformationReceived = context =>
                    {
                        RoleMapping.MapKeyCloakRolesToRoleClaims(context);
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddHttpContextAccessor();
        services.ConfigureCookieOidcRefresh(CookieAuthenticationDefaults.AuthenticationScheme, oidcScheme);
        services.AddAuthorization();
        services.AddScoped<AuthenticationStateProvider, CustomRevalidatingAuthenticationStateProvider>();
        services.AddCascadingAuthenticationState();

        return services;
    }
}