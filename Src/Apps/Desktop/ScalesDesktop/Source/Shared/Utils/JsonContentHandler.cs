using System.Text;
using System.Text.Json;
using TscZebra.Plugin.Abstractions.Enums;
using Ws.Shared.Converters.Json;

namespace ScalesDesktop.Source.Shared.Utils;

public class JsonContentHandler : DelegatingHandler
{
    private JsonSerializerOptions JsonOptions { get; } = new(JsonSerializerDefaults.Web)
    {
        Converters = { new IpAddressJsonConverter(), new EnumJsonConverter<PrinterTypes>() }
    };

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
        if (response.Content.Headers.ContentType?.MediaType != "application/json") return response;

        string jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
        object? jsonDocument = JsonSerializer.Deserialize<object>(jsonString, JsonOptions);
        string serializedContent = JsonSerializer.Serialize(jsonDocument, JsonOptions);
        response.Content = new StringContent(serializedContent, Encoding.UTF8, "application/json");

        return response;
    }
}