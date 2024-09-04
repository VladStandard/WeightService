using DeviceControl.Source.Shared.Auth;
using Refit;
using Ws.DeviceControl.Models;

namespace DeviceControl.Source.Shared.Refit.Clients;

internal class WebRefitClient : IRefitClient
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
            .AddHttpMessageHandler<AcceptLanguageHandler>()
            .AddHttpMessageHandler<ServerAuthorizationMessageHandler>();
    }
}