using DeviceControl.Source.Shared.Services;
using Refit;
using Ws.DeviceControl.Models;

namespace DeviceControl.Source.Shared.Refit.Endpoints;

internal class WebRefitEndpoint : IRefitEndpoint
{
    public void Configure(WebApplicationBuilder builder)
    {
        string apiUrl = builder.Configuration.GetValue<string>("WebApi")!;

        builder.Services.AddRefitClient<IWebApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new(apiUrl))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            })
            .AddHttpMessageHandler(() => new AcceptLanguageHandler());;

        builder.Services.AddScoped<AdminEndpoints>();
        builder.Services.AddScoped<DevicesEndpoints>();
        builder.Services.AddScoped<DiagnosticEndpoints>();
        builder.Services.AddScoped<References1CEndpoints>();
        builder.Services.AddScoped<ReferencesEndpoints>();
    }
}