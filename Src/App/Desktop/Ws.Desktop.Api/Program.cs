using System.Text.Json;
using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Arms.Common;
using Ws.Desktop.Api.App.Features.Arms.Impl;
using Ws.Desktop.Api.App.Features.PalletMen.Common;
using Ws.Desktop.Api.App.Features.PalletMen.Impl;
using Ws.Desktop.Api.App.Features.Pallets.Common;
using Ws.Desktop.Api.App.Features.Pallets.Impl;
using Ws.Desktop.Api.App.Features.Plu.Piece.Common;
using Ws.Desktop.Api.App.Features.Plu.Piece.Impl;
using Ws.Desktop.Api.App.Features.Plu.Weight.Common;
using Ws.Desktop.Api.App.Features.Plu.Weight.Impl;
using Ws.Domain.Services;
using Ws.Labels.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddEfCore();

builder.Services.AddScoped<IArmService, ArmService>();
builder.Services.AddScoped<IPluPieceService, PluPieceService>();
builder.Services.AddScoped<IPluWeightService, PluWeightService>();
builder.Services.AddScoped<IPalletManService, PalletManService>();
builder.Services.AddScoped<IPalletApiService, PalletApiService>();

builder.Services.AddDomainServices();
builder.Services.AddLabelsServices();

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