using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Authorization;
using Ws.Mobile.Api;
using Ws.Mobile.Api.App.Shared.Extensions;
using Ws.Mobile.Api.App.Shared.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddLocalization()
    .AddHelpers<IMobileApiAssembly>()
    .AddMiddlewares<IMobileApiAssembly>()
    .AddApiServices<IMobileApiAssembly>();

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
app.SetupVsLocalization();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GenerateLabelExceptionHandlingMiddleware>();

app.Run();