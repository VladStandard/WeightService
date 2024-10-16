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

WebApplication app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.SetupVsLocalization();

app.UseHttpsRedirection();
app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();