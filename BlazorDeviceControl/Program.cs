// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using BlazorDeviceControl.Service;
using BlazorDownloadFile;
using DataCore.Protocols;
using Microsoft.AspNetCore.Authentication.Negotiate;
using MudBlazor.Services;
using Radzen;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;

AppSettingsHelper appSettings = AppSettingsHelper.Instance;
//IConfiguration Configuration;
//IWebHostEnvironment env;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
// Add builder.Services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// Inject.
builder.Services.AddHotKeys();
//builder.Services.AddSingleton<JsonSettingsBase>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddMudServices();
// Authentication & Authorization.
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
// Windows authentication may not be applied with Kestrel without this line
builder.Services.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy);
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
// Other.
builder.Services.AddControllersWithViews();
builder.Services.AddBlazorDownloadFile();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<IFileDownload, FileDownload>();
//
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

WebApplication app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
// Authentication & Authorization after app.UseRouting().
app.UseAuthentication();
app.UseAuthorization();
// Last step.
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapBlazorHub();
//    endpoints.MapFallbackToPage("/_Host");
//});
appSettings.DataAccess.JsonControl.SetupForBlazorApp(app.Environment.ContentRootPath,
    NetUtils.GetLocalHostName(false), nameof(BlazorDeviceControl));
app.Run();

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        CreateHostBuilder(args).Build().Run();
//    }

//    private static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
//        .ConfigureWebHostDefaults(webBuilder =>
//        {
//            webBuilder.UseStartup<Startup>();
//        })
//        .ConfigureAppConfiguration((config) =>
//        {
//            config.SetBasePath(Directory.GetCurrentDirectory());
//        })
//        ;
//}
