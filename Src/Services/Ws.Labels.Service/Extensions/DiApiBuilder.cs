using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Ws.Labels.Service.Api;

namespace Ws.Labels.Service.Extensions;

internal static class DiApiBuilder
{
    internal static void AddPalychApi(this IServiceCollection services, PalychSettingsModel serviceSettings)
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