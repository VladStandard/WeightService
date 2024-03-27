using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Features.Boxes.Common;
using Ws.PalychExchangeApi.Features.Boxes.Services;
using Ws.PalychExchangeApi.Features.Brands.Common;
using Ws.PalychExchangeApi.Features.Brands.Services;
using Ws.PalychExchangeApi.Features.Bundles.Common;
using Ws.PalychExchangeApi.Features.Bundles.Services;
using Ws.PalychExchangeApi.Features.Clips.Common;
using Ws.PalychExchangeApi.Features.Clips.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddXmlSerializerFormatters();

builder.Services.AddDbContext<WsDbContext>();

builder.Services.AddScoped<IBoxService, BoxService>();
builder.Services.AddScoped<IClipService, ClipService>();
builder.Services.AddScoped<IBundleService, BundleService>();
builder.Services.AddScoped<IBrandService, BrandService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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