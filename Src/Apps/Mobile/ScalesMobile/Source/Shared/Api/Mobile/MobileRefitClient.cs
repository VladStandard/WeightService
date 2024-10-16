using Microsoft.Extensions.Configuration;
using Refit;
using ScalesMobile.Source.Shared.Extensions;
using ScalesTablet.Source.Shared.Api;
using Ws.Mobile.Models;
using Ws.Shared.Handlers;

namespace ScalesMobile.Source.Shared.Api.Mobile;

internal class MobileRefitClient : IRefitClient
{
    public void Configure(MauiAppBuilder builder)
    {
        IConfigurationSection oidcConfiguration = builder.Configuration.GetSection("Api");

        builder.Services.AddRefitClient<IMobileApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new(oidcConfiguration.GetValueSafe<string>("BaseUrl")))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            })
            .AddHttpMessageHandler<AcceptLanguageHandler>();
    }
}