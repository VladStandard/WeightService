using Microsoft.Extensions.Configuration;
using ScalesDesktop.Source.Shared.Api.Desktop.Handlers;
using Ws.Shared.Handlers;

namespace ScalesDesktop.Source.Shared.Api.Desktop;

internal class DesktopRefitClient : IRefitClient
{
    public void Configure(MauiAppBuilder builder)
    {
        IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Api");
        builder.Services.AddRefitClient<IDesktopApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{oidcConfiguration.GetValue<string>("BaseUrl") ?? ""}"))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            })
            .AddHttpMessageHandler<HostNameMessageHandler>()
            .AddHttpMessageHandler<AcceptLanguageHandler>();
    }
}