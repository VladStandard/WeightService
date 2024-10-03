using System.Globalization;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Authorization;
using Ws.Database.EntityFramework;
using Ws.Desktop.Api;
using Ws.Desktop.Api.App.Shared.Middlewares;
using Ws.Labels.Service;
using Ws.Labels.Service.Settings;
using Ws.Shared.Constants;
using Ws.Shared.Extensions;

CultureInfo.DefaultThreadCurrentCulture = Cultures.Ru;
CultureInfo.DefaultThreadCurrentUICulture = Cultures.Ru;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

PalychSettings palychSettings = builder.Configuration
    .GetSection("Palych").Get<PalychSettings>() ?? throw new NullReferenceException();

builder.Services
    .AddAuthorization(PolicyAuthUtils.RegisterAuthorization)
    .AddAuthentication(ArmAuthenticationOptions.DefaultScheme)
    .AddScheme<ArmAuthenticationOptions, ArmAuthenticationHandler>(
         ArmAuthenticationOptions.DefaultScheme, _ => { }
    );

builder.Services
    .AddEfCore()
    .AddLabelsServices(palychSettings)
    .AddLocalization()
    .AddHelpers<IDesktopApiAssembly>()
    .AddMiddlewares<IDesktopApiAssembly>()
    .AddApiServices<IDesktopApiAssembly>();

builder.Services
    .AddEndpointsApiExplorer()
    .AddControllers(options =>
    {
        options.Filters.Add(new AllowAnonymousFilter());
        options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressConsumesConstraintForFormFileParameters = true;
        options.SuppressInferBindingSourcesForParameters = true;
        options.SuppressModelStateInvalidFilter = true;
        options.SuppressMapClientErrors = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddHttpContextAccessor();

CultureInfo[] supportedCultures = [Cultures.Ru, Cultures.En];
RequestLocalizationOptions localizationOptions = new()
{
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    DefaultRequestCulture = new(Cultures.Ru.Name)
};

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.UseRequestLocalization(localizationOptions);
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GenerateLabelExceptionHandlingMiddleware>();

app.Run();