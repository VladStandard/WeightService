using System.Globalization;
using Ws.DeviceControl.Api;
using Ws.DeviceControl.Api.App.Shared.Middlewares;
using Ws.DeviceControl.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// INTERNAL
builder.Services
    .BaseSetup()
    .AddValidators<IDeviceControlModelsAssembly>()
    .AddValidators<IDeviceControlApiAssembly>()
    .AddHelpers<IDeviceControlApiAssembly>()
    .AddApiServices<IDeviceControlApiAssembly>()
    .AddMiddlewares<IDeviceControlApiAssembly>()
    .AddAuth(builder.Configuration);

// EXTERNAL
builder.Services
    .AddEfCore()
    .AddLocalization()
    .AddHttpContextAccessor()
    .AddEndpointsApiExplorer();

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