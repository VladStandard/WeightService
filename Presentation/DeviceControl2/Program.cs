using Blazorise;
using Blazorise.Icons.FontAwesome;
using DeviceControl2.Source.App;
using DeviceControl2.Source.Shared.Auth.ClaimsTransform;
using DeviceControl2.Source.Shared.Auth.ClaimsTransform.CacheProviders;
using DeviceControl2.Source.Shared.Auth.ClaimsTransform.CacheProviders.Common;
using DeviceControl2.Source.Shared.Auth.Policies;
using DeviceControl2.Source.Shared.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Domain.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddBlazorise().AddEmptyProviders().AddFontAwesomeIcons();
builder.Services.AddFluentUIComponents();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization(PolicyAuthUtils.RegisterAuthorization);
builder.Services.AddLocalization();
builder.Services.AddDomainServices();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<Redirector>();
builder.Services.AddScoped<IClaimsCacheProvider, ClaimsInMemoryCacheProvider>();
builder.Services.AddScoped<IClaimsTransformation, WsClaimsTransformation>();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();