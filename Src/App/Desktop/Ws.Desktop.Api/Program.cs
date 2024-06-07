using System.Text.Json;
using System.Text.Json.Serialization;
using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Arms.Common;
using Ws.Desktop.Api.App.Features.Arms.Impl;
using Ws.Desktop.Api.App.Features.Plus.Common;
using Ws.Desktop.Api.App.Features.Plus.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEfCore();
builder.Services.AddScoped<IArmService, ArmService>();
builder.Services.AddScoped<IPluService, PluService>();

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
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
    });

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();