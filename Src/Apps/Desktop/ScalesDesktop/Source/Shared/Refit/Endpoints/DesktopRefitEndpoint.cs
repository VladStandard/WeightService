using Microsoft.Extensions.Configuration;
using ScalesDesktop.Source.Shared.Services.Endpoints;
using Ws.Desktop.Models;

namespace ScalesDesktop.Source.Shared.Refit.Endpoints;

internal class DesktopRefitEndpoint : IRefitEndpoint
{
    public void Configure(MauiAppBuilder builder)
    {
        IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Api");
        builder.Services.AddRefitClient<IDesktopApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new($"{oidcConfiguration.GetValue<string>("BaseUrl") ?? ""}"))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            });

        builder.Services.AddScoped<PalletEndpoints>();
        builder.Services.AddScoped<ArmEndpoints>();
        builder.Services.AddScoped<PluEndpoints>();
    }
}