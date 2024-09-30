namespace ScalesDesktop.Source.Shared.Refit.Clients;

internal class DesktopRefitClient : IRefitClient
{
    public void Configure(MauiAppBuilder builder)
    {
        builder.Services.AddRefitClient<IDesktopApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new("https://scales-api-dev.kolbasa-vs.local/api/desktop"))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            })
            .AddHttpMessageHandler<HostNameMessageHandler>();
    }
}