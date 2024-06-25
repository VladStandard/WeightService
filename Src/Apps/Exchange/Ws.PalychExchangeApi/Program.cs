using Microsoft.Extensions.Logging.EventLog;
using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Extensions;
using Ws.PalychExchangeApi.Features.Boxes.Common;
using Ws.PalychExchangeApi.Features.Boxes.Services;
using Ws.PalychExchangeApi.Features.Brands.Common;
using Ws.PalychExchangeApi.Features.Brands.Services;
using Ws.PalychExchangeApi.Features.Bundles.Common;
using Ws.PalychExchangeApi.Features.Bundles.Services;
using Ws.PalychExchangeApi.Features.Characteristics.Common;
using Ws.PalychExchangeApi.Features.Characteristics.Services;
using Ws.PalychExchangeApi.Features.Clips.Common;
using Ws.PalychExchangeApi.Features.Clips.Services;
using Ws.PalychExchangeApi.Features.Plus.Common;
using Ws.PalychExchangeApi.Features.Plus.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddXmlSerializerFormatters();

builder.Services.AddEfCore();

builder.Services.AddValidators();
builder.Services.AddScoped<IBoxService, BoxService>();
builder.Services.AddScoped<IClipService, ClipService>();
builder.Services.AddScoped<IBundleService, BundleService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IPluService, PluService>();
builder.Services.AddScoped<ICharacteristicService, CharacteristicService>();


#pragma warning disable CA1416

builder.Logging.AddEventLog(eventLogSetting =>
{
    eventLogSetting.SourceName = "Ws.Palych.ExchangeApi";
});

#pragma warning restore CA1416



WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();