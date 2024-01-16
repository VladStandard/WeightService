using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Tailwind;
using DeviceControl.Auth;
using DeviceControl.Auth.Common;
using DeviceControl.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Ws.Services;
using Ws.StorageCore.Helpers;

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
builder.Services.AddVsServices();
builder.Services
    .AddBlazorise()
    .AddTailwindProviders()
    .AddFontAwesomeIcons();

builder.Logging.SetMinimumLevel(LogLevel.Warning);

builder.Services.AddLocalization();

#endregion

#region AddScoped

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<JsService>();
builder.Services.AddScoped<IClaimsTransformation, CustomClaimsTransformation>();
builder.Services.AddSingleton<IUserCacheService, UserCacheService>();

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

SqlCoreHelper.Instance.SetSessionFactory(false);

app.Run();

#endregion
