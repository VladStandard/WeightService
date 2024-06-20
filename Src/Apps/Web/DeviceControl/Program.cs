using Blazorise;
using Blazorise.Icons.FontAwesome;
using DeviceControl.Source.App;
using DeviceControl.Source.Shared.Auth.Extensions;
using DeviceControl.Source.Shared.RenderZpl;
using Microsoft.FluentUI.AspNetCore.Components;
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
