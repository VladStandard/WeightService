using Ws.Shared.Converters.Json;

namespace Ws.Desktop.Models.Features.PalletMen;

public sealed record PalletMan
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("fio")]
    [JsonConverter(typeof(FioJsonConverter))]
    public required Fio Fio { get; init; }

    [JsonPropertyName("password")]
    public required string Password { get; init; }
}