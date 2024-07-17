using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

namespace KeycloakBlazor.Source.Utils.Auth;

public class CustomRevalidatingAuthenticationStateProvider(ILoggerFactory logger, IHttpContextAccessor httpContextAccessor, CookieOidcRefresher cookieOidcRefresher) : RevalidatingServerAuthenticationStateProvider(logger)
{
    protected override TimeSpan RevalidationInterval => TimeSpan.FromSeconds(30);

    protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
    {
        HttpContext? httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null) return false;

        AuthenticateResult authResult = await httpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (authResult.Ticket == null) return false;

        CookieValidatePrincipalContext context = new(
            httpContext,
            new(CookieAuthenticationDefaults.AuthenticationScheme, null, typeof(CookieAuthenticationHandler)),
            new(),
            authResult.Ticket
        );
        await cookieOidcRefresher.ValidateOrRefreshCookieAsync(context, "KeycloakOidc");

        return context.Principal != null;
    }
}