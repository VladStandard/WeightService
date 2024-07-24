namespace Ws.Desktop.Models.Features.PalletMen;

public sealed record PalletMan
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("fio")]
    public required Fio Fio { get; init; }

    [JsonPropertyName("password")]
    public required string Password { get; init; }
}