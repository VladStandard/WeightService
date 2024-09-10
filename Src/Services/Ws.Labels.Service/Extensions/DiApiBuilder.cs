using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Ws.Labels.Service.Api;

namespace Ws.Labels.Service.Extensions;

internal static class DiApiBuilder
{
    internal static void AddPalychApi(this IServiceCollection services)
    {
        const string login = "Администратор";
        const string password = "111";
        string authorizationToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{password}"));

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
                c.BaseAddress = new("http://dev1c.kolbasa-vs.local/palych_spe/hs/Exchange/");
                c.DefaultRequestHeaders.Authorization = new("Basic", authorizationToken);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AllowAutoRedirect = false,
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            });
    }
}