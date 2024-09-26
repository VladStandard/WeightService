using Blazorise;
using Blazorise.Icons.FontAwesome;
using DeviceControl;
using DeviceControl.Source.App;
using DeviceControl.Source.Shared.Auth.Extensions;
using DeviceControl.Source.Shared.Refit;
using Fluxor;
using Ws.Shared.Constants;

const string oidcScheme = "KeycloakOidc";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHelpers<IDeviceControlAssembly>()
    .AddRefitEndpoints<IDeviceControlAssembly>()
    .AddDelegatingHandlers<IDeviceControlAssembly>();

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });

builder.Services
    .AddBlazorise()
    .AddEmptyProviders()
    .AddFontAwesomeIcons()
    .AddWMBOS()
    .AddLocalization()
    .AddFluxor(c => c.ScanAssemblies(typeof(IDeviceControlAssembly).Assembly))
    .AddFluentUIComponents(c => c.ValidateClassNames = false)
    .ConfigureKeycloakAuthorization(builder.Configuration.GetSection("Oidc"), oidcScheme);

builder.RegisterRefitClients();

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
app.UseRequestLocalization(Cultures.Ru.Name);

app.MapGroup(RouteUtils.Authorization).MapLoginAndLogout(oidcScheme);
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();