using KeycloakBlazor.Source.App;
using KeycloakBlazor.Source.Utils;
using KeycloakBlazor.Source.Utils.Api.Keycloak;
using KeycloakBlazor.Source.Utils.Auth;
using KeycloakBlazor.Source.Utils.Services;
using Microsoft.FluentUI.AspNetCore.Components;
using Refit;

const string oidcScheme = "KeycloakOidc";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Oidc");

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();
builder.Services.ConfigureKeycloakAuthorization(oidcConfiguration, oidcScheme);

string keycloakAdminUrl = $"{oidcConfiguration.GetValue<string>("Authority")}/admin/realms/{oidcConfiguration.GetValue<string>("Realm")}";
builder.Services.AddTransient<ServerAuthorizationMessageHandler>();
builder.Services.AddRefitClient<IKeycloakApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new(keycloakAdminUrl))
    .AddHttpMessageHandler<ServerAuthorizationMessageHandler>();

builder.Services.AddScoped<UserApi>();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.MapGroup(RouteUtils.Authorization).MapLoginAndLogout(oidcScheme);

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
