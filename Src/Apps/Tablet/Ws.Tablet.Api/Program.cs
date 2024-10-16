using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Ws.Shared.Extensions;
using Ws.Tablet.Api;
using Ws.Tablet.Api.App.Shared.Auth;
using Ws.Tablet.Api.App.Shared.Extensions;
using Ws.Tablet.Api.App.Shared.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddLocalization()
    .AddHelpers<ITabletApiAssembly>()
    .AddMiddlewares<ITabletApiAssembly>()
    .AddApiServices<ITabletApiAssembly>();

builder.Services
    .AddEndpointsApiExplorer()
    .AddControllers(options =>
    {
        options.Filters.Add(new AuthorizeFilter());
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

builder.Services
    .AddAuthentication(ArmAuthenticationOptions.DefaultScheme)
    .AddScheme<ArmAuthenticationOptions, ArmAuthenticationHandler>(
        ArmAuthenticationOptions.DefaultScheme, _ => { }
    );

builder.Services.AddHttpContextAccessor();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.SetupVsLocalization();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<GenerateLabelExceptionHandlingMiddleware>();

app.Run();