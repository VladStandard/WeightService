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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();