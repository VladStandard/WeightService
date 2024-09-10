using System.Net.Mime;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Ws.Database.EntityFramework;
using Ws.Desktop.Api;
using Ws.Desktop.Api.App.Shared.Auth;
using Ws.Desktop.Api.App.Shared.Middlewares;
using Ws.Labels.Service;
using Ws.Shared.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthorization(PolicyAuthUtils.RegisterAuthorization)
    .AddAuthentication(ArmAuthenticationOptions.DefaultScheme)
    .AddScheme<ArmAuthenticationOptions, ArmAuthenticationHandler>(
         ArmAuthenticationOptions.DefaultScheme, _ => { }
    );

builder.Services
    .AddEfCore()
    .AddLabelsServices()
    .AddHelpers<IDesktopApiAssembly>()
    .AddMiddlewares<IDesktopApiAssembly>()
    .AddApiServices<IDesktopApiAssembly>();

builder.Services
    .AddEndpointsApiExplorer()
    .AddControllers(options =>
    {
        options.Filters.Add(new AllowAnonymousFilter());
        options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
    })
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

builder.Services.AddHttpContextAccessor();


WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GenerateLabelExceptionHandlingMiddleware>();

app.Run();