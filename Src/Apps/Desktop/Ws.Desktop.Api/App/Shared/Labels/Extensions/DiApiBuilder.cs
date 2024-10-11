using Refit;
using Ws.Desktop.Api.App.Shared.Labels.Api;
using Ws.Desktop.Api.App.Shared.Labels.Settings;

namespace Ws.Desktop.Api.App.Shared.Labels.Extensions;

internal static class DiApiBuilder
{
    internal static void AddPalychApi(this IServiceCollection services, PalychSettings serviceSettings)
    {
        services.AddRefitClient<IPalychApi>(new()
        {
            ContentSerializer = new XmlContentSerializer(new()
            {
                XmlReaderWriterSettings = new()
                {
                    ReaderSettings = new()
                    {
                        IgnoreWhitespace = true,
                        IgnoreComments = true,
                    }
                }
            })
        })
        .ConfigureHttpClient(c =>
        {
            c.Timeout = TimeSpan.FromSeconds(10);
            c.BaseAddress = new(serviceSettings.Url);
            c.DefaultRequestHeaders.Authorization = new("Basic", serviceSettings.AuthorizationToken);
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            AllowAutoRedirect = false,
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        });
    }
}