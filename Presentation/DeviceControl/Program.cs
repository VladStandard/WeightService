using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Tailwind;
using DeviceControl.Auth.ClaimsTransform;
using DeviceControl.Auth.ClaimsTransform.CacheProviders;
using DeviceControl.Auth.ClaimsTransform.CacheProviders.Common;
using DeviceControl.Auth.Policies;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Ws.Domain.Services;
using Ws.Labels.Service;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Add

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization(PolicyAuthUtils.RegisterAuthorization);
builder.Services.AddRazorPages(options => { options.RootDirectory = "/Features"; });
builder.Services.AddServerSideBlazor();
builder.Services.AddOptions();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

builder.Services.AddDomainServices();
builder.Services.AddLabelsServices();

builder.Services
    .AddBlazorise()
    .AddTailwindProviders()
    .AddFontAwesomeIcons();

builder.Logging.SetMinimumLevel(LogLevel.Warning);

builder.Services.AddLocalization();

#endregion

#region AddScoped

builder.Services.AddScoped<Redirector>();
builder.Services.AddScoped<IClaimsCacheProvider, ClaimsInMemoryCacheProvider>();
builder.Services.AddScoped<IClaimsTransformation, WsClaimsTransformation>();

#endregion

#region App

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.UseRequestLocalization("ru-RU");

app.Run();

#endregion