using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ws.Desktop.Models.ValueTypes;

namespace Ws.Desktop.Models.Features.PalletMen;

[Serializable]
public sealed record PalletMan
{
    [Required]
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [Required]
    [JsonPropertyName("fio")]
    public required Fio Fio { get; init; }

    [Required]
    [JsonPropertyName("password")]
    public required string Password { get; init; }

}