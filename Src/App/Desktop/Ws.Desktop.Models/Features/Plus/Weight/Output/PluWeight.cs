using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ws.Desktop.Models.Features.Plus.Weight.Output;

[Serializable]
public record PluWeight
{
    [Required]
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [Required]
    [JsonPropertyName("number")]
    public required ushort Number { get; init; }

    [Required]
    [JsonPropertyName("bundleCount")]
    public required byte BundleCount { get; init; }

    [Required]
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [Required]
    [JsonPropertyName("fullName")]
    public required string FullName { get; init; }

    [Required]
    [JsonPropertyName("box")]
    public required string Box { get; init; }

    [Required]
    [JsonPropertyName("bundle")]
    public required string Bundle { get; init; }

    [Required]
    [JsonPropertyName("tareWeight")]
    public required decimal TareWeight { get; init; }
};