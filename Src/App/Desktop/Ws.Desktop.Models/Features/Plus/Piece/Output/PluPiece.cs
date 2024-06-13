using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ws.Desktop.Models.Features.Plus.Piece.Output;


public record Nesting
{
    [Required]
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [Required]
    [JsonPropertyName("bundleCount")]
    public required byte BundleCount { get; init; }

    [Required]
    [JsonPropertyName("box")]
    public required string Box { get; init; }
}


[Serializable]
public record PluPiece
{
    [Required]
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [Required]
    [JsonPropertyName("number")]
    public required ushort Number { get; init; }

    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [Required]
    [JsonPropertyName("fullName")]
    public required string FullName { get; init; }

    [Required]
    [JsonPropertyName("bundle")]
    public required string Bundle { get; init; }

    [Required]
    [JsonPropertyName("weightNet")]
    public required decimal WeightNet { get; init; }

    [Required]
    [JsonPropertyName("nestings")]
    public required List<Nesting> Nestings { get; init; }
};