// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorDownloadFile;
using DeviceControl.Services;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using WsFileSystemCore.Helpers;
using WsStorageCore.Helpers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Add

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization(options => options.FallbackPolicy = options.DefaultPolicy);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddOptions();
builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddBlazorDownloadFile();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();

#endregion

#region AddScoped

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped<RouteService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<LocalStorageService>();

builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<IFileDownload, FileDownload>();
builder.Services.AddScoped<IUserRightsService, UserRightsService>();


#endregion

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

WsSqlContextManagerHelper.Instance.SetupJsonWebApp(
    app.Environment.ContentRootPath,
    null,
    true
);
try
{
    app.Run();
}
catch (Exception ex)
{
    WsFileLoggerHelper.Instance.StoreException(ex);
}