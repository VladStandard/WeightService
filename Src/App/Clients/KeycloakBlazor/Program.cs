using KeycloakBlazor.Source.App;
using KeycloakBlazor.Source.Utils.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

const string OIDC_SCHEME = "KeycloakOidc";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Oidc");

builder.Services.AddAuthentication(OIDC_SCHEME)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OIDC_SCHEME, options =>
    {
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.SignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        options.Authority = oidcConfiguration.GetValue<string>("Authority");
        options.ClientId = oidcConfiguration.GetValue<string>("ClientId");
        options.ClientSecret = oidcConfiguration.GetValue<string>("ClientSecret");
        options.RequireHttpsMetadata = oidcConfiguration.GetValue<bool>("RequireHttpsMetadata");
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.MapInboundClaims = true;

        options.Events = new()
        {
            OnUserInformationReceived = context =>
            {
                RoleMapping.MapKeyCloakRolesToRoleClaims(context);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.ConfigureCookieOidcRefresh(CookieAuthenticationDefaults.AuthenticationScheme, OIDC_SCHEME);
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorization();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.MapGroup("/auth").MapLoginAndLogout(OIDC_SCHEME);

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
