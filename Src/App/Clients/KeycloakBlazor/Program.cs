using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using KeycloakBlazor.Source.App;
using KeycloakBlazor.Source.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Oidc");

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddOpenIdConnect(options =>
    {
        options.Authority = oidcConfiguration.GetValue<string>("Authority");
        options.ClientId = oidcConfiguration.GetValue<string>("ClientId");
        options.ClientSecret = oidcConfiguration.GetValue<string>("ClientSecret");
        options.RequireHttpsMetadata = oidcConfiguration.GetValue<bool>("RequireHttpsMetadata");
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
        options.MapInboundClaims = true;
        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.Scope.Add("roles");

        options.Events = new()
        {
            OnTokenValidated = context =>
            {
                return Task.CompletedTask;
            },
            OnUserInformationReceived = context =>
            {
                RoleMapping.MapKeyCloakRolesToRoleClaims(context);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.MapPost("/logout", async context =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    await context.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
});

app.MapGet("/login", async context =>
    await context.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new() { RedirectUri = "/" }));

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
