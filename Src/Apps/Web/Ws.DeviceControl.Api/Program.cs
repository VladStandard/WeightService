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

CultureInfo[] supportedCultures = [Cultures.Ru, Cultures.En];
RequestLocalizationOptions localizationOptions = new()
{
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    DefaultRequestCulture = new(Cultures.En.Name)
};

WebApplication app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();