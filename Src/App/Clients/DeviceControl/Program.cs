using Blazorise;
using Blazorise.Icons.FontAwesome;
using DeviceControl.Source.App;
using DeviceControl.Source.Shared.Auth.ClaimsTransform;
using DeviceControl.Source.Shared.Auth.ClaimsTransform.CacheProviders;
using DeviceControl.Source.Shared.Auth.ClaimsTransform.CacheProviders.Common;
using DeviceControl.Source.Shared.Auth.Policies;
using DeviceControl.Source.Shared.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Domain.Services;
using Ws.Labels.Service;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddBlazorise().AddEmptyProviders().AddFontAwesomeIcons();
builder.Services.AddFluentUIComponents();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization(PolicyAuthUtils.RegisterAuthorization);

builder.Services.AddLocalization();
builder.Services.AddDomainServices();
builder.Services.AddLabelsServices();

builder.Services.AddMemoryCache();

builder.Services.AddSingleton<Redirector>();
builder.Services.AddScoped<IClaimsTransformation, WsClaimsTransformation>();
builder.Services.AddScoped<IClaimsCacheProvider, ClaimsInMemoryCacheProvider>();


WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseRequestLocalization("ru-RU");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();