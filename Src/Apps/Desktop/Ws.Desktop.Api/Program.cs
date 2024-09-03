using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Ws.Database.EntityFramework;
using Ws.Desktop.Api;
using Ws.Desktop.Api.App.Middlewares;
using Ws.Labels.Service;
using Ws.Shared.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

#region Ready services

builder.Services
    .AddEfCore()
    .AddLabelsServices()
    .AddApiServices<IDesktopApiAssembly>();

#endregion


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


builder.Services.AddTransient<GenerateLabelExceptionHandlingMiddleware>();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.UseMiddleware<GenerateLabelExceptionHandlingMiddleware>();

app.Run();