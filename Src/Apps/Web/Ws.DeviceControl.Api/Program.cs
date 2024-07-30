using Ws.DeviceControl.Api.App.Middlewares;
using Ws.DeviceControl.Api.App.Shared.Internal;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// INTERNAL
builder.Services.BaseSetup();
builder.Services.AddValidators();
builder.Services.AddApiServices();
builder.Services.AddAuth(builder.Configuration);

builder.Services.AddScoped<UserManager>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

// EXTERNAL
builder.Services.AddEfCore();
builder.Services.AddLocalization();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();