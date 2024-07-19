using Blazorise;
using Blazorise.Icons.FontAwesome;
using DeviceControl.Source.App;
using DeviceControl.Source.Shared.Auth;
using DeviceControl.Source.Shared.Auth.Extensions;
using DeviceControl.Source.Shared.Refit;
using Fluxor;
using Ws.Domain.Services;
using Ws.Labels.Service;

const string oidcScheme = "KeycloakOidc";
const string culture = "ru-RU";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.ApplyRefitConfigurations();

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddBlazorise().AddEmptyProviders().AddFontAwesomeIcons();
builder.Services.AddFluentUIComponents(c => c.ValidateClassNames = false);
builder.Services.ConfigureKeycloakAuthorization(builder.Configuration.GetSection("Oidc"), oidcScheme);

builder.Services.AddLocalization();
builder.Services.AddDomainServices();
builder.Services.AddLabelsServices();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<Redirector>();
builder.Services.AddTransient<ServerAuthorizationMessageHandler>();

builder.Services.AddFluxor(c =>
{
    c.WithLifetime(StoreLifetime.Singleton);
    c.ScanAssemblies(typeof(Program).Assembly);
});

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseRequestLocalization(culture);
app.MapGroup(RouteUtils.Authorization).MapLoginAndLogout(oidcScheme);
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
