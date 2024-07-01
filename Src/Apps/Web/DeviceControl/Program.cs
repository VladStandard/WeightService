using Blazorise;
using Blazorise.Icons.FontAwesome;
using DeviceControl.Source.App;
using DeviceControl.Source.Shared.Api;
using DeviceControl.Source.Shared.Auth;
using DeviceControl.Source.Shared.Auth.Extensions;
using DeviceControl.Source.Shared.RenderZpl;
using DeviceControl.Source.Shared.Services;
using Microsoft.FluentUI.AspNetCore.Components;
using Refit;
using Ws.DeviceControl.Models;
using Ws.Domain.Services;
using Ws.Labels.Service;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

const string oidcScheme = "KeycloakOidc";
IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Oidc");

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddBlazorise().AddEmptyProviders().AddFontAwesomeIcons();
builder.Services.AddFluentUIComponents();
builder.Services.ConfigureKeycloakAuthorization(oidcConfiguration, oidcScheme);

builder.Services.AddLocalization();
builder.Services.AddDomainServices();
builder.Services.AddLabelsServices();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<Redirector>();
builder.Services.AddScoped<IRenderLabelService, RenderLabelService>();

string keycloakAdminUrl = $"{oidcConfiguration.GetValue<string>("Authority")}/admin/realms/{oidcConfiguration.GetValue<string>("Realm")}";
builder.Services.AddTransient<ServerAuthorizationMessageHandler>();

#region Refit

builder.Services.AddRefitClient<IKeycloakApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new(keycloakAdminUrl))
    .AddHttpMessageHandler<ServerAuthorizationMessageHandler>();

builder.Services.AddRefitClient<IWebApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new(builder.Configuration.GetValue<string>("WebApi") ?? ""))
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });

#endregion


builder.Services.AddScoped<DatabaseApi>();
builder.Services.AddScoped<UserApi>();

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

app.UseRequestLocalization("ru-RU");
app.MapGroup(RouteUtils.Authorization).MapLoginAndLogout(oidcScheme);
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
