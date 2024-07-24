using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Authorization;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Common;
using Ws.DeviceControl.Api.App.Middlewares;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Create;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.Scan(scan => scan
    .FromAssembliesOf(typeof(IProductionSiteService))
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("ApiService")))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

builder.Services.Scan(scan => scan
    .FromAssembliesOf(typeof(ProductionSiteCreateValidator))
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Validator")))
    .AsSelf()
    .WithScopedLifetime()
);


builder.Services.AddEfCore();
builder.Services.AddLocalization();


builder.Services
    .AddControllers(options => {
        options.Filters.Add(new AllowAnonymousFilter());
        options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
    })
    .ConfigureApiBehaviorOptions(options => {
        options.SuppressConsumesConstraintForFormFileParameters = true;
        options.SuppressInferBindingSourcesForParameters = true;
        options.SuppressModelStateInvalidFilter = true;
        options.SuppressMapClientErrors = true;
    })
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();