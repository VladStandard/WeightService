// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorDownloadFile;
using DataCore.Files;
using Microsoft.AspNetCore.Authentication.Negotiate;
using MudBlazor.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Add

// Add services to the container.
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
// Inject.
//builder.Services.AddSingleton<JsonSettingsBase>();
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<Radzen.DialogService>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<IFileDownload, FileDownload>();

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

// Authentication & Authorization after app.UseRouting().
// app.UseAuthentication();
// app.UseAuthorization();
// Last step.
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
JsonSettingsHelper.Instance.SetupWebApp(app.Environment.ContentRootPath, nameof(BlazorDeviceControl));
try
{
    app.Run();
}
catch (Exception ex)
{
    FileLoggerHelper.Instance.StoreException(ex);
}