using Microsoft.AspNetCore.Mvc.Authorization;
using Ws.Database.EntityFramework;
using Ws.PalychExchange.Api;
using Ws.PalychExchange.Api.App.Shared.Extensions;
using Ws.PalychExchange.Api.App.Shared.Middlewares;
using Ws.Shared.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add(new AllowAnonymousFilter());
        options.Filters.Add(new ProducesAttribute("application/xml"));
    })
    .AddXmlSerializerFormatters();

builder.Services
    .AddEfCore()
    .AddValidators()
    .AddMiddlewares<IPalychExchangeAssembly>()
    .AddApiServices<IPalychExchangeAssembly>();

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

app.UseRouting();
app.MapControllers();

app.Run();