using Ws.Database.EntityFramework;
using Ws.PalychExchange.Api.Extensions;
using Ws.PalychExchange.Api.Features.Plus.Services;
using Ws.PalychExchange.Api.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddXmlSerializerFormatters();

builder.Services.AddEfCore();


builder.Services.Scan(scan => scan
    .FromAssembliesOf(typeof(PluApiService))
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("ApiService")))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

builder.Services.AddValidators();

#pragma warning disable CA1416

builder.Logging.AddEventLog(eventLogSetting =>
{
    eventLogSetting.Filter = (providerName, _) => !providerName.StartsWith("Microsoft");
    eventLogSetting.SourceName = "Ws.Palych.ExchangeApi";
});

#pragma warning restore CA1416


WebApplication app = builder.Build();

app.UseMiddleware<LoggingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();