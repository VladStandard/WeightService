using BF.Utilities.Handlers;
using Microsoft.Extensions.Configuration;
using Refit;
using ScalesMobile.Source.Shared.Extensions;
using Ws.Mobile.Models;

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