using KeycloakBlazor;
using KeycloakBlazor.Source.Api.Keycloak;
using KeycloakBlazor.Source.App;
using KeycloakBlazor.Source.Utils;
using KeycloakBlazor.Source.Utils.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

const string OIDC_SCHEME = "KeycloakOidc";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Oidc");

builder.Services.AddAuthentication(OIDC_SCHEME)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
    {
        opt.Events.OnRedirectToAccessDenied  = context =>
        {
            context.Response.Redirect(RouteUtils.Home);
            return Task.CompletedTask;
        };
    })
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

builder.Services.ConfigureCookieOidcRefresh(CookieAuthenticationDefaults.AuthenticationScheme, OIDC_SCHEME);
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ServerAuthorizationMessageHandler>();
builder.Services.AddHttpClient<IKeycloakApi, KeycloakApi>(client =>
        client.BaseAddress = new("http://10.0.204.55:8006/admin/realms/blazor/"))
    .AddHttpMessageHandler<ServerAuthorizationMessageHandler>();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.MapGroup(RouteUtils.Authorization).MapLoginAndLogout(OIDC_SCHEME);

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
