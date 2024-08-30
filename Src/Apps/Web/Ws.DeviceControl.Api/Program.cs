using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Ws.DeviceControl.Api.App.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// INTERNAL
builder.Services.BaseSetup();
builder.Services.AddValidators();
builder.Services.AddApiServices();
builder.Services.AddAuth(builder.Configuration);

builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<ErrorMessages>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// EXTERNAL
builder.Services.AddEfCore();
builder.Services.AddLocalization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();

// Localization
builder.Services.AddLocalization();
string[] supportedCultures = ["en-US", "ru-RU"];
RequestLocalizationOptions localizationOptions = new()
{
    SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList(),
    SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList(),
    DefaultRequestCulture = new("en-US")
};

WebApplication app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();