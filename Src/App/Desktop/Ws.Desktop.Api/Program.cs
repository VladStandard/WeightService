using TscZebra.Plugin.Abstractions.Enums;
using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Arms.Common;
using Ws.Desktop.Api.App.Features.Arms.Impl;
using Ws.Shared.Converters.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEfCore();
builder.Services.AddScoped<IArmService, ArmService>();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressConsumesConstraintForFormFileParameters = true;
        options.SuppressInferBindingSourcesForParameters = true;
        options.SuppressModelStateInvalidFilter = true;
        options.SuppressMapClientErrors = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new IpAddressJsonConverter());
        options.JsonSerializerOptions.Converters.Add(new EnumJsonConverter<PrinterTypes>());
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.WriteIndented = true;
    });

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();