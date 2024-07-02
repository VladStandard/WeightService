using DeviceControl.Source.Shared.Services;
using Refit;
using Ws.DeviceControl.Models;

namespace DeviceControl.Source.Refit.Endpoints;

internal class WebRefitEndpoint : IRefitEndpoint
{
    public void Configure(WebApplicationBuilder builder)
    {
        builder.Services.AddRefitClient<IWebApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new(builder.Configuration.GetValue<string>("WebApi") ?? ""))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            });

        builder.Services.AddScoped<DatabaseApi>();
    }
}