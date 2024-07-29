using System.Net.Mime;
using System.Text.Json;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Ws.DeviceControl.Api.App.Features.References.ProductionSites.Common;
using Ws.DeviceControl.Api.App.Middlewares;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Create;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);

builder.Services.AddAuthorization()
    .AddKeycloakAuthorization(options =>
    {
        options.EnableRolesMapping = RolesClaimTransformationSource.ResourceAccess;
        options.RolesResource = builder.Configuration.GetSection("Keycloak").GetValue<string>("resource");
    }).AddAuthorization(PolicyAuthUtils.RegisterAuthorization).AddAuthorizationBuilder();

builder.Services.AddEndpointsApiExplorer();

#region ApiServices

builder.Services.Scan(scan => scan
    .FromAssembliesOf(typeof(IProductionSiteService))
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("ApiService")))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

#endregion

#region Validators

builder.Services.Scan(scan => scan
    .FromAssembliesOf(typeof(ProductionSiteCreateValidator))
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Validator")))
    .AsSelf()
    .WithScopedLifetime()
);

#endregion

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

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();