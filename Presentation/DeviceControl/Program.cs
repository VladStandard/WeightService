using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Tailwind;
using DeviceControl.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Radzen;
using Ws.Services;
using Ws.StorageCore.Helpers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Add

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization(options => { options.FallbackPolicy = options.DefaultPolicy; });
builder.Services.AddRazorPages(options => { options.RootDirectory = "/Features"; });
builder.Services.AddServerSideBlazor();
builder.Services.AddOptions();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddBlazoredLocalStorage();
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
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IClaimsTransformation, CustomClaimsTransformation>();

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

string[] supportedCultures = { "ru-RU", "en-US" };
RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("ru-RU")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

SqlCoreHelper.Instance.SetSessionFactory(false);

app.Run();

#endregion
